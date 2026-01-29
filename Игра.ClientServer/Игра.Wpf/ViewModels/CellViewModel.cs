using System.Windows.Media;

namespace Игра.Wpf.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        public int Row { get; }
        public int Column { get; }

        private Brush _backgroundColor;
        public Brush BackgroundColor
        {
            get => _backgroundColor;
            set => Set(ref _backgroundColor, value);
        }

        public CellViewModel(int row, int column)
        {
            Row = row;
            Column = column;
            _backgroundColor = Brushes.Gray;
        }

        public void UpdateColor(bool isActive, bool isWin)
        {
            if (isWin)
            {
                BackgroundColor = Brushes.Green;
            }
            else
            {
                BackgroundColor = isActive ? Brushes.Yellow : Brushes.Gray;
            }
        }
    }
}