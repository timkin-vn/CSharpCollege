using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Calculator.Wpf.ViewModel
{
    internal class CalculatorViewModel : INotifyPropertyChanged
    {
        private readonly CalculatorService _service = new CalculatorService();
        private readonly CalculatorState _state = new CalculatorState();

        public string DisplayOutput { get; set; } = "0";
        public ObservableCollection<string> OperationLog { get; set; } = new ObservableCollection<string>();

        public event PropertyChangedEventHandler PropertyChanged;

        public void InsertDigit(string digit)
        {
            _service.InsertDigit(_state, digit);
            DisplayResult();
        }

        public void Clear()
        {
            _service.Clear(_state);
            DisplayResult();
        }

        public void InsertOperation(string opCode)
        {
            _service.InsertOperation(_state, opCode);
            DisplayResult();

            if (!string.IsNullOrEmpty(_state.OperationLog))
            {
                OperationLog.Add(_state.OperationLog);
            }
        }

        public void ClearOperationLog()
        {
            OperationLog.Clear();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void DisplayResult()
        {
            DisplayOutput = _state.XRegister.ToString();
            OnPropertyChanged(nameof(DisplayOutput));
        }
    }
}