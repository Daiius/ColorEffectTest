using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ColorEffectTest.Converters
{
    /// <summary>
    /// Converts enum into IsChecked parameter, written with reference to
    /// https://stackoverflow.com/questions/397556/how-to-bind-radiobuttons-to-an-enum
    /// </summary>
    public class ComparisonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value?.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value?.Equals(true) == true ? parameter : Binding.DoNothing;
        }
    }
}
