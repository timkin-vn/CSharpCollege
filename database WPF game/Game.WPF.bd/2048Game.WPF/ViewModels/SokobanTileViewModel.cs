using System;
using System.ComponentModel;
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

        // Symbols redesigned for clarity:
        // 0 - empty
        // 1 - wall -> "█"
        // 2 - box  -> "■"
        // 3 - target -> "○"
        // 4 - player -> "▲"
        // 5 - box on target -> "◎"
        // 6 - player on target -> "★"
        public string Text => GetText();

        private string GetText()
        {
            switch (Value)
            {
                case 1: return "67"; // wall
                case 2: return "■"; // box
                case 3: return "○"; // target
                case 4: return "▲"; // player
                case 5: return "◎"; // box on target
                case 6: return "★"; // player on target
                default: return "";
            }
        }

        public Brush Background => GetBackground();

        private Brush GetBackground()
        {
            // Targets (3) and objects on targets (5,6) previously yellow -> now orange
            if (Value == 3 || Value == 5 || Value == 6)
            {
                return new SolidColorBrush(Color.FromRgb(255, 165, 0)); // orange
            }

            // Brown (boxes) -> turquoise; Green (player) -> red
            switch (Value)
            {
                case 1: return new SolidColorBrush(Color.FromRgb(80, 80, 80)); // wall (dark)
                case 2: return new SolidColorBrush(Color.FromRgb(204, 0, 102)); // turquoise for box
                case 4: return new SolidColorBrush(Color.FromRgb(200, 30, 30)); // player -> red
                default: return new SolidColorBrush(Color.FromRgb(230, 230, 230)); // empty
            }
        }

        public Brush Foreground => GetForeground();

        private Brush GetForeground()
        {
            // Ensure good contrast:
            // Boxes (2) and box-on-target (5) get dark foreground.
            if (Value == 2 || Value == 5)
                return new SolidColorBrush(Color.FromRgb(20, 20, 20));

            // Player (4) and player-on-target (6) are red/orange backgrounds -> use white foreground
            if (Value == 4 || Value == 6)
                return new SolidColorBrush(Color.FromRgb(255, 255, 255));

            // Default dark foreground for readability
            return new SolidColorBrush(Color.FromRgb(20, 20, 20));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
