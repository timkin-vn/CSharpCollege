
using Calculator.Business.Models;
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

       
        public void InsertOperation(string opCode)
        {
            _service.PressOperation(_state, opCode);

            string operation = _state.OperationLog; 
            double result = _state.XRegister;

            OutputDisplay = result.ToString();
            OnPropertyChanged(nameof(OutputDisplay));

            
            if (!string.IsNullOrEmpty(operation))
            {
                OperationLog.Add($"{operation} = {result}"); 
            }
        }

        
        public void ClearLog()
        {
            OperationLog.Clear();
        }

        public void OperationEqualFromView_Click(object sender, EventArgs e)
        {
            string currentOperation = _state.OperationLog; 

            
            _service.PressOperation(_state, "=");

            
            double result = _state.ZRegister;

            
            string operationWithResult = $"{currentOperation} = {result}";
            OperationLog.Add(operationWithResult);  

            
            OutputDisplay = result.ToString();
            OnPropertyChanged(nameof(OutputDisplay));

            
            InsertOperation(operationWithResult);
        }

        public void HistoryListBox_SelectedIndexChanged(string selectedItem)
        {
            if (string.IsNullOrEmpty(selectedItem))
                return;

            
            var parts = selectedItem.Split('=');
            if (parts.Length == 2)
            {
                string operation = parts[0].Trim();  
                string resultString = parts[1].Trim();  

                
                if (double.TryParse(resultString, out double result))
                {
                    
                    _state.XRegister = result;
                    OutputDisplay = $"{operation} = {result}";  
                    OnPropertyChanged(nameof(OutputDisplay));  
                }
                else
                {
                    
                    OutputDisplay = "Ошибка";
                    OnPropertyChanged(nameof(OutputDisplay));
                }
            }
        }


        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
