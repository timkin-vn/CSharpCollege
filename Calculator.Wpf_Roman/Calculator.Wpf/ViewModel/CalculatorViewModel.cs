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

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void DisplayResult()
        {
            DisplayOutput = _state.XRegister.ToString();
            OnPropertyChanged(nameof(DisplayOutput));
        }
        public void DegOperation(string opCode)
        {

            _service.DegretOperation(_state, opCode);
            DisplayResult();
            ShowList(opCode, _state);

        }
        public void RowOperation(string opCode)
        {

            _service.RowOperation(_state, opCode);
            DisplayResult();
            ShowList(opCode, _state);

        }
        public void Neg_Poss_Operation(string opCode)
        {

            _service.Negativ_and_Positiv_Operation(_state, opCode);
            DisplayResult();
            ShowList(opCode, _state);

        }
        
        public void Log2Operation(string opCode)
        {

            _service.LogOperation(_state, opCode);
            DisplayResult();
            ShowList(opCode, _state);

        }
        private void ShowList(string list, CalculatorState state)
        {
            if (!string.IsNullOrEmpty(_state.OperationLog))
            {
                OperationLog.Add(_state.OperationLog);
            }
        }
    }
}
