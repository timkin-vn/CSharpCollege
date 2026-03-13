using Calculator.Business.Models;
using Calculator.Business.Services;
using System.ComponentModel;

namespace Calculator.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly CalculatorModel _calculatorModel = new CalculatorModel();
        private readonly CalculatorService _calculatorService = new CalculatorService();

        public event PropertyChangedEventHandler PropertyChanged;

        public string Result => _calculatorModel.RegisterX.ToString();

        public void PressDigit(string digitString)
        {
            _calculatorService.PressDigit(_calculatorModel, digitString);
            OnPropertyChanged(nameof(Result));
        }

        public void PressClear()
        {
            _calculatorService.PressClear(_calculatorModel);
            OnPropertyChanged(nameof(Result));
        }

        public void PressOperation(string operationCode)
        {
            _calculatorService.PressOperation(_calculatorModel, operationCode);
            OnPropertyChanged(nameof(Result));
        }

        public void PressSqrt()
        {
            _calculatorService.PressSqrt(_calculatorModel);
            OnPropertyChanged(nameof(Result));
        }

        public void PressPower()
        {
            _calculatorService.PressPower(_calculatorModel);
            OnPropertyChanged(nameof(Result));
        }

        public void PressPercent()
        {
            _calculatorService.PressPercent(_calculatorModel);
            OnPropertyChanged(nameof(Result));
        }

        public void PressAbs()
        {
            _calculatorService.PressAbs(_calculatorModel);
            OnPropertyChanged(nameof(Result));
        }

        public void PressSquare()
        {
            _calculatorService.PressSquare(_calculatorModel);
            OnPropertyChanged(nameof(Result));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}