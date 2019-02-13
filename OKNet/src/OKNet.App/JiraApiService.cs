using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OKNet.App.ViewModel;
using OKNet.Core;
using OKNet.Infrastructure.Jira;

namespace OKNet.App
{
    public class JiraApiService
    {
        public IEnumerable<IssueViewModel> ParseIssues(ApiResponse<APIIssueRequestRoot> issueResult)
        {
            return issueResult.Data.issues.Select(
                model => new IssueViewModel
                {
                    Key = model.key,
                    Name = model.fields.summary,
                    Component = new ObservableCollection<ComponentViewModel>(
                        model.fields.components.Select(component => new ComponentViewModel
                        {
                            Id = Convert.ToInt32(component.id),
                            Name = component.name
                        })),
                    StatusCategory = model.fields.status.statusCategory.name,
                    ProjectId = Convert.ToInt32(model.fields.project.id),
                    Updated = model.fields.updated
                });
        }

        public static ObservableCollection<ProjectViewModel> ParseProjects(ApiResponse<List<ProjectApiModel>> projects)
        {
            return new ObservableCollection<ProjectViewModel>(projects.Data.Select(model =>
                new ProjectViewModel
                {
                    Name = model.name,
                    Key = model.key,
                    Id = Convert.ToInt32(model.id)
                }));
        }

        public static int GetTotalIssues(ApiResponse<APIIssueRequestRoot> issueResult)
        {
            return issueResult.Data.total;
        }
    }
}