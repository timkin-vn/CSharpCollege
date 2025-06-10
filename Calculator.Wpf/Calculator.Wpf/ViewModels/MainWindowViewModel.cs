using Calculator.Business.Models;
using Calculator.Business.Services;
using System.ComponentModel;
using System;
namespace Calculator.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly CalculatorState _state = new CalculatorState();
        private readonly CalculatorService _service = new CalculatorService();
        private int _decimalPlaces = 3; // начальное значение

        public event PropertyChangedEventHandler PropertyChanged;

   
        public MainWindowViewModel()
        {
            _state.RegisterX = 0.0;
            OnPropertyChanged(nameof(DisplayValue));
        }

    
        public string DisplayValue
        {
            get
            {
                if (double.IsNaN(_state.RegisterX) || double.IsInfinity(_state.RegisterX))
                    return "не число";
                
                if (Math.Abs(_state.RegisterX % 1) < double.Epsilon)
                    return ((long)_state.RegisterX).ToString();
                return _state.RegisterX.ToString("F" + _decimalPlaces);
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

        public void PressEqual()
        {
            _service.PressEqual(_state);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void SquareRoot_Click(object sender, System.EventArgs e)
        {
            _service.SquareRoot(_state);
            ShowResult();
        }

        public void PressPi_Click(object sender, System.EventArgs e)
        {
            _service.PressPi(_state);
            ShowResult();
        }

        public void DecimalPoint_Click(object sender, System.EventArgs e)
        {
            _service.DecimalPoint(_state);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void IncreaseDecimalPlaces()
        {
            if (_decimalPlaces < 10)
                _decimalPlaces++;
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void DecreaseDecimalPlaces()
        {
            if (_decimalPlaces > 0)
            {
                _decimalPlaces--;
                _state.RegisterX = Math.Round(_state.RegisterX, _decimalPlaces, MidpointRounding.AwayFromZero);
                OnPropertyChanged(nameof(DisplayValue));
            }
        }

        public void RoundToSelectedPrecision()
        {
            double value;
            if (double.TryParse(_state.RegisterX.ToString(), out value))
            {
                value = Math.Round(value, _decimalPlaces, MidpointRounding.AwayFromZero);
                _state.RegisterX = value; 
                OnPropertyChanged(nameof(DisplayValue));
            }
        }

        private void ShowResult()
        {
            OnPropertyChanged(nameof(DisplayValue));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}