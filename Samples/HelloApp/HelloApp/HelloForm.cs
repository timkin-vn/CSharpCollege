using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HelloApp
{
    public partial class HelloForm : Form
    {
        public HelloForm()
        {
            InitializeComponent();
        }

        private void HelloButton_Click(object sender, EventArgs e)
        {
            var name = NameInputTextBox.Text;
            var message = string.IsNullOrEmpty(name) ? "Привет, незнакомец!" : $"Привет, {name}!";
            MessageBox.Show(message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
