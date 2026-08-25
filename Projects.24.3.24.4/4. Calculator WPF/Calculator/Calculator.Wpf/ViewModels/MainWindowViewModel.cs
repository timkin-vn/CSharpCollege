using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            OnPropertyChanged(nameof(DisplayValue)); // "DisplayValue"
        }

        public void PressClear()
        {
            _calculatorService.PressClear(_calculatorModel);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressOperatin(string operationCode)
        {
            _calculatorService.PressOperation(_calculatorModel, operationCode);
            OnPropertyChanged(nameof(DisplayValue));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
