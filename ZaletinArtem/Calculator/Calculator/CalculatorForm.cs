using Calculator.Business.Entites;
using Calculator.Business.Services;
using System;
using System.Windows.Forms;

namespace Calculator
{
    public partial class CalculatorForm : Form
    {
        private CalculatorService _service = new CalculatorService();

        private CalculatorState _state = new CalculatorState();

        public CalculatorForm()
        {
            InitializeComponent();
        }

        private void DigitButton_Click(object sender, EventArgs e)
        {
            var senderButton = (Button)sender;
            var digitText = senderButton.Text;

            _service.InsertDigit(_state, digitText);
            DisplayLabel.Text = _state.XRegister.ToString("G");
        }

        private void OperationButton_Click(object sender, EventArgs e)
        {
            var senderButton = (Button)sender;
            var opCode = senderButton.Text;

            _service.InsertOperation(_state, opCode);
            DisplayLabel.Text = _state.Result.ToString("G");

            if (opCode != "=")
            {
                OperationLabel.Text = _state.DisplayedOperation;
            }
            else
            {
                OperationLabel.Text = string.Empty;
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            _service.Clear(_state);
            DisplayLabel.Text = _state.Result.ToString("G");
            OperationLabel.Text = _state.DisplayedOperation;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            _service.InsertOperation(_state, "+");
            DisplayLabel.Text = _state.Result.ToString("G");
            OperationLabel.Text = _state.DisplayedOperation;
        }

        private void SubtractButton_Click(object sender, EventArgs e)
        {
            _service.InsertOperation(_state, "-");
            DisplayLabel.Text = _state.Result.ToString("G");
            OperationLabel.Text = _state.DisplayedOperation;
        }

        private void MultiplyButton_Click(object sender, EventArgs e)
        {
            _service.InsertOperation(_state, "*");
            DisplayLabel.Text = _state.Result.ToString("G");
            OperationLabel.Text = _state.DisplayedOperation;
        }

        private void DivideButton_Click(object sender, EventArgs e)
        {
            _service.InsertOperation(_state, "/");
            DisplayLabel.Text = _state.Result.ToString("G");
            OperationLabel.Text = _state.DisplayedOperation;
        }

        private void SpecialOperationButton_Click(object sender, EventArgs e)
        {
            var senderButton = (Button)sender;
            var specialOpCode = senderButton.Text;

            _service.InsertOperation(_state, specialOpCode);
            DisplayLabel.Text = _state.Result.ToString("G");
            OperationLabel.Text = _state.DisplayedOperation;
        }

        private void ClearEntryButton_Click(object sender, EventArgs e)
        {
            _service.ClearEntry(_state);
            DisplayLabel.Text = _state.XRegister.ToString();
        }

        private void DecimalButton_Click(object sender, EventArgs e)
        {
            _service.InsertDecimalPoint(_state);
            DisplayLabel.Text = _state.XRegister.ToString();
        }

        private void BackspaceButton_Click(object sender, EventArgs e)
        {
            _service.Backspace(_state);
            DisplayLabel.Text = _state.XRegister.ToString();
        }
    }
}
