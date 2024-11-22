using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorApp
{
    public partial class CalculatorForm : Form
    {
        public CalculatorForm()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var input = XTextBox.Text;
            if (!double.TryParse(input, out var x))
            {
                MessageBox.Show("Неверно введено первое число", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                XTextBox.Focus();
                XTextBox.SelectAll();
                return;
            }

            input = YTextBox.Text;
            if (!double.TryParse(input, out var y))
            {
                MessageBox.Show("Неверно введено второе число", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                YTextBox.Focus();
                YTextBox.SelectAll();
                return;
            }

            var result = x + y;

            ResultLabel.Text = $"Сумма = {result}";
        }
    }
}
