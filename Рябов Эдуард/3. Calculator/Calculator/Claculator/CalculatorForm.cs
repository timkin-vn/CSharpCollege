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

        private void DarkTheme(object sender, EventArgs e)
        {
            CalculatorFormDarkTheme cf2 = new CalculatorFormDarkTheme();
            cf2.Show();
            Hide();
        }

        private void Sqrt(object sender, EventArgs e)
        {
            double dn, res;


            dn = Convert.ToDouble(DisplayLabel.Text);
            res = Math.Sqrt(dn);
            DisplayLabel.Text = res.ToString();
        }

        private void inPow(object sender, EventArgs e)
        {
            double dn, res;


            dn = Convert.ToDouble(DisplayLabel.Text);
            res = Math.Pow(dn,2);
            DisplayLabel.Text = res.ToString();
        }

        private void oneToNum(object sender, EventArgs e)
        {
            double dn, res;


            dn = Convert.ToDouble(DisplayLabel.Text);
            res = 1 / dn;
            DisplayLabel.Text = res.ToString();
        }

        
    }
}
