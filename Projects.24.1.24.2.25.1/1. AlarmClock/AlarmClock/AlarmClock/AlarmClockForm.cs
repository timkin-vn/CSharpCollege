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

        public AlarmClockForm()
        {
            InitializeComponent();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            DisplayLabel.Text = DateTime.Now.ToLongTimeString();

            if (!_alarmState.IsAlarmActive)
            {
                return;
            }

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

        private void TimerTimer_Tick(object sender, EventArgs e)
        {
            if (_alarmState.IsTimerActive && !_alarmState.IsTimerPaused)
            {
                _alarmState.TimerRemaining = _alarmState.TimerRemaining.Subtract(TimeSpan.FromSeconds(1));

                if (_alarmState.TimerRemaining <= TimeSpan.Zero)
                {
                    _timerTimer.Stop();
                    _alarmState.IsTimerActive = false;
                    _alarmState.TimerRemaining = TimeSpan.Zero;
                    UpdateTimerDisplay();
                    SystemSounds.Exclamation.Play();
                    MessageBox.Show("Время таймера истекло!", "Таймер", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    UpdateTimerDisplay();
                }
            }
        }

        private void StartTimerButton_Click(object sender, EventArgs e)
        {
            if (_alarmState.TimerDuration == TimeSpan.Zero)
            {
                MessageBox.Show("Сначала установите время!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_alarmState.IsTimerActive)
            {
                _alarmState.IsTimerActive = true;
                _alarmState.IsTimerPaused = false;
                _alarmState.TimerRemaining = _alarmState.TimerDuration;
                _timerTimer.Start();
            }
            else if (_alarmState.IsTimerPaused)
            {
                _alarmState.IsTimerPaused = false;
                _timerTimer.Start();
            }
            UpdateTimerDisplay();
        }

        private void TimerPayzaButton_Click(object sender, EventArgs e)
        {
            if (_alarmState.IsTimerActive && !_alarmState.IsTimerPaused)
            {
                _alarmState.IsTimerPaused = true;
                _timerTimer.Stop();
            }
        }

        private void TimerStopButton_Click(object sender, EventArgs e)
        {
            _timerTimer.Stop();
            _alarmState.IsTimerActive = false;
            _alarmState.IsTimerPaused = false;
            _alarmState.TimerRemaining = TimeSpan.Zero;
            UpdateTimerDisplay();
        }

        private void ResetTimerButton_Click(object sender, EventArgs e)
        {
            _timerTimer.Stop();
            _alarmState.IsTimerActive = false;
            _alarmState.IsTimerPaused = false;
            _alarmState.TimerRemaining = _alarmState.TimerDuration;
            UpdateTimerDisplay();
        }

        private void button2_Click(object sender, EventArgs e) // Кнопка "Настроить таймер"
        {
            using (var timerSettingsForm = new TimerSettingsForm())
            {
                timerSettingsForm.TimerDuration = _alarmState.TimerDuration;
                if (timerSettingsForm.ShowDialog() == DialogResult.OK)
                {
                    _alarmState.TimerDuration = timerSettingsForm.TimerDuration;
                    _alarmState.TimerRemaining = timerSettingsForm.TimerDuration;
                    UpdateTimerDisplay();
                }
            }
        }

        private void UpdateTimerDisplay()
        {
            TimerDisplayLabel.Text = _alarmState.TimerRemaining.ToString(@"hh\:mm\:ss");
        }


    }

}
