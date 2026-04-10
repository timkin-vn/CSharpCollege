using Calculator.Business.Services;
using System.ComponentModel;
using Calculator.Business.Models;

namespace Calculator.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly CalculatorModel _calculatorModel = new CalculatorModel();

        private readonly CalculatorService _calculatorService = new CalculatorService();

        public event PropertyChangedEventHandler PropertyChanged;

        public string DisplayValue => _calculatorModel.RegisterX.ToString();

        public void PressDigit(string digitString)
        {
            _calculatorService.PressDigit(_calculatorModel, digitString);
            UpdateDisplay();
        }

        public void PressClear()
        {
            _calculatorService.PressClear(_calculatorModel);
            UpdateDisplay();
        }

        public void PressOperation(string operationCode)
        {
            _calculatorService.PressOperation(_calculatorModel, operationCode);
            UpdateDisplay();
        }

        public void PressEqual()
        {
            _calculatorService.PressEqual(_calculatorModel);
            UpdateDisplay();
        }

        public void PressBackspace()
        {
            _calculatorService.PressBackspace(_calculatorModel);
            UpdateDisplay();
        }

        public void PressToggleSign()
        {
            _calculatorService.PressToggleSign(_calculatorModel);
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            OnPropertyChanged(nameof(DisplayValue));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
