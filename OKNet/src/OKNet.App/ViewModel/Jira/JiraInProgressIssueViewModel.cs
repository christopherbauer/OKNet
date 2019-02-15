using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OKNet.App.ViewModel.Design;

namespace OKNet.App.ViewModel.Jira
{
    public class JiraInProgressIssueViewModel : JiraIssueViewModelBase
    {
        private int _page;
        public ObservableCollection<JiraProjectViewModel> GetVisibleProjects => new ObservableCollection<JiraProjectViewModel>(Projects.ToList());
        public string GetIssue => $"{IssuesTotal} issue(s) In Progress";

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
                    .Skip(Page * 50).Take(50));
            }
        }

        public override void AddOrUpdateNewIssues(IEnumerable<JiraIssueViewModel> issueViewModels)
        {
            base.AddOrUpdateNewIssues(issueViewModels);
            if (Issues != null && Issues.Any())
            {
                GraphViewModel.RefreshIssueCounts(Issues.ToList());
            }
            OnPropertyChanged(nameof(GetVisibleIssues));
            OnPropertyChanged(nameof(GetIssue));
        }

        public JiraInProgressGraphViewModel GraphViewModel = new JiraInProgressGraphViewModel();
    }

    public class TickValue
    {
        public string DisplayText { get; set; }
        public int Height { get; set; }
    }

    public class JiraInProgressGraphViewModel : ViewModelBase
    {
        public ObservableCollection<JiraProjectIssueCountViewModel> _issuesCount = new ObservableCollection<JiraProjectIssueCountViewModel>();
        private int _issuesTotal;
        private int _width;
        private List<TickValue> _ticks;
        public string GraphHeight { get; set; }

        public ObservableCollection<JiraProjectIssueCountViewModel> IssuesCount
        {
            get => _issuesCount;
            set => SetValue(ref _issuesCount, value);
        }

        public int IssuesTotal
        {
            get => _issuesTotal;
            set => SetValue(ref _issuesTotal, value);
        }

        public int Width
        {
            get => _width;
            set => SetValue(ref _width, value);
        }

        public List<TickValue> Ticks
        {
            get => _ticks;
            set => SetValue(ref _ticks, value);
        }

        public void RefreshIssueCounts(List<JiraIssueViewModel> issues)
        {
//             var projectCounts = issues
//                .GroupBy(model => model.ProjectId, (i, models) => new { i, models });
//
//            ProjectIssuesCount = new ObservableCollection<JiraProjectIssueCountViewModel>(
//                projectCounts);
//            (category, models) => new JiraProjectIssueCountViewModel
//                    {
//                        Total = IssuesTotal,
//                        Count = models.Count(),
//                        Status = category,
//                        ParentHeight = GraphHeight,
//                        BackgroundColor = StatusColorDictionary[category]
//                    }));
        }
    }
}