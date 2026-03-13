using Calculator.Business.Models;
using Calculator.Business.Services;
using System.ComponentModel;

namespace Calculator.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private CalculatorModel _calculatorModel = new CalculatorModel();
        private CalculatorService _calculatorService = new CalculatorService();

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

        public void PressEqual()
        {
            _calculatorService.PressEqual(_calculatorModel);
            OnPropertyChanged(nameof(Result));
        }

        public void PressBackspace()
        {
            _calculatorService.PressBackspace(_calculatorModel);
            OnPropertyChanged(nameof(Result));
        }

        public void PressChangeSign()
        {
            _calculatorService.PressChangeSign(_calculatorModel);
            OnPropertyChanged(nameof(Result));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}