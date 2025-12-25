using FifteenGame.Business.Models;
using System.ComponentModel;
using System.Windows.Media;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel : INotifyPropertyChanged
    {
        private readonly Cell _cell;

        public CellViewModel(Cell cell)
        {
            _cell = cell;
        }

        public int Row => _cell.Row;
        public int Column => _cell.Column;

        public string DisplayText
        {
            get
            {
                if (!_cell.IsOpened)
                    return _cell.IsFlagged ? "🚩" : "";

                if (_cell.IsMine)
                    return "💣";

                return _cell.MinesAround > 0 ? _cell.MinesAround.ToString() : "";
            }
        }

        public Brush BackgroundColor
        {
            get
            {
                if (!_cell.IsOpened)
                    return Brushes.LightGray;

                if (_cell.IsMine)
                    return Brushes.Black; 

                return Brushes.Black;
            }
        }

        public Brush TextColor
        {
            get
            {
                if (!_cell.IsOpened)
                    return Brushes.Black;

                if (_cell.IsMine)
                    return Brushes.Black;

                switch (_cell.MinesAround)
                {
                    case 1: return Brushes.Blue;
                    case 2: return Brushes.Green;
                    case 3: return Brushes.Red;
                    case 4: return Brushes.Purple;
                    case 5: return Brushes.DarkRed;
                    case 6: return Brushes.Teal;
                    case 7: return Brushes.Black;
                    case 8: return Brushes.Gray;
                    default: return Brushes.Black;
                }
            }
        }

        public bool IsEnabled => !_cell.IsOpened;

        public void Update()
        {
            OnPropertyChanged(nameof(DisplayText));
            OnPropertyChanged(nameof(BackgroundColor));
            OnPropertyChanged(nameof(TextColor));
            OnPropertyChanged(nameof(IsEnabled));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}