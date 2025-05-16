using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorWPF.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private CalculatorState _state = new CalculatorState();
        private CalculatorService _service = new CalculatorService();

        public event PropertyChangedEventHandler PropertyChanged;

        public string DisplayValue
        {
            get
            {
                if (_state.HasDecimalPoint)
                {
                    return _state.RegisterX.ToString("0." + new string('0', _state.DecimalPlaces));
                }
                return _state.RegisterX.ToString();
            }
        }

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

        public void AddDecimal()
        {
            _service.AddDecimal(_state);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressSquare()
        {
            _service.PressSquare(_state);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressSquareRoot()
        {
            _service.PressSquareRoot(_state);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressLogarithm()
        {
            _service.PressLogarithm(_state);
            OnPropertyChanged(nameof(DisplayValue));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
