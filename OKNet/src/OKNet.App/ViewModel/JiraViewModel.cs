using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace OKNet.App.ViewModel
{
    public class JiraViewModel : WindowConfigViewModel
    {
        public ObservableCollection<IssueViewModel> Issues { get; set; }
        public ObservableCollection<ProjectViewModel> Projects { get; set; }
        public int IssueCount => Issues.Count;
        public string GetIssue => $"{IssueCount} issue(s) Done today";
        public void RefreshProjectCounts()
        {
            foreach (var projectViewModel in Projects)
            {
                projectViewModel.CountCompleted =
                    Issues.Count(viewModel => viewModel.ProjectId == Convert.ToInt32(projectViewModel.Id));
            }
        }
    }
}