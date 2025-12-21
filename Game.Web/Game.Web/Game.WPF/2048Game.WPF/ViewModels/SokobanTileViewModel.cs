using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Media;

namespace _2048Game.WPF.ViewModels
{
    public class SokobanTileViewModel : INotifyPropertyChanged
    {
        public int Value { get; set; }

        private bool _isRecentlyPlaced;
        public bool IsRecentlyPlaced
        {
            get => _isRecentlyPlaced;
            set
            {
                if (_isRecentlyPlaced != value)
                {
                    _isRecentlyPlaced = value;
                    OnPropertyChanged(nameof(IsRecentlyPlaced));
                }
            }
        }

        public string Text => GetText();

        private string GetText()
        {
            switch (Value)
            {
                case 1: return ""; // wall
                case 2: return "?"; // box
                case 3: return "."; // target
                case 4: return "@"; // player
                case 5: return "?"; // box on target
                case 6: return "@"; // player on target
                default: return "";
            }
        }

        public Brush Background => GetBackground();

        private Brush GetBackground()
        {
            // Make target cells yellow (including box on target and player on target)
            if (Value == 3 || Value == 5 || Value == 6)
            {
                return new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 235, 59)); // yellow
            }

            switch (Value)
            {
                case 1: return new SolidColorBrush(System.Windows.Media.Color.FromRgb(80, 80, 80));
                case 2: return new SolidColorBrush(System.Windows.Media.Color.FromRgb(160, 120, 80));
                case 4: return new SolidColorBrush(System.Windows.Media.Color.FromRgb(50, 150, 50));
                default: return new SolidColorBrush(System.Windows.Media.Color.FromRgb(230, 230, 230));
            }
        }

        public Brush Foreground => GetForeground();

        private Brush GetForeground()
        {
            // Dark foreground for symbols
            if (Value == 2 || Value == 5)
                return new SolidColorBrush(System.Windows.Media.Color.FromRgb(40, 20, 20));

            return new SolidColorBrush(System.Windows.Media.Color.FromRgb(20, 20, 20));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
