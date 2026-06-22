using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.ComponentModel;
using System.Globalization;

namespace Calculator.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly CalculatorModel _calculatorModel = new CalculatorModel();
        private readonly CalculatorService _calculatorService = new CalculatorService();
        private readonly CultureInfo _displayCulture = CultureInfo.GetCultureInfo("ru-RU");

        public event PropertyChangedEventHandler PropertyChanged;

        public string Result
        {
            get
            {
                if (double.IsNaN(_calculatorModel.RegisterX))
                {
                    return "Ошибка";
                }

                if (double.IsPositiveInfinity(_calculatorModel.RegisterX) || double.IsNegativeInfinity(_calculatorModel.RegisterX))
                {
                    return "∞";
                }

                string text = _calculatorModel.RegisterX.ToString("G15", _displayCulture);

                if (_calculatorModel.IsDrob && !text.Contains(","))
                {
                    text += ",";
                }

                return text;
            }
        }

        public void PressDigit(string digitString)
        {
            _calculatorService.PressDigit(_calculatorModel, digitString);
            UpdateResult();
        }

        public void PressComma()
        {
            _calculatorService.PressComma(_calculatorModel);
            UpdateResult();
        }

        public void PressClear()
        {
            _calculatorService.PressClear(_calculatorModel);
            UpdateResult();
        }

        public void PressOperation(string operationCode)
        {
            _calculatorService.PressOperation(_calculatorModel, operationCode);
            UpdateResult();
        }

        public void PressEqual()
        {
            _calculatorService.PressEqual(_calculatorModel);
            UpdateResult();
        }

        public void PressSquare()
        {
            _calculatorService.PressSquare(_calculatorModel);
            UpdateResult();
        }

        public void PressSquareRoot()
        {
            _calculatorService.PressSquareRoot(_calculatorModel);
            UpdateResult();
        }

        private void UpdateResult()
        {
            OnPropertyChanged(nameof(Result));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
