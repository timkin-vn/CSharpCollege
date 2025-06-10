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

namespace Claculator
{
    public partial class CalculatorForm : Form
    {
        private readonly CalculatorState _state = new CalculatorState();

        private readonly CalculatorService _service = new CalculatorService();

        private System.Windows.Forms.Button RoundDownButton;
        private System.Windows.Forms.Button RoundUpButton;

        private int _currentDecimals = 1; 
        public CalculatorForm()
        {
            InitializeComponent();

            this.RoundDownButton = new System.Windows.Forms.Button();
            this.RoundDownButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.RoundDownButton.Location = new System.Drawing.Point(255, 324);
            this.RoundDownButton.Name = "RoundDownButton";
            this.RoundDownButton.Size = new System.Drawing.Size(35, 71);
            this.RoundDownButton.TabIndex = 21;
            this.RoundDownButton.Text = "↓";
            this.RoundDownButton.UseVisualStyleBackColor = true;
            this.RoundDownButton.Click += new System.EventHandler(this.RoundDownButton_Click);
            // 
            // RoundUpButton
            // 
            this.RoundUpButton = new System.Windows.Forms.Button();
            this.RoundUpButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.RoundUpButton.Location = new System.Drawing.Point(295, 324);
            this.RoundUpButton.Name = "RoundUpButton";
            this.RoundUpButton.Size = new System.Drawing.Size(35, 71);
            this.RoundUpButton.TabIndex = 22;
            this.RoundUpButton.Text = "↑";
            this.RoundUpButton.UseVisualStyleBackColor = true;
            this.RoundUpButton.Click += new System.EventHandler(this.RoundUpButton_Click);

            this.Controls.Add(this.RoundDownButton);
            this.Controls.Add(this.RoundUpButton);
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

        private void OperationButton_Click(object sender, EventArgs e)
        {
            var operation = ((Button)sender).Text;
            _service.PressOperation(_state, operation);
            ShowResult();
        }

        private void EqualButton_Click(object sender, EventArgs e)
        {
            _service.PressEqual(_state);
            ShowResult();
        }

        private void XToYButton_Click(object sender, EventArgs e)
        {
            _service.MoveXToY(_state);
        }

        private void ShowResult()
        {
            DisplayLabel.Text = _state.RegisterX.ToString();
        }

        private void ShowResult(int decimals = -1)
        {
            if (decimals >= 0)
                DisplayLabel.Text = _state.RegisterX.ToString("F" + decimals);
            else
                DisplayLabel.Text = _state.RegisterX.ToString();
        }

        private void CalculatorForm_Load(object sender, EventArgs e)
        {

        }

        private void SquareRoot_Click(object sender, EventArgs e)
        {
            _service.SquareRoot(_state);
            ShowResult();
        }

        private void PressPi_Click(object sender, EventArgs e)
        {
            _service.PressPi(_state);
            ShowResult();
        }

        private void DecimalPoint_Click(object sender, EventArgs e)
        {
            _service.DecimalPoint(_state);
            ShowResult();
        }

        private void RoundDownButton_Click(object sender, EventArgs e)
        {
            if (_currentDecimals > 0)
                _currentDecimals--;
            ShowRoundedResult();
        }

        private void RoundUpButton_Click(object sender, EventArgs e)
        {
            if (_currentDecimals < 3)
                _currentDecimals++;
            ShowRoundedResult();
        }

        private void ShowRoundedResult()
        {
            double value = Math.Round(_state.RegisterX, _currentDecimals, MidpointRounding.AwayFromZero);
            DisplayLabel.Text = value.ToString("F" + _currentDecimals);
        }

        public void RoundDownToOneDecimal(CalculatorState state)
        {
            state.RegisterX = Math.Round(state.RegisterX, 1, MidpointRounding.AwayFromZero);
        }

        public void RoundUpToTwoDecimals(CalculatorState state)
        {
            state.RegisterX = Math.Round(state.RegisterX, 2, MidpointRounding.AwayFromZero);
        }

        public void RoundToInteger(CalculatorState state)
        {
            state.RegisterX = Math.Round(state.RegisterX, 0, MidpointRounding.AwayFromZero);
        }

        public void RoundToTenths(CalculatorState state)
        {
            state.RegisterX = Math.Round(state.RegisterX, 1, MidpointRounding.AwayFromZero);
        }

        public void RoundToHundredths(CalculatorState state)
        {
            state.RegisterX = Math.Round(state.RegisterX, 2, MidpointRounding.AwayFromZero);
        }

        public void RoundToThousandths(CalculatorState state)
        {
            state.RegisterX = Math.Round(state.RegisterX, 3, MidpointRounding.AwayFromZero);
        }
    }
}

