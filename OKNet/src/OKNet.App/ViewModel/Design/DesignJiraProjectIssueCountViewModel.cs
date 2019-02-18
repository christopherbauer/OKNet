using System.Collections.Generic;
using System.Collections.ObjectModel;
using OKNet.App.ViewModel.Jira;

namespace OKNet.App.ViewModel.Design
{
    public class DesignJiraProjectIssueCountViewModel : JiraProjectIssueCountViewModel
    {
        public DesignJiraProjectIssueCountViewModel()
        {
            ProjectId = 1;
            ProjectName = "OKNet";
            Height = 300;
            Width = 200;
            var todoCount = 20;
            var inDevCount = 4;
            var inQaCount = 12;
            var pmReviewCount = 11;
            var doneCount = 54;
            AllProjectIssuesCount = todoCount + inDevCount + inQaCount + pmReviewCount + doneCount;
            ProjectIssues = new ObservableCollection<JiraIssueCountViewModel>(new List<JiraIssueCountViewModel>
            {
                new JiraIssueCountViewModel { BackgroundColor="Green", IssueCount=doneCount, Status="Done" },
                new JiraIssueCountViewModel { BackgroundColor="LightBlue", IssueCount=pmReviewCount, Status="PM Review" }, 
                new JiraIssueCountViewModel { BackgroundColor="Yellow", IssueCount=inQaCount, Status="In QA" }, 
                new JiraIssueCountViewModel { BackgroundColor="LightGreen", IssueCount=inDevCount, Status="In Dev" }, 
                new JiraIssueCountViewModel { BackgroundColor="Silver", IssueCount=todoCount, Status="To Do" }, 
            });
        }
    }
}