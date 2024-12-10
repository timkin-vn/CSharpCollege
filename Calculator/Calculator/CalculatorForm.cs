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
        }

        private void CalculatorForm_Load(object sender, EventArgs e)
        {
            DisplayLabel.Text = _state.XRegister.ToString();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            _service.Clear(_state);
            DisplayLabel.Text = _state.XRegister.ToString();
        }

        private void OperationButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var opCode = button.Text;
            _service.PressOperation(_state, opCode);
            DisplayLabel.Text = _state.XRegister.ToString();
        }

        private void SquareRoot_Click(object sender, EventArgs e)
        {
            _service.SquareRoot(_state);
            DisplayLabel.Text = _state.XRegister.ToString();
        }

        private void Percentage_Click(object sender, EventArgs e)
        {
            _service.Percentage(_state);
            DisplayLabel.Text = _state.XRegister.ToString();
        }

    }
}
