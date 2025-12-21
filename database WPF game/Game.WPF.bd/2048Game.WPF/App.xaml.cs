using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace _2048Game.WPF
{
    public class TileForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int v)
                return v <= 4 ? Brushes.Black : Brushes.White;

            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
