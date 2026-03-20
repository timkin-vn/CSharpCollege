using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_CALCULATOR.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private CalculatorModel _calculatorModel = new CalculatorModel();

        private CalculatorService _calculatorService = new CalculatorService();

        public event PropertyChangedEventHandler PropertyChanged;

        public string Result => _calculatorModel.RegisterX.ToString();

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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

        public void PressSquare()
        {
            _calculatorService.PressSquare(_calculatorModel);
            OnPropertyChanged(nameof(Result));
        }

        public void PressComma()
        {
            _calculatorService.PressComma(_calculatorModel);
            OnPropertyChanged(nameof(Result));
        }

        public void PressEqual()
        {
            _calculatorService.PressEqual(_calculatorModel);
            OnPropertyChanged(nameof(Result));
        }

    }
}
