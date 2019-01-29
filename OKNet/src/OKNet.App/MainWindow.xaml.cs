using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using OKNet.App.ViewModel;
using OKNet.Common;
using OKNet.Core;
using OKNet.Infrastructure.Jira;
using Type = System.Type;

namespace OKNet.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
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
                if (windowConfig.Type == "jira")
                {
                    var jiraConfig = configService.GetConfig<JiraConfig>(pathString);
                    
                    string url = $"https://{jiraConfig.ApiHost}";
                    string apiBase = "/search";
                    var jiraQuery = new JiraQuery().ResolvedSince(DateTime.Today).StatusCategoryIs("Done")
                        .OrderBy("updated");

                    var issueResult = new ApiRequestService().MakeRequestWithBasicAuth<APIIssueRequestRoot>(new Uri($"{url}{apiBase}"), jiraConfig.Username, jiraConfig.Password, jiraQuery.ToString());

                    var projects = new ApiRequestService().MakeRequestWithBasicAuth<List<ProjectApiModel>>(new Uri($"{url}/project"), jiraConfig.Username, jiraConfig.Password, "");

                    if (issueResult.StatusCode == 200)
                    {
                        var issueViewModels = issueResult.Data.issues.Select(
                            model => new IssueViewModel
                            {
                                Key = model.key,
                                Name = model.fields.summary,
                                Component = new ObservableCollection<ComponentViewModel>(
                                    model.fields.components.Select(component => new ComponentViewModel
                                    {
                                        Id = Convert.ToInt32(component.id),
                                        Name = component.name
                                    })),
                                ProjectId = Convert.ToInt32(model.fields.project.id),
                                Updated = model.fields.updated
                            });
                        var item = new JiraViewModel
                        {
                            Width = jiraConfig.Width,
                            Height = jiraConfig.Height,
                            Projects = new ObservableCollection<ProjectViewModel>(projects.Data.Select(model =>
                                new ProjectViewModel
                                {
                                    Name = model.name,
                                    Key = model.key,
                                    Id = Convert.ToInt32(model.id)
                                }))
                        };
                        item.AddNewIssues(issueViewModels);
                        item.RefreshProjectCounts();
                        windowConfigViewModels.Add(item);
                    }
                }

            }

            DataContext = new WindowViewModel { Windows = windowConfigViewModels };
            InitializeComponent();
        }
    }
}
