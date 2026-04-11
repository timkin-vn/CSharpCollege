﻿using Calculator.Business.Models;
using Calculator.Business.Services;
using System.ComponentModel;

namespace Calculator.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly CalculatorModel _calculatorModel = new CalculatorModel();
        private readonly CalculatorService _calculatorService = new CalculatorService();

        public MainWindowViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string DisplayValue => _calculatorModel.DisplayText;

        public void PressDigit(string digitString)
        {
            _calculatorService.PressDigit(_calculatorModel, digitString);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressClear()
        {
            _calculatorService.PressClear(_calculatorModel);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressOperation(string operationCode)
        {
            _calculatorService.PressOperation(_calculatorModel, operationCode);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressEqual()
        {
            _calculatorService.PressEqual(_calculatorModel);
            OnPropertyChanged(nameof(DisplayValue));
        }

public void PressDecimalSeparator()
{
    _calculatorService.PressDecimalSeparator(_calculatorModel);
    OnPropertyChanged(nameof(DisplayValue));
}

public void PressToggleSign()
{
    _calculatorService.PressToggleSign(_calculatorModel);
    OnPropertyChanged(nameof(DisplayValue));
}

public void PressSquareRoot()
{
    _calculatorService.PressSquareRoot(_calculatorModel);
    OnPropertyChanged(nameof(DisplayValue));
}

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
