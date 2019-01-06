namespace OKNet.App.ViewModel
{
    public class BasicWebsiteViewModel : WindowConfigViewModel
    {
        private string _uri;
        public string Uri
        {
            get => _uri;
            set => SetValue(ref _uri, value);
        }
    }
}