using AlarmClock.Forms;
using AlarmClock.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlarmClock
{
    public partial class AlarmClockForm : Form
    {
        private AlarmState _alarmState = new AlarmState();
        private bool _stopwatchRunning = false;
        private TimeSpan _stopwatchTime = TimeSpan.Zero;
        private DateTime _stopwatchStartTime;

        private DateTime _customTime = new DateTime(2025, 1, 1, 0, 0, 0);

        public AlarmClockForm()
        {
            InitializeComponent();
            UpdateControls();
        }
        private void StopwatchButton_Click(object sender, EventArgs e)
        {
            if (!_stopwatchRunning)
            {
                // Запуск секундомера
                _stopwatchRunning = true;
                _stopwatchStartTime = DateTime.Now;
                StopwatchButton.Text = "Стоп";
                this.Text = "Будильник — секундомер работает";
            }
            else
            {
                // Остановка секундомера
                _stopwatchRunning = false;
                _stopwatchTime = DateTime.Now - _stopwatchStartTime;
                StopwatchButton.Text = "Секундомер";
                this.Text = "Будильник";
            }
        }
        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            if (_stopwatchRunning)
            {
                TimeSpan elapsed = DateTime.Now - _stopwatchStartTime;
                DisplayLabel.Text = elapsed.ToString(@"hh\:mm\:ss");
            }
            else
            {
                // Обычный режим — показываем системное время
                DisplayLabel.Text = DateTime.Now.ToLongTimeString();

                // ... твой оригинальный код будильника остаётся без изменений ...
                if (!_alarmState.IsAlarmActive) return;

                if (!_alarmState.IsAwakeActivated &&
                    DateTime.Now.Hour == _alarmState.AlarmTime.Hour &&
                    DateTime.Now.Minute == _alarmState.AlarmTime.Minute)
                {
                    _alarmState.IsAwakeActivated = true;
                    var awakeForm = new AwakeForm();
                    awakeForm.AlarmState = _alarmState;
                    awakeForm.FormClosed += AwakeForm_FormClosed;
                    awakeForm.ShowDialog();
                }

                if (_alarmState.IsSoundActive && _alarmState.IsAwakeActivated)
                {
                    SystemSounds.Beep.Play();
                }
            }
        }

        private void AwakeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form)sender).FormClosed -= AwakeForm_FormClosed;
            UpdateControls();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            var form = new AboutForm();
            form.ShowDialog();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            var form = new SettingsForm();
            form.AlarmState = _alarmState;
            if (form.ShowDialog() == DialogResult.OK)
            {
                UpdateControls();
            }
        }

        private void UpdateControls()
        {
            if (_alarmState.IsAlarmActive)
            {
                Text = $"Будильник (ожидает срабатывания в {_alarmState.AlarmTime.ToShortTimeString()})";
            }
            else
            {
                Text = "Будильник";
            }
        }
    }
}