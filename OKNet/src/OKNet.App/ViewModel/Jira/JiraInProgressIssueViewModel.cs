using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OKNet.Infrastructure.Jira;

namespace OKNet.App.ViewModel.Jira
{
    public class JiraInProgressIssueViewModel : JiraIssueViewModelBase
    {
        public override ObservableCollection<JiraProjectViewModel> GetVisibleProjects => new ObservableCollection<JiraProjectViewModel>(Projects.Where(model => model.Count > 0).ToList());

        public override string GetIssue => $"{IssuesTotal} issue(s) In Progress";

        public override void AddOrUpdateNewIssues(IEnumerable<JiraIssueViewModel> issueViewModels)
        {
            var jiraIssueViewModelList = issueViewModels.ToList();
            var removeViewModels = jiraIssueViewModelList.Where(model => model.Key != JiraData.StatusCategoryDictionary[JiraStatusCategory.IN_PROGRESS]).ToList();
            foreach (var viewModel in removeViewModels)
            {
                var jiraIssueViewModel = Issues.Single(currentIssue => currentIssue.Key == viewModel.Key);
                if (!viewModel.Equals(jiraIssueViewModel))
                {
                    Issues.Remove(jiraIssueViewModel);
                }
            }

            issueViewModels = jiraIssueViewModelList.Except(removeViewModels);
            base.AddOrUpdateNewIssues(issueViewModels);
        }
    }
}