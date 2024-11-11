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
using static System.Windows.Forms.AxHost;

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
            DisplayLabel.Text = _state.XRegister.ToString();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            _service.Clear(_state);
            DisplayLabel.Text = _state.XRegister.ToString();
        }

        private void OperationButton_Click(object sender, EventArgs e)
        {
            var senderButton = (Button)sender;
            var opCode = senderButton.Text;

            /*_service.InsertOperation(_state, opCode);
            DisplayLabel.Text = _state.XRegister.ToString();*/

            if (opCode == "x^2")
            {
                _state.XRegister = Math.Pow(_state.XRegister, 2);
            }
            else if (opCode == "√x")
            {
                if (_state.XRegister >= 0)
                {
                    _state.XRegister = Math.Sqrt(_state.XRegister);
                }
                else
                {
                    _state.XRegister = double.NaN; // Ошибка: корень из отрицательного числа
                }
            }
            else if (opCode == "1/x")
            {
                if (_state.XRegister != 0)
                {
                    _state.XRegister = 1 / _state.XRegister;
                }
                else
                {
                    _state.XRegister = double.NaN; // Обработка деления на ноль
                }
            }
                
                DisplayLabel.Text = _state.XRegister.ToString();
        }

    }
}
