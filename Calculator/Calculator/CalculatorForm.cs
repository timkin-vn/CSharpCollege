using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
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
            _state.ErrorMessage = null;
            ShowResult();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            _service.Clear(_state);
            ShowResult();
        }

        private void ShowResult()
        {
            if (!string.IsNullOrEmpty(_state.ErrorMessage))
            {
                DisplayLabel.Text = _state.ErrorMessage;
            }
            else
            {
                DisplayLabel.Text = _state.RegisterX.ToString(CultureInfo.InvariantCulture);
            }
        }
        private void XToYButton_Click(object sender, EventArgs e)
        {
            _service.MoveXToY(_state);
        }

        private void OperationButton_Click(object sender, EventArgs e)
        {
            var operationCode = ((Button)sender).Text;
            _service.PressOperation(_state, operationCode);
            ShowResult();
        }

        private void EqualButton_Click(object sender, EventArgs e)
        {
            _service.PressEqual(_state);
            ShowResult();
        }

        private void sinusButton_Click(object sender, EventArgs e)
        {
            _service.PressSin(_state);
            ShowResult();
        }

        private void logarifmButton_Click(object sender, EventArgs e)
        {
            _service.PressLog(_state);
            ShowResult();
        }

        private void cosinusButton_Click(object sender, EventArgs e)
        {
            _service.PressCos(_state);
            ShowResult();
        }

        private void tangensButton_Click(object sender, EventArgs e)
        {
            _service.PressTan(_state);
            ShowResult();
        }

        private void degreeButton_Click(object sender, EventArgs e)
        {
            _service.PressDegree(_state);
            ShowResult();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            _service.PressUpdateSign(_state);
            ShowResult();
        }

        private void percentButton_Click(object sender, EventArgs e)
        {
            _service.PressPercent(_state);
            ShowResult();
        }
    }
}
