using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OKNet.Infrastructure.Jira;

namespace OKNet.App.ViewModel.Jira
{
    public class JiraCompletedIssueViewModel : JiraIssueViewModelBase
    {
        public override ObservableCollection<JiraProjectViewModel> GetVisibleProjects => new ObservableCollection<JiraProjectViewModel>(Projects.Where(model => model.Count > 0).ToList());
        public override void AddOrUpdateNewIssues(IEnumerable<JiraIssueViewModel> issueViewModels)
        {
            base.AddOrUpdateNewIssues(issueViewModels, model => !model.ResolutionDate.HasValue || model.StatusCategoryKey != JiraData.StatusCategoryDictionary[JiraStatusCategory.COMPLETE]);
        }

        public override void Cleanup()
        {
            var removeIssues = Issues.Values.Where(model => model.ResolutionDate.HasValue && DateTime.Now.Subtract(model.ResolutionDate.Value) > TimeSpan.FromHours(24)).ToList();
            foreach (var jiraIssueViewModel in removeIssues)
            {
                Issues.Remove(jiraIssueViewModel.Key);
            }
            base.Cleanup();
        }

        public override string GetIssue => $"{IssuesTotal} issue(s) Done today";
    }
}