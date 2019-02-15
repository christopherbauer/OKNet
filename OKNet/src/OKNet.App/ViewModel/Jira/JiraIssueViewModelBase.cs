using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OKNet.App.ViewModel.Jira
{
    public class JiraIssueViewModelBase : WindowConfigViewModel
    {
        private ObservableCollection<JiraIssueViewModel> _issues = new ObservableCollection<JiraIssueViewModel>();
        private ObservableCollection<JiraProjectViewModel> _projects;
        private int _issuesTotal;

        public int IssuesTotal
        {
            get => _issuesTotal;
            set => SetValue(ref _issuesTotal, value);
        }

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
                projectViewModel.CountCompleted = Issues.Count(viewModel =>
                    viewModel.ProjectId == Convert.ToInt32(projectViewModel.Id));
            }
        }

        public virtual void AddOrUpdateNewIssues(IEnumerable<JiraIssueViewModel> issueViewModels)
        {
            //This could be better but YOLO
            var issuesToUpdate = issueViewModels.Where(model => Issues.Any(currentIssues => currentIssues.Key == model.Key));
            foreach (var issueViewModel in issuesToUpdate)
            {
                Issues.Remove(Issues.Single(currentIssue => currentIssue.Key == issueViewModel.Key));
            }

            var newIssues = issueViewModels.OrderByDescending(model => model.Updated)
                .Where(model => Issues.All(viewModel => viewModel.Key != model.Key));
            foreach (var issueViewModel in newIssues)
            {
                Issues.Add(issueViewModel);
            }


            IssuesTotal = Issues.Count;
        }

        public override void Refresh()
        {
            foreach (var issueViewModel in Issues)
            {
                issueViewModel.Refresh();
            }
        }
    }
}