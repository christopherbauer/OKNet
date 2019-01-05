using System.Collections.Generic;
using OKNet.Core;

namespace OKNet.App.ViewModel
{
    public class WindowConfigViewModel
    {
        public int ColumnCount => Windows.Count;
        public List<WindowConfig> Windows { get; set; }
    }
}