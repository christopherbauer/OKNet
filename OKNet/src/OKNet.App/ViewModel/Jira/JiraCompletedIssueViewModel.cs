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

        public override string GetIssue => $"{IssuesTotal} issue(s) Done today";
    }
}