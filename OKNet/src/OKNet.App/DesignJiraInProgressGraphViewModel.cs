using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OKNet.App.ViewModel.Design;
using OKNet.App.ViewModel.Jira;

namespace OKNet.App
{
    public class DesignJiraInProgressGraphViewModel : JiraInProgressGraphViewModel
    {
        public DesignJiraInProgressGraphViewModel()
        {
            Width = 400;
            GraphHeight = "200";
            Margin = "2";

            var todoCount = 5;
            var inDevCount = 2;
            var inQaCount = 1;
            var pmReviewCount = 2;
            var doneCount = 4;


            var todoCount2 = 8;
            var inDevCount2 = 5;
            var inQaCount2 = 4;
            var pmReviewCount2 = 0;
            var doneCount2 = 10;

            ProjectIssuesCount = new ObservableCollection<JiraProjectIssueCountViewModel>
            {
                new DesignJiraProjectIssueCountViewModel(),
                new JiraProjectIssueCountViewModel
                {
                    ProjectId = 2,
                    ProjectName = "OKNetJira",
                    Height = Convert.ToInt32(GraphHeight),
                    ProjectIssues = new ObservableCollection<JiraIssueCountViewModel>(new List<JiraIssueCountViewModel>
                    {
                        new JiraIssueCountViewModel
                        {
                            BackgroundColor = "Green", IssueCount = doneCount, Status = "Done"
                        },
                        new JiraIssueCountViewModel
                        {
                            BackgroundColor = "LightBlue", IssueCount = pmReviewCount, Status = "PM Review"
                        },
                        new JiraIssueCountViewModel
                        {
                            BackgroundColor = "Yellow", IssueCount = inQaCount, Status = "In QA"
                        },
                        new JiraIssueCountViewModel
                        {
                            BackgroundColor = "LightGreen", IssueCount = inDevCount, Status = "In Dev"
                        },
                        new JiraIssueCountViewModel
                        {
                            BackgroundColor = "Silver", IssueCount = todoCount, Status = "To Do"
                        },
                    })
                },
                new JiraProjectIssueCountViewModel
                {
                    ProjectId = 3,
                    ProjectName = "OKNetCommon",
                    Height = Convert.ToInt32(GraphHeight),
                    ProjectIssues = new ObservableCollection<JiraIssueCountViewModel>(new List<JiraIssueCountViewModel>
                    {
                        new JiraIssueCountViewModel
                        {
                            BackgroundColor = "Green", IssueCount = doneCount2, Status = "Done"
                        },
                        new JiraIssueCountViewModel
                        {
                            BackgroundColor = "LightBlue", IssueCount = pmReviewCount2, Status = "PM Review"
                        },
                        new JiraIssueCountViewModel
                        {
                            BackgroundColor = "Yellow", IssueCount = inQaCount2, Status = "In QA"
                        },
                        new JiraIssueCountViewModel
                        {
                            BackgroundColor = "LightGreen", IssueCount = inDevCount2, Status = "In Dev"
                        },
                        new JiraIssueCountViewModel
                        {
                            BackgroundColor = "Silver", IssueCount = todoCount2, Status = "To Do"
                        },
                    })
                }
            };
        }
    }
}