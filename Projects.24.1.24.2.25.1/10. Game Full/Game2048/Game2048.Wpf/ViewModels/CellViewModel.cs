using System.Windows.Media;

namespace TwentyFortyEight.Wpf.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        public int Value { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public string Text => Value == 0 ? "" : Value.ToString();

        public Brush Background => GetNeonBackground(Value);

        public Brush Foreground => (Value == 4 || Value == 256 || Value == 2048 || Value == 0)
            ? CreateColorBrush(11, 12, 16)
            : Brushes.White;

        private static Brush GetNeonBackground(int val)
        {
            switch (val)
            {
                case 0: return CreateColorBrush(31, 40, 51);
                case 2: return CreateColorBrush(69, 162, 158);
                case 4: return CreateColorBrush(102, 252, 241);
                case 8: return CreateColorBrush(63, 81, 181);
                case 16: return CreateColorBrush(156, 39, 176);
                case 32: return CreateColorBrush(233, 30, 99);
                case 64: return CreateColorBrush(244, 67, 54);
                case 128: return CreateColorBrush(255, 152, 0);
                case 256: return CreateColorBrush(255, 235, 59);
                case 512: return CreateColorBrush(76, 175, 80);
                case 1024: return CreateColorBrush(0, 188, 212);
                case 2048: return CreateColorBrush(255, 255, 255);
                default: return CreateColorBrush(0, 0, 0);
            }
        }

        private static SolidColorBrush CreateColorBrush(int r, int g, int b)
        {
            var brush = new SolidColorBrush(Color.FromRgb((byte)r, (byte)g, (byte)b));
            brush.Freeze();
            return brush;
        }
    }
}