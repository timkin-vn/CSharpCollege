using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Checkers.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(parameter as string == "Selection")
            {
                if (value is bool isSelected && isSelected)
                    return new SolidColorBrush(Color.FromRgb(52, 152, 219));
                return new SolidColorBrush(Colors.Transparent);
            }
            else if (parameter as string == "ValidMove")
            {
                if (value is bool isValidMove && isValidMove)
                    return new SolidColorBrush(Color.FromRgb(46, 204, 113));
                return new SolidColorBrush(Colors.Transparent);
            }
            else
            {
                if (value is bool isDark && isDark)
                    return new SolidColorBrush(Color.FromRgb(139, 69, 19));
                return new SolidColorBrush(Color.FromRgb(245, 222, 179));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
