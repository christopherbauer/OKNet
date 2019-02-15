using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OKNet.App.ViewModel;
using OKNet.App.ViewModel.Jira;
using OKNet.Core;
using OKNet.Infrastructure.Jira;

namespace OKNet.App
{
    public class JiraApiService
    {
        public IEnumerable<JiraIssueViewModel> ParseIssues(ApiResponse<APIIssueRequestRoot> issueResult)
        {
            return issueResult.Data.issues.Select(
                model => new JiraIssueViewModel
                {
                    Key = model.key,
                    Name = model.fields.summary,
                    Component = new ObservableCollection<JiraComponentViewModel>(
                        model.fields.components.Select(component => new JiraComponentViewModel
                        {
                            Id = Convert.ToInt32(component.id),
                            Name = component.name
                        })),
                    StatusCategory = model.fields.status.statusCategory.name,
                    Status = model.fields.status.name,
                    ProjectId = Convert.ToInt32(model.fields.project.id),
                    Updated = model.fields.updated
                });
        }

        public static ObservableCollection<JiraProjectViewModel> ParseProjects(ApiResponse<List<ProjectApiModel>> projects,
            int parentWidth)
        {
            return new ObservableCollection<JiraProjectViewModel>(projects.Data.Select(model =>
                new JiraProjectViewModel
                {
                    Name = model.name,
                    Key = model.key,
                    Id = Convert.ToInt32(model.id),
                    Width = (int) (Math.Floor(parentWidth / 3m) - 4)
                }));
        }

        public static int GetTotalIssues(ApiResponse<APIIssueRequestRoot> issueResult)
        {
            return issueResult.Data.total;
        }
    }
}