using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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
            OnPropertyChanged(nameof(GetVisibleIssues));
            OnPropertyChanged(nameof(GetIssue));
        }
    }
}