using System.Windows.Media;

namespace OKNet.App.ViewModel.Jira
{
    public class JiraProjectViewModel : ViewModelBase
    {
        private int _count;

        public string Name { get; set; }
        public int Id { get; set; }
        public string Key { get; set; }
        public int Width { get; set; }

        public int Count
        {
            get => _count;
            set => SetValue(ref _count, value);
        }
    }
}