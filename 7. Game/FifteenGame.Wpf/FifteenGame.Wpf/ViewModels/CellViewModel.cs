using FifteenGame.Business.Models;
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
                switch (GemType)
                {
                    case 0: return Brushes.Red;
                    case 1: return Brushes.Blue;
                    case 2: return Brushes.Green;
                    case 3: return Brushes.Yellow;
                    case 4: return Brushes.Purple;
                    case 5: return Brushes.Orange;
                    default: return Brushes.Gray;
                }
            }
        }

        public ICommand ClickCommand { get; set; }
    }
}