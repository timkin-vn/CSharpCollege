using System.ComponentModel;
using System.Windows.Media;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel : INotifyPropertyChanged
    {
        private char _state;
        public char State
        {
            get { return _state; }
            set
            {
                _state = value;
                OnPropertyChanged("State");
                OnPropertyChanged("Text");
                OnPropertyChanged("Color");
            }
        }

        private bool _isShipDestroyed;
        public bool IsShipDestroyed
        {
            get { return _isShipDestroyed; }
            set
            {
                _isShipDestroyed = value;
                OnPropertyChanged("IsShipDestroyed");
            }
        }

        public string Text
        {
            get
            {
                if (State == 'H' && IsShipDestroyed) return "X";
                if (State == 'M') return "O";
                if (State == 'F') return "F";
                return "";
            }
        }

        public Brush Color
        {
            get
            {
                if (State == 'H') return IsShipDestroyed ? Brushes.DarkRed : Brushes.Red;
                if (State == 'M') return Brushes.Gray;
                if (State == 'F') return Brushes.Yellow;
                return Brushes.LightBlue;
            }
        }

        public int Row { get; set; }
        public int Column { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}