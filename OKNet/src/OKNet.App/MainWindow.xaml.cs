using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OKNet.App.ViewModel;
using OKNet.Core;

namespace OKNet.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var configService = new ConfigService();
            var names = configService.GetNames("windows").ToList();
            var windows = new List<WindowConfig>();
            for (var i = 0; i < names.Count(); i++)
            {
                var pathString = $"windows:{names[i]}";
                Console.WriteLine(pathString);
                windows.Add(configService.GetConfig<WindowConfig>(pathString));
            }

            DataContext = new WindowConfigViewModel { Windows = windows };
        }
    }
}
