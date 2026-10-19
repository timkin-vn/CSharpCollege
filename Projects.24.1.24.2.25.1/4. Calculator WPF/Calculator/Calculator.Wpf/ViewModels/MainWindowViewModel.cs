using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.ComponentModel;

namespace Calculator.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly CalculatorModel _calculatorModel = new CalculatorModel();
        private readonly CalculatorService _calculatorService = new CalculatorService();
        private string _errorMessage;

        public event PropertyChangedEventHandler PropertyChanged;
        public string Result => _errorMessage ?? _calculatorModel.RegisterX.ToString("G");

        public void PressDigit(string digitString)
        {
            _errorMessage = null;
            _calculatorService.PressDigit(_calculatorModel, digitString);
            OnPropertyChanged(nameof(Result));
        }

        public void PressClear()
        {
            _errorMessage = null;
            _calculatorService.PressClear(_calculatorModel);
            OnPropertyChanged(nameof(Result));
        }

        public void PressOperation(string operationCode)
        {
            _errorMessage = null;
            try
            {
                if (operationCode == "=") _calculatorService.PressEqual(_calculatorModel);
                else _calculatorService.PressOperation(_calculatorModel, operationCode);
            }
            catch (Exception ex) { _errorMessage = ex.Message; }
            OnPropertyChanged(nameof(Result));
        }

        public void PressUnaryOperation(string operationCode)
        {
            _errorMessage = null;
            try { _calculatorService.PressUnaryOperation(_calculatorModel, operationCode); }
            catch (Exception ex) { _errorMessage = ex.Message; }
            OnPropertyChanged(nameof(Result));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
