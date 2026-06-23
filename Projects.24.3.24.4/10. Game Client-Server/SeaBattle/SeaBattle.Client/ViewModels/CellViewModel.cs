using System.Windows.Media;
using SeaBattle.Common;

namespace SeaBattle.Client.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        public int Row { get; private set; }
        public int Col { get; private set; }
        public bool HideShips { get; private set; }

        private CellState _state;

        public CellViewModel(int row, int col, bool hideShips)
        {
            Row = row;
            Col = col;
            HideShips = hideShips;
            _state = CellState.Empty;
        }

        public CellState State
        {
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    OnPropertyChanged();
                    OnPropertyChanged("Text");
                    OnPropertyChanged("Background");
                }
            }
        }

        public string Text
        {
            get
            {
                switch (_state)
                {
                    case CellState.Miss: return "•";
                    case CellState.Hit: return "✕";
                    case CellState.Sunk: return "✕";
                    default: return string.Empty;
                }
            }
        }

        public Brush Background
        {
            get
            {
                switch (_state)
                {
                    case CellState.Ship: return HideShips ? Water : ShipBrush;
                    case CellState.Miss: return new SolidColorBrush(Color.FromRgb(0x5B, 0x7D, 0xA3));
                    case CellState.Hit: return new SolidColorBrush(Color.FromRgb(0xFF, 0x5A, 0x4D));
                    case CellState.Sunk: return new SolidColorBrush(Color.FromRgb(0x7A, 0x1F, 0x1A));
                    default: return Water;
                }
            }
        }

        private static Brush Water { get { return new SolidColorBrush(Color.FromRgb(0x16, 0x36, 0x55)); } }
        private static Brush ShipBrush { get { return new SolidColorBrush(Color.FromRgb(0x9B, 0xF8, 0xF4)); } }
    }
}
