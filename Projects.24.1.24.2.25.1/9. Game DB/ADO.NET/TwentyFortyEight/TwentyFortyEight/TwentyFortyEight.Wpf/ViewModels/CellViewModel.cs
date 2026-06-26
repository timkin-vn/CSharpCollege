using System.Windows.Media;

namespace TwentyFortyEight.Wpf.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        public int Value { get; set; }

        public string Text => Value == 0 ? "" : Value.ToString();

        public int Row { get; set; }

        public int Column { get; set; }

        public Brush Background
        {
            get
            {
                switch (Value)
                {
                    case 0:    return new SolidColorBrush(Color.FromRgb(205, 193, 180));
                    case 2:    return new SolidColorBrush(Color.FromRgb(238, 228, 218));
                    case 4:    return new SolidColorBrush(Color.FromRgb(237, 224, 200));
                    case 8:    return new SolidColorBrush(Color.FromRgb(242, 177, 121));
                    case 16:   return new SolidColorBrush(Color.FromRgb(245, 149,  99));
                    case 32:   return new SolidColorBrush(Color.FromRgb(246, 124,  95));
                    case 64:   return new SolidColorBrush(Color.FromRgb(246,  94,  59));
                    case 128:  return new SolidColorBrush(Color.FromRgb(237, 207, 114));
                    case 256:  return new SolidColorBrush(Color.FromRgb(237, 204,  97));
                    case 512:  return new SolidColorBrush(Color.FromRgb(237, 200,  80));
                    case 1024: return new SolidColorBrush(Color.FromRgb(237, 197,  63));
                    case 2048: return new SolidColorBrush(Color.FromRgb(237, 194,  46));
                    default:   return new SolidColorBrush(Color.FromRgb( 60,  58,  50));
                }
            }
        }

        public Brush Foreground
        {
            get
            {
                return Value <= 4
                    ? new SolidColorBrush(Color.FromRgb(119, 110, 101))
                    : Brushes.White;
            }
        }
    }
}
