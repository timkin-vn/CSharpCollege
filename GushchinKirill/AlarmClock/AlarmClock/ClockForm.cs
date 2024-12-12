using AlarmClock.Forms;
using AlarmClock.Models;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace AlarmClock
{
    public partial class ClockForm : Form
    {
        private AlarmSettings _settings = new AlarmSettings();

        private AwakeForm _awakeForm;

        public ClockForm()
        {
            InitializeComponent();
            // Убираем заголовок окна
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void AlarmTimer_Tick(object sender, EventArgs e)
        {
            // Обновление индикатора времени
            DisplayLabel.Text = DateTime.Now.ToLongTimeString();

            // Проверка срабатывания
            if (_settings.IsAlarmActive && DateTime.Now.TimeOfDay >= _settings.AlarmTime.TimeOfDay)
            {
                if (_awakeForm == null || _awakeForm.IsDisposed)
                {
                    _awakeForm = new AwakeForm();
                }

                _awakeForm.Settings = _settings;
                _awakeForm.Show();

                if (_settings.IsSoundActive)
                {
                    SystemSounds.Beep.Play();
                }
            }
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
            form.ShowDialog();
        }

        private void StopwatchButton_Click(object sender, EventArgs e)
        {
            new StopwatchForm().ShowDialog();
        }

        private void TimerButton_Click(object sender, EventArgs e)
        {
            new TimerForm().ShowDialog();
        }

        private void FocusPeriodsButton_Click(object sender, EventArgs e)
        {
            new FocusPeriodsForm().ShowDialog();
        }
    }
}