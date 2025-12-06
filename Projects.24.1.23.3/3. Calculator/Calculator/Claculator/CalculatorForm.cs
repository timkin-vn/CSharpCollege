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

namespace Claculator
{
    public partial class CalculatorForm : Form
    {
        private readonly CalculatorState _state = new CalculatorState();

        private readonly CalculatorService _service = new CalculatorService();

        private double _historyY;
        private double _historyX;
        private string _historyOp = string.Empty;
        private bool _historyAvailable = false;

        public CalculatorForm()
        {
            InitializeComponent();
        }

        private void DigitButton_Click(object sender, EventArgs e)
        {
            var digit = ((Button)sender).Text;
            _service.PressDigit(_state, digit);
            ShowResult();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            _service.Clear(_state);
            ShowResult();
        }

        private void OperationButton_Click(object sender, EventArgs e)
        {
            var operation = ((Button)sender).Text.Trim();

            _historyY = _state.RegisterY;
            _historyX = _state.RegisterX;
            if (operation != "=") 
                _historyOp = operation;
            _historyAvailable = true;

            _service.PressOperation(_state, operation);
            ShowResult();
        }

        private void EqualButton_Click(object sender, EventArgs e)
        {
            _service.PressEqual(_state);

            var result = _state.RegisterX;

            string entry;
            if (!string.IsNullOrEmpty(_historyOp))
                entry = $"{_historyY} {_historyOp} {_historyX} = {result}";
            else
                entry = $"{_historyX} = {result}";

            AddToHistory(entry);

            _historyAvailable = false;
            _historyOp = string.Empty;

            ShowResult();
        }

        private void XToYButton_Click(object sender, EventArgs e)
        {
            _service.MoveXToY(_state);
        }

        private void ShowResult()
        {
            DisplayLabel.Text = _state.RegisterX.ToString();
        }

        private void ClearEntry_Click(object sender, EventArgs e)
        {
            _service.ClearEntry(_state);
            ShowResult();
        }

        private void ToggleSign_Click(object sender, EventArgs e)
        {
            _service.ToggleSign(_state);
            ShowResult();
        }

        private void AddToHistory(string text)
        {
            HistoryListBox.Items.Add(text);
        }
    }
}
