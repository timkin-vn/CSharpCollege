using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class CalculatorForm : Form
    {
        private readonly CalculatorState _state = new CalculatorState();

        private readonly CalculatorService _service = new CalculatorService();

        public CalculatorForm()
        {
            InitializeComponent();
        }

        private void DigitButton_Click(object sender, EventArgs e)
        {
            var digit = ((Button)sender).Text;
            _service.PressDigit(_state, digit);
            ShowResult();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            _service.Clear(_state);
            ShowResult();
        }

        private void ShowResult()
        {
            DisplayLabel.Text = _state.RegisterX.ToString();
        }

        private void XToYButton_Click(object sender, EventArgs e)
        {
            _service.MoveXToY(_state);
        }

        private void OperationButton_Click(object sender, EventArgs e)
        {
            _state.bonus = 10.0;
            _state.IsFloat = false;
            var operationCode = ((Button)sender).Text;
            _service.PressOperation(_state, operationCode);
            ShowResult();
        }

        private void EqualButton_Click(object sender, EventArgs e)
        {
            _service.PressEqual(_state);
            ShowResult();
        }

        private void CalculatorForm_Load(object sender, EventArgs e)
        {

        }

        private void FloatButton_Click(object sender, EventArgs e)
        {
            _state.IsFloat = true;
            _state.bonus = 10.0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _state.RegisterY = _state.RegisterY > 0 ? _state.RegisterY : 1;
            _state.RegisterX = _state.RegisterY / 100 * _state.RegisterX;
            ShowResult();
        }
    }
}
