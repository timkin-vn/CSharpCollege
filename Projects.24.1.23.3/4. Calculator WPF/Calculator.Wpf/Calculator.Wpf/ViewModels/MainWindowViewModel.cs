using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;


namespace Calculator.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private CalculatorState _state = new CalculatorState();

        private CalculatorService _service = new CalculatorService();

        public double _historyY;
        public double _historyX;
        private string _historyOp = string.Empty;
        private bool _historyAvailable = false;

        public ObservableCollection<string> History { get; }
            = new ObservableCollection<string>();

        public event PropertyChangedEventHandler PropertyChanged;

        public string DisplayValue
        {
            get => _state.RegisterX.ToString();
        }

        public void PressDigit(string digit)
        {
            _service.PressDigit(_state, digit);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressClear()
        {
            _service.Clear(_state);
            OnPropertyChanged(nameof(DisplayValue));
        }
        public void PressClearEntry()
        {
            _service.ClearEntry(_state);
            OnPropertyChanged(nameof(DisplayValue));
        }
        public void PressToggleSign()
        {
            _service.ToggleSign(_state);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressOperation(string operationCode)
        {            
            if (operationCode != "=")
            {
                _historyOp = operationCode;
            }

            _historyAvailable = true;

            _service.PressOperation(_state, operationCode);
            OnPropertyChanged(nameof(DisplayValue));
        }

        public void PressEqual()
        {
            _historyY = _state.RegisterY;
            _historyX = _state.RegisterX;
            _service.PressEqual(_state);

            var result = _state.RegisterX;

            string entry;
            if (!string.IsNullOrEmpty(_historyOp))
                entry = $"{_historyY} {_historyOp} {_historyX} = {result}";
            else
                entry = $"{_historyX} = {result}";

            AddToHistory(entry);

            _historyAvailable = false;
            _historyOp = string.Empty;

            OnPropertyChanged(nameof(DisplayValue));
        }

        private void AddToHistory(string text)
        {
            History.Add(text);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
