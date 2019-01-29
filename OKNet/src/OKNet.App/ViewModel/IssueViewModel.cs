using System;
using System.Collections.ObjectModel;
using System.Linq;
using Humanizer;
using Humanizer.DateTimeHumanizeStrategy;

namespace OKNet.App.ViewModel
{
    public class IssueViewModel : ViewModelBase
    {
        private string _name;
        private string _key;
        private ObservableCollection<ComponentViewModel> _component;
        private int _projectId;
        private DateTime _updated;

        public string GetComponent => string.Join(",", Component.Select(model => model.Name));
        public string GetUpdatedHumanReadable => Updated.Humanize();

        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }

        public string Key
        {
            get => _key;
            set => SetValue(ref _key, value);
        }

        public ObservableCollection<ComponentViewModel> Component
        {
            get => _component;
            set => SetValue(ref _component, value);
        }

        public int ProjectId
        {
            get => _projectId;
            set => SetValue(ref _projectId, value);
        }

        public DateTime Updated
        {
            get => _updated;
            set => SetValue(ref _updated, value);
        }
    }
}