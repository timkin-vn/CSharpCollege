using FifteenGame.Wpf.Commands;
using System.Windows.Input;
using System.Windows.Media;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int GemType { get; set; }
        public bool IsSelected { get; set; }
        public bool IsMatched { get; set; }

        public Brush GemColor
        {
            get
            {
                if (IsMatched) return Brushes.Transparent;
                switch (GemType)
                {
                    case 0: return Brushes.Crimson;
                    case 1: return Brushes.DodgerBlue;
                    case 2: return Brushes.LimeGreen;
                    case 3: return Brushes.Gold;
                    case 4: return Brushes.BlueViolet;
                    case 5: return Brushes.DarkOrange;
                    default: return Brushes.Gray;
                }
            }
        }

        public Brush BorderColor => IsSelected ? Brushes.White : Brushes.Transparent;
        public ICommand ClickCommand { get; set; }
    }
}