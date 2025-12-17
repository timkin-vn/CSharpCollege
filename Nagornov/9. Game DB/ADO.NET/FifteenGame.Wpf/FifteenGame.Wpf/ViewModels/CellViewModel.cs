using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel : INotifyPropertyChanged
    {
        private int _adjacentMines;
        private bool _isRevealed;
        private bool _hasMine;
        private bool _isFlagged;
        private bool _isExploded;

        public int Row { get; set; }
        public int Column { get; set; }

        public int AdjacentMines
        {
            get => _adjacentMines;
            set { _adjacentMines = value; OnPropertyChanged(); }
        }

        public bool IsRevealed
        {
            get => _isRevealed;
            set { _isRevealed = value; OnPropertyChanged(); OnPropertyChanged(nameof(DisplayText)); OnPropertyChanged(nameof(CellColor)); }
        }

        public bool HasMine
        {
            get => _hasMine;
            set { _hasMine = value; OnPropertyChanged(); OnPropertyChanged(nameof(DisplayText)); }
        }

        public bool IsFlagged
        {
            get => _isFlagged;
            set { _isFlagged = value; OnPropertyChanged(); OnPropertyChanged(nameof(DisplayText)); }
        }

        public bool IsExploded
        {
            get => _isExploded;
            set { _isExploded = value; OnPropertyChanged(); OnPropertyChanged(nameof(DisplayText)); OnPropertyChanged(nameof(CellColor)); }
        }

        public string DisplayText
        {
            get
            {
                if (IsFlagged && !IsRevealed)
                    return "🚩";
                if (!IsRevealed)
                    return "";
                if (HasMine)
                    return IsExploded ? "💥" : "💣";
                return AdjacentMines > 0 ? AdjacentMines.ToString() : "";
            }
        }

        public Brush CellColor
        {
            get
            {
                if (!IsRevealed)
                    return Brushes.LightGray;
                if (HasMine)
                    return IsExploded ? Brushes.DarkRed : Brushes.Red;
                return Brushes.White;
            }
        }

        public Brush TextColor
        {
            get
            {
                if (!IsRevealed || HasMine)
                    return Brushes.Black;
                switch (AdjacentMines)
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}