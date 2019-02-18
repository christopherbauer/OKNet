using OKNet.App.ViewModel.Jira;

namespace OKNet.App.ViewModel.Design
{
    public class DesignJiraProjectViewModel : JiraProjectViewModel
    {
        public DesignJiraProjectViewModel()
        {
            Id = 1;
            Name = "OKNET";
            Width = 300;
            Key = "OK";
        }
    }
}