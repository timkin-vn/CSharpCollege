using AlarmClock.Forms;
using AlarmClock.Model;
using System;
using System.Windows.Forms;

namespace AlarmClock
{
    public partial class ClockForm : Form
    {
        private AlarmClockState _clockState = new AlarmClockState();
        private TimerState _timerState;
        private System.Windows.Forms.Timer _timerCountdown;

        public ClockForm()
        {
            InitializeComponent();
            _timerCountdown = new System.Windows.Forms.Timer();
            _timerCountdown.Interval = 1000;
            _timerCountdown.Tick += TimerCountdown_Tick;
        }

        private void ClockForm_Load(object sender, EventArgs e)
        {
            var theme = ThemeManager.LoadTheme();
            ThemeManager.ApplyTheme(this, theme);
            UpdateView();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            DisplayLabel.Text = DateTime.Now.ToLongTimeString();

            // --- Проверка будильника ---
            if (_clockState.IsAlarmActive)
            {
                if (!_clockState.IsAwakeActivated &&
                    DateTime.Now.Minute == _clockState.AlarmTime.Minute &&
                    DateTime.Now.Hour == _clockState.AlarmTime.Hour)
                {
                    _clockState.IsAwakeActivated = true;

                    var awakeForm = new AwakeForm { ClockState = _clockState };
                    awakeForm.FormClosed += AwakeForm_FormClosed;
                    awakeForm.ShowDialog();
                }

                if (_clockState.IsSoundActive && _clockState.IsAwakeActivated)
                {
                    System.Media.SystemSounds.Beep.Play();
                }
            }
        }

        private void TimerCountdown_Tick(object sender, EventArgs e)
        {
            if (_timerState == null || !_timerState.IsActive)
                return;

            _timerState.Remaining = _timerState.Remaining.Subtract(TimeSpan.FromSeconds(1));

            if (_timerState.Remaining.TotalSeconds <= 0)
            {
                _timerCountdown.Stop();
                _timerState.IsActive = false;
                StopTimerState();

                MessageBox.Show("Время вышло!", "Таймер",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (_timerState.IsSoundActive)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        System.Media.SystemSounds.Beep.Play();
                        System.Threading.Thread.Sleep(200);
                    }
                }
                return;
            }

            // Обновляем заголовок окна
            Text = string.Format("Таймер: {0}", _timerState.Remaining.ToString(@"hh\:mm\:ss"));

            // Звуковой сигнал в последние 10 секунд
            if (_timerState.IsSoundActive && _timerState.Remaining.TotalSeconds <= 10)
            {
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void AwakeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form)sender).FormClosed -= AwakeForm_FormClosed;
            _clockState.IsAlarmActive = false;
            _clockState.IsAwakeActivated = false;
            UpdateView();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            var aboutForm = new AboutForm();
            aboutForm.FormClosed += (s, args) => ApplyCurrentTheme();
            aboutForm.ShowDialog();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm { ClockState = _clockState };
            if (settingsForm.ShowDialog() != DialogResult.OK)
                return;
            UpdateView();
        }

        private void DisableAlarmButton_Click(object sender, EventArgs e)
        {
            _clockState.IsAlarmActive = false;
            _clockState.IsAwakeActivated = false;
            UpdateView();
        }

        private void TimerButton_Click(object sender, EventArgs e)
        {
            if (_timerState != null && _timerState.IsActive)
            {
                // Если таймер уже активен — просто открываем окно с текущим состоянием
                var timerForm = new TimerForm(_timerState, OnTimerStopped);
                timerForm.FormClosed += (s, args) => ApplyCurrentTheme();
                timerForm.ShowDialog();
            }
            else
            {
                var timerForm = new TimerForm(null, OnTimerStarted);
                timerForm.FormClosed += (s, args) => ApplyCurrentTheme();
                timerForm.ShowDialog();
            }
        }

        private void StopTimerButton_Click(object sender, EventArgs e)
        {
            StopTimerState();
            UpdateView();
        }

        private void OnTimerStarted(TimerState state)
        {
            _timerState = state;
            _timerCountdown.Start();
            UpdateView();
        }

        private void OnTimerStopped()
        {
            StopTimerState();
            UpdateView();
        }

        private void StopTimerState()
        {
            _timerCountdown.Stop();
            if (_timerState != null)
                _timerState.IsActive = false;
            _timerState = null;
        }

        private void UpdateView()
        {
            if (_clockState.IsAlarmActive)
            {
                Text = string.Format("Будильник. Ожидается срабатывание в {0}", _clockState.AlarmTime.ToShortTimeString());
                DisableAlarmButton.Enabled = true;
            }
            else if (_timerState != null && _timerState.IsActive)
            {
                Text = string.Format("Таймер: {0}", _timerState.Remaining.ToString(@"hh\:mm\:ss"));
                DisableAlarmButton.Enabled = false;
            }
            else
            {
                Text = "Будильник";
                DisableAlarmButton.Enabled = false;
            }

            // Кнопка отмены таймера видна только при активном таймере
            StopTimerButton.Visible = (_timerState != null && _timerState.IsActive);
            TimerButton.Enabled = !StopTimerButton.Visible;
        }

        private void ApplyCurrentTheme()
        {
            var theme = ThemeManager.LoadTheme();
            ThemeManager.ApplyTheme(this, theme);
        }
    }
}
