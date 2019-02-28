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

        private Dictionary<string, JiraIssueViewModel> _issues = new Dictionary<string, JiraIssueViewModel>();
        private ObservableCollection<JiraProjectViewModel> _projects = new ObservableCollection<JiraProjectViewModel>();
        private int _issuesTotal;
        private int _page = 1;
        private int _pageSize = 25;
        private Dictionary<string, string> _statusColors;
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

        public Dictionary<string, JiraIssueViewModel> Issues
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
                return new ObservableCollection<JiraIssueViewModel>(Issues.Values.OrderByDescending(model => model.Updated)
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

        public Dictionary<string, string> StatusColors
        {
            get => _statusColors;
            set
            {
                SetValue(ref _statusColors, value);
                Refresh();
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
                Issues.Remove(viewModel.Key);
            }

            var updateIssueViewModels = issueViewModelList.Except(removeIssueViewModels).ToList();

            var issuesToUpdate = updateIssueViewModels.Where(model => Issues.Any(currentIssues => currentIssues.Key == model.Key));
            foreach (var issueViewModel in issuesToUpdate)
            {
                if (Issues.ContainsKey(issueViewModel.Key))
                {
                    Issues[issueViewModel.Key] = issueViewModel;
                    isDirty = true;
                }
                else
                {
                    Issues.Add(issueViewModel.Key, issueViewModel);
                    isDirty = true;
                }
            }

            var newIssues = updateIssueViewModels.OrderByDescending(model => model.Updated)
                .Where(model => Issues.All(viewModel => viewModel.Key != model.Key));
            foreach (var issueViewModel in newIssues)
            {
                Issues.Add(issueViewModel.Key, issueViewModel);
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
            Logger.Trace($"------- PERF - End {nameof(AddOrUpdateNewIssues)}");
        }

        private void RefreshProjectCounts()
        {
            foreach (var projectViewModel in Projects)
            {
                projectViewModel.Count = Issues.Values.Count(viewModel =>
                    viewModel.ProjectId == Convert.ToInt32(projectViewModel.Id));
            }
        }

        public void SetupIssueColors()
        {
            foreach (var issue in Issues)
            {
                issue.Value.StatusColor = StatusColors != null && StatusColors.ContainsKey(issue.Value.Status) ? StatusColors[issue.Value.Status] : "Silver";
            }
        }

        public override void Refresh()
        {
            RefreshProjectCounts();
            SetupIssueColors();
            foreach (var issueViewModel in Issues)
            {
                issueViewModel.Value.Refresh();
            }
        }
    }
}