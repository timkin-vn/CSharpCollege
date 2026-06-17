using System.Windows.Media;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        private int _value;

        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
                OnPropertyChanged(nameof(Text));
                OnPropertyChanged(nameof(BackgroundBrush));
            }
        }

        public string Text => Value == 0 ? "" : Value.ToString();

        public Brush BackgroundBrush => GetColor(Value);

        private Brush GetColor(int val)
        {
            switch (val)
            {
                case 0: return Brushes.LightGray;
                case 2: return Brushes.LemonChiffon;
                case 4: return Brushes.Khaki;
                case 8: return Brushes.Orange;
                case 16: return Brushes.DarkOrange;
                case 32: return Brushes.OrangeRed;
                case 64: return Brushes.Red;
                case 128: return Brushes.MediumVioletRed;
                case 256: return Brushes.HotPink;
                case 512: return Brushes.DeepPink;
                case 1024: return Brushes.Gold;
                case 2048: return Brushes.Goldenrod;
                default: return Brushes.Black;
            }
        }

        public int Row { get; set; }
        public int Column { get; set; }
    }
}