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

        public string Result => _calculatorModel.InputText;

        private void UpdateResult()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Result)));
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
    }
}
