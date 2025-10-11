using System.Windows.Media;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int AdjacentMines { get; set; }
        public bool IsRevealed { get; set; }
        public bool HasMine { get; set; }
        public bool IsFlagged { get; set; }
        public bool IsExploded { get; set; } // Новая property для анимации взрыва

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
    }
}