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
        private Business.Models.CalculatorContext _state = new Business.Models.CalculatorContext();

        private Business.Services.CalculatorLogic _service = new Business.Services.CalculatorLogic();

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

       

        private void SinButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var opCode = button.Text;
            _service.Sin_Operation(_state);
            DisplayLabel.Text = _state.XRegister.ToString();
        }

        private void CosButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var opCode = button.Text;
            _service.Cos_Operation(_state);
            DisplayLabel.Text = _state.XRegister.ToString();
        }

        private void Neg_PostButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var opCode = button.Text;
            _service.Negativ_and_Positiv_Operation(_state);
            DisplayLabel.Text = _state.XRegister.ToString();
        }
    }
}
