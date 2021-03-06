using System;
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

        public override void Cleanup()
        {
            var removeIssues = Issues.Values.Where(model => DateTime.Now.Subtract(model.Updated) > TimeSpan.FromDays(30)).ToList();
            foreach (var jiraIssueViewModel in removeIssues)
            {
                Issues.Remove(jiraIssueViewModel.Key);
            }

            base.Cleanup();
        }

        public override void AddOrUpdateNewIssues(IEnumerable<JiraIssueViewModel> issueViewModels)
        {
            base.AddOrUpdateNewIssues(issueViewModels, model => model.StatusCategoryKey != JiraData.StatusCategoryDictionary[JiraStatusCategory.IN_PROGRESS]);
        }
    }
}