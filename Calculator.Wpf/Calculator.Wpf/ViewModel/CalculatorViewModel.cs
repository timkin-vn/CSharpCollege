using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Calculator.Wpf.ViewModel
{
    internal class CalculatorViewModel : INotifyPropertyChanged
    {
        private CalculatorService _service = new CalculatorService();

        private CalculatorState _state = new CalculatorState();

        public string DisplayOutput { get; set; } = "0"; 
        public string OperationLogText { get; set; } = "0";

        public ObservableCollection<string> OperationLog { get; set; } = new ObservableCollection<string>();

        public event PropertyChangedEventHandler PropertyChanged;

        public void InsertDigit(string digit)
        {
            _service.InsertDigit(_state, digit);
            DisplayResult();
        } 
        public void InsertDecimalPoint(string digit)
        {
            _service.InsertDecimalPoint(_state);
            DisplayResult();
        }

        public void Clear()
        {
            _service.Clear(_state);
            DisplayResult();
        }  
        
        public void RowOperation(string opCode)
        {

            _service.RowOperation(_state, opCode);
            DisplayResult();
            ShowList(opCode, _state);
            OperationLogTextyResult(); ;

        }
        public void PowOperation(string opCode)
        {

            _service.PowOperationOperation(_state, opCode);
            DisplayResult();
            ShowList(opCode, _state);
            OperationLogTextyResult();

        } 
        public void Fractional_DivisioOperation(string opCode)
        {

            _service.Fractional_DivisionOperation(_state, opCode);
            DisplayResult();
            ShowList(opCode, _state);
            OperationLogTextyResult();

        }
        public void Neg_Poss_Operation(string opCode)
        {

            _service.Negativ_and_Positiv_Operation(_state, opCode);
            DisplayResult();
            ShowList(opCode, _state);
            OperationLogTextyResult();

        }
        public void LnOperation(string opCode)
        {

            _service.LnOperation(_state, opCode);
            DisplayResult();
          
            ShowList(opCode, _state);
            OperationLogTextyResult();

        }

        public void BackspaceOperation()
        {
            _service.Backspace(_state);
            DisplayResult();
            OperationLogTextyResult();
        }

        public void InsertOperation(string opCode)
        {
            _service.PressOperation(_state, opCode);
            DisplayResult();
           

            if (!string.IsNullOrEmpty(_state.OperationLog))
            {
                OperationLog.Add(_state.OperationLog);
            }
            OperationLogTextyResult();
        }

        public void ClearOperationLog()
        {
            OperationLog.Clear();
        } 
        public void ClearEmOperationLog()
        {
           
            _service.ClearEm (_state);
            OperationLogTextyResult();
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
        private void OperationLogTextyResult()
        {
            OperationLogText = _state.OperationLog;
            OnPropertyChanged(nameof(OperationLogText));
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
