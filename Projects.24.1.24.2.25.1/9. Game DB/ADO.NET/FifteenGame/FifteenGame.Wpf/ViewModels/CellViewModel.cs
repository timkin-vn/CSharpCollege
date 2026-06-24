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
                OnPropertyChanged(nameof(TileBrush));
            }
        }

        public string Text =>
            Value == 0 ? "" : Value.ToString();

        public int Row { get; set; }

        public int Column { get; set; }

        public Brush TileBrush
        {
            get
            {
                switch (Value)
                {
                    case 0: return Brushes.LightGray;
                    case 2: return Brushes.Beige;
                    case 4: return Brushes.Bisque;
                    case 8: return Brushes.Orange;
                    case 16: return Brushes.DarkOrange;
                    case 32: return Brushes.OrangeRed;
                    case 64: return Brushes.Red;
                    case 128: return Brushes.Gold;
                    case 256: return Brushes.Goldenrod;
                    case 512: return Brushes.Yellow;
                    case 1024: return Brushes.YellowGreen;
                    case 2048: return Brushes.LimeGreen;
                    default: return Brushes.Black;
                }
            }
        }
    }
}