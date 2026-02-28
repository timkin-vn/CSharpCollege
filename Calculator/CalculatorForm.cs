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

namespace Calculator
{
    public partial class CalculatorForm : Form
    {
        private readonly CalculatorState _state = new CalculatorState();

        private readonly CalculatorService _service = new CalculatorService();

        public CalculatorForm()
        {
            InitializeComponent();

            
            this.AcceptButton = null;
            this.KeyPreview = true; 
            this.KeyDown += CalculatorForm_KeyDown;
            

        }
       

        private void CalculatorForm_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9 ||
                e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
            {
                string digit = e.KeyCode.ToString().Replace("D", "").Replace("NumPad", "");
                _service.PressDigit(_state, digit);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Add || e.KeyCode == Keys.Oemplus)
            {
                _service.PressOperation(_state, "+");
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus)
            {
                _service.PressOperation(_state, "-");
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Multiply)
            {
                _service.PressOperation(_state, "*");
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Divide)
            {
                _service.PressOperation(_state, "/");
                e.Handled = true;
            }
            
            else if (e.KeyCode == Keys.S) 
            {
                _service.PressSin(_state);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.C) 
            {
                _service.PressCos(_state);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.T) 
            {
                _service.PressTan(_state);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Q) 
            {
                _service.PressSqrt(_state);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.P) 
            {
                _service.PressPercent(_state);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.L) 
            {
                _service.PressLog(_state);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.K) 
            {
                _service.PressDegree(_state);
                e.Handled = true;
            }
            
            else if (e.KeyCode == Keys.Enter)
            {
                _service.PressEqual(_state);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Back)
            {
                _service.PressBack(_state);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                _service.Clear(_state);
                e.Handled = true;
            }
            

            if (e.Handled)
            {
                ShowResult();
            }
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

        private void ShowResult()
        {
            DisplayLabel.Text = _state.RegisterX.ToString();
        }

        private void XToYButton_Click(object sender, EventArgs e)
        {
            _service.MoveXToY(_state);
        }

        private void OperationButton_Click(object sender, EventArgs e)
        {
            var operationCode = ((Button)sender).Text;
            _service.PressOperation(_state, operationCode);
            ShowResult();
        }

        private void EqualButton_Click(object sender, EventArgs e)
        {
            _service.PressEqual(_state);
            ShowResult();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            _service.PressBack(_state);
            ShowResult();
        }

        private void Cos_Button_Click(object sender, EventArgs e)
        {
            _service.PressCos(_state);
            ShowResult();
        }

        private void Sin_Butoon_Click(object sender, EventArgs e)
        {
            _service.PressSin(_state);
            ShowResult();
        }

        private void Log_Button_Click(object sender, EventArgs e)
        {
            _service.PressLog(_state);
            ShowResult();
        }

        private void Tg_Button_Click(object sender, EventArgs e)
        {
            _service.PressTan(_state);
            ShowResult();
        }

        private void sqr_btn_Click(object sender, EventArgs e)
        {
            _service.PressSqrt(_state);
            ShowResult();
        }

        private void inkvadrat_Click(object sender, EventArgs e)
        {
            _service.PressDegree(_state);
            ShowResult();
        }

        private void Procent_Click(object sender, EventArgs e)
        {
            _service.PressPercent(_state);
            ShowResult();
        }
    }
}
