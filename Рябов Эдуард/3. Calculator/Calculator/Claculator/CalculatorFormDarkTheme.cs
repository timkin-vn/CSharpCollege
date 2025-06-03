using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Calculator.Business.Models;
using Calculator.Business.Services;

namespace Claculator
{
    public partial class CalculatorFormDarkTheme : Form
    {

        private readonly CalculatorState _state = new CalculatorState();

        private readonly CalculatorService _service = new CalculatorService();

        public CalculatorFormDarkTheme()
        {
            InitializeComponent();
        }



        private void DigitButton_Click(object sender, EventArgs e)
        {
            var digit = ((Button)sender).Text;
            _service.PressDigit(_state, digit);
            EqualButton_Click_1();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            CalculatorForm cf1 = new CalculatorForm();
            cf1.Show();
            Hide();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            _service.Clear(_state);
            EqualButton_Click_1();
        }


        private void OperationButton_Click(object sender, EventArgs e)
        {
            var operation = ((Button)sender).Text;
            _service.PressOperation(_state, operation);
            EqualButton_Click_1();
        }

        private void EqualButton_Click_1()
        {
            DisplayLabel.Text = _state.RegisterX.ToString();
        }

        private void GoToDarkTheme_Click(object sender, EventArgs e)
        {
            _service.Clear(_state);
            EqualButton_Click_1();
        }

        private void XToYButton_Click(object sender, EventArgs e)
        {
            _service.MoveXToY(_state);
        }

        private void Square_Click(object sender, EventArgs e)
        {
            _service.Square(_state);
            ShowResult();
        }

        private void SquareRoot_Click(object sender, EventArgs e)
        {
            _service.SquareRoot(_state);
            ShowResult();
        }


        private void Inverse_Click(object sender, EventArgs e)
        {
            _service.Inverse(_state);
            ShowResult();
        }

        private void ShowResult()
        {
            DisplayLabel.Text = _state.RegisterX.ToString();
        }
    }
}
