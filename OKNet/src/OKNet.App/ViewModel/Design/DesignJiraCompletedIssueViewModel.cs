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
            PageSize = 5;
            Projects = new ObservableCollection<JiraProjectViewModel>
            {
                new JiraProjectViewModel { Id = 1, Name = "OKNET", Key = "OK"},
                new JiraProjectViewModel { Id = 2, Name = "OKNETCore", Key = "OKCORE"},
                new JiraProjectViewModel { Id = 3, Name = "OKNETJira", Key = "OKJira" },
                new JiraProjectViewModel { Id = 4, Name = "OKNETCommon", Key = "OKCMN" }
            };

            AddOrUpdateNewIssues(new List<JiraIssueViewModel>
            {
                new DesignJiraIssueViewModel(),
                GetIssueViewModel("OKNET-2", "PoC Website ViewModel", 1, new List<JiraComponentViewModel> {new JiraComponentViewModel {Name = "WPF", Id = 1}}),
                GetIssueViewModel("OKNET-3", "PoC Jira Completed ViewModel", 1, new List<JiraComponentViewModel> {new JiraComponentViewModel {Name = "JIRA", Id = 2}}),
                GetIssueViewModel("OKNET-4", "PoC Jira In-Progress ViewModel", 1, new List<JiraComponentViewModel> {new JiraComponentViewModel {Name = "JIRA", Id = 1}}),
                GetIssueViewModel("OKNETJira-1", "Develop Completed Issue API Call", 3, new List<JiraComponentViewModel> {new JiraComponentViewModel {Name = "", Id = 2}}),
                GetIssueViewModel("OKNETJira-2", "Develop In-Progress Issue API Call", 3, new List<JiraComponentViewModel> {new JiraComponentViewModel {Name = "", Id = 2}}),
                GetIssueViewModel("OKNETJira-3", "Refactor Issue Refresh Code to Normalize the refresh interval", 3, new List<JiraComponentViewModel> {new JiraComponentViewModel {Name = "", Id = 2}}),
                GetIssueViewModel("OKNETJira-3", "Issue Refresh Code should occasionally do a longer check and remove any items that do not show up in subsequent calls", 3, new List<JiraComponentViewModel> {new JiraComponentViewModel {Name = "", Id = 2}}),
                GetIssueViewModel("OKNETCommon-1", "Develop configuration viewmodel base for kiosk settings", 4, new List<JiraComponentViewModel> {new JiraComponentViewModel {Name = "WPF", Id = 3}}),
                GetIssueViewModel("OKNET-5", "Add Paging Display to In-Progress VM", 1, new List<JiraComponentViewModel> {new JiraComponentViewModel {Name = "JIRA", Id = 1}}),
                GetIssueViewModel("OKNET-6", "Add Paging Display to Complete VM", 1, new List<JiraComponentViewModel> {new JiraComponentViewModel {Name = "JIRA", Id = 1}}),
                GetIssueViewModel("OKNET-7", "Add Project Issue Breakdown Stacked bar chart to In-Progress VM", 1, new List<JiraComponentViewModel> {new JiraComponentViewModel {Name = "JIRA", Id = 1}}),
            });
        }

        int _executions;
        readonly Random _random = new Random();
        private DateTime GetUtcDropoff()
        {
            _executions++;
            return DateTime.Now.AddMinutes(-_executions * _random.Next(3, 20));
        }
        private JiraIssueViewModel GetIssueViewModel(string key, string name, int projectId,
            List<JiraComponentViewModel> componentViewModels)
        {
            return new JiraIssueViewModel { Updated=GetUtcDropoff(), Key = key, Name = name, ProjectId = projectId, Component = new ObservableCollection<JiraComponentViewModel>(componentViewModels) };
        }
    }
}