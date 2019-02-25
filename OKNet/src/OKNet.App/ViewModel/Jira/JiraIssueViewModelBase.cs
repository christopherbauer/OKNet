using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using NLog;
using OKNet.App.Command;

namespace OKNet.App.ViewModel.Jira
{
    public class JiraIssueViewModelBase : WindowConfigViewModel
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private ObservableCollection<JiraIssueViewModel> _issues = new ObservableCollection<JiraIssueViewModel>();
        private ObservableCollection<JiraProjectViewModel> _projects;
        private int _issuesTotal;
        private int _page = 1;
        private int _pageSize = 25;
        public ICommand TurnPageCommand;

        public JiraIssueViewModelBase()
        {
            TurnPageCommand = new DelegateCommand(() =>
            {
                var newPage = (Page < TotalPages ? Page + 1 : 1);
                Logger.Trace($"Set page from {Page} to {newPage} ({GetType().Name})");
                Page = newPage;

            }, o => true);
        }
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
                OnPropertyChanged(nameof(GetPageNumber));
                OnPropertyChanged(nameof(GetVisibleIssues));
            }
        }

        public ObservableCollection<JiraIssueViewModel> GetVisibleIssues
        {
            get
            {
                return new ObservableCollection<JiraIssueViewModel>(Issues.OrderByDescending(model => model.Updated)
                    .Skip((Page-1) * PageSize).Take(PageSize).ToList());
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

        public string GetPageNumber => $"{Page} / {TotalPages}";
        public int TotalPages
        {
            get
            {
                if (IssuesTotal > 0)
                {
                    var totalPages = (IssuesTotal / PageSize) + (IssuesTotal % PageSize > 0 ? 1 : 0);
                    Logger.Trace($"New Total Pages: {totalPages} ({IssuesTotal} / {PageSize}) ({GetType().Name})");
                    return totalPages;
                }

                return 1;
            }
        }

        public virtual void AddOrUpdateNewIssues(IEnumerable<JiraIssueViewModel> issueViewModels)
        {
            AddOrUpdateNewIssues(issueViewModels, model => false);
        }

        public virtual void AddOrUpdateNewIssues(IEnumerable<JiraIssueViewModel> issueViewModels, Func<JiraIssueViewModel, bool> removalPredicate)
        {
            var isDirty = false;
            //Efficiency
            var issueViewModelList = issueViewModels.ToList();

            var removeIssueViewModels = issueViewModelList.Where(removalPredicate).ToList();
            foreach (var viewModel in removeIssueViewModels)
            {
                var issueViewModel = Issues.SingleOrDefault(currentIssue => currentIssue.Key == viewModel.Key);
                if (issueViewModel != null && !viewModel.Equals(issueViewModel))
                {
                    Issues.Remove(issueViewModel);
                    isDirty = true;
                }
            }

            var updateIssueViewModels = issueViewModelList.Except(removeIssueViewModels).ToList();

            var issuesToUpdate = updateIssueViewModels.Where(model => Issues.Any(currentIssues => currentIssues.Key == model.Key));
            foreach (var issueViewModel in issuesToUpdate)
            {
                var jiraIssueViewModel = Issues.Single(currentIssue => currentIssue.Key == issueViewModel.Key);
                if(!issueViewModel.Equals(jiraIssueViewModel))
                {
                    Issues.Remove(jiraIssueViewModel);
                    isDirty = true;
                }
            }

            var newIssues = updateIssueViewModels.OrderByDescending(model => model.Updated)
                .Where(model => Issues.All(viewModel => viewModel.Key != model.Key));
            foreach (var issueViewModel in newIssues)
            {
                Issues.Add(issueViewModel);
                isDirty = true;
            }

            if (isDirty)
            {
                Page = 1;
                IssuesTotal = Issues.Count;
                OnPropertyChanged(nameof(GetPageNumber));
                Refresh();
                OnPropertyChanged(nameof(GetVisibleProjects));
                OnPropertyChanged(nameof(Issues));
                OnPropertyChanged(nameof(GetVisibleIssues));
                OnPropertyChanged(nameof(GetIssue));
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