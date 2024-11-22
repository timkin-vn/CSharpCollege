using Calculator.Business.Entites;
using Calculator.Business.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Wpf.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private CalculatorService _service = new CalculatorService();

        private CalculatorState _state = new CalculatorState();

        public string OutputDisplay { get; set; } = "0";

        public ObservableCollection<string> OperationLog { get; set; } = new ObservableCollection<string>();

        public event PropertyChangedEventHandler PropertyChanged;

        public void InsertDigit(string digit)
        {
            _service.InsertDigit(_state, digit);
            OutputDisplay = _state.XRegister.ToString();
            OnPropertyChanged(nameof(OutputDisplay));
        }

        public void Clear()
        {
            _service.Clear(_state);
            OutputDisplay = _state.XRegister.ToString();
            OnPropertyChanged(nameof(OutputDisplay));
        }

        public void PressOperation(string opCode)
        {
            _service.InsertOperation(_state, opCode);
            OutputDisplay = _state.XRegister.ToString();
            OnPropertyChanged(nameof(OutputDisplay));
            if (!string.IsNullOrEmpty(_state.OperationLog))
            {
                OperationLog.Add(_state.OperationLog);
            }
        }

        public void ClearLog()
        {
            OperationLog.Clear();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
