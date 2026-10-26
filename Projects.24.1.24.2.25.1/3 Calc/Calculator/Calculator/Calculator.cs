using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public class Calculator : Form
    {
        private Label display;

        private double operand1 = 0;
        private string currentOperation = "";
        private bool waitingForSecondOperand = false;
        private bool startNewInput = true;

        public Calculator()
        {
            InitializeCalculator();
        }

        private void InitializeCalculator()
        {
            Text = "Calculator";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(800, 400);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = Color.Thistle;

            display = new Label();
            display.Text = "0";
            display.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            display.TextAlign = ContentAlignment.MiddleRight;
            display.BorderStyle = BorderStyle.FixedSingle;
            display.BackColor = Color.White;
            display.Location = new Point(20, 20);
            display.Size = new Size(760, 60);
            Controls.Add(display);

            int w = 90;
            int h = 55;
            int gap = 10;

            int leftX = 20;
            int topY = 100;

            int col1 = leftX;
            int col2 = col1 + w + gap;
            int col3 = col2 + w + gap;

            int digitsX = 380;
            int dcol1 = digitsX;
            int dcol2 = dcol1 + w + gap;
            int dcol3 = dcol2 + w + gap;

            int row1 = topY;
            int row2 = row1 + h + gap;
            int row3 = row2 + h + gap;
            int row4 = row3 + h + gap;

            AddButton("sqrt", col1, row1, w, h, Operation_Click);
            AddButton("^", col2, row1, w, h, Operation_Click);
            AddButton("mod", col3, row1, w, h, Operation_Click);
            AddButton("/", col1, row2, w, h, Operation_Click);
            AddButton("*", col2, row2, w, h, Operation_Click);
            AddButton("-", col3, row2, w, h, Operation_Click);
            AddButton("div", col1, row3, w, h, Operation_Click);
            AddButton("module", col2, row3, w * 2 + gap, h, Operation_Click);

            AddButton("C", col1, row4, w * 3 + gap * 2, h, Clear_Click);

            AddButton("7", dcol1, row1, w, h, Digit_Click);
            AddButton("8", dcol2, row1, w, h, Digit_Click);
            AddButton("9", dcol3, row1, w, h, Digit_Click);
            AddButton("4", dcol1, row2, w, h, Digit_Click);
            AddButton("5", dcol2, row2, w, h, Digit_Click);
            AddButton("6", dcol3, row2, w, h, Digit_Click);
            AddButton("1", dcol1, row3, w, h, Digit_Click);
            AddButton("2", dcol2, row3, w, h, Digit_Click);
            AddButton("3", dcol3, row3, w, h, Digit_Click);
            AddButton("+", dcol1, row4, w, h, Operation_Click);
            AddButton("0", dcol2, row4, w, h, Digit_Click);
            AddButton(",", dcol3, row4, w, h, Comma_Click);

            Button equalButton = new Button();
            equalButton.Text = "=";
            equalButton.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            equalButton.BackColor = Color.Aquamarine;
            equalButton.Location = new Point(dcol3 + w + 20, row1);
            equalButton.Size = new Size(90, h * 4 + gap * 3);
            equalButton.Click += Equal_Click;
            Controls.Add(equalButton);
        }

        private void AddButton(string text, int x, int y, int w, int h, EventHandler clickHandler)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btn.Location = new Point(x, y);
            btn.Size = new Size(w, h);
            btn.Click += clickHandler;
            Controls.Add(btn);
        }

        private void Digit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (display.Text == "Ошибка")
                display.Text = "0";

            if (startNewInput || display.Text == "0")
            {
                display.Text = btn.Text;
                startNewInput = false;
            }
            else
            {
                display.Text += btn.Text;
            }
        }

        private void Comma_Click(object sender, EventArgs e)
        {
            if (display.Text == "Ошибка")
                display.Text = "0";

            if (startNewInput)
            {
                display.Text = "0,";
                startNewInput = false;
                return;
            }

            if (!display.Text.Contains(","))
                display.Text += ",";
        }

        private void Operation_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string op = btn.Text;

            if (!TryParseDisplay(out double currentValue))
            {
                ShowError();
                return;
            }

            if (op == "sqrt")
            {
                if (currentValue < 0)
                {
                    ShowError();
                    return;
                }

                display.Text = Math.Sqrt(currentValue).ToString(CultureInfo.CurrentCulture);
                startNewInput = true;
                return;
            }

            if (op == "module")
            {
                display.Text = Math.Abs(currentValue).ToString(CultureInfo.CurrentCulture);
                startNewInput = true;
                return;
            }

            operand1 = currentValue;
            currentOperation = op;
            waitingForSecondOperand = true;
            startNewInput = true;
        }

        private void Equal_Click(object sender, EventArgs e)
        {
            if (!waitingForSecondOperand || string.IsNullOrEmpty(currentOperation))
                return;

            if (!TryParseDisplay(out double operand2))
            {
                ShowError();
                return;
            }

            double result = 0;

            try
            {
                switch (currentOperation)
                {
                    case "+":
                        result = operand1 + operand2;
                        break;
                    case "-":
                        result = operand1 - operand2;
                        break;
                    case "*":
                        result = operand1 * operand2;
                        break;
                    case "/":
                        if (operand2 == 0)
                        {
                            ShowError();
                            return;
                        }
                        result = operand1 / operand2;
                        break;
                    case "^":
                        result = Math.Pow(operand1, operand2);
                        break;
                    case "mod":
                        if (operand2 == 0)
                        {
                            ShowError();
                            return;
                        }
                        result = operand1 % operand2;
                        break;
                    case "div":
                        if (operand2 == 0)
                        {
                            ShowError();
                            return;
                        }
                        result = (int)operand1 / (int)operand2;
                        break;
                    default:
                        ShowError();
                        return;
                }

                display.Text = result.ToString(CultureInfo.CurrentCulture);
                waitingForSecondOperand = false;
                currentOperation = "";
                startNewInput = true;
            }
            catch
            {
                ShowError();
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            display.Text = "0";
            operand1 = 0;
            currentOperation = "";
            waitingForSecondOperand = false;
            startNewInput = true;
        }

        private bool TryParseDisplay(out double value)
        {
            return double.TryParse(display.Text, NumberStyles.Float, CultureInfo.CurrentCulture, out value);
        }

        private void ShowError()
        {
            display.Text = "Ошибка";
            operand1 = 0;
            currentOperation = "";
            waitingForSecondOperand = false;
            startNewInput = true;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Calculator
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "Calculator";
            this.Load += new System.EventHandler(this.Calculator_Load);
            this.ResumeLayout(false);

        }

        private void Calculator_Load(object sender, EventArgs e)
        {

        }
    }
}