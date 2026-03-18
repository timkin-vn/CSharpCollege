using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.Globalization;
using System.Windows.Forms;

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

        private void CommaButton_Click(object sender, EventArgs e)
        {
            string separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            _service.PressDecimal(_calculatorModel, separator);
            DisplayValue();
        }

        private void SqrtButton_Click(object sender, EventArgs e)
        {
            _service.PressSqrt(_calculatorModel);
            DisplayValue();
        }

        private void SqrButton_Click(object sender, EventArgs e)
        {
            _service.PressSqr(_calculatorModel);
            DisplayValue();
        }

        private void DisplayValue()
        {
            DisplayLabel.Text = _calculatorModel.RegisterX.ToString(CultureInfo.CurrentCulture);
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            _service.PressClear(_calculatorModel);
            DisplayValue();
        }

        private void MoveXToYButton_Click(object sender, EventArgs e)
        {
            _service.MoveXToY(_calculatorModel);
            DisplayValue();
        }

        private void OperationButton_Click(object sender, EventArgs e)
        {
            _service.PressOperation(_calculatorModel, ((Button)sender).Text);
            DisplayValue();
        }

        private void EqualButton_Click(object sender, EventArgs e)
        {
            _service.PressEqual(_calculatorModel);
            DisplayValue();
        }

        private void CalculatorForm_Load(object sender, EventArgs e) { }
        private void DisplayLabel_Click(object sender, EventArgs e) { }
    }
}