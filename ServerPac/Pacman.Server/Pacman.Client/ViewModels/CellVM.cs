using Pacman.Common.Enums;
using System.ComponentModel;
using System.Windows.Media; 

namespace Pacman.Client.ViewModels
{
    public class CellVM : INotifyPropertyChanged
    {
        private CellType _type;

        public CellType Type
        {
            get => _type;
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged(nameof(Type));
                    OnPropertyChanged(nameof(Color));
                }
            }
        }

        public Brush Color
        {
            get
            {
                switch (Type)
                {
                    
                    case CellType.Wall:
                        return new SolidColorBrush(System.Windows.Media.Color.FromRgb(25, 25, 166));

                   
                    case CellType.Coin:
                        return Brushes.Gold;

                    
                    case CellType.Pacman:
                        return Brushes.Yellow;

                    
                    case CellType.Ghost:
                        return Brushes.Red;

                    
                    default:
                        return Brushes.Transparent;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}