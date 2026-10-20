using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        private MoveDirection _direction = MoveDirection.Empty;

        public int Row { get; set; }
        public int Column { get; set; }

        public MoveDirection Direction
        {
            get => _direction;
            set
            {
                if (_direction != value)
                {
                    _direction = value;
                    OnPropertyChanged(nameof(Direction));
                    OnPropertyChanged(nameof(Symbol));
                    OnPropertyChanged(nameof(ForegroundColor));
                }
            }
        }

        public string Symbol
        {
            get
            {
                switch (Direction)
                {
                    case MoveDirection.Miss:
                        return "•";
                    case MoveDirection.Hit:
                        return "✕";
                    case MoveDirection.Destroyed:
                        return "☠";
                    default:
                        return string.Empty;
                }
            }
        }

        public Brush ForegroundColor
        {
            get
            {
                switch (Direction)
                {
                    case MoveDirection.Miss:
                        return Brushes.Blue;
                    case MoveDirection.Hit:
                        return Brushes.Red;
                    case MoveDirection.Destroyed:
                        return Brushes.White;
                    default:
                        return Brushes.Transparent;
                }
            }
        }
    }
}