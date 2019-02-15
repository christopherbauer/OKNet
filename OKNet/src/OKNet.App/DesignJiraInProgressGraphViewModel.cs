using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OKNet.App.ViewModel.Design;
using OKNet.App.ViewModel.Jira;

namespace OKNet.App
{
    public class DesignJiraInProgressGraphViewModel : JiraInProgressGraphViewModel
    {
        public DesignJiraInProgressGraphViewModel()
        {
            GraphHeight = "200";
            IssuesTotal = 200;

            var todoCount = 5;
            var inDevCount = 2;
            var inQaCount = 1;
            var pmReviewCount = 2;
            var doneCount = 4;
            var total = 200;
            Width = 400;
            IssuesCount = new ObservableCollection<JiraProjectIssueCountViewModel>(
                new List<JiraProjectIssueCountViewModel>
                {
                    new DesignJiraProjectIssueCountViewModel(),
                    new JiraProjectIssueCountViewModel
                    {
                        ProjectId = 2,
                        ProjectName = "OKNetJira",
                        ProjectIssues =  new ObservableCollection<JiraIssueCountViewModel>(new List<JiraIssueCountViewModel>
                        {
                            new JiraIssueCountViewModel { BackgroundColor="Green", Count=doneCount, ParentHeight=GraphHeight, Status="Done", Total=total },
                            new JiraIssueCountViewModel { BackgroundColor="LightBlue", Count=pmReviewCount, ParentHeight=GraphHeight, Status="PM Review", Total=total },
                            new JiraIssueCountViewModel { BackgroundColor="Yellow", Count=inQaCount, ParentHeight=GraphHeight, Status="In QA", Total=total },
                            new JiraIssueCountViewModel { BackgroundColor="LightGreen", Count=inDevCount, ParentHeight=GraphHeight, Status="In Dev", Total=total },
                            new JiraIssueCountViewModel { BackgroundColor="Silver", Count=todoCount, ParentHeight=GraphHeight, Status="To Do", Total=total },
                        })
                    }
                });
            var numTicks = 10;
            var heightPerTick = Convert.ToInt32(GraphHeight) / numTicks;
            Ticks = Enumerable.Range(1, numTicks).Select(i => new TickValue { DisplayText = ((total/numTicks) * i).ToString(), Height = heightPerTick}).Reverse().ToList();
        }
    }
}