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

namespace Calculator
{
    public partial class CalculatorForm : Form
    {
        private CalculatorModel _calculatorModel = new CalculatorModel();

        private CalculatorService _service = new CalculatorService();
        public CalculatorForm()
        {
            InitializeComponent();
        }

        private void DigitButton_Click(object sender, EventArgs e)
        {
            _service.PressDigit(_calculatorModel, ((Button)sender).Text);
            DisplayValue();
        }

        private void DisplayValue()
        {
            DisplayLabel.Text = _calculatorModel.RegisterX.ToString();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            _service.PressClear(_calculatorModel);
            DisplayValue();
        }

        private void MoveXToYButton_Click(object sender, EventArgs e)
        {
            _service.MoveXtoY(_calculatorModel);
            DisplayValue();
        }

        private void OperationButtonClick(object sender, EventArgs e)
        {
            _service.PressOperation(_calculatorModel, ((Button)sender).Text);
            DisplayValue();
        }

        private void equalButton_Click(object sender, EventArgs e)
        {
            _service.PressEqual(_calculatorModel);
            DisplayValue();
        }
    }
}
