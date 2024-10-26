using AlarmClock.Models;
using System;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace AlarmClock.Forms
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            radioButton4.Checked = true; 
        }

        public AlarmSettings Settings { get; set; }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!DateTime.TryParse(AlarmTimeTextBox.Text, out var alarmTime))
            {
                MessageBox.Show("Время указано неверно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Settings.AlarmTime = alarmTime;
            Settings.AlarmMessage = AlarmMessageTextBox.Text;
            Settings.IsAlarmActive = IsAlarmActiveCheckBox.Checked;
            Settings.IsSoundActive = IsSoundActiveCheckBox.Checked;

            if (radioButton1.Checked)
            {
                Settings.Theme = "Light";
            }
            else if (radioButton2.Checked)
            {
                Settings.Theme = "Dark";
            }
            else if (radioButton3.Checked)
            {
                Settings.Theme = "Random";
            }
            else if (radioButton4.Checked)
            {
                Settings.Theme = "Auto";
            }

            DialogResult = DialogResult.OK;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            AlarmTimeTextBox.Text = Settings.AlarmTime.ToString("HH:mm");
            AlarmMessageTextBox.Text = Settings.AlarmMessage;
            IsAlarmActiveCheckBox.Checked = Settings.IsAlarmActive;
            IsSoundActiveCheckBox.Checked = Settings.IsSoundActive;

            // цветной режимы тут
            if (Settings.Theme == "Light")
            {
                radioButton1.Checked = true;
            }
            else if (Settings.Theme == "Dark")
            {
                radioButton2.Checked = true;
            }
            else if (Settings.Theme == "Random")
            {
                radioButton3.Checked = true;
            }
            else if (Settings.Theme == "Auto")
            {
                radioButton4.Checked = true;
            }

            ApplyTheme(Settings.Theme);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                ApplyTheme("Light");
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                ApplyTheme("Dark");
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                ApplyTheme("Random");
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                ApplyAutoTheme();
            }
        }

        private void ApplyTheme(string theme)
        {
            switch (theme)
            {
                case "Light":
                    this.BackColor = Color.FromArgb(240, 240, 240);
                    this.ForeColor = Color.Black;
                    AlarmTimeTextBox.BackColor = Color.White;
                    AlarmMessageTextBox.BackColor = Color.White;
                    OkButton.BackColor = SystemColors.Control;
                    CancelButton.BackColor = SystemColors.Control;
                    break;
                case "Dark":
                    this.BackColor = Color.FromArgb(17, 18, 20);
                    this.ForeColor = Color.White;
                    AlarmTimeTextBox.BackColor = Color.FromArgb(30, 30, 30);
                    AlarmMessageTextBox.BackColor = Color.FromArgb(30, 30, 30);
                    OkButton.BackColor = Color.FromArgb(30, 30, 30);
                    CancelButton.BackColor = Color.FromArgb(30, 30, 30);
                    break;
                case "Random":
                    Random random = new Random();
                    this.BackColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                    this.ForeColor = Color.Black;
                    AlarmTimeTextBox.BackColor = Color.White;
                    AlarmMessageTextBox.BackColor = Color.White;
                    OkButton.BackColor = SystemColors.Control;
                    CancelButton.BackColor = SystemColors.Control;
                    break;
            }

            ApplyFontColor();
        }

        private void ApplyAutoTheme()
        {
            var now = DateTime.Now;
            if (now.Hour > 16 || (now.Hour == 16 && now.Minute >= 34))
            {
                ApplyTheme("Dark");
            }
            else
            {
                ApplyTheme("Light");
            }
        }

        private void ApplyFontColor()
        {
            label1.ForeColor = this.ForeColor;
            label2.ForeColor = this.ForeColor;
            AlarmTimeTextBox.ForeColor = this.ForeColor;
            AlarmMessageTextBox.ForeColor = this.ForeColor;
            IsAlarmActiveCheckBox.ForeColor = this.ForeColor;
            IsSoundActiveCheckBox.ForeColor = this.ForeColor;
            OkButton.ForeColor = this.ForeColor;
            CancelButton.ForeColor = this.ForeColor;
            radioButton1.ForeColor = this.ForeColor;
            radioButton2.ForeColor = this.ForeColor;
            radioButton3.ForeColor = this.ForeColor;
            radioButton4.ForeColor = this.ForeColor;
        }
    }
}