using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Calculator.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private CalculatorState _state = new CalculatorState();

        private CalculatorService _service = new CalculatorService();

        public event PropertyChangedEventHandler PropertyChanged;


        public string DisplayValue
        {
            get => _state.RegisterX.ToString();
        }

        public void PressChange()
        {
            
            _service.change();
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

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void PressSin()
        {
            double value = double.Parse(_state.RegisterX.ToString());
            _service.PressSin(_state);
            
            OnPropertyChanged(nameof(DisplayValue));
            
        }

        public void PressBack()
        {
            _service.Back(_state);

            OnPropertyChanged(nameof(DisplayValue));

        }

        public void PressCos()
        {
            _service.Cos(_state);

            OnPropertyChanged(nameof(DisplayValue));

        }

        public void PressTg()
        {
            _service.Tan(_state);

            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressSqrt()
        {
            _service.Sqrt(_state);

            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressDegree()
        {
            _service.Degree(_state);

            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressLog()
        {
            _service.Log(_state);

            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressPercent()
        {
            _service.Percent(_state);

            OnPropertyChanged(nameof(DisplayValue));
        }

        public void ChangeSign()
        {
            _service.SignChange(_state);

            OnPropertyChanged(nameof(DisplayValue));
        }







    }
}
