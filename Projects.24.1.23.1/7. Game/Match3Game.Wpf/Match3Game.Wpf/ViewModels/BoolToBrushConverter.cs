using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Match3Game.Wpf.ViewModels
{
    public class BoolToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isSelected = (bool)value;
            return isSelected ? Brushes.Orange : Brushes.LightBlue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
