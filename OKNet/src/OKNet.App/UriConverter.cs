using System;
using System.Globalization;
using System.Windows.Data;

namespace OKNet.App
{
    public class UriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Uri(value.ToString(), UriKind.Absolute);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Uri) value).ToString();
        }
    }
}