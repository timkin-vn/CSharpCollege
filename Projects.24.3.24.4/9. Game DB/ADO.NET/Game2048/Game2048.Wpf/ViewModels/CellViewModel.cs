using System.Windows.Media;

namespace Game2048.Wpf.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        private int _value;
        public int Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Text));
                    OnPropertyChanged(nameof(Background));
                    OnPropertyChanged(nameof(Foreground));
                }
            }
        }
        public string Text { get { return _value == 0 ? string.Empty : _value.ToString(); } }
        public Brush Background { get { return new SolidColorBrush(GetColor(_value)); } }
        public Brush Foreground { get { return _value <= 4 ? Brushes.DimGray : Brushes.White; } }

        private static Color GetColor(int value)
        {
            switch (value)
            {
                case 0: return Color.FromRgb(0xCD, 0xC1, 0xB4);
                case 2: return Color.FromRgb(0xEE, 0xE4, 0xDA);
                case 4: return Color.FromRgb(0xED, 0xE0, 0xC8);
                case 8: return Color.FromRgb(0xF2, 0xB1, 0x79);
                case 16: return Color.FromRgb(0xF5, 0x95, 0x63);
                case 32: return Color.FromRgb(0xF6, 0x7C, 0x5F);
                case 64: return Color.FromRgb(0xF6, 0x5E, 0x3B);
                case 128: return Color.FromRgb(0xED, 0xCF, 0x72);
                case 256: return Color.FromRgb(0xED, 0xCC, 0x61);
                case 512: return Color.FromRgb(0xED, 0xC8, 0x50);
                case 1024: return Color.FromRgb(0xED, 0xC5, 0x3F);
                case 2048: return Color.FromRgb(0xED, 0xC2, 0x2E);
                default: return Color.FromRgb(0x3C, 0x3A, 0x32);
            }
        }
    }
}
