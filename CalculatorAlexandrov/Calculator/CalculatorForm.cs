using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class CalculatorForm : Form
    {
        private CalculatorState _state = new CalculatorState();
        private CalculatorService _service = new CalculatorService();

        public CalculatorForm()
        {
            InitializeComponent();
        }

        private void DigitButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var digit = button.Text;
            _service.InsertDigit(_state, digit);
            DisplayLabel.Text = _state.XRegister.ToString();
            label1.Text += digit.ToString();
            AddToHistory(digit.ToString());
        }

        private void CalculatorForm_Load(object sender, EventArgs e)
        {
            DisplayLabel.Text = _state.XRegister.ToString();
            label1.Text = "0";

        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            _service.Clear(_state);
            DisplayLabel.Text = _state.XRegister.ToString();
            label1.Text = "0";
            
            AddToHistory("0");
        }

        private void OperationButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var opCode = button.Text;
            _service.PressOperation(_state, opCode);
            DisplayLabel.Text = _state.ZRegister.ToString();
            label1.Text += opCode.ToString();

            
            AddToHistory(opCode);
        }

        private void OperationEqual_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var opCode = button.Text;
            _service.PressOperation(_state, opCode);
            DisplayLabel.Text = _state.ZRegister.ToString();
            label1.Text += opCode.ToString();

            label1.Text += _state.ZRegister.ToString();

            
            AddToHistory(_state.ZRegister.ToString());
        }

        private void AddToHistory(string operation)
        {
            historyListBox.Items.Add(operation); 
        }

        
        private void HistoryListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            var selectedItem = historyListBox.SelectedItem.ToString();

            if (double.TryParse(selectedItem, out double value))
            {
                _state.XRegister = value;
                DisplayLabel.Text = _state.XRegister.ToString();
            }
            else
            {
                
                _service.PressOperation(_state, selectedItem);
                DisplayLabel.Text = _state.ZRegister.ToString();
            }
        }
    }
}
