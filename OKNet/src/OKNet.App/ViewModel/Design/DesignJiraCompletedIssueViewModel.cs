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