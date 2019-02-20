using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OKNet.App.ViewModel.Jira;

namespace OKNet.App.ViewModel.Design
{
    public class DesignJiraInProgressIssueViewModel : JiraInProgressIssueViewModel
    {
        public DesignJiraInProgressIssueViewModel()
        {
            Width = "800";
            Height = "800";
            Projects = new ObservableCollection<JiraProjectViewModel>
                {new JiraProjectViewModel {Key = "OKNET", Id = 1, Count = 500, Name = "ONKET Development"}};
            AddOrUpdateNewIssues(new List<JiraIssueViewModel>
            {
                new DesignJiraIssueViewModel(),
                new JiraIssueViewModel
                {
                    Key = "OKNET-100", Name = "OKNET Jira In Progress", ProjectId = 1, StatusCategory = "Development",
                    Updated = DateTime.UtcNow.AddHours(-3),
                    Component = new ObservableCollection<JiraComponentViewModel> {new JiraComponentViewModel {Id = 1, Name = "OKNET"}}
                }
            });
            Refresh();
        }
    }
}