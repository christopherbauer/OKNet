using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OKNet.App.ViewModel.Design
{
    public class DesignWindowViewModel : WindowViewModel
    {
        public DesignWindowViewModel()
        {
            Windows = new ObservableCollection<ViewModelBase>(new List<ViewModelBase>
            {
                new DesignJiraCompletedIssueViewModel(),
                new DesignJiraInProgressIssueViewModel(),
            });
            IsDebugMode = true;
        }
    }
}