namespace Game2048.Wpf.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        private int _value;
        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        public string DisplayText => Value == 0 ? "" : Value.ToString();
    }
}
