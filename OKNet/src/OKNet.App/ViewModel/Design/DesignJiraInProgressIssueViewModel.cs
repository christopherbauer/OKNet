using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OKNet.App.ViewModel.Design
{
    public class DesignJiraInProgressIssueViewModel : JiraInProgressIssueViewModel
    {
        public DesignJiraInProgressIssueViewModel()
        {
            Width = "800";
            Height = "800";
            Projects = new ObservableCollection<ProjectViewModel>
                {new ProjectViewModel {Key = "OKNET", Id = 1, CountInProgress = 500, Name = "ONKET Development"}};
            AddOrUpdateNewIssues(new List<IssueViewModel>
            {
                new DesignIssueViewModel(),
                new IssueViewModel
                {
                    Key = "OKNET-100", Name = "OKNET Jira In Progress", ProjectId = 1, StatusCategory = "Development",
                    Updated = DateTime.UtcNow.AddHours(-3),
                    Component = new ObservableCollection<ComponentViewModel> {new ComponentViewModel {Id = 1, Name = "OKNET"}}
                }
            });
            RefreshProjectCounts();
        }
    }
}