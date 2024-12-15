using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ApplicationProcessing.Utilites.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    internal class BoolToCollapsedVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Visibility))
                throw new InvalidOperationException();

            if (parameter is not null && ((string)parameter).Equals("!"))
                value = !(bool)value;

            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Visibility))
                throw new InvalidOperationException();

            return (Visibility)(value) == Visibility.Visible ? true : false;
        }
    }
}