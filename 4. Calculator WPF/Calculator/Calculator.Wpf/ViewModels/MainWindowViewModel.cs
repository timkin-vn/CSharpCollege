using Calculator.Business.Models;
using Calculator.Business.Services;
using System.ComponentModel;

namespace Calculator.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly CalculatorModel _calculatorModel = new CalculatorModel();
        private readonly CalculatorService _calculatorService = new CalculatorService();
        private string _displayValue = "0";

        public event PropertyChangedEventHandler PropertyChanged;

        public string DisplayValue => _displayValue;

        public void PressDigit(string digitString)
        {
            _calculatorService.PressDigit(_calculatorModel, digitString);
            SyncDisplayFromModel();
        }

        public void PressDecimal()
        {
            _calculatorService.PressDecimal(_calculatorModel);
            SyncDisplayFromModel();
        }

        public void PressClear()
        {
            _calculatorService.PressClear(_calculatorModel);
            SyncDisplayFromModel();
        }

        public void PressMoveXToY()
        {
            _calculatorService.PressMoveXToY(_calculatorModel);
            SyncDisplayFromModel();
        }

        public void PressOperation(string operationCode)
        {
            _calculatorService.PressOperation(_calculatorModel, operationCode);
            SyncDisplayFromModel();
        }

        public void PressSin()
        {
            _calculatorService.PressSin(_calculatorModel);
            SyncDisplayFromModel();
        }

        public void PressCos()
        {
            _calculatorService.PressCos(_calculatorModel);
            SyncDisplayFromModel();
        }

        public void PressTg()
        {
            _calculatorService.PressTg(_calculatorModel);
            SyncDisplayFromModel();
        }

        public void PressCtg()
        {
            _calculatorService.PressCtg(_calculatorModel);
            SyncDisplayFromModel();
        }

        private void SyncDisplayFromModel()
        {
            _displayValue = _calculatorModel.DisplayText;
            Notify();
        }

        private void Notify()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayValue)));
        }
    }
}
