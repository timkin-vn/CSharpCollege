using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsHello
{
    public partial class HelloForm : Form
    {
        public HelloForm()
        {
            InitializeComponent();
        }

        private void HelloButton_Click(object sender, EventArgs e)
        {
            var nameInput = NameInputTextBox.Text;

            if (string.IsNullOrWhiteSpace(nameInput))
            {
                MessageBox.Show("Здравствуйте, незнакомец!", "Приветствие", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show($"Привет, {nameInput}!", "Приветствие", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
