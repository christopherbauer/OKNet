using System.Collections.ObjectModel;

namespace OKNet.App.ViewModel.Design
{
    public class DesignJiraViewModel : JiraViewModel
    {
        public DesignJiraViewModel()
        {
            Width = "800px";
            Height = "800px";
            Projects = new ObservableCollection<ProjectViewModel>
            {
                new ProjectViewModel { Id = 1, Name = "Central-Park", Key = "CP"},
                new ProjectViewModel { Id = 2, Name = "Inventory", Key = "I"},
                new ProjectViewModel { Id = 3, Name = "New Booking Engine", Key = "NBE" }
            };
            Issues = new ObservableCollection<IssueViewModel>
            {
                new IssueViewModel { Key = "RATES-1", Name = "Create Nightly Rate Plan", ProjectId = 2, Component = new ObservableCollection<ComponentViewModel> {new ComponentViewModel {Name = "RatesV2", Id = 1} } },
                new IssueViewModel { Key = "RATES-2", Name = "Create Derived Rate Plan", ProjectId = 2, Component = new ObservableCollection<ComponentViewModel> {new ComponentViewModel {Name = "RatesV2", Id = 1} } },
                new IssueViewModel { Key = "RATES-3", Name = "Create Package Rate Plan", ProjectId = 2, Component = new ObservableCollection<ComponentViewModel> {new ComponentViewModel {Name = "RatesV2", Id = 1} } },
                new IssueViewModel { Key = "RATES-4", Name = "Create Interval Rate Plan", ProjectId = 2, Component = new ObservableCollection<ComponentViewModel> {new ComponentViewModel {Name = "RatesV2", Id = 1} } },
                new IssueViewModel { Key = "CP-1", Name = "Create Park", ProjectId = 1, Component = new ObservableCollection<ComponentViewModel> {new ComponentViewModel {Name = "DevCP", Id = 2} } },
                new IssueViewModel { Key = "NBE-1", Name = "Create new Booking Engine", ProjectId = 3, Component = new ObservableCollection<ComponentViewModel> {new ComponentViewModel {Name = "NBE", Id = 3} } }
            };
            RefreshProjectCounts();
        }
    }
}