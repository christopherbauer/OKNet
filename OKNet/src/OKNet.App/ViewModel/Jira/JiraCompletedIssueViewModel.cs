using System.Collections.ObjectModel;
using System.Linq;

namespace OKNet.App.ViewModel.Jira
{
    public class JiraCompletedIssueViewModel : JiraIssueViewModelBase
    {
        public ObservableCollection<JiraProjectViewModel> GetVisibleProjects => new ObservableCollection<JiraProjectViewModel>(Projects.Where(model => model.CountCompleted > 0).ToList());
        public string GetIssue => $"{IssuesTotal} issue(s) Done today";
    }
}