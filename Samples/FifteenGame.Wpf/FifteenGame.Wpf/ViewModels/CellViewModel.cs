using System.ComponentModel;

namespace WordGame.ViewModels
{
    public class CellViewModel : INotifyPropertyChanged
    {
        private char _letter;
        private bool _isSelected;
        private string _backgroundColor = "LightGray";

        public char Letter
        {
            get => _letter;
            set
            {
                _letter = value;
                OnPropertyChanged(nameof(Letter));
            }
        }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public string BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }

        public int Row { get; set; }
        public int Column { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}