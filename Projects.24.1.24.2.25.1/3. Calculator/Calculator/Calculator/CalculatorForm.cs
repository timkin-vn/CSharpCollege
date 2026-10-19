using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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

        private readonly Dictionary<Control, Tuple<Color, Color>> _lightColors =
            new Dictionary<Control, Tuple<Color, Color>>();

        private ListBox _historyList;

        private Button _themeButton;

        private bool _isDark;

        public CalculatorForm()
        {
            InitializeComponent();
            KeyPreview = true;
            BuildSidePanel();
            SnapshotColors(this);
        }

        private void BuildSidePanel()
        {
            const int panelWidth = 190;

            ClientSize = new Size(ClientSize.Width + panelWidth, ClientSize.Height);

            var panel = new Panel
            {
                Dock = DockStyle.Right,
                Width = panelWidth,
                Padding = new Padding(8)
            };

            _historyList = new ListBox
            {
                Dock = DockStyle.Fill,
                IntegralHeight = false,
                TabStop = false
            };

            var clearHistoryButton = CreateSideButton("Очистить историю", ClearHistoryButton_Click);
            clearHistoryButton.Dock = DockStyle.Bottom;
            clearHistoryButton.Height = 32;

            _themeButton = CreateSideButton("Тёмная тема", ThemeButton_Click);
            _themeButton.Dock = DockStyle.Top;
            _themeButton.Height = 32;

            var inputRow = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 44,
                ColumnCount = 3,
                RowCount = 1
            };

            for (var i = 0; i < 3; i++)
            {
                inputRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / 3));
            }

            inputRow.Controls.Add(CreateSideButton(".", PointButton_Click), 0, 0);
            inputRow.Controls.Add(CreateSideButton("±", SignButton_Click), 1, 0);
            inputRow.Controls.Add(CreateSideButton("←", BackspaceButton_Click), 2, 0);

            panel.Controls.Add(_historyList);
            panel.Controls.Add(clearHistoryButton);
            panel.Controls.Add(_themeButton);
            panel.Controls.Add(inputRow);

            Controls.Add(panel);
        }

        private Button CreateSideButton(string text, EventHandler handler)
        {
            var button = new Button
            {
                Text = text,
                Dock = DockStyle.Fill,
                TabStop = false
            };
            button.Click += handler;
            return button;
        }

        private void SnapshotColors(Control control)
        {
            _lightColors[control] = Tuple.Create(control.BackColor, control.ForeColor);

            foreach (Control child in control.Controls)
            {
                SnapshotColors(child);
            }
        }

        private void ApplyTheme(Control control)
        {
            if (_isDark)
            {
                var isInput = control is Button || control is ListBox;
                control.BackColor = isInput ? Color.FromArgb(50, 50, 56) : Color.FromArgb(32, 32, 36);
                control.ForeColor = Color.Gainsboro;
            }
            else
            {
                var colors = _lightColors[control];
                control.BackColor = colors.Item1;
                control.ForeColor = colors.Item2;
            }

            foreach (Control child in control.Controls)
            {
                ApplyTheme(child);
            }
        }

        private void ThemeButton_Click(object sender, EventArgs e)
        {
            _isDark = !_isDark;
            _themeButton.Text = _isDark ? "Светлая тема" : "Тёмная тема";
            ApplyTheme(this);
        }

        private void Execute(Action action)
        {
            action();
            DisplayValue();
        }

        private void DigitButton_Click(object sender, EventArgs e)
        {
            _service.PressDigit(_calculatorModel, ((Button)sender).Text);
            DisplayValue();
        }

        private string FormatRegister()
        {
            var value = _calculatorModel.RegisterX;

            if (!_calculatorModel.IsDecimalEntry)
            {
                return value.ToString();
            }

            var text = value.ToString("F" + _calculatorModel.DecimalDigits);

            if (_calculatorModel.DecimalDigits == 0)
            {
                text += CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            }

            return text;
        }

        private void SyncHistory()
        {
            var history = _calculatorModel.History;

            if (_historyList.Items.Count == history.Count)
            {
                return;
            }

            _historyList.BeginUpdate();
            _historyList.Items.Clear();

            foreach (var item in history)
            {
                _historyList.Items.Add(item);
            }

            _historyList.EndUpdate();
            _historyList.TopIndex = Math.Max(0, _historyList.Items.Count - 1);
        }

        private void DisplayValue()
        {
            DisplayLabel.Text = _calculatorModel.HasError ? "Ошибка" : FormatRegister();
            SyncHistory();
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

        private void PointButton_Click(object sender, EventArgs e)
        {
            Execute(() => _service.PressDecimalPoint(_calculatorModel));
        }

        private void SignButton_Click(object sender, EventArgs e)
        {
            Execute(() => _service.PressToggleSign(_calculatorModel));
        }

        private void BackspaceButton_Click(object sender, EventArgs e)
        {
            Execute(() => _service.PressBackspace(_calculatorModel));
        }

        private void ClearHistoryButton_Click(object sender, EventArgs e)
        {
            _calculatorModel.History.Clear();
            SyncHistory();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:
                    Execute(() => _service.PressEqual(_calculatorModel));
                    return true;

                case Keys.Back:
                    Execute(() => _service.PressBackspace(_calculatorModel));
                    return true;

                case Keys.Escape:
                    Execute(() => _service.PressClear(_calculatorModel));
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            var key = e.KeyChar;

            if (key >= '0' && key <= '9')
            {
                Execute(() => _service.PressDigit(_calculatorModel, key.ToString()));
            }
            else if (key == '+' || key == '-' || key == '*' || key == '/')
            {
                Execute(() => _service.PressOperation(_calculatorModel, key.ToString()));
            }
            else if (key == '=')
            {
                Execute(() => _service.PressEqual(_calculatorModel));
            }
            else if (key == '.' || key == ',')
            {
                Execute(() => _service.PressDecimalPoint(_calculatorModel));
            }
            else
            {
                base.OnKeyPress(e);
                return;
            }

            e.Handled = true;
        }
    }
}