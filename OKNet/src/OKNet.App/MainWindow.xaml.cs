using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OKNet.App.ViewModel;
using OKNet.Common;
using OKNet.Core;
using OKNet.Infrastructure.Jira;

namespace OKNet.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            InitialLoad();
        }

        private void InitialLoad()
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

                    var jiraQuery = new JiraQuery().StatusCategoryIs(JiraStatusCategory.IN_PROGRESS).OrderBy("updated");


                    var jiraConfigPassword = Encoding.UTF8.GetString(Convert.FromBase64String(jiraConfig.Password));

                    var projectsResult = new ApiRequestService().MakeRequestWithBasicAuth<List<ProjectApiModel>>(new Uri($"{url}/project"), jiraConfig.Username, jiraConfigPassword, "");

                    var viewModel = new JiraInProgressIssueViewModel
                    {
                        Width = jiraConfig.Width,
                        Height = jiraConfig.Height,
                    };

                    if (projectsResult.StatusCode == 200)
                    {
                        viewModel.Projects = JiraApiService.ParseProjects(projectsResult);
                    }
                    windowConfigViewModels.Add(viewModel);


                    bool lastPage = false;
                    var startAt = 0;
                    while (!lastPage)
                    {
                        var issueResult = new ApiRequestService().MakeRequestWithBasicAuth<APIIssueRequestRoot>(new Uri($"{url}{apiBase}"), jiraConfig.Username, jiraConfigPassword, jiraQuery.ToString(), startAt);
                        if (issueResult.StatusCode == 200)
                        {
                            viewModel.IssuesTotal = issueResult.Data.total;
                            viewModel.AddNewIssues(jiraApiService.ParseIssues(issueResult));
                            viewModel.RefreshProjectCounts();
                        }
                        if (issueResult.Data.startAt >= issueResult.Data.total)
                        {
                            lastPage = true;
                        }
                        else
                        {
                            startAt += issueResult.Data.issues.Length;
                        }
                    }
                }
                if (windowConfig.Type == "jira-complete")
                {
                    var jiraConfig = configService.GetConfig<JiraConfig>(pathString);

                    string url = $"https://{jiraConfig.ApiHost}";
                    string apiBase = "/search";

                    var jiraQuery = new JiraQuery().ResolvedSince(DateTime.Today).StatusCategoryIs(JiraStatusCategory.COMPLETE)
                        .OrderBy("updated");


                    var jiraConfigPassword = Encoding.UTF8.GetString(Convert.FromBase64String(jiraConfig.Password));

                    var issueResult = new ApiRequestService().MakeRequestWithBasicAuth<APIIssueRequestRoot>(new Uri($"{url}{apiBase}"), jiraConfig.Username, jiraConfigPassword, jiraQuery.ToString());

                    var projects = new ApiRequestService().MakeRequestWithBasicAuth<List<ProjectApiModel>>(new Uri($"{url}/project"), jiraConfig.Username, jiraConfigPassword, "");

                    if (issueResult.StatusCode == 200)
                    {
                        var item = new JiraCompletedIssueViewModel
                        {
                            Width = jiraConfig.Width,
                            Height = jiraConfig.Height,
                            Projects = new ObservableCollection<ProjectViewModel>(projects.Data.Select(model =>
                                new ProjectViewModel
                                {
                                    Name = model.name,
                                    Key = model.key,
                                    Id = Convert.ToInt32(model.id),
                                })),
                            IssuesTotal = issueResult.Data.total
                        };
                        item.AddNewIssues(jiraApiService.ParseIssues(issueResult));
                        item.RefreshProjectCounts();
                        windowConfigViewModels.Add(item);
                    }
                }

            }

            DataContext = new WindowViewModel
            { Windows = new ObservableCollection<ViewModelBase>(windowConfigViewModels) };
        }
    }
}
