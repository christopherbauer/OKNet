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
        private int _page;
        private int _pageSize;

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
            get => _projects;
            set
            {
                SetValue(ref _projects, value);
                OnPropertyChanged(nameof(GetVisibleProjects));
            }
        }

        public int Page
        {
            get => _page;
            set
            {
                SetValue(ref _page, value);
                OnPropertyChanged(nameof(GetVisibleIssues));
            }
        }

        public ObservableCollection<JiraIssueViewModel> GetVisibleIssues
        {
            get
            {
                return new ObservableCollection<JiraIssueViewModel>(Issues.OrderByDescending(model => model.Updated)
                    .Skip(Page * PageSize).Take(PageSize));
            }
        }

        public virtual ObservableCollection<JiraProjectViewModel> GetVisibleProjects => new ObservableCollection<JiraProjectViewModel>(Projects.ToList());
        public virtual string GetIssue => $"{IssuesTotal}";

        public int PageSize
        {
            get => _pageSize;
            set
            {
                SetValue(ref _pageSize, value);
                OnPropertyChanged(nameof(GetVisibleIssues));
            }
        }

        private void RefreshProjectCounts()
        {
            foreach (var projectViewModel in Projects)
            {
                projectViewModel.Count = Issues.Count(viewModel =>
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
            OnPropertyChanged(nameof(GetVisibleProjects));
            OnPropertyChanged(nameof(GetVisibleIssues));
            OnPropertyChanged(nameof(GetIssue));
        }

        public override void Refresh()
        {
            RefreshProjectCounts();
            foreach (var issueViewModel in Issues)
            {
                issueViewModel.Refresh();
            }
        }
    }
}