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
            _state.bonus = 10.0;
            _state.IsFloat = false;
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

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
