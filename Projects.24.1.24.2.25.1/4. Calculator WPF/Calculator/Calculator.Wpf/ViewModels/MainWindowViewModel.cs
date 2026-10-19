using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private CalculatorModel _calculatorModel = new CalculatorModel();

        private CalculatorService _calculatorService = new CalculatorService();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> History { get; } = new ObservableCollection<string>();

        public string Result
        {
            get
            {
                if (_calculatorModel.HasError)
                {
                    return "Ошибка";
                }

                var value = _calculatorModel.RegisterX;

                if (!_calculatorModel.IsDecimalEntry)
                {
                    return value.ToString();
                }

                var text = value.ToString("F" + _calculatorModel.DecimalDigits);

                if (_calculatorModel.DecimalDigits == 0)
                {
                    text += CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                }

                return text;
            }
        }

        public void PressDigit(string digitString)
        {
            _calculatorService.PressDigit(_calculatorModel, digitString);
            Refresh();
        }

        public void PressClear()
        {
            _calculatorService.PressClear(_calculatorModel);
            Refresh();
        }

        public void PressOperation(string operationCode)
        {
            _calculatorService.PressOperation(_calculatorModel, operationCode);
            Refresh();
        }

        public void PressEqual()
        {
            _calculatorService.PressEqual(_calculatorModel);
            Refresh();
        }

        public void PressDecimalPoint()
        {
            _calculatorService.PressDecimalPoint(_calculatorModel);
            Refresh();
        }

        public void PressBackspace()
        {
            _calculatorService.PressBackspace(_calculatorModel);
            Refresh();
        }

        public void PressToggleSign()
        {
            _calculatorService.PressToggleSign(_calculatorModel);
            Refresh();
        }

        public void ClearHistory()
        {
            _calculatorModel.History.Clear();
            History.Clear();
        }

        private void Refresh()
        {
            while (History.Count < _calculatorModel.History.Count)
            {
                History.Add(_calculatorModel.History[History.Count]);
            }

            OnPropertyChanged(nameof(Result));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}