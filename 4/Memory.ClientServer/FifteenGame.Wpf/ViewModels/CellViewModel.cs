using System.ComponentModel;
using System.Windows.Media;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel : INotifyPropertyChanged
    {
        private int _number;

        public int Row { get; set; }
        public int Column { get; set; }

        public int Number
        {
            get => _number;
            set
            {
                if (_number != value)
                {
                    _number = value;
                    OnPropertyChanged(nameof(Number));
                    OnPropertyChanged(nameof(IsEmpty));
                    OnPropertyChanged(nameof(TileColor)); 
                }
            }
        }

        public bool IsEmpty => Number == 0;

        public Brush TileColor
        {
            get
            {
                switch (Number)
                {
                    case 0: return Brushes.WhiteSmoke;
                    case 2: return Brushes.Beige;
                    case 4: return Brushes.Bisque;
                    case 8: return Brushes.Orange;
                    case 16: return Brushes.Gold;
                    case 32: return Brushes.DarkOrange;
                    case 64: return Brushes.OrangeRed;
                    case 128: return Brushes.LightSalmon;
                    case 256: return Brushes.Salmon;
                    case 512: return Brushes.LightCoral;
                    case 1024: return Brushes.Coral;
                    case 2048: return Brushes.Red;
                    default: return Brushes.Gray;
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
