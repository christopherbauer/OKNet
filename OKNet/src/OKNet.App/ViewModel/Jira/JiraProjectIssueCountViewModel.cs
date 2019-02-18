using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace OKNet.App.ViewModel.Jira
{
    public class JiraProjectIssueCountViewModel : ViewModelBase
    {
        private int _projectId;
        private int _width;
        private string _projectName;
        private int _height;
        private int _allProjectIssuesCount;
        private ObservableCollection<JiraIssueCountViewModel> _projectIssues;


        public int ProjectId
        {
            get => _projectId;
            set => SetValue(ref _projectId, value);
        }

        public ObservableCollection<JiraIssueCountViewModel> ProjectIssues
        {
            get => _projectIssues;
            set
            {
                SetValue(ref _projectIssues, value);
                Refresh();
            }
        }

        public string ProjectName
        {
            get => _projectName;
            set => SetValue(ref _projectName, value);
        }

        public int Width
        {
            get => _width;
            set => SetValue(ref _width, value);
        }

        public int Height
        {
            get => _height;
            set
            {
                SetValue(ref _height, value);
                Refresh();
            }
        }

        public int AllProjectIssuesCount
        {
            get => _allProjectIssuesCount;
            set => SetValue(ref _allProjectIssuesCount, value);
        }

        public override void Refresh()
        {
            if (ProjectIssues != null)
            {
                decimal totalLeftover = 0;
                foreach (var jiraIssueCountViewModel in ProjectIssues)
                {
                    jiraIssueCountViewModel.ParentHeight = Height;
                    jiraIssueCountViewModel.AllProjectIssueCountTotal = AllProjectIssuesCount;
                    totalLeftover += jiraIssueCountViewModel.Leftover;
                }
                for (var i = 0; i <= totalLeftover; i++)
                {
                    ProjectIssues[i].HeightAdjustment = 1;
                }
            }

            base.Refresh();
        }
    }
}