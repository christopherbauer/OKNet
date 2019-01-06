namespace OKNet.App.ViewModel
{
    public abstract class WindowConfigViewModel : ViewModelBase
    {
        private string _type;
        private string _height;
        private string _width;
        public string Width
        {
            get => _width;
            set => SetValue(ref _width, value);
        }

        public string Height
        {
            get => _height;
            set => SetValue(ref _height, value);
        }

        public string Type
        {
            get => _type;
            set => SetValue(ref _type, value);
        }
    }
}