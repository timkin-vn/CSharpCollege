using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Wpf.ViewModel
{
    internal class CalculatorViewModel : INotifyPropertyChanged
    {
        private CalculatorService _service = new CalculatorService();

        private CalculatorState _state = new CalculatorState();

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
            _service.PressOperation(_state, opCode);
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

        public void CalculateSquareRoot()
        {
            _state.XRegister = Math.Sqrt(_state.XRegister); // Assuming XRegister holds the current value
            DisplayResult();
        }

        public void CalculatePercentage()
        {
            _state.XRegister = _state.XRegister / 100; // Calculate percentage
            DisplayResult();
        }

        public void MemoryAdd()
        {
            _state.Memory += _state.XRegister; // Assuming Memory is a property in CalculatorState
            DisplayResult();
        }

        public void MemorySubtract()
        {
            _state.Memory -= _state.XRegister;
            DisplayResult();
        }

        public void MemoryRecall()
        {
            _state.XRegister = _state.Memory; // Recall the stored memory value
            DisplayResult();
        }

        public void MemoryClear()
        {
            _state.Memory = 0; // Clear memory
            DisplayResult();
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
