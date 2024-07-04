using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SVPP_CS_WPF_Lab7_Characteristics_houses_Db
{
    /// <summary>
    ///  Конвертер для отображения нулевых значений.
    /// </summary>
    internal class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int intValue = (int)value;
            return intValue == 0 ? string.Empty : intValue.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string stringValue = value as string;
            if (string.IsNullOrEmpty(stringValue))
            {
                return 0;
            }

            if (int.TryParse(stringValue, out int intValue))
            {
                return intValue;
            }
            return 0;
        }
    }
}
