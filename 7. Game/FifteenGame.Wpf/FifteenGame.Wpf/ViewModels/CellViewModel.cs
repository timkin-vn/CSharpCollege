using Minesweeper.Business.Models;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;

namespace Minesweeper.Wpf.ViewModels
{
    public class CellViewModel : INotifyPropertyChanged
    {
        private GameCell _gameCell;

        public int Row { get; set; }
        public int Column { get; set; }

        public string DisplayText
        {
            get
            {
                if (!IsRevealed)
                    return IsFlagged ? "🚩" : "";

                if (HasMine)
                    return "💣";

                return AdjacentMinesCount > 0 ? AdjacentMinesCount.ToString() : "";
            }
        }

        public Brush BackgroundColor
        {
            get
            {
                if (!IsRevealed)
                    return Brushes.LightGray;

                if (HasMine)
                    return Brushes.Red;

                return Brushes.White;
            }
        }

        public Brush TextColor
        {
            get
            {
                if (!IsRevealed || HasMine)
                    return Brushes.Black;

                // Заменяем switch expression на обычный switch
                switch (AdjacentMinesCount)
                {
                    case 1: return Brushes.Blue;
                    case 2: return Brushes.Green;
                    case 3: return Brushes.Red;
                    case 4: return Brushes.DarkBlue;
                    case 5: return Brushes.DarkRed;
                    case 6: return Brushes.Teal;
                    case 7: return Brushes.Black;
                    case 8: return Brushes.Gray;
                    default: return Brushes.Black;
                }
            }
        }

        public bool IsRevealed => _gameCell?.IsRevealed ?? false;
        public bool IsFlagged => _gameCell?.IsFlagged ?? false;
        public bool HasMine => _gameCell?.HasMine ?? false;
        public int AdjacentMinesCount => _gameCell?.AdjacentMinesCount ?? 0;

        public ICommand RevealCommand { get; set; }
        public ICommand FlagCommand { get; set; }

        public CellViewModel(GameCell gameCell)
        {
            _gameCell = gameCell;
            Row = gameCell.Row;
            Column = gameCell.Column;
        }

        public void Update()
        {
            OnPropertyChanged(nameof(DisplayText));
            OnPropertyChanged(nameof(BackgroundColor));
            OnPropertyChanged(nameof(TextColor));
            OnPropertyChanged(nameof(IsRevealed));
            OnPropertyChanged(nameof(IsFlagged));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}