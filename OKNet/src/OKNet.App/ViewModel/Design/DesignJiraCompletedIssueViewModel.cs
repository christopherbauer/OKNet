using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OKNet.App.ViewModel.Design
{
    public class DesignJiraCompletedIssueViewModel : JiraCompletedIssueViewModel
    {
        public DesignJiraCompletedIssueViewModel()
        {
            Width = "800";
            Height = "800";
            Projects = new ObservableCollection<ProjectViewModel>
            {
                new ProjectViewModel { Id = 1, Name = "OKNET", Key = "OK"},
                new ProjectViewModel { Id = 2, Name = "OKNETCore", Key = "OKCORE"},
                new ProjectViewModel { Id = 3, Name = "OKNETJira", Key = "OKJira" },
                new ProjectViewModel { Id = 4, Name = "OKNETCommon", Key = "OKCMN" }
            };

            var executions = 0;
            var random = new Random();
            DateTime GetUtcDropoff()
            {
                executions++;
                return DateTime.UtcNow.AddMinutes(-executions * random.Next(3, 20));
            }

            AddOrUpdateNewIssues(new List<IssueViewModel>
            {
                new DesignIssueViewModel(),
                new IssueViewModel { Updated=GetUtcDropoff(), Key = "OKNET-2", Name = "PoC Website ViewModel", ProjectId = 1, Component = new ObservableCollection<ComponentViewModel> {new ComponentViewModel {Name = "WPF", Id = 1} } },
                new IssueViewModel { Updated=GetUtcDropoff(), Key = "OKNET-3", Name = "PoC Jira Completed ViewModel", ProjectId = 1, Component = new ObservableCollection<ComponentViewModel> {new ComponentViewModel {Name = "JIRA", Id = 2} } },
                new IssueViewModel { Updated=GetUtcDropoff(), Key = "OKNET-4", Name = "PoC Jira In-Progress ViewModel", ProjectId = 1, Component = new ObservableCollection<ComponentViewModel> {new ComponentViewModel {Name = "JIRA", Id = 1} } },
                new IssueViewModel { Updated=GetUtcDropoff(), Key = "OKNETJira-1", Name = "Develop Completed Issue API Call", ProjectId = 3, Component = new ObservableCollection<ComponentViewModel> {new ComponentViewModel {Name = "", Id = 2} } },
                new IssueViewModel { Updated=GetUtcDropoff(), Key = "OKNETCommon-1", Name = "Develop configuration viewmodel base for kiosk settings", ProjectId = 4, Component = new ObservableCollection<ComponentViewModel> {new ComponentViewModel {Name = "WPF", Id = 3} } }
            });
            RefreshProjectCounts();
        }
    }
}