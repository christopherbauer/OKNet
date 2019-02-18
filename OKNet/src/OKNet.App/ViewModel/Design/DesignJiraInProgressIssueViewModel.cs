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
                {new JiraProjectViewModel {Key = "OKNET", Id = 1, CountInProgress = 500, Name = "ONKET Development"}};
            AddOrUpdateNewIssues(new List<JiraIssueViewModel>
            {
                new DesignJiraIssueViewModel(),
                new JiraIssueViewModel
                {
                    Key = "OKNET-2", Name = "OKNET Jira In Progress", ProjectId = 1, Status = "Fixed",
                    StatusCategory = "Done", Updated = DateTime.UtcNow.AddHours(-3),
                    Component = new ObservableCollection<JiraComponentViewModel>
                        {new JiraComponentViewModel {Id = 1, Name = "OKNET"}}
                },
                new JiraIssueViewModel
                {
                    Key = "OKNET-3", Name = "OKNET Jira Bar Chart", ProjectId = 1, Status = "In Development",
                    StatusCategory = "Development", Updated = DateTime.UtcNow.AddHours(-1),
                    Component = new ObservableCollection<JiraComponentViewModel>
                        {new JiraComponentViewModel {Id = 1, Name = "OKNET"}}
                }
            });
            GraphViewModel = new DesignJiraInProgressGraphViewModel();
        }
    }
}