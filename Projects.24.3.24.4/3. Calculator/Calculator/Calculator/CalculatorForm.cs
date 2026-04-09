using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;

namespace Calculator
{
    public partial class CalculatorForm : Form
    {
        private readonly CalculatorModel _calculatorModel = new CalculatorModel();
        private readonly CalculatorService _calculatorService = new CalculatorService();

        public CalculatorForm()
        {
            InitializeComponent();
            EnableBusinessOptions();
            ConfigureUiFromCulture();
            KeyPreview = true;
            KeyDown += CalculatorForm_KeyDown;
            DisplayResult();
        }

        private void EnableBusinessOptions()
        {
            SetInternalBooleanProperty("IsRepeatedEqualEnabled", true);
            SetInternalBooleanProperty("IsDivisionByZeroHandled", true);
        }

        private void SetInternalBooleanProperty(string propertyName, bool value)
        {
            var property = typeof(CalculatorModel).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (property == null)
            {
                return;
            }

            var setter = property.GetSetMethod(true);
            if (setter == null)
            {
                return;
            }

            setter.Invoke(_calculatorModel, new object[] { value });
        }

        private void ConfigureUiFromCulture()
        {
            DecimalSeparatorButton.Text = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        }

        private void DigitButton_Click(object sender, EventArgs e)
        {
            var digitString = ((Button)sender).Text;
            _calculatorService.PressDigit(_calculatorModel, digitString);
            DisplayResult();
        }

        private void OperationButton_Click(object sender, EventArgs e)
        {
            var operationCode = ((Button)sender).Text;
            _calculatorService.PressOperation(_calculatorModel, operationCode);
            DisplayResult();
        }

        private void EqualButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressEqual(_calculatorModel);
            DisplayResult();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressClear(_calculatorModel);
            DisplayResult();
        }

        private void DecimalSeparatorButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressDecimalSeparator(_calculatorModel);
            DisplayResult();
        }

        private void BackspaceButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressBackspace(_calculatorModel);
            DisplayResult();
        }

        private void ToggleSignButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressToggleSign(_calculatorModel);
            DisplayResult();
        }

        private void PercentButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressPercent(_calculatorModel);
            DisplayResult();
        }

        private void SquareRootButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressSquareRoot(_calculatorModel);
            DisplayResult();
        }

        private void CalculatorForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
            {
                _calculatorService.PressDigit(_calculatorModel, ((char)('0' + (e.KeyCode - Keys.D0))).ToString());
            }
            else if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
            {
                _calculatorService.PressDigit(_calculatorModel, ((char)('0' + (e.KeyCode - Keys.NumPad0))).ToString());
            }
            else if (e.KeyCode == Keys.Add || (e.KeyCode == Keys.Oemplus && e.Shift))
            {
                _calculatorService.PressOperation(_calculatorModel, "+");
            }
            else if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus)
            {
                _calculatorService.PressOperation(_calculatorModel, "-");
            }
            else if (e.KeyCode == Keys.Multiply)
            {
                _calculatorService.PressOperation(_calculatorModel, "*");
            }
            else if (e.KeyCode == Keys.Divide || e.KeyCode == Keys.OemQuestion)
            {
                _calculatorService.PressOperation(_calculatorModel, "/");
            }
            else if (e.KeyCode == Keys.Enter)
            {
                _calculatorService.PressEqual(_calculatorModel);
            }
            else if (e.KeyCode == Keys.Back)
            {
                _calculatorService.PressBackspace(_calculatorModel);
            }
            else if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Delete)
            {
                _calculatorService.PressClear(_calculatorModel);
            }
            else if (e.KeyCode == Keys.Decimal || e.KeyCode == Keys.Oemcomma || e.KeyCode == Keys.OemPeriod)
            {
                _calculatorService.PressDecimalSeparator(_calculatorModel);
            }
            else
            {
                return;
            }

            DisplayResult();
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void DisplayResult()
        {
            DisplayLabel.Text = _calculatorModel.DisplayText;
        }
    }
}
