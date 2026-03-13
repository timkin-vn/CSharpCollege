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
        private void Digit0Button_Click(object sender, EventArgs e) // для рабочего 0
        {
            DigitButton_Click(sender, e);
        }

        private void DisplayValue()
        {
            DisplayLabel.Text = _calculatorModel.RegisterX.ToString();
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
        // Для кнопки корня
        private void RootButton_Click(object sender, EventArgs e)
        {
            _service.PressSqrt(_calculatorModel);
            DisplayValue();
        }

        // Для кнопки возведения в степень
        private void PowButton_Click(object sender, EventArgs e)
        {
            _service.PressOperation(_calculatorModel, "^");
            DisplayValue();
        }
    }
    }
