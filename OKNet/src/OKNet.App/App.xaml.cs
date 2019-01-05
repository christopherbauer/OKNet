using System;
using System.Linq;
using System.Windows;
using OKNet.Core;

namespace OKNet.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var configService = new ConfigService();
            var names = configService.GetNames("windows").ToList();
            for (int i = 0; i < names.Count; i++)
            {
                var pathString = $"windows:{names[i]}";
                Console.WriteLine(pathString);
                var configuration = configService.GetConfig<WindowConfig>(pathString);
            }
        }
    }
}
