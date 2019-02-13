using System.Collections.ObjectModel;

namespace OKNet.App.ViewModel
{
    public class WindowViewModel : ViewModelBase
    {
        private ObservableCollection<ViewModelBase> _windows;

        public ObservableCollection<ViewModelBase> Windows
        {
            get => _windows;
            set => SetValue(ref _windows, value);
        }
    }
}