using AlarmClock.Forms;
using AlarmClock.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AlarmClock
{
    public partial class ClockForm : Form
    {
        private AlarmSettings _settings = new AlarmSettings();

        public ClockForm()
        {
            InitializeComponent();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            DisplayLabel.Text = DateTime.Now.ToLongTimeString();
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            var form = new SettingsForm();
            form.Settings = _settings;
            if (form.ShowDialog() == DialogResult.OK)
            {
                ApplyTheme(_settings.Theme);
            }
        }

        private void ClockForm_Load(object sender, EventArgs e)
        {
            // проверка
            if (_settings.Theme == "Auto")
            {
                ApplyAutoTheme();
            }
            else
            {
                ApplyTheme(_settings.Theme);
            }
        }

        private void ApplyTheme(string theme)
        {
            switch (theme)
            {
                case "Light":
                    this.BackColor = Color.FromArgb(240, 240, 240);
                    this.ForeColor = Color.Black;
                    break;
                case "Dark":
                    this.BackColor = Color.FromArgb(17, 18, 20);
                    this.ForeColor = Color.White;
                    break;
                case "Random":
                    Random random = new Random();
                    this.BackColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                    this.ForeColor = Color.Black;
                    break;
                case "Auto":
                    ApplyAutoTheme();
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
            AboutButton.ForeColor = this.ForeColor;
            SettingsButton.ForeColor = this.ForeColor;
            ExitButton.ForeColor = this.ForeColor;

            if (this.BackColor == Color.FromArgb(17, 18, 20))
            {
                AboutButton.BackColor = Color.FromArgb(30, 30, 30);
                SettingsButton.BackColor = Color.FromArgb(30, 30, 30);
                ExitButton.BackColor = Color.FromArgb(30, 30, 30);
            }
            else
            {
                AboutButton.BackColor = SystemColors.Control;
                SettingsButton.BackColor = SystemColors.Control;
                ExitButton.BackColor = SystemColors.Control;
            }
        }
    }
}