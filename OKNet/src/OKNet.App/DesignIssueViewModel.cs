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
            Key = "OKNET-1";
            Status = "In Development";
            StatusCategory = "Development";
            Name = "Create OKNET Kiosk Viewer WPF App";
            ProjectId = 1;
            Component = new ObservableCollection<ComponentViewModel>
                {new ComponentViewModel {Name = "", Id = 1}};
            Updated = DateTime.Now.AddHours(-2);
        }
    }
}