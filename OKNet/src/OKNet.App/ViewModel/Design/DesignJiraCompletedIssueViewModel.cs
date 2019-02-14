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
                new ProjectViewModel { Id = 1, Name = "Central-Park", Key = "CP"},
                new ProjectViewModel { Id = 2, Name = "Inventory", Key = "I"},
                new ProjectViewModel { Id = 3, Name = "New Booking Engine", Key = "NBE" },
                new ProjectViewModel { Id = 4, Name = "Booking Engine", Key = "BE" }
            };

            var executions = 0;
            var random = new Random();
            DateTime GetUtcDropoff()
            {
                executions++;
                return DateTime.UtcNow.AddMinutes(-executions*random.Next(3, 20));
            }

            AddOrUpdateNewIssues(new List<IssueViewModel>
            {
                new IssueViewModel { Updated=GetUtcDropoff(), Key = "RATES-1", Name = "Create Nightly Rate Plan", ProjectId = 2, Component = new ObservableCollection<ComponentViewModel> {new ComponentViewModel {Name = "RatesV2", Id = 1} } },
                new IssueViewModel { Updated=GetUtcDropoff(), Key = "RATES-2", Name = "Create Derived Rate Plan", ProjectId = 2, Component = new ObservableCollection<ComponentViewModel> {new ComponentViewModel {Name = "RatesV2", Id = 1} } },
                new IssueViewModel { Updated=GetUtcDropoff(), Key = "RATES-3", Name = "Create Package Rate Plan", ProjectId = 2, Component = new ObservableCollection<ComponentViewModel> {new ComponentViewModel {Name = "RatesV2", Id = 1} } },
                new IssueViewModel { Updated=GetUtcDropoff(), Key = "RATES-4", Name = "Create Interval Rate Plan", ProjectId = 2, Component = new ObservableCollection<ComponentViewModel> {new ComponentViewModel {Name = "RatesV2", Id = 1} } },
                new IssueViewModel { Updated=GetUtcDropoff(), Key = "CP-1", Name = "Create Park", ProjectId = 1, Component = new ObservableCollection<ComponentViewModel> {new ComponentViewModel {Name = "DevCP", Id = 2} } },
                new IssueViewModel { Updated=GetUtcDropoff(), Key = "NBE-1", Name = "Create new Booking Engine", ProjectId = 3, Component = new ObservableCollection<ComponentViewModel> {new ComponentViewModel {Name = "NBE", Id = 3} } }
            });
            RefreshProjectCounts();
        }
    }
}