
using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Calculator
{
    public partial class CalculatorForm : Form
    {
        private readonly CalculatorModel _calculatorModel =
            new CalculatorModel();

        private readonly CalculatorService _service =
            new CalculatorService();

        private Label HistoryLabel;
        private ListBox HistoryListBox;
        private Button ClearHistoryButton;

        public CalculatorForm()
        {
            InitializeComponent();

            EqualButton.Click -= OperationButton_Click;
            EqualButton.Click += EqualButton_Click;

            InitializeHistoryControls();

            ClientSize = new Size(900, 422);
        }

        private void InitializeHistoryControls()
        {
            HistoryLabel = new Label
            {
                Name = "HistoryLabel",
                Text = "История вычислений",
                Location = new Point(660, 15),
                Size = new Size(220, 30),
                Font = new Font(
                    "Microsoft Sans Serif",
                    12F,
                    FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };

            HistoryListBox = new ListBox
            {
                Name = "HistoryListBox",
                Location = new Point(660, 50),
                Size = new Size(220, 300),
                HorizontalScrollbar = true,
                IntegralHeight = false,
                Font = new Font(
                    "Microsoft Sans Serif",
                    10F,
                    FontStyle.Regular)
            };

            ClearHistoryButton = new Button
            {
                Name = "ClearHistoryButton",
                Text = "Очистить историю",
                Location = new Point(660, 360),
                Size = new Size(220, 40)
            };

            ClearHistoryButton.Click += ClearHistoryButton_Click;

            Controls.Add(HistoryLabel);
            Controls.Add(HistoryListBox);
            Controls.Add(ClearHistoryButton);

            HistoryLabel.BringToFront();
            HistoryListBox.BringToFront();
            ClearHistoryButton.BringToFront();
        }

        private void DigitButton_Click(object sender, EventArgs e)
        {
            _service.PressDigit(
                _calculatorModel,
                ((Button)sender).Text);

            DisplayValue();
        }

        private void DisplayValue()
        {
            DisplayLabel.Text = _calculatorModel.RegisterX.ToString(
                "G",
                System.Globalization.CultureInfo.CurrentCulture);
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
            try
            {
                _service.PressOperation(
                    _calculatorModel,
                    ((Button)sender).Text);

                DisplayValue();
                RefreshHistory();
            }
            catch (DivideByZeroException ex)
            {
                ShowCalculationError(ex.Message);
            }
            catch (ArithmeticException ex)
            {
                ShowCalculationError(ex.Message);
            }
        }

        private void EqualButton_Click(object sender, EventArgs e)
        {
            try
            {
                _service.PressEqual(_calculatorModel);

                DisplayValue();
                RefreshHistory();
            }
            catch (DivideByZeroException ex)
            {
                ShowCalculationError(ex.Message);
            }
            catch (ArithmeticException ex)
            {
                ShowCalculationError(ex.Message);
            }
        }

        private void RefreshHistory()
        {
            HistoryListBox.BeginUpdate();

            try
            {
                HistoryListBox.Items.Clear();

                foreach (string entry in _service.History)
                {
                    HistoryListBox.Items.Add(entry);
                }
            }
            finally
            {
                HistoryListBox.EndUpdate();
            }
        }

        private void ClearHistoryButton_Click(
            object sender,
            EventArgs e)
        {
            _service.ClearHistory();
            RefreshHistory();
        }

        private void ShowCalculationError(string message)
        {
            MessageBox.Show(
                message,
                "Ошибка вычисления",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }
}
