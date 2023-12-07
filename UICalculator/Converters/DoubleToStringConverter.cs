using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UICalculator.Converters
{
    public class DoubleToStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if(value is double convertingDouble)
            {
                return convertingDouble.ToString();
            }
            return 0;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if(value is string convertingBackString)
            {
                try
                {
                    return System.Convert.ToDouble(convertingBackString);
                }
                catch
                {
                    return 0;
                }
            }
            return null;
        }
    }
}
