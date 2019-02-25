using System;
using System.Collections.ObjectModel;
using OKNet.App.ViewModel.Jira;

namespace OKNet.App.ViewModel.Design
{
    public class DesignJiraIssueViewModel : JiraIssueViewModel
    {
        public DesignJiraIssueViewModel()
        {
            Key = "OKNET-1";
            Status = "In Development";
            StatusCategoryName = "Development";
            Name = "Create OKNET Kiosk Viewer WPF App";
            ProjectId = 1;
            Component = new ObservableCollection<JiraComponentViewModel>
                {new JiraComponentViewModel {Name = "", Id = 1}};
            Updated = DateTime.Now.AddHours(-2);
        }
    }
}