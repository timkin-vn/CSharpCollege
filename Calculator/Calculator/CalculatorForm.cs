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

        private CalcHistory _history = new CalcHistory();

        

        
        public int ravno = 0;


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

            

            _service.InsertOperation(_state, _history, opCode);

            if(ravno == 1)
            {
                history.temp_history.Add(new CalcHistory(_history));
                
                _history.Clear();
                ravno -= 2;
            }
            

            ravno++;    

            DisplayLabel.Text = _state.XRegister.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            new history().ShowDialog();
        }

        
    }
}
