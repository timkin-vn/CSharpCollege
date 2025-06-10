using Calculator.Business.Models;
using Calculator.Business.Services;
using System.ComponentModel;

namespace Calculator.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly CalculatorState _state = new CalculatorState();
        private readonly CalculatorService _service = new CalculatorService();

        public event PropertyChangedEventHandler PropertyChanged;

        public string DisplayValue => _state.RegisterX.ToString();

        public void PressDigit(string digit)
        {
            _service.PressDigit(_state, digit);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressClear()
        {
            _service.Clear(_state);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressOperation(string operationCode)
        {
            _service.PressOperation(_state, operationCode);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressProcent()
        {
            _state.RegisterY = _state.RegisterY > 0 ? _state.RegisterY : 1;
            _state.RegisterX = _state.RegisterY / 100 * _state.RegisterX;
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressFloat()
        {
            _state.IsFloat = true;
            _state.bonus = 10.0;
        }

        public void PressBackspace()
        {
            _service.PressBackspace(_state);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressEqual()
        {
            _service.PressEqual(_state);
            OnPropertyChanged(nameof(DisplayValue));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}