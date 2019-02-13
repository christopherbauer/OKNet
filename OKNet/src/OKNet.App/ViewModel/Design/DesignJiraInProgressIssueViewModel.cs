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
            Projects = new ObservableCollection<JiraProjectViewModel>
                {new JiraProjectViewModel {Key = "RATES", Id = 1, CountInProgress = 500, Name = "RATES"}};
            AddNewIssues(new List<JiraIssueViewModel>
            {
                new DesignIssueViewModel(),
                new JiraIssueViewModel
                {
                    Key = "RATES-100", Name = "Rates In Progress Item", ProjectId = 1, StatusCategory = "In Development",
                    Updated = DateTime.UtcNow.AddHours(-3),
                    Component = new ObservableCollection<ComponentViewModel> {new ComponentViewModel {Id = 1, Name = "RATES"}}
                }
            });
            RefreshProjectCounts();
        }
    }
}