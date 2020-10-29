using System;
using System.Globalization;
using Xamarin.Forms;

namespace Boilerplate.App.Converters
{
    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Inverse(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Inverse(value);
        }

        private bool Inverse(object val)
        {
            if (val is bool boolVal)
            {
                return !boolVal;
            }
            return false;
        }
    }
}
