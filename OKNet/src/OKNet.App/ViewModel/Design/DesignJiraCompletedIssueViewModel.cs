using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OKNet.App.ViewModel.Jira;

namespace OKNet.App.ViewModel.Design
{
    public class DesignJiraCompletedIssueViewModel : JiraCompletedIssueViewModel
    {
        public DesignJiraCompletedIssueViewModel()
        {
            Width = "800";
            Height = "800";
            Page = 0;
            Projects = new ObservableCollection<JiraProjectViewModel>
            {
                new JiraProjectViewModel { Id = 1, Name = "OKNET", Key = "OK"},
                new JiraProjectViewModel { Id = 2, Name = "OKNETCore", Key = "OKCORE"},
                new JiraProjectViewModel { Id = 3, Name = "OKNETJira", Key = "OKJira" },
                new JiraProjectViewModel { Id = 4, Name = "OKNETCommon", Key = "OKCMN" }
            };
            var executions = 0;
            var random = new Random();
            DateTime GetUtcDropoff()
            {
                executions++;
                return DateTime.UtcNow.AddMinutes(-executions * random.Next(3, 20));
            }

            AddOrUpdateNewIssues(new List<JiraIssueViewModel>
            {
                new DesignJiraIssueViewModel(),
                new JiraIssueViewModel { Updated=GetUtcDropoff(), Key = "OKNET-2", Name = "PoC Website ViewModel", ProjectId = 1, Component = new ObservableCollection<JiraComponentViewModel> {new JiraComponentViewModel {Name = "WPF", Id = 1} } },
                new JiraIssueViewModel { Updated=GetUtcDropoff(), Key = "OKNET-3", Name = "PoC Jira Completed ViewModel", ProjectId = 1, Component = new ObservableCollection<JiraComponentViewModel> {new JiraComponentViewModel {Name = "JIRA", Id = 2} } },
                new JiraIssueViewModel { Updated=GetUtcDropoff(), Key = "OKNET-4", Name = "PoC Jira In-Progress ViewModel", ProjectId = 1, Component = new ObservableCollection<JiraComponentViewModel> {new JiraComponentViewModel {Name = "JIRA", Id = 1} } },
                new JiraIssueViewModel { Updated=GetUtcDropoff(), Key = "OKNETJira-1", Name = "Develop Completed Issue API Call", ProjectId = 3, Component = new ObservableCollection<JiraComponentViewModel> {new JiraComponentViewModel {Name = "", Id = 2} } },
                new JiraIssueViewModel { Updated=GetUtcDropoff(), Key = "OKNETCommon-1", Name = "Develop configuration viewmodel base for kiosk settings", ProjectId = 4, Component = new ObservableCollection<JiraComponentViewModel> {new JiraComponentViewModel {Name = "WPF", Id = 3} } }
            });
            Refresh();
        }
    }
}