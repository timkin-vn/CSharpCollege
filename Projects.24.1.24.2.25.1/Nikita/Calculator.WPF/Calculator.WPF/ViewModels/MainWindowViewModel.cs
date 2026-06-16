using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Business.Models;
using Calculator.Business.Services;

namespace Calculator.WPF.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private CalculatorModel _calculatorModel = new CalculatorModel();
        private CalculatorService _calculatorService = new CalculatorService();
        public string Result => _calculatorModel.RegisterX.ToString();
        public void PressDigit(string digitString)
        {
            _calculatorService.PressDigit(_calculatorModel, digitString);
            OnPropertyChanged(nameof(Result));
        }
        public event PropertyChangedEventHandler PropertyChanged;
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
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public void PressPresent()
        {
            _calculatorService.PreePrecent(_calculatorModel);
            OnPropertyChanged(nameof(Result));
        }
    }
}
