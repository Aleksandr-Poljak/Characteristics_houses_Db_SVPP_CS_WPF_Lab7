using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SVPP_CS_WPF_Lab7_Characteristics_houses_Db
{
    /// <summary>
    /// Конвертер для обработки null-значений.
    /// </summary>
    class NullToStringConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is null) return string.Empty;
            return value.ToString()!;
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string valueStr = (string)value;
            if (valueStr.Length == 0) return null;
            return value;
        }
    }
}
