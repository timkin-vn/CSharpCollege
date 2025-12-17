using FifteenGame.Common.Definitions; 
using System.ComponentModel;
using System.Windows.Media;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellVM : INotifyPropertyChanged
    {
        
        public int Row { get; set; }
        public int Column { get; set; }

        
        public bool IsMyField { get; set; }

        private CellState _state;
        public CellState State
        {
            get { return _state; }
            set
            {
                _state = value;
                Refresh(); 
            }
        }

       
        public Brush Background
        {
            get
            {
                switch (State)
                {
                    case CellState.Sunk: return Brushes.DarkRed;       
                    case CellState.Hit: return Brushes.OrangeRed;      
                    case CellState.Miss: return Brushes.LightSteelBlue;
                    case CellState.Ship:
                        return IsMyField ? Brushes.Gray : Brushes.CornflowerBlue; 
                    default: return Brushes.CornflowerBlue;            
                }
            }
        }

        public string Symbol
        {
            get
            {
                switch (State)
                {
                    case CellState.Sunk: return "✖";
                    case CellState.Hit: return "×";
                    case CellState.Miss: return "•";
                    case CellState.Ship: return IsMyField ? "■" : "";
                    default: return "";
                }
            }
        }

        public Brush TextColor
        {
            get { return State == CellState.Sunk ? Brushes.White : Brushes.Yellow; }
        }

        
        public void Refresh()
        {
            OnPropertyChanged(nameof(State));
            OnPropertyChanged(nameof(Background));
            OnPropertyChanged(nameof(Symbol));
            OnPropertyChanged(nameof(TextColor));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}