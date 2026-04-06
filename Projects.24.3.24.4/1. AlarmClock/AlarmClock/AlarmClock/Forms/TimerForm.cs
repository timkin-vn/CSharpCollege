using AlarmClock.Model;
using System;
using System.Windows.Forms;

namespace AlarmClock.Forms
{
    public partial class TimerForm : Form
    {
        private TimerState _timerState;
        private System.Windows.Forms.Timer _countdownTimer;
        private Action<TimerState> _onStarted;
        private Action _onStopped;

        internal TimerForm(TimerState existingState, Action<TimerState> onStarted)
        {
            InitializeComponent();
            _onStarted = onStarted;
            _countdownTimer = new System.Windows.Forms.Timer();
            _countdownTimer.Interval = 1000;
            _countdownTimer.Tick += CountdownTimer_Tick;

            if (existingState != null)
            {
                // Показываем оставшееся время
                _timerState = existingState;
                ShowCountdownMode();
                UpdateCountdownDisplay();
                _countdownTimer.Start();
            }
        }

        internal TimerForm(TimerState existingState, Action onStopped)
        {
            InitializeComponent();
            _onStopped = onStopped;
            _countdownTimer = new System.Windows.Forms.Timer();
            _countdownTimer.Interval = 1000;
            _countdownTimer.Tick += CountdownTimer_Tick;

            if (existingState != null)
            {
                _timerState = existingState;
                ShowCountdownMode();
                UpdateCountdownDisplay();
                _countdownTimer.Start();
            }
        }

        private void TimerForm_Load(object sender, EventArgs e)
        {
            var theme = ThemeManager.LoadTheme();
            ThemeManager.ApplyTheme(this, theme);

            if (_timerState == null)
            {
                HoursUpDown.Value = 0;
                MinutesUpDown.Value = 5;
                SecondsUpDown.Value = 0;
            }
        }

        private void ShowCountdownMode()
        {
            TitleLabel.Text = "Осталось";
            HideInputControls();
            CountdownLabel.Visible = true;
            StopButton.Visible = true;
        }

        private void HideInputControls()
        {
            HoursUpDown.Visible = false;
            MinutesUpDown.Visible = false;
            SecondsUpDown.Visible = false;
            HoursLabel.Visible = false;
            MinutesLabel.Visible = false;
            SecondsLabel.Visible = false;
            IsSoundCheckBox.Visible = false;
            StartButton.Visible = false;
        }

        private void ShowInputControls()
        {
            TitleLabel.Text = "Установите время";
            HoursUpDown.Visible = true;
            MinutesUpDown.Visible = true;
            SecondsUpDown.Visible = true;
            HoursLabel.Visible = true;
            MinutesLabel.Visible = true;
            SecondsLabel.Visible = true;
            IsSoundCheckBox.Visible = true;
            StartButton.Visible = true;

            CountdownLabel.Visible = false;
            StopButton.Visible = false;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            int hours = (int)HoursUpDown.Value;
            int minutes = (int)MinutesUpDown.Value;
            int seconds = (int)SecondsUpDown.Value;

            var totalSeconds = hours * 3600 + minutes * 60 + seconds;
            if (totalSeconds <= 0)
            {
                MessageBox.Show("Установите время больше нуля", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _timerState = new TimerState
            {
                Duration = TimeSpan.FromSeconds(totalSeconds),
                Remaining = TimeSpan.FromSeconds(totalSeconds),
                IsActive = true,
                IsSoundActive = IsSoundCheckBox.Checked
            };

            ShowCountdownMode();
            UpdateCountdownDisplay();
            _countdownTimer.Start();

            if (_onStarted != null)
                _onStarted(_timerState);
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            if (_timerState == null || !_timerState.IsActive)
                return;

            _timerState.Remaining = _timerState.Remaining.Subtract(TimeSpan.FromSeconds(1));

            if (_timerState.Remaining.TotalSeconds <= 0)
            {
                _countdownTimer.Stop();
                _timerState.IsActive = false;
                OnTimerFinished();
                return;
            }

            UpdateCountdownDisplay();

            if (_timerState.IsSoundActive && _timerState.Remaining.TotalSeconds <= 10)
            {
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void UpdateCountdownDisplay()
        {
            CountdownLabel.Text = _timerState.Remaining.ToString(@"hh\:mm\:ss");
        }

        private void OnTimerFinished()
        {
            ShowInputControls();

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
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            _countdownTimer.Stop();
            if (_timerState != null)
                _timerState.IsActive = false;

            ShowInputControls();

            if (_onStopped != null)
                _onStopped();

            Close();
        }
    }
}
