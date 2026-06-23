using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Business.Models;
using System.Windows.Media;
using System.Windows.Data;

namespace Checkers.Converters
{
    public class CheckerColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CheckerColor color)
            {
                return color == CheckerColor.White ?
                    new SolidColorBrush(Colors.White) :
                    new SolidColorBrush(Colors.Black);
            }
            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
