using System;
using System.Windows.Input;
using System.Windows.Media;
using Minesweeper.Common.Models;

namespace Minesweeper.Client.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        private readonly int _row;
        private readonly int _col;
        private Cell _cell;

        public CellViewModel(int row, int col, Action<int, int> reveal, Action<int, int> flag)
        {
            _row = row;
            _col = col;
            _cell = new Cell();
            RevealCommand = new RelayCommand(p => reveal(_row, _col));
            FlagCommand = new RelayCommand(p => flag(_row, _col));
        }

        public ICommand RevealCommand { get; private set; }
        public ICommand FlagCommand { get; private set; }

        public void Update(Cell cell)
        {
            _cell = cell;
            OnPropertyChanged(nameof(Text));
            OnPropertyChanged(nameof(Background));
            OnPropertyChanged(nameof(Foreground));
        }

        public string Text
        {
            get
            {
                if (_cell.IsRevealed)
                {
                    if (_cell.IsMine) return "\u25CF";
                    return _cell.AdjacentMines == 0 ? string.Empty : _cell.AdjacentMines.ToString();
                }
                if (_cell.IsFlagged) return "\u2691";
                return string.Empty;
            }
        }

        public Brush Background
        {
            get
            {
                if (_cell.IsRevealed)
                {
                    if (_cell.IsMine) return new SolidColorBrush(Color.FromRgb(0xEF, 0x53, 0x50));
                    return new SolidColorBrush(Color.FromRgb(0xE0, 0xE0, 0xE0));
                }
                return new SolidColorBrush(Color.FromRgb(0xBD, 0xBD, 0xBD));
            }
        }

        public Brush Foreground
        {
            get
            {
                if (!_cell.IsRevealed)
                    return _cell.IsFlagged ? new SolidColorBrush(Color.FromRgb(0xD3, 0x2F, 0x2F)) : Brushes.Transparent;
                if (_cell.IsMine) return Brushes.Black;
                return NumberColor(_cell.AdjacentMines);
            }
        }

        private static Brush NumberColor(int value)
        {
            switch (value)
            {
                case 1: return new SolidColorBrush(Color.FromRgb(0x19, 0x76, 0xD2));
                case 2: return new SolidColorBrush(Color.FromRgb(0x38, 0x8E, 0x3C));
                case 3: return new SolidColorBrush(Color.FromRgb(0xD3, 0x2F, 0x2F));
                case 4: return new SolidColorBrush(Color.FromRgb(0x7B, 0x1F, 0xA2));
                case 5: return new SolidColorBrush(Color.FromRgb(0xBF, 0x36, 0x0C));
                case 6: return new SolidColorBrush(Color.FromRgb(0x00, 0x83, 0x8F));
                case 7: return new SolidColorBrush(Color.FromRgb(0x42, 0x42, 0x42));
                case 8: return new SolidColorBrush(Color.FromRgb(0x75, 0x75, 0x75));
                default: return Brushes.Transparent;
            }
        }
    }
}
