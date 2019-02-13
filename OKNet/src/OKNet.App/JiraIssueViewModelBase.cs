using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OKNet.App.ViewModel;

namespace OKNet.App
{
    public class JiraIssueViewModelBase : WindowConfigViewModel
    {
        protected ObservableCollection<JiraIssueViewModel> _issues = new ObservableCollection<JiraIssueViewModel>();
        protected ObservableCollection<JiraProjectViewModel> _projects;
        public int IssuesTotal { get; set; }

        public ObservableCollection<JiraIssueViewModel> Issues
        {
            get => _issues;
            set => SetValue(ref _issues, value);
        }

        public ObservableCollection<JiraProjectViewModel> Projects
        {
            get { return _projects; }
            set
            {
                SetValue(ref _projects, value);
                OnPropertyChanged(nameof(JiraInProgressIssueViewModel.GetVisibleProjects));
            }
        }

        public void RefreshProjectCounts()
        {
            foreach (var projectViewModel in Projects)
            {
                projectViewModel.CountCompleted = Issues.Count(viewModel => viewModel.ProjectId == Convert.ToInt32(projectViewModel.Id));
            }
        }

        public void AddNewIssues(IEnumerable<JiraIssueViewModel> issueViewModels)
        {
            var newIssues = issueViewModels.OrderByDescending(model => model.Updated).Where(model => Issues.All(viewModel => viewModel.Key != model.Key));
            foreach (var issueViewModel in newIssues)
            {
                Issues.Add(issueViewModel);
            }
        }
    }
}