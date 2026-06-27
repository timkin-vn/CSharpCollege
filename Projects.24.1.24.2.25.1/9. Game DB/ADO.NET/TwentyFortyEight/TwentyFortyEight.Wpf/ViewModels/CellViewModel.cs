using System.Windows.Media;

namespace TwentyFortyEight.Wpf.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        public int Value { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public string Text => Value == 0 ? "" : Value.ToString();

        public Brush Background => GetEcoBackground(Value);

        public Brush Foreground => Value <= 4
            ? CreateColorBrush(46, 76, 42)
            : Brushes.White;

        private static Brush GetEcoBackground(int val)
        {
            switch (val)
            {
                case 0: return CreateColorBrush(210, 220, 212);
                case 2: return CreateColorBrush(232, 245, 233);
                case 4: return CreateColorBrush(200, 230, 201);
                case 8: return CreateColorBrush(165, 214, 167);
                case 16: return CreateColorBrush(129, 199, 132);
                case 32: return CreateColorBrush(102, 187, 106);
                case 64: return CreateColorBrush(76, 175, 80);
                case 128: return CreateColorBrush(67, 160, 71);
                case 256: return CreateColorBrush(56, 142, 60);
                case 512: return CreateColorBrush(46, 125, 50);
                case 1024: return CreateColorBrush(27, 94, 32);
                case 2048: return CreateColorBrush(14, 54, 16);
                default: return CreateColorBrush(24, 38, 20);
            }
        }

        private static SolidColorBrush CreateColorBrush(int r, int g, int b)
        {
            return new SolidColorBrush(Color.FromRgb((byte)r, (byte)g, (byte)b));
        }
    }
}