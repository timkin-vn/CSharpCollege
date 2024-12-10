using AlarmClock.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlarmClock.Forms
{
    public partial class SettingsForm : Form
    {
        public AlarmSettings Settings { get; set; }

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            int num1 = int.Parse(AlarmTimeTextBox.Text);
            int num2 = int.Parse(textBox1.Text);
            int num3 = int.Parse(textBox2.Text);
            int num4 = int.Parse(textBox3.Text);

            string time = (num1*10 + num2).ToString() + ":" + (num3 * 10 + num4).ToString();

            if (!DateTime.TryParse(time, out var alarmTime))
            {
                MessageBox.Show("Неверно задано время срабатывания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Settings.AlarmTime = alarmTime;
            Settings.AlarmMessage = AlarmMessageTextBox.Text;
            Settings.IsAlarmActive = IsAlarmActiveCheckBox.Checked;
            Settings.IsSoundActive = IsSoundActiveCheckBox.Checked;

            DialogResult = DialogResult.OK;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            string dateTM = Settings.AlarmTime.ToString("HH:mm");
            string[] dT = dateTM.Select(str => str.ToString()).ToArray();

            foreach (var item in dT)
            {
                Console.WriteLine(item.ToString());
            }

            AlarmTimeTextBox.Text = dT[0];
            textBox1.Text = dT[1];
            textBox2.Text = dT[3];
            textBox3.Text = dT[4];
            AlarmMessageTextBox.Text = Settings.AlarmMessage;
            IsAlarmActiveCheckBox.Checked = Settings.IsAlarmActive;
            IsSoundActiveCheckBox.Checked = Settings.IsSoundActive;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int num1 = int.Parse(AlarmTimeTextBox.Text);
            int num4 = int.Parse(textBox1.Text);

            if (num1 < 2)
            {
                AlarmTimeTextBox.Text = (num1 + 1).ToString();
            }
            else
            {
                AlarmTimeTextBox.Text = "0";
            }

            if (num1 == 1 && num4 > 3)
            {
                textBox1.Text = "3";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int num2 = int.Parse(AlarmTimeTextBox.Text);
            int num4 = int.Parse(textBox1.Text);

            if (num2 > 0)
            {
                AlarmTimeTextBox.Text = (num2 - 1).ToString();
            }
            else
            {
                AlarmTimeTextBox.Text = "2";
            }

            if (num2 == 0 && num4 > 3)
            {
                textBox1.Text = "3";
            }
        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            int num8 = int.Parse(textBox3.Text);
            if (num8<9)
            {
                textBox3.Text = (num8 + 1).ToString();
            }
            else
            {
                textBox3.Text = "0";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int num7 = int.Parse(textBox3.Text);
            if (num7 > 0)
            {
                textBox3.Text = (num7 - 1).ToString();
            }
            else
            {
                textBox3.Text = "9";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            int num6 = int.Parse(textBox2.Text);
            if (num6 < 5)
            {
                textBox2.Text = (num6 + 1).ToString();
            }
            else
            {
                textBox2.Text = "0";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int num5 = int.Parse(textBox2.Text);
            if (num5 > 0)
            {
                textBox2.Text = (num5 - 1).ToString();
            }
            else
            {
                textBox2.Text = "5";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int num4 = int.Parse(textBox1.Text);
            int num1 = int.Parse(AlarmTimeTextBox.Text);

            if (num4 < 9 && num1 != 2)
            {
                textBox1.Text = (num4 + 1).ToString();
            }
            else if (num1 == 2 && num4 < 3)
            {
                textBox1.Text = (num4 + 1).ToString();
            }
            else
            {
                textBox1.Text = "0";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int num3 = int.Parse(textBox1.Text);
            int num1 = int.Parse(AlarmTimeTextBox.Text);

            if (num3 > 0)
            {
                textBox1.Text = (num3 - 1).ToString();
            }
            else if (num1==2)
            {
                textBox1.Text = "3";
            }
            else
            {
                textBox1.Text = "9";
            }
        }
    }
}
