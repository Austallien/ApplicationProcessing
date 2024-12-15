using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ApplicationProcessing.Utilites.Converters
{
    [ValueConversion(typeof(double), typeof(double))]
    internal class SubstractDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double originalValue = (double)value;
            double substractValue = System.Convert.ToDouble(parameter ?? 0);

            return originalValue - substractValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return -1;
        }
    }
}