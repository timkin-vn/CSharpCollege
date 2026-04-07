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
        private CalculatorModel _calculatorModel = new CalculatorModel();

        private CalculatorService _calculatorService = new CalculatorService();

        public CalculatorForm()
        {
            InitializeComponent();
            DisplayResult();
        }

        private void DigitButton_Click(object sender, EventArgs e)
        {
            var digitString = ((Button)sender).Text;
            _calculatorService.PressDigit(_calculatorModel, digitString);
            DisplayResult();
        }
        
        private void PointButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressDecimal(_calculatorModel);
            DisplayResult();
        }

        private void DisplayResult()
        {
            DisplayLabel.Text = _calculatorModel.DisplayText;
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressClear(_calculatorModel);
            DisplayResult();
        }

        private void MoveXToYButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressMoveXToY(_calculatorModel);
            DisplayResult();
        }

        private void OperationButton_Click(object sender, EventArgs e)
        {
            var operationCode = ((Button)sender).Text;
            _calculatorService.PressOperation(_calculatorModel, operationCode);
            DisplayResult();
        }

        private void SinButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressSin(_calculatorModel);
            DisplayResult();
        }

        private void CosButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressCos(_calculatorModel);
            DisplayResult();
        }

        private void TanButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressTg(_calculatorModel);
            DisplayResult();
        }

        private void CotButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressCtg(_calculatorModel);
            DisplayResult();
        }
    }
}