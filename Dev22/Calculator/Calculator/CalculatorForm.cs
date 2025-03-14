using Calculator.Business.Entites;
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
        private CalculatorService _service = new CalculatorService();

        private CalculatorState _state = new CalculatorState();

        public CalculatorForm()
        {
            InitializeComponent();
        }

        private void DigitButton_Click(object sender, EventArgs e)
        {
            var senderButton = (Button)sender;
            var digitText = senderButton.Text;

            _service.InsertDigit(_state, digitText);
            DisplayLabel.Text = _state.XRegister.ToString();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            _service.Clear(_state);
            DisplayLabel.Text = _state.XRegister.ToString();
        }

        private void OperationButton_Click(object sender, EventArgs e)
        {
            var senderButton = (Button)sender;
            var opCode = senderButton.Text;

            _service.InsertOperation(_state, opCode);
            DisplayLabel.Text = _state.XRegister.ToString();
        }

        private void SquareButton_Click(object sender, EventArgs e)
        {
            _service.Square(_state);
            DisplayLabel.Text = _state.XRegister.ToString();
        }

        private void ExponentButton_Click(object sender, EventArgs e)
        {
            _service.PrepareForExponentiation(_state);
            DisplayLabel.Text = _state.XRegister.ToString();
        }

        private void SquareRootButton_Click(object sender, EventArgs e)
        {
            _service.SquareRoot(_state);
            DisplayLabel.Text = _state.XRegister.ToString();
        }

        private void BackspaceButton_Click(object sender, EventArgs e)
        {
            _service.Backspace(_state);
            DisplayLabel.Text = _state.XRegister.ToString();
        }

        private void DecimalButton_Click(object sender, EventArgs e)
        {
            _service.InsertDecimal(_state);
            DisplayLabel.Text = _state.XRegister.ToString();
        }
    }
}
