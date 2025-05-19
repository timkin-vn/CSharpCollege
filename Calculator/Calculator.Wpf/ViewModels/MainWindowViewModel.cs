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

        public void PressDot()
        {
            _service.PressDot(_state);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressSignChange()
        {
            _service.PressSignChange(_state);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressBackspace()
        {
            _service.PressBackspace(_state);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressClearAll()
        {
            _service.ClearAll(_state);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressClearEntry()
        {
            _service.ClearEntry(_state);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressEqual()
        {
            _service.PressEqual(_state);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressOperation(string operationCode)
        {
            _service.PressOperation(_state, operationCode);
            OnPropertyChanged(nameof(DisplayValue));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
