using System.Collections.Generic;

namespace OKNet.App.ViewModel.Design
{
    public class DesignWindowViewModel : WindowViewModel
    {
        public DesignWindowViewModel()
        {
            Windows = new List<ViewModelBase>
            {
                new DesignJiraCompletedIssueViewModel(),
                new DesignJiraInProgressIssueViewModel(),
            };
        }
    }
}