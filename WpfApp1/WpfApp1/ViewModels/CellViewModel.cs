using System.Windows.Input;
using System.Windows.Media;

namespace SimpleMinesweeper.ViewModels
{
    public class CellViewModel
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsOpened { get; set; }
        public bool IsFlagged { get; set; }
        public bool HasBomb { get; set; }
        public int AdjacentBombs { get; set; }
        public bool IsGameOver { get; set; } // Новое свойство

        public string DisplayText
        {
            get
            {
                if (!IsOpened && !IsGameOver)
                    return IsFlagged ? "🚩" : "";

                if (HasBomb)
                    return "💣";

                return AdjacentBombs > 0 ? AdjacentBombs.ToString() : "";
            }
        }

        // Новое свойство для цвета фона
        public Brush BackgroundColor
        {
            get
            {
                if (IsGameOver && HasBomb)
                    return Brushes.Red; // Бомбы красным при проигрыше
                else if (IsOpened)
                    return Brushes.LightGray;
                else
                    return Brushes.LightBlue;
            }
        }

        public ICommand OpenCommand { get; set; }
        public ICommand FlagCommand { get; set; }
    }
}