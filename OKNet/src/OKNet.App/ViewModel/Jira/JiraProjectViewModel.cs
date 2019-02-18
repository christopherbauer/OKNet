using System.Windows.Media;

namespace OKNet.App.ViewModel.Jira
{
    public class JiraProjectViewModel : ViewModelBase
    {
        private int _countCompleted;
        public string Name { get; set; }
        public string Key { get; set; }
        public int Id { get; set; }
        public ImageSource Icon { get; set; }

        public int CountCompleted
        {
            get => _countCompleted;
            set => SetValue(ref _countCompleted, value);
        }

        public int CountInProgress { get; set; }
        public int Width { get; set; }
    }
}