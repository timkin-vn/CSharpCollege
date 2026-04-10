using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.Drawing;
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

            EqualButton.Click -= OperationButton_Click;
            EqualButton.Click += EqualButton_Click;

            KeyPreview = true;
            KeyDown += CalculatorForm_KeyDown;

            AddExtraButtons();
            DisplayResult();
        }

        private void AddExtraButtons()
        {
            var backspaceButton = new Button
            {
                Name = "BackspaceButton",
                Text = "←",
                Font = new Font("Microsoft Sans Serif", 24F),
                Size = new Size(75, 59),
                Location = new Point(93, 313),
            };
            backspaceButton.Click += BackspaceButton_Click;
            Controls.Add(backspaceButton);

            var signButton = new Button
            {
                Name = "SignButton",
                Text = "+/-",
                Font = new Font("Microsoft Sans Serif", 18F),
                Size = new Size(75, 59),
                Location = new Point(174, 313),
            };
            signButton.Click += SignButton_Click;
            Controls.Add(signButton);
        }

        private void DigitButton_Click(object sender, EventArgs e)
        {
            var digitString = ((Button)sender).Text;
            _calculatorService.PressDigit(_calculatorModel, digitString);
            DisplayResult();
        }

        private void DisplayResult()
        {
            DisplayLabel.Text = _calculatorModel.RegisterX.ToString();
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

        private void EqualButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressEqual(_calculatorModel);
            DisplayResult();
        }

        private void BackspaceButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressBackspace(_calculatorModel);
            DisplayResult();
        }

        private void SignButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressToggleSign(_calculatorModel);
            DisplayResult();
        }

        private void CalculatorForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
            {
                _calculatorService.PressDigit(_calculatorModel, (e.KeyCode - Keys.D0).ToString());
            }
            else if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
            {
                _calculatorService.PressDigit(_calculatorModel, (e.KeyCode - Keys.NumPad0).ToString());
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.Add:
                    case Keys.Oemplus:
                        _calculatorService.PressOperation(_calculatorModel, "+");
                        break;

                    case Keys.Subtract:
                    case Keys.OemMinus:
                        _calculatorService.PressOperation(_calculatorModel, "-");
                        break;

                    case Keys.Multiply:
                        _calculatorService.PressOperation(_calculatorModel, "*");
                        break;

                    case Keys.Divide:
                    case Keys.OemQuestion:
                        _calculatorService.PressOperation(_calculatorModel, "/");
                        break;

                    case Keys.Enter:
                        _calculatorService.PressEqual(_calculatorModel);
                        break;

                    case Keys.Back:
                        _calculatorService.PressBackspace(_calculatorModel);
                        break;

                    case Keys.Escape:
                        _calculatorService.PressClear(_calculatorModel);
                        break;
                }
            }

            DisplayResult();
        }
    }
}
