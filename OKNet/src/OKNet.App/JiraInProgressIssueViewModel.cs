using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OKNet.App.ViewModel;

namespace OKNet.App
{
    public class JiraInProgressIssueViewModel : WindowConfigViewModel
    {
        private ObservableCollection<ProjectViewModel> _projects;
        private ObservableCollection<IssueViewModel> _issues = new ObservableCollection<IssueViewModel>();

        public ObservableCollection<IssueViewModel> Issues
        {
            get => _issues;
            set => SetValue(ref _issues, value);
        }

        public ObservableCollection<ProjectViewModel> Projects
        {
            get { return _projects; }
            set
            {
                SetValue(ref _projects, value);
                OnPropertyChanged(nameof(GetVisibleProjects));
            }
        }

        public ObservableCollection<ProjectViewModel> GetVisibleProjects => new ObservableCollection<ProjectViewModel>(Projects.ToList());
        public int IssueCount => Issues.Count;
        public string GetIssue => $"{IssueCount} issue(s) In Progress";
        public void RefreshProjectCounts()
        {
            foreach (var projectViewModel in Projects)
            {
                projectViewModel.CountCompleted = Issues.Count(viewModel => viewModel.ProjectId == Convert.ToInt32(projectViewModel.Id));
            }
        }

        public void AddNewIssues(IEnumerable<IssueViewModel> issueViewModels)
        {
            var newIssues = issueViewModels.OrderByDescending(model => model.Updated).Where(model => Issues.All(viewModel => viewModel.Key != model.Key));
            foreach (var issueViewModel in newIssues)
            {
                Issues.Add(issueViewModel);
            }
        }

    }
}