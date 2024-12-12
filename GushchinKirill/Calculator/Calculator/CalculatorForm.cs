using Calculator.Business.Entites;
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

namespace Calculator
{
    public partial class CalculatorForm : Form
    {
        private CalculatorService _service = new CalculatorService();

        private CalculatorState _state = new CalculatorState();

        private bool num=false;
        public CalculatorForm()
        {
            InitializeComponent();
        }

        private void DigitButton_Click(object sender, EventArgs e)
        {
            var senderButton = (Button)sender;
            var digitText = senderButton.Text;

            _service.InsertDigit(_state, digitText);
            if (num) 
            {
                DisplayLabel.Text = DisplayLabel.Text.ToString()+ digitText.ToString();
                return;
            }
            DisplayLabel.Text = _state.XRegister.ToString();
            num = true;
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            _service.Clear(_state);
            DisplayLabel.Text = "";
        }

        private void OperationButton_Click(object sender, EventArgs e)
        {
            var senderButton = (Button)sender;
            var opCode = senderButton.Text;
            num= true;
            _service.InsertOperation(_state, opCode);

            if (opCode == "=")
            {
                listBox1.Items.Add(DisplayLabel.Text + '=' + _state.XRegister);
                DisplayLabel.Text = _state.XRegister.ToString();
                num=false;
                return;
            }
            DisplayLabel.Text = DisplayLabel.Text.ToString() + opCode.ToString();
        }

        private void ListClearButton_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void CalculatorForm_Load(object sender, EventArgs e)
        {

        }
    }
}
