using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace OKNet.App
{
    public class WindowConfigConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var length = value.ToString();
            Regex converter = new Regex(@"[0-9]+", RegexOptions.IgnoreCase);
            var splitOps = converter.Split(length);
            var def = new ColumnDefinition { Width = new GridLength(50, GridUnitType.Star)};
            return def;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}