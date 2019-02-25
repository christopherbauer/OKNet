using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Humanizer;
using OKNet.App.Command;

namespace OKNet.App.ViewModel.Jira
{
    public class JiraIssueViewModel : ViewModelBase
    {
        private string _name;
        private string _key;
        private ObservableCollection<JiraComponentViewModel> _component;
        private int _projectId;
        private DateTime _updated;
        private string _statusCategoryName;
        private string _statusCategoryKey;
        private string _status;
        private DateTime? _resolutionDate;

        public string GetComponent => string.Join(",", Component.Select(model => model.Name));
        public string GetUpdatedHumanReadable => Updated.Humanize(false);
        public string StatusColor => StatusColorDictionary.ContainsKey(Status) ? StatusColorDictionary[Status] : "Silver";
        public ICommand UpdatedCommand => new DelegateCommand(() => OnPropertyChanged(nameof(GetUpdatedHumanReadable)), o => true);

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

        public ObservableCollection<JiraComponentViewModel> Component
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

        public string StatusCategoryName
        {
            get => _statusCategoryName;
            set => SetValue(ref _statusCategoryName, value);
        }


        public string Status
        {
            get => _status;
            set => SetValue(ref _status, value);
        }

        public string StatusCategoryKey
        {
            get => _statusCategoryKey;
            set => _statusCategoryKey = value;
        }

        public DateTime? ResolutionDate
        {
            get => _resolutionDate;
            set => _resolutionDate = value;
        }

        public Dictionary<string, string> StatusColorDictionary => new Dictionary<string, string>
        {
            { "To Do", "Silver" },
            { "Ready for dev", "Silver" },
            { "In Dev", "LightGreen" },
            { "Code Review", "LimeGreen" },
            { "Ready for QA", "PaleGoldenrod" },
            { "In QA", "Gold" },
            { "PM review", "LightSkyBlue" },
            { "Ready for business", "LightSkyBlue" },
        };

        public override bool Equals(object obj)
        {
            var model = obj as JiraIssueViewModel;
            return model != null &&
                   _name == model._name &&
                   _key == model._key &&
//                   EqualityComparer<ObservableCollection<JiraComponentViewModel>>.Default.Equals(_component, model._component) &&
                   _projectId == model._projectId &&
                   _updated == model._updated &&
                   _statusCategoryName == model._statusCategoryName &&
                   _status == model._status;
        }

        public override void Refresh()
        {
            UpdatedCommand.Execute(null);
        }
    }
}