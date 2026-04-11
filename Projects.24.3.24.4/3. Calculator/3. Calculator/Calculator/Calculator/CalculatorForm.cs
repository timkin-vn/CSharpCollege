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

            ClientSize = new Size(463, 455);
            AddExtraButtons();
            DisplayResult();
        }

        private void AddExtraButtons()
        {
            var decimalButton = new Button
            {
                Text = ".",
                Font = new Font("Microsoft Sans Serif", 27.75F),
                Size = new Size(75, 59),
                Location = new Point(93, 313),
            };
            decimalButton.Click += DecimalButton_Click;
            Controls.Add(decimalButton);

            var squareButton = new Button
            {
                Text = "x²",
                Font = new Font("Microsoft Sans Serif", 20F),
                Size = new Size(75, 59),
                Location = new Point(12, 378),
            };
            squareButton.Click += SquareButton_Click;
            Controls.Add(squareButton);

            var squareRootButton = new Button
            {
                Text = "√",
                Font = new Font("Microsoft Sans Serif", 24F),
                Size = new Size(75, 59),
                Location = new Point(93, 378),
            };
            squareRootButton.Click += SquareRootButton_Click;
            Controls.Add(squareRootButton);
        }

        private void DigitButton_Click(object sender, EventArgs e)
        {
            var digitString = ((Button)sender).Text;
            _calculatorService.PressDigit(_calculatorModel, digitString);
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

        private void EqualButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressEqual(_calculatorModel);
            DisplayResult();
        }

        private void DecimalButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressDecimalSeparator(_calculatorModel);
            DisplayResult();
        }

        private void PercentButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressPercent(_calculatorModel);
            DisplayResult();
        }

        private void SquareButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressSquare(_calculatorModel);
            DisplayResult();
        }

        private void SquareRootButton_Click(object sender, EventArgs e)
        {
            _calculatorService.PressSquareRoot(_calculatorModel);
            DisplayResult();
        }
    }
}
