using System.Collections.ObjectModel;
using System.Linq;

namespace OKNet.App.ViewModel.Jira
{
    public class JiraCompletedIssueViewModel : JiraIssueViewModelBase
    {
        public override ObservableCollection<JiraProjectViewModel> GetVisibleProjects => new ObservableCollection<JiraProjectViewModel>(Projects.Where(model => model.Count > 0).ToList());
        public override string GetIssue => $"{IssuesTotal} issue(s) Done today";
    }
}