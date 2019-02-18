using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        protected internal Timer AppHeartbeatTimer = new Timer { Interval = (int)TimeSpan.FromSeconds(5).TotalMilliseconds };
        private DateTime _lastUpdate = DateTime.MinValue;

        public MainWindow()
        {
            InitializeComponent();

            AppHeartbeatTimer.Start();

            InitialLoad();
        }

        private async void InitialLoad()
        {

            var configService = new ConfigService();

            var names = configService.GetNames("windows").ToList();
            var windowConfigViewModels = new List<ViewModelBase>();
            for (var i = 0; i < names.Count(); i++)
            {
                var pathString = $"windows:{names[i]}";
                Console.WriteLine(pathString);
                var windowConfig = configService.GetConfig<WindowConfig>(pathString);
                if (windowConfig.Type == "web")
                {
                    var websiteConfig = configService.GetConfig<WebsiteConfig>(pathString);
                    windowConfigViewModels.Add(new BasicWebsiteViewModel
                    {
                        Uri = websiteConfig.Uri,
                        Type = websiteConfig.Type,
                        Width = websiteConfig.Width,
                        Height = websiteConfig.Height
                    });
                }

                var jiraApiService = new JiraApiService();
                if (windowConfig.Type == "jira-inprogress")
                {
                    var jiraConfig = configService.GetConfig<JiraConfig>(pathString);

                    string url = $"https://{jiraConfig.ApiHost}";
                    string apiBase = "/search";

                    var jiraQuery = new JiraQuery().StatusCategoryIs(JiraStatusCategory.IN_PROGRESS).UpdatedSince(-30, JiraTimeDifference.Days).OrderBy("updated");

                    var jiraConfigPassword = Encoding.UTF8.GetString(Convert.FromBase64String(jiraConfig.Password));

                    var projectsResult = new ApiRequestService().MakeRequestWithBasicAuth<List<ProjectApiModel>>(new Uri($"{url}/project"), jiraConfig.Username, jiraConfigPassword, "");

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


                    bool lastPage = false;
                    var startAt = 0;

                    var issueResult = jiraApiService.MakeRequestWithBasicAuth<APIIssueRequestRoot>(new Uri($"{url}{apiBase}"), jiraConfig.Username, jiraConfigPassword, jiraQuery.ToString());
                    if (issueResult.StatusCode == 200)
                    {
                        viewModel.IssuesTotal = issueResult.Data.total;
                        viewModel.AddOrUpdateNewIssues(jiraApiService.ParseIssues(issueResult));
                        viewModel.RefreshProjectCounts();
                    }
                    if (issueResult.Data.startAt + 50 >= issueResult.Data.total)
                    {
                        lastPage = true;
                    }
                    else
                    {
                        startAt += issueResult.Data.issues.Length;
                    }

                    SetupJiraWindow(lastPage, startAt, url, apiBase, jiraConfig, jiraConfigPassword, jiraQuery, viewModel, jiraApiService, issueResult.Data.total, JiraStatusCategory.IN_PROGRESS);
                }

                if (windowConfig.Type == "jira-complete")
                {
                    var jiraConfig = configService.GetConfig<JiraConfig>(pathString);

                    string url = $"https://{jiraConfig.ApiHost}";
                    string apiBase = "/search";

                    var jiraQuery = new JiraQuery().ResolvedSince(DateTime.Today).StatusCategoryIs(JiraStatusCategory.COMPLETE)
                        .OrderBy("updated");


                    var jiraConfigPassword = Encoding.UTF8.GetString(Convert.FromBase64String(jiraConfig.Password));
                    
                    var projectsResult = new ApiRequestService().MakeRequestWithBasicAuth<List<ProjectApiModel>>(new Uri($"{url}/project"),
                        jiraConfig.Username, jiraConfigPassword, "");

                    var viewModel = new JiraInProgressIssueViewModel
                    {
                        Width = jiraConfig.Width,
                        Height = jiraConfig.Height,
                    };

                    if (projectsResult.StatusCode == 200)
                    {
                        viewModel.Projects = new ObservableCollection<JiraProjectViewModel>(projectsResult.Data.Select(
                            model => new JiraProjectViewModel
                                {
                                    Name = model.name,
                                    Key = model.key,
                                    Id = Convert.ToInt32(model.id),
                                    Width = (int) (Convert.ToInt32(jiraConfig.Width) / 3m - 4)
                                }));
                    }

                    windowConfigViewModels.Add(viewModel);
                    
                    bool lastPage = false;
                    var startAt = 0;

                    var issueResult = jiraApiService.MakeRequestWithBasicAuth<APIIssueRequestRoot>(new Uri($"{url}{apiBase}"),
                        jiraConfig.Username, jiraConfigPassword, jiraQuery.ToString());

                    if (issueResult.StatusCode == 200)
                    {
                        viewModel.IssuesTotal = issueResult.Data.total;
                        viewModel.AddOrUpdateNewIssues(jiraApiService.ParseIssues(issueResult));
                        viewModel.RefreshProjectCounts();
                    }
                    if (issueResult.Data.startAt + 50 >= issueResult.Data.total)
                    {
                        lastPage = true;
                    }
                    else
                    {
                        startAt += issueResult.Data.issues.Length;
                    }

                    SetupJiraWindow(lastPage, startAt, url, apiBase, jiraConfig, jiraConfigPassword, jiraQuery, viewModel, jiraApiService, issueResult.Data.total, JiraStatusCategory.COMPLETE);

                }

            }

            var isDebugMode = configService.Get<bool>("isDebugMode");
            DataContext = new WindowViewModel { Windows = new ObservableCollection<ViewModelBase>(windowConfigViewModels), IsDebugMode = isDebugMode };

            AppHeartbeatTimer.Elapsed += (sender, args) => Dispatcher.Invoke(RefreshHierarchy);
        }

        private async Task SetupJiraWindow(bool lastPage, int startAt, string url, string apiBase,
            JiraConfig jiraConfig, string jiraConfigPassword, JiraQuery jiraQuery, JiraInProgressIssueViewModel viewModel,
            JiraApiService jiraApiService, int issuesTotal, JiraStatusCategory status)
        {
            while (!lastPage)
            {
                var at = startAt;

                await MakeAsyncRequest(url, apiBase, jiraConfig, jiraConfigPassword, jiraQuery, viewModel, jiraApiService, at);

                if (startAt + 50 >= issuesTotal)
                {
                    lastPage = true;
                }
                else
                {
                    startAt += 50;
                }
            }

            _lastUpdate = DateTime.Now;
            AppHeartbeatTimer.Elapsed += (sender, args) => Dispatcher.Invoke(() =>
            {
                Console.WriteLine("ELAPSED");
                if (DateTime.Now.Subtract(_lastUpdate) > TimeSpan.FromMinutes(1))
                {
                    Console.WriteLine("TRYUPDATE");
                    MakeAsyncRequest(url, apiBase, jiraConfig, jiraConfigPassword, new JiraQuery().StatusCategoryIs(status).UpdatedSince(-15, JiraTimeDifference.Minutes).OrderBy("updated"), viewModel,
                        jiraApiService, 0);
                    _lastUpdate = DateTime.Now;
                }

            });

        }

        private async Task MakeAsyncRequest(string url, string apiBase, JiraConfig jiraConfig, string jiraConfigPassword,
            JiraQuery jiraQuery, JiraInProgressIssueViewModel viewModel, JiraApiService jiraApiService, int startAt)
        {
            void Callback()
            {
                var issueResult = jiraApiService.MakeRequestWithBasicAuth<APIIssueRequestRoot>(
                    new Uri($"{url}{apiBase}"), jiraConfig.Username, jiraConfigPassword, jiraQuery.ToString());
                if (issueResult.StatusCode == 200)
                {
                    viewModel.AddOrUpdateNewIssues(jiraApiService.ParseIssues(issueResult));
                    viewModel.RefreshProjectCounts();
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
