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
            Projects = new ObservableCollection<JiraProjectViewModel>
            {
                new JiraProjectViewModel { Id = 1, Name = "Central-Park", Key = "CP"},
                new JiraProjectViewModel { Id = 2, Name = "Inventory", Key = "I"},
                new JiraProjectViewModel { Id = 3, Name = "New Booking Engine", Key = "NBE" },
                new JiraProjectViewModel { Id = 4, Name = "Booking Engine", Key = "BE" }
            };

            var executions = 0;
            var random = new Random();
            DateTime GetUtcDropoff()
            {
                executions++;
                return DateTime.UtcNow.AddMinutes(-executions*random.Next(3, 20));
            }

            AddNewIssues(new List<JiraIssueViewModel>
            {
                new JiraIssueViewModel { Updated=GetUtcDropoff(), Key = "RATES-1", Name = "Create Nightly Rate Plan", ProjectId = 2, Component = new ObservableCollection<ComponentViewModel> {new ComponentViewModel {Name = "RatesV2", Id = 1} } },
                new JiraIssueViewModel { Updated=GetUtcDropoff(), Key = "RATES-2", Name = "Create Derived Rate Plan", ProjectId = 2, Component = new ObservableCollection<ComponentViewModel> {new ComponentViewModel {Name = "RatesV2", Id = 1} } },
                new JiraIssueViewModel { Updated=GetUtcDropoff(), Key = "RATES-3", Name = "Create Package Rate Plan", ProjectId = 2, Component = new ObservableCollection<ComponentViewModel> {new ComponentViewModel {Name = "RatesV2", Id = 1} } },
                new JiraIssueViewModel { Updated=GetUtcDropoff(), Key = "RATES-4", Name = "Create Interval Rate Plan", ProjectId = 2, Component = new ObservableCollection<ComponentViewModel> {new ComponentViewModel {Name = "RatesV2", Id = 1} } },
                new JiraIssueViewModel { Updated=GetUtcDropoff(), Key = "CP-1", Name = "Create Park", ProjectId = 1, Component = new ObservableCollection<ComponentViewModel> {new ComponentViewModel {Name = "DevCP", Id = 2} } },
                new JiraIssueViewModel { Updated=GetUtcDropoff(), Key = "NBE-1", Name = "Create new Booking Engine", ProjectId = 3, Component = new ObservableCollection<ComponentViewModel> {new ComponentViewModel {Name = "NBE", Id = 3} } }
            });
            RefreshProjectCounts();
        }
    }
}