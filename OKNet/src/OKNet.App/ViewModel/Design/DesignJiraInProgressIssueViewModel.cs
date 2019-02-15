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
        }
    }

    public class DesignJiraProjectIssueCountViewModel : JiraProjectIssueCountViewModel
    {
        public DesignJiraProjectIssueCountViewModel()
        {
            ProjectId = 1;
            _projectName = "OKNet";
            var todoCount = 20;
            var inDevCount = 4;
            var inQaCount = 12;
            var pmReviewCount = 11;
            var doneCount = 54;
            var total = 200;

            ProjectIssues = new ObservableCollection<JiraIssueCountViewModel>(new List<JiraIssueCountViewModel>
            {
                new JiraIssueCountViewModel { BackgroundColor="Green", Count=doneCount, ParentHeight="200", Status="Done", Total=total },
                new JiraIssueCountViewModel { BackgroundColor="LightBlue", Count=pmReviewCount, ParentHeight="200", Status="PM Review", Total=total }, 
                new JiraIssueCountViewModel { BackgroundColor="Yellow", Count=inQaCount, ParentHeight="200", Status="In QA", Total=total }, 
                new JiraIssueCountViewModel { BackgroundColor="LightGreen", Count=inDevCount, ParentHeight="200", Status="In Dev", Total=total }, 
                new JiraIssueCountViewModel { BackgroundColor="Silver", Count=todoCount, ParentHeight="200", Status="To Do", Total=total }, 
            });
        }
    }

    public class JiraIssueCountViewModel : ViewModelBase
    {
        private string _parentHeight;
        private int _total;
        private int _count;

        public string Status { get; set; }
        public string BackgroundColor { get; set; }

        public int Count
        {
            get => _count;
            set => SetValue(ref _count, value);
        }

        public int Total
        {
            get => _total;
            set => SetValue(ref _total, value);
        }

        public string ParentHeight
        {
            get => _parentHeight;
            set => SetValue(ref _parentHeight, value);
        }

        public int Height => (int) (Convert.ToInt32(ParentHeight) * ((decimal)Count / Total));
    }

    public class JiraProjectIssueCountViewModel : ViewModelBase
    {
        private int _projectId;
        private ObservableCollection<JiraIssueCountViewModel> _projectIssues;
        protected string _projectName;

        public int ProjectId
        {
            get => _projectId;
            set => SetValue(ref _projectId, value);
        }

        public ObservableCollection<JiraIssueCountViewModel> ProjectIssues
        {
            get => _projectIssues;
            set => SetValue(ref _projectIssues, value);
        }

        public string ProjectName
        {
            get => _projectName;
            set => SetValue(ref _projectName, value);
        }
    }
}