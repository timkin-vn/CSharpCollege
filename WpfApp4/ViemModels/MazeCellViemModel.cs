using System.Windows.Media;

namespace WpfApp4.ViewModels
{
    public class MazeCellViewModel
    {
        public string Symbol { get; set; } = "";
        public Brush Background { get; set; } = Brushes.White;
        public Brush Foreground { get; set; } = Brushes.Black;

        public MazeCellViewModel() { }

        public MazeCellViewModel(string symbol, Brush background, Brush foreground)
        {
            Symbol = symbol;
            Background = background;
            Foreground = foreground;
        }
    }
}