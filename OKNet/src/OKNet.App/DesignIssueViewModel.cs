using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OKNet.App.ViewModel;

namespace OKNet.App
{
    public class DesignIssueViewModel : IssueViewModel
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