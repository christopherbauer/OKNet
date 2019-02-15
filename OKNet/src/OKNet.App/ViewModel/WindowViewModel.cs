using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;

namespace OKNet.App.ViewModel
{
    public class WindowViewModel : ViewModelBase
    {
        private ObservableCollection<ViewModelBase> _windows;
        private bool _isDebugMode;

        public bool IsDebugMode
        {
            get => _isDebugMode;
            set => SetValue(ref _isDebugMode, value);
        }

        public ObservableCollection<ViewModelBase> Windows
        {
            get => _windows;
            set => SetValue(ref _windows, value);
        }

        public string AssemblyVersion => $"v{Assembly.GetExecutingAssembly().GetName().Version}";
    }
}