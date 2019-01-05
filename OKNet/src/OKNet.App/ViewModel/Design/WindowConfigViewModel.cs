using System.Collections.Generic;
using OKNet.Core;

namespace OKNet.App.ViewModel
{
    public class DesignWindowConfigViewModel : WindowConfigViewModel
    {
        public DesignWindowConfigViewModel()
        {
            Windows = new List<WindowConfig>
            {
                new WindowConfig {Height = "100%", Width = "50%", Type = "web", Uri = "https://www.google.com"},
                new WindowConfig {Height = "100%", Width = "50%", Type = "web", Uri = "https://www.google.com"}
            };
        }
    }
}