using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private CalculatorState _state = new CalculatorState();

        private CalculatorService _service = new CalculatorService();

        public event PropertyChangedEventHandler PropertyChanged;

        public string DisplayValue => _state.InputBuffer;

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
        public void PressEqual()
        {
            _service.PressEqual(_state);
            OnPropertyChanged(nameof(DisplayValue));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void PressBackspace()
        {
            _service.Backspace(_state);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressSquareRoot()
        {
            _service.SquareRoot(_state);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressSquare()
        {
            _service.Square(_state);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressToggleSign()
        {
            _service.ToggleSign(_state);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressDecimalPoint()
        {
            _service.PressDecimalPoint(_state);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressInverse()
        {
            _service.Invert(_state);
            OnPropertyChanged(nameof(DisplayValue));
        }

    }
}
