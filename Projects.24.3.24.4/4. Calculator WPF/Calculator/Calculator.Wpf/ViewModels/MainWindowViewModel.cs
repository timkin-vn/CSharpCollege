using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Calculator.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly CalculatorModel _calculatorModel = new CalculatorModel();

        private readonly CalculatorService _calculatorService = new CalculatorService();

        private string _commaSuffix = "";

        private string _inputString = "0";

        public string DisplayValue => _inputString;

        public void PressDigit(string digitString)
        {
            if (_inputString == "0") _inputString = digitString;
            else _inputString += digitString;

            if (double.TryParse(_inputString.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double val))
            {
                _calculatorModel.SetRegisterX(val);
            }
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressClear()
        {
            _calculatorService.PressClear(_calculatorModel);
            _inputString = "0"; 
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressOperation(string operationCode)
        {
            _calculatorService.PressOperation(_calculatorModel, operationCode);
            OnPropertyChanged(nameof(DisplayValue));
        }

        private double ToRadians(double degrees) => degrees * Math.PI / 180;

        public void PressSin()
        {
            double value = _calculatorModel.RegisterX;
            double result = Math.Sin(ToRadians(value));
            _calculatorModel.SetRegisterX(result);

            _inputString = result.ToString();
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressCos()
        {
            double value = _calculatorModel.RegisterX;
            double result = Math.Cos(ToRadians(value));
            _calculatorModel.SetRegisterX(result);

            _inputString = result.ToString();
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressTan()
        {
            double value = _calculatorModel.RegisterX;
            double radians = ToRadians(value);

            if (Math.Abs(Math.Cos(radians)) < 1e-10) return; 

            double result = Math.Tan(radians);
            _calculatorModel.SetRegisterX(result);

            _inputString = result.ToString();
            OnPropertyChanged(nameof(DisplayValue));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void PressComma()
        {
            if (!_inputString.Contains(","))
            {
                _inputString += ",";
                OnPropertyChanged(nameof(DisplayValue));
            }
        }
    }
}
