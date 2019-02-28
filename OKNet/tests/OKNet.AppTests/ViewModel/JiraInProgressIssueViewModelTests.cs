using System;
using OKNet.App.ViewModel.Jira;
using OKNet.Infrastructure.Jira;
using Xunit;

namespace OKNet.AppTests.ViewModel
{
    public class JiraInProgressIssueViewModelTests
    {
        public class When_Cleanup
        {
            [Fact]
            public void ThenShould_NotRemoveIssuesYoungerThan30Days()
            {
                //arrange
                var viewModel = new JiraInProgressIssueViewModel();
                var confirmIssueKey = "OKNET-1";
                viewModel.AddOrUpdateNewIssues(new[]
                {
                    new JiraIssueViewModel { Key=confirmIssueKey, StatusCategoryKey = JiraData.StatusCategoryDictionary[JiraStatusCategory.IN_PROGRESS], Updated = DateTime.Now }, 
                    new JiraIssueViewModel { Key="OKNET-2", StatusCategoryKey = JiraData.StatusCategoryDictionary[JiraStatusCategory.IN_PROGRESS], Updated = DateTime.Now }, 
                    new JiraIssueViewModel { Key="OKNET-3", StatusCategoryKey = JiraData.StatusCategoryDictionary[JiraStatusCategory.IN_PROGRESS], Updated = DateTime.Now}, 
                });

                //act
                viewModel.Cleanup();

                //assert
                Assert.True(viewModel.Issues.ContainsKey(confirmIssueKey));
            }
            [Fact]
            public void ThenShould_RemoveIssuesOlderThan30Days()
            {
                //arrange
                var viewModel = new JiraInProgressIssueViewModel();
                var removeIssueKey = "OKNET-2";
                viewModel.AddOrUpdateNewIssues(new[]
                {
                    new JiraIssueViewModel { Key="OKNET-1", StatusCategoryKey = JiraData.StatusCategoryDictionary[JiraStatusCategory.IN_PROGRESS], Updated = DateTime.Now }, 
                    new JiraIssueViewModel { Key=removeIssueKey, StatusCategoryKey = JiraData.StatusCategoryDictionary[JiraStatusCategory.IN_PROGRESS], Updated = DateTime.Now.AddDays(-31)}, 
                    new JiraIssueViewModel { Key="OKNET-3", StatusCategoryKey = JiraData.StatusCategoryDictionary[JiraStatusCategory.IN_PROGRESS], Updated = DateTime.Now}, 
                });

                //act
                viewModel.Cleanup();

                //assert
                Assert.False(viewModel.Issues.ContainsKey(removeIssueKey));
            }
        }
    }
}
