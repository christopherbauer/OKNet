using System.Windows.Media;

namespace OKNet.App.ViewModel
{
    public class ProjectViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public int Id { get; set; }
        public ImageSource Icon { get; set; }
        public int CountCompleted { get; set; }
        public int CountInProgress { get; set; }
    }
}