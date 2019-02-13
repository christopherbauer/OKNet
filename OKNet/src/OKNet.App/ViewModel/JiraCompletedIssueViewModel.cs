using System.Collections.ObjectModel;
using System.Linq;

namespace OKNet.App.ViewModel
{
    public class JiraCompletedIssueViewModel : JiraIssueViewModelBase
    {
        public ObservableCollection<ProjectViewModel> GetVisibleProjects => new ObservableCollection<ProjectViewModel>(Projects.Where(model => model.CountCompleted > 0).ToList());
        public string GetIssue => $"{IssuesTotal} issue(s) Done today";
    }
}