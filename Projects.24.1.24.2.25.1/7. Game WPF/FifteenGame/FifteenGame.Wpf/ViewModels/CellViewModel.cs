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
                case 0:
                    return Brushes.Transparent;
                case 2:
                case 4:
                    return new SolidColorBrush(Color.FromRgb(230, 245, 230));
                case 8:
                case 16:
                    return new SolidColorBrush(Color.FromRgb(195, 230, 195));
                case 32:
                case 64:
                    return new SolidColorBrush(Color.FromRgb(140, 200, 140));
                case 128:
                case 256:
                    return new SolidColorBrush(Color.FromRgb(75, 165, 75));
                case 512:
                case 1024:
                    return new SolidColorBrush(Color.FromRgb(35, 120, 35));
                case 2048:
                    return new SolidColorBrush(Color.FromRgb(10, 80, 10));
                default:
                    return new SolidColorBrush(Color.FromRgb(20, 40, 20));
            }
        }
    }
}