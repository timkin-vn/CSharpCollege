using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorWPF.ViewModels
{
    public class MainWindowsViewModel : INotifyPropertyChanged
    {
        private CalculatorState _state = new CalculatorState();
        private CalculatorService _service = new CalculatorService();
        private ObservableCollection<string> _calculationHistory = new ObservableCollection<string>();
        private string _currentExpression = string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

        public string DisplayValue
        {
            get { return _state.RegisterX.ToString(); }
            set
            {
                if (_state.RegisterX.ToString() != value)
                {
                    if (double.TryParse(value, out double result))
                    {
                        _state.RegisterX = result;
                        OnPropertyChanged(nameof(DisplayValue));
                    }
                }
            }
        }

        public ObservableCollection<string> CalculationHistory
        {
            get { return _calculationHistory; }
            set
            {
                _calculationHistory = value;
                OnPropertyChanged(nameof(CalculationHistory));
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
            if (operationCode == "=")
            {
                if (!string.IsNullOrEmpty(_currentExpression))
                {
                    double secondNumber = _state.RegisterX;
                    _service.PressEqual(_state);
                    string fullExpression = $"{_currentExpression} {secondNumber} = {_state.RegisterX}";
                    _calculationHistory.Add(fullExpression);
                    _currentExpression = string.Empty;
                    OnPropertyChanged(nameof(DisplayValue));
                }
            }
            else
            {
                if (string.IsNullOrEmpty(_currentExpression))
                {
                    _currentExpression = _state.RegisterX.ToString();
                }
                _currentExpression += $" {operationCode}"; 
                _service.PressOperation(_state, operationCode);
                OnPropertyChanged(nameof(DisplayValue));
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void PressLog()
        {
            double value = double.Parse(_state.RegisterX.ToString());
            _service.PressLog(_state);
            _calculationHistory.Add($"log({value}) = {_state.RegisterX}");
            OnPropertyChanged(nameof(DisplayValue));
            OnPropertyChanged(nameof(CalculationHistory));
        }

        public void PressSin()
        {
            double value = double.Parse(_state.RegisterX.ToString());
            _service.PressSin(_state);
            _calculationHistory.Add($"sin({value}) = {_state.RegisterX}");
            OnPropertyChanged(nameof(DisplayValue));
            OnPropertyChanged(nameof(CalculationHistory));
        }

        public void PressCos()
        {
            double value = double.Parse(_state.RegisterX.ToString());
            _service.PressCos(_state);
            _calculationHistory.Add($"cos({value}) = {_state.RegisterX}");
            OnPropertyChanged(nameof(DisplayValue));
            OnPropertyChanged(nameof(CalculationHistory));
        }

        public void PressTg()
        {   
            double value = double.Parse(_state.RegisterX.ToString());
            _service.PressTan(_state);
            _calculationHistory.Add($"tg({value}) = {_state.RegisterX}");
            OnPropertyChanged(nameof(DisplayValue));
            OnPropertyChanged(nameof(CalculationHistory));
        }

        public void PressSqrt()
        {
            double value = double.Parse(_state.RegisterX.ToString());
            _service.PressSqrt(_state);
            _calculationHistory.Add($"sqrt({value}) = {_state.RegisterX}");
            OnPropertyChanged(nameof(DisplayValue));
            OnPropertyChanged(nameof(CalculationHistory));
        }

        public void PressDegree()
        {
            double value = double.Parse(_state.RegisterX.ToString());
            _service.PressDegree(_state);
            _calculationHistory.Add($"({value})^2 = {_state.RegisterX}");
            OnPropertyChanged(nameof(DisplayValue));
            OnPropertyChanged(nameof(CalculationHistory));
        }

        public void PressUpdSign()
        {
            double value = double.Parse(_state.RegisterX.ToString());
            _service.PressUpdateSign(_state);
            _calculationHistory.Add($"+/-({value}) = {_state.RegisterX}");
            OnPropertyChanged(nameof(DisplayValue));
            OnPropertyChanged(nameof(CalculationHistory));
        }

        public void PressPercent()
        {
            double value = double.Parse(_state.RegisterX.ToString());
            _service.PressPercent(_state);
            _calculationHistory.Add($"%({value}) = {_state.RegisterX}");
            OnPropertyChanged(nameof(DisplayValue));
            OnPropertyChanged(nameof(CalculationHistory));
        }

        public void PressCleanHistory()
        {
            _calculationHistory.Clear();
            OnPropertyChanged(nameof(CalculationHistory));
        }
    }
}
