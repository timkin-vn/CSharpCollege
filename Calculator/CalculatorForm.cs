using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Calculator
{
    public partial class CalculatorForm : Form
    {
        private readonly CalculatorState _state = new CalculatorState();
        private readonly CalculatorService _service = new CalculatorService();

        public CalculatorForm()
        {
            InitializeComponent();
            InitializeButtons();
            UpdateDisplay();
        }

        private void InitializeButtons()
        {
            // Основные цифры
            CreateDigitButtons();

            // Основные операции
            CreateButton("+", 150, 130, OperationButton_Click);
            CreateButton("-", 150, 180, OperationButton_Click);
            CreateButton("*", 150, 230, OperationButton_Click);
            CreateButton("/", 150, 280, OperationButton_Click);
            CreateButton("=", 200, 280, EqualButton_Click);
            CreateButton("C", 150, 80, ClearButton_Click);

            // Научные функции
            CreateButton("sin", 250, 80, ScientificButton_Click);
            CreateButton("cos", 300, 80, ScientificButton_Click);
            CreateButton("tan", 350, 80, ScientificButton_Click);
            CreateButton("√", 250, 130, ScientificButton_Click);
            CreateButton("x²", 300, 130, ScientificButton_Click);
            CreateButton("x^y", 350, 130, OperationButton_Click);
            CreateButton("π", 250, 180, ScientificButton_Click);
            CreateButton("e", 300, 180, ScientificButton_Click);
            CreateButton("n!", 350, 180, ScientificButton_Click);
            CreateButton("mod", 250, 230, OperationButton_Click);
            CreateButton("±", 300, 230, ScientificButton_Click);
            CreateButton("%", 350, 230, ScientificButton_Click);

            // Память
            CreateButton("M+", 250, 280, MemoryButton_Click);
            CreateButton("M-", 300, 280, MemoryButton_Click);
            CreateButton("MR", 350, 280, MemoryButton_Click);
            CreateButton("MC", 400, 280, MemoryButton_Click);

            // Режимы
            CreateButton("DEG", 400, 80, ModeButton_Click);
            CreateButton("RAD", 400, 130, ModeButton_Click);
        }

        private void CreateDigitButtons()
        {
            // Кнопки 0-9
            for (int i = 0; i < 10; i++)
            {
                int row = 3 - (i - 1) / 3;
                int col = (i - 1) % 3;

                if (i == 0)
                {
                    row = 4;
                    col = 0;
                }

                var btn = new Button
                {
                    Text = i.ToString(),
                    Size = new Size(50, 50),
                    Location = new Point(50 * col, 30 + 50 * row),
                    Tag = i.ToString()
                };

                btn.Click += DigitButton_Click;
                this.Controls.Add(btn);
            }
        }

        private void CreateButton(string text, int x, int y, EventHandler handler)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(50, 50),
                Location = new Point(x, y),
                Tag = text
            };

            btn.Click += handler;
            this.Controls.Add(btn);
        }

        private void DigitButton_Click(object sender, EventArgs e)
        {
            var digit = ((Button)sender).Tag.ToString();
            _service.PressDigit(_state, digit);
            UpdateDisplay();
        }

        private void OperationButton_Click(object sender, EventArgs e)
        {
            var operation = ((Button)sender).Tag.ToString();
            _service.PressOperation(_state, operation);
            UpdateDisplay();
        }

        private void EqualButton_Click(object sender, EventArgs e)
        {
            _service.PressEqual(_state);
            UpdateDisplay();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            _service.Clear(_state);
            UpdateDisplay();
        }

        private void ScientificButton_Click(object sender, EventArgs e)
        {
            var function = ((Button)sender).Tag.ToString();

            try
            {
                switch (function)
                {
                    case "sin":
                        _service.Sin(_state);
                        break;
                    case "cos":
                        _service.Cos(_state);
                        break;
                    case "tan":
                        _service.Tan(_state);
                        break;
                    case "√":
                        _service.SquareRoot(_state);
                        break;
                    case "x²":
                        _service.PowerOfTwo(_state);
                        break;
                    case "n!":
                        _service.Factorial(_state);
                        break;
                    case "π":
                        _service.SetPi(_state);
                        break;
                    case "e":
                        _service.SetE(_state);
                        break;
                    case "±":
                        _service.ChangeSign(_state);
                        break;
                    case "%":
                        _service.Percent(_state);
                        break;
                }

                UpdateDisplay();
            }
            catch (Exception ex)
            {
                _state.HasError = true;
                _state.LastError = ex.Message;
                UpdateDisplay();
            }
        }

        private void MemoryButton_Click(object sender, EventArgs e)
        {
            var operation = ((Button)sender).Tag.ToString();

            switch (operation)
            {
                case "M+":
                    _service.MemoryAdd(_state);
                    break;
                case "M-":
                    _service.MemorySubtract(_state);
                    break;
                case "MR":
                    _service.MemoryRecall(_state);
                    break;
                case "MC":
                    _service.MemoryClear(_state);
                    break;
            }

            UpdateDisplay();
        }

        private void ModeButton_Click(object sender, EventArgs e)
        {
            var mode = ((Button)sender).Tag.ToString();
            _state.IsRadiansMode = (mode == "RAD");

            // Подсветка активного режима
            foreach (Control c in this.Controls)
            {
                if (c is Button btn && (btn.Tag?.ToString() == "DEG" || btn.Tag?.ToString() == "RAD"))
                {
                    btn.BackColor = (btn.Tag.ToString() == mode) ? Color.LightGreen : SystemColors.Control;
                }
            }
        }

        private void UpdateDisplay()
        {
            if (_state.HasError)
            {
                DisplayLabel.Text = "Error: " + _state.LastError;
                _state.HasError = false;
            }
            else
            {
                DisplayLabel.Text = _state.RegisterX.ToString("G15");
            }
        }
    }
}