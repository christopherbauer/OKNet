using System;
using System.Collections.ObjectModel;

namespace OKNet.App.ViewModel.Design
{
    public class DesignIssueViewModel : JiraIssueViewModel
    {
        public DesignIssueViewModel()
        {
            Key = "RATES-1";
            Status = "In Dev";
            StatusCategory = "In Development";
            Name = "Create Nightly Rate Plan";
            ProjectId = 2;
            Component = new ObservableCollection<ComponentViewModel>
                {new ComponentViewModel {Name = "RatesV2", Id = 1}};
            Updated = DateTime.Now.AddHours(-2);
        }
    }
}