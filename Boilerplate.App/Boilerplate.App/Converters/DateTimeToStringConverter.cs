using System;
using System.Globalization;
using Xamarin.Forms;

namespace Boilerplate.App.Converters
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateVal)
            {
                return dateVal != default ?
                    dateVal.ToShortDateString() :
                    string.Empty;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (DateTime.TryParse(value.ToString(), out DateTime dateVal))
            {
                return dateVal;
            }
            return new DateTime();
        }
    }
}
