using System.Windows.Media;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        private int _cellNumber;

        public int Value
        {
            get => _cellNumber;
            set
            {
                if (_cellNumber == value) return;
                _cellNumber = value;
                OnPropertyChanged(nameof(Value));
                OnPropertyChanged(nameof(Text));
                OnPropertyChanged(nameof(BackgroundBrush));
            }
        }

        public string Text => _cellNumber > 0 ? _cellNumber.ToString() : string.Empty;

        public Brush BackgroundBrush => FetchGreensPalette(_cellNumber);

        public int Row { get; set; }
        public int Column { get; set; }

        private Brush FetchGreensPalette(int number)
        {
            switch (number)
            {
                case 0: return Brushes.Transparent;
                case 2:
                case 4: return new SolidColorBrush(Color.FromRgb(69, 162, 158));
                case 8:
                case 16: return new SolidColorBrush(Color.FromRgb(102, 252, 241)); 
                case 32:
                case 64: return new SolidColorBrush(Color.FromRgb(255, 0, 128));
                case 128:
                case 256: return new SolidColorBrush(Color.FromRgb(187, 134, 252));
                case 512:
                case 1024: return new SolidColorBrush(Color.FromRgb(255, 215, 0)); 
                case 2048: return new SolidColorBrush(Color.FromRgb(255, 255, 255));
                default: return new SolidColorBrush(Color.FromRgb(50, 50, 70));
            }
        }
    }
}