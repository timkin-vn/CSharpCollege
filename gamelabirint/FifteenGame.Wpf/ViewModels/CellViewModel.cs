using System.Windows.Media;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel
    {
        public Brush Background { get; set; }
        public Brush Foreground { get; set; }
        public string Symbol { get; set; }
        public double FontSize { get; set; }

        public CellViewModel(Business.Models.CellType type)
        {
            FontSize = 20; 

            switch (type)
            {
                case Business.Models.CellType.Wall:
                    Symbol = "█";
                    Background = Brushes.DarkSlateGray;
                    Foreground = Brushes.White;

                    FontSize = 24;
                    break;

                case Business.Models.CellType.Player:
                    Symbol = "●";
                    Background = Brushes.LightGreen;
                    Foreground = Brushes.DarkGreen;
                    FontSize = 24;
                    break;

                case Business.Models.CellType.Exit:
                    Symbol = "★";
                    Background = Brushes.Gold;
                    Foreground = Brushes.DarkOrange;
                    FontSize = 24;
                    break;

                case Business.Models.CellType.Empty:
                default:
                    Symbol = " ";
                    Background = Brushes.White;
                    Foreground = Brushes.Black;
                    break;
            }
        }
    }
}