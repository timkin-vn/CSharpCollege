﻿using Calculator.Business.Models;
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
using static System.Windows.Forms.AxHost;

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
            var operationCode = ((Button)sender).Text;
            _service.PressOperation(_state, operationCode);
            ShowResult();
        }

        private void EqualButton_Click(object sender, EventArgs e)
        {
            _service.PressEqual(_state);
            ShowResult();
        }

        private void SignToggleButton_Click(object sender, EventArgs e)
        {
            _service.ToggleSign(_state);
            DisplayLabel.Text = _state.RegisterX.ToString();
        }

        private void BackspaceButton_Click(object sender, EventArgs e)
        {
            _service.Backspace(_state);
            DisplayLabel.Text = _state.RegisterX.ToString();
        }


        private void SqrtButton_Click(object sender, EventArgs e)
        {
            _service.PressSqrt(_state);
            DisplayLabel.Text = _state.RegisterX.ToString();
        }

        private void PowerButton_Click(object sender, EventArgs e)
        {
            _service.PressOperation(_state, "^");
            ShowResult();
        }
    }

}
