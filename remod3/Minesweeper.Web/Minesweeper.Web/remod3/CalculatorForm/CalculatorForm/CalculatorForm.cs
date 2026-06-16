using System.Drawing.Drawing2D;
using Calculator.Business.Services;
using CalculatorForm.Business.Models;

namespace CalculatorForm {
    public sealed partial class CalculatorForm : Form {
        private readonly CalculatorState _state = new CalculatorState();
        private readonly CalculatorService _service = new CalculatorService();

        public CalculatorForm() {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(CalculatorForm_KeyDown);
            this.DoubleBuffered = true;
        }

        protected override void OnPaintBackground(PaintEventArgs e) {
            using var brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(30, 30, 30),
                Color.FromArgb(10, 10, 10),
                90F);
            e.Graphics.FillRectangle(brush, this.ClientRectangle);
        }

        private void CalculatorForm_KeyDown(object? sender, KeyEventArgs e) {
            if (e.KeyCode != Keys.Escape) return;
            CalculatorService.Clear(_state);
            SineButton.Text = @"sin";
            ShowResult();
            e.Handled = true;
        }

        private void DigitButton_Click(object sender, EventArgs e) {
            var digit = ((Button)sender).Text;
            _service.PressDigit(_state, digit);
            ShowResult();
        }

        private void ClearButton_Click(object sender, EventArgs e) {
            CalculatorService.Clear(_state);
            SineButton.Text = @"sin";
            ShowResult();
        }

        private void OperationButton_Click(object sender, EventArgs e) {
            var operation = ((Button)sender).Text;
            _service.PressOperation(_state, operation);
            ShowResult();
        }

        private void EqualButton_Click(object sender, EventArgs e) {
            CalculatorService.PressEqual(_state);
            ShowResult();
        }

        private void FunctionButton_Click(object sender, EventArgs e) {
            var function = ((Button)sender).Text;

            switch (function) {
                case "√":
                    CalculatorService.SquareRoot(_state);
                    break;
                case "sin":
                case "cos":
                case "tan":
                case "cot":
                case "arcsin":
                case "arccos":
                case "arctan":
                case "arccot":
                    CalculatorService.ComputeTrigFunction(_state);
                    break;
                case "x²":
                    CalculatorService.Square(_state);
                    break;
                case "%":
                    CalculatorService.Percent(_state);
                    break;
                case "+/-":
                    CalculatorService.ChangeSign(_state);
                    break;
                case ".":
                    CalculatorService.AddDecimal(_state);
                    break;
                case "MS":
                    CalculatorService.MemoryStore(_state);
                    break;
                case "MR":
                    CalculatorService.MemoryRecall(_state);
                    break;
            }

            ShowResult();
        }

        private void TrigMenuButton_Click(object sender, EventArgs e) {
            ContextMenuStrip trigMenu = new ContextMenuStrip();
            trigMenu.BackColor = Color.FromArgb(40, 40, 40);
            trigMenu.ForeColor = Color.White;
            trigMenu.Font = new Font("Segoe UI", 10F);
            trigMenu.Items.Add("sin").Click += (_, _) => SetTrigFunction("sin");
            trigMenu.Items.Add("cos").Click += (_, _) => SetTrigFunction("cos");
            trigMenu.Items.Add("tan").Click += (_, _) => SetTrigFunction("tan");
            trigMenu.Items.Add("cot").Click += (_, _) => SetTrigFunction("cot");
            trigMenu.Items.Add("arcsin").Click += (_, _) => SetTrigFunction("arcsin");
            trigMenu.Items.Add("arccos").Click += (_, _) => SetTrigFunction("arccos");
            trigMenu.Items.Add("arctan").Click += (_, _) => SetTrigFunction("arctan");
            trigMenu.Items.Add("arccot").Click += (_, _) => SetTrigFunction("arccot");
            trigMenu.Show(TrigMenuButton, new Point(0, TrigMenuButton.Height));
        }

        private void SetTrigFunction(string function) {
            _state.SelectedFunction = function;
            SineButton.Text = function;
        }

        private void ShowResult() {
            DisplayLabel.Text = !string.IsNullOrEmpty(_state.CurrentInput) ? _state.CurrentInput : _state.RegisterX.ToString("F6").TrimEnd('0').TrimEnd('.');
            ExpressionLabel.Text = _state.Expression;
        }
    }
}