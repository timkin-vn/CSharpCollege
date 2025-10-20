using System.Windows.Input;
using System.Windows.Media;

namespace Saper.ViewModels
{
    public class CellViewModel
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsOpened { get; set; }
        public bool IsFlagged { get; set; }
        public bool HasBomb { get; set; }
        public int AdjacentBombs { get; set; }
        public bool IsGameOver { get; set; }
        public bool ShowBombs { get; set; } 

        public string DisplayText
        {
            get
            {
                if (!IsOpened)
                    return IsFlagged ? "🚩" : "";

                if (HasBomb)
                    return "💣";

                return AdjacentBombs > 0 ? AdjacentBombs.ToString() : "";
            }
        }

        public Brush BackgroundColor
        {
            get
            {
                if (ShowBombs && HasBomb) 
                    return Brushes.Red;
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
