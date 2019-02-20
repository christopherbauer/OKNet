using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using OKNet.App.ViewModel;
using OKNet.App.ViewModel.Jira;
using OKNet.Common;
using OKNet.Core;
using OKNet.Infrastructure.Jira;
using Timer = System.Timers.Timer;

namespace OKNet.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Timer AppHeartbeatTimer = new Timer { Interval = (int)TimeSpan.FromSeconds(5).TotalMilliseconds };
        private readonly JiraApiService _jiraApiService;
        private readonly ConfigService _configService;

        public MainWindow()
        {
            _configService = new ConfigService();
            _jiraApiService = new JiraApiService(_configService);

            InitializeComponent();

            AppHeartbeatTimer.Start();

            InitialLoad();
        }

        private async void InitialLoad()
        {
            var names = _configService.GetNames("windows").ToList();
            var windowConfigViewModels = new List<ViewModelBase>();
            for (var i = 0; i < names.Count(); i++)
            {
                var pathString = $"windows:{names[i]}";
                Console.WriteLine(pathString);
                var windowConfig = _configService.GetConfig<WindowConfig>(pathString);
                if (windowConfig.Type == "web")
                {
                    var websiteConfig = _configService.GetConfig<WebsiteConfig>(pathString);
                    windowConfigViewModels.Add(new BasicWebsiteViewModel
                    {
                        Uri = websiteConfig.Uri,
                        Type = websiteConfig.Type,
                        Width = websiteConfig.Width,
                        Height = websiteConfig.Height
                    });
                }

                if (windowConfig.Type == "jira-inprogress")
                {
                    var jiraConfig = _jiraApiService.GetJiraConfig(pathString);

                    string url = $"https://{jiraConfig.ApiHost}";

                    var jiraQuery = new JiraQuery().StatusCategoryIs(JiraStatusCategory.IN_PROGRESS).UpdatedSince(-30, JiraTimeDifference.Days).OrderBy("updated");

                    var jiraConfigPassword = Encoding.UTF8.GetString(Convert.FromBase64String(jiraConfig.Password));

                    var projectsResult = new ApiRequestService().MakeRequestWithBasicAuth<List<ProjectApiModel>>(new Uri($"{url}/project"), jiraConfig.Username, jiraConfigPassword, string.Empty);

                    var viewModel = new JiraInProgressIssueViewModel
                    {
                        Width = jiraConfig.Width,
                        Height = jiraConfig.Height,
                    };

                    if (projectsResult.StatusCode == 200)
                    {
                        viewModel.Projects = JiraApiService.ParseProjects(projectsResult, Convert.ToInt32(jiraConfig.Width));
                    }
                    windowConfigViewModels.Add(viewModel);

                    InitializeIssueAutoUpdate(url, jiraConfig, jiraQuery, viewModel, _jiraApiService, JiraStatusCategory.IN_PROGRESS);
                }

                if (windowConfig.Type == "jira-complete")
                {
                    var jiraQuery = new JiraQuery().ResolvedSince(DateTime.Today).StatusCategoryIs(JiraStatusCategory.COMPLETE)
                       .OrderBy("updated");

                    var jiraConfig = _jiraApiService.GetJiraConfig(pathString);
                    string url = $"https://{jiraConfig.ApiHost}";
                    var jiraConfigPassword = Encoding.UTF8.GetString(Convert.FromBase64String(jiraConfig.Password));
                    var projectsResult = new ApiRequestService().MakeRequestWithBasicAuth<List<ProjectApiModel>>(new Uri($"{url}/project"),
                       jiraConfig.Username, jiraConfigPassword, string.Empty);

                    var viewModel = new JiraCompletedIssueViewModel
                    {
                        Width = jiraConfig.Width,
                        Height = jiraConfig.Height,
                    };

                    if (projectsResult.StatusCode == 200)
                    {
                        viewModel.Projects = new ObservableCollection<JiraProjectViewModel>(projectsResult.Data.Select(
                            model =>
                                new JiraProjectViewModel
                                {
                                    Name = model.name,
                                    Key = model.key,
                                    Id = Convert.ToInt32(model.id),
                                    Width = (int)(Convert.ToInt32(jiraConfig.Width) / 3m - 4)
                                }));
                    }

                    windowConfigViewModels.Add(viewModel);

                    InitializeIssueAutoUpdate(url, jiraConfig, jiraQuery, viewModel, _jiraApiService, JiraStatusCategory.COMPLETE);
                }
            }

            var isDebugMode = _configService.Get<bool>("isDebugMode");
            DataContext = new WindowViewModel { Windows = new ObservableCollection<ViewModelBase>(windowConfigViewModels), IsDebugMode = isDebugMode };

            AppHeartbeatTimer.Elapsed += (sender, args) => Dispatcher.Invoke(RefreshHierarchy);
        }

        private void InitializeIssueAutoUpdate(string url, JiraConfig jiraConfig,
            JiraQuery jiraQuery, JiraIssueViewModelBase viewModel, JiraApiService jiraApiService,
            JiraStatusCategory jiraStatusCategory)
        {
            const string apiBase = "/search";
            
            var issueResult = new ApiRequestService().MakeRequestWithBasicAuth<APIIssueRequestRoot>(new Uri($"{url}{apiBase}"),
                jiraConfig.Username, Encoding.UTF8.GetString(Convert.FromBase64String(jiraConfig.Password)), jiraQuery.ToString());

            if (issueResult.StatusCode == 200)
            {
                viewModel.IssuesTotal = issueResult.Data.total;
                viewModel.AddOrUpdateNewIssues(jiraApiService.ParseIssues(issueResult));
                viewModel.Refresh();
            }

            var lastPage = IsLastPage(50, issueResult.Data.startAt, issueResult.Data.total);

            var startAt = 0;
            if (!lastPage)
            {
                startAt += issueResult.Data.issues.Length;
            }

            SetupJiraWindow(lastPage, startAt, url, apiBase, jiraConfig, jiraQuery, viewModel, jiraApiService,
                issueResult.Data.total, jiraStatusCategory);
        }

        private bool IsLastPage(int pageSize, int startAt, int total)
        {
            return startAt + pageSize >= total;
        }

        private async Task SetupJiraWindow(bool lastPage, int startAt, string url, string apiBase,
            JiraConfig jiraConfig, JiraQuery jiraQuery, JiraIssueViewModelBase viewModel,
            JiraApiService jiraApiService, int issuesTotal, JiraStatusCategory status)
        {
            while (!lastPage)
            {
                var at = startAt;

                await MakeAsyncRequest(url, apiBase, jiraConfig, jiraQuery, viewModel, jiraApiService, at);

                lastPage = IsLastPage(50, startAt, issuesTotal);

                if (!lastPage)
                {
                    startAt += 50;
                }
            }

            var lastUpdate = DateTime.Now;
            AppHeartbeatTimer.Elapsed += delegate
            {
                Dispatcher.Invoke(() =>
                {
                    if (DateTime.Now.Subtract(lastUpdate) > TimeSpan.FromSeconds(15))
                    {
                        Console.WriteLine($"Try update {Enum.GetName(typeof(JiraStatusCategory),status)}");
                        MakeAsyncRequest(url, apiBase, jiraConfig, new JiraQuery().StatusCategoryIs(status).UpdatedSince(-15, JiraTimeDifference.Minutes).OrderBy("updated"), viewModel, jiraApiService, 0);
                        lastUpdate = DateTime.Now;
                    }
                });
            };

        }

        private async Task MakeAsyncRequest(string url, string apiBase, JiraConfig jiraConfig,
            JiraQuery jiraQuery, JiraIssueViewModelBase viewModel, JiraApiService jiraApiService, int startAt)
        {
            void Callback()
            {
                var issueResult = new ApiRequestService().MakeRequestWithBasicAuth<APIIssueRequestRoot>(
                    new Uri($"{url}{apiBase}"), jiraConfig.Username, Encoding.UTF8.GetString(Convert.FromBase64String(jiraConfig.Password)), $"{jiraQuery}&startAt={startAt}");

                if (issueResult.StatusCode == 200)
                {
                    viewModel.AddOrUpdateNewIssues(jiraApiService.ParseIssues(issueResult));
                    viewModel.Refresh();
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            await Task.Run(() => Dispatcher.Invoke((Action)Callback));
        }

        private void RefreshHierarchy()
        {
            var viewModel = (WindowViewModel)DataContext;
            foreach (var viewModelWindow in viewModel.Windows)
            {
                viewModelWindow.Refresh();
            }
        }
    }
}
