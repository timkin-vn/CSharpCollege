using AlarmClock.Forms;
using AlarmClock.Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AlarmClock
{
    public partial class ClockForm : Form
    {
        private AlarmClockState _clockState = new AlarmClockState();

        public ClockForm()
        {
            InitializeComponent();
        }

        private void ClockForm_Load(object sender, EventArgs e)
        {
            // Применяем сохранённую тему при загрузке
            var theme = ThemeManager.LoadTheme();
            ThemeManager.ApplyTheme(this, theme);
            UpdateView();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            DisplayLabel.Text = DateTime.Now.ToLongTimeString();

            if (!_clockState.IsAlarmActive)
            {
                return;
            }

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
            {
                return;
            }

            UpdateView();
        }

        private void DisableAlarmButton_Click(object sender, EventArgs e)
        {
            _clockState.IsAlarmActive = false;
            _clockState.IsAwakeActivated = false;
            UpdateView();
        }

        private void UpdateView()
        {
            if (_clockState.IsAlarmActive)
            {
                Text = string.Format("Будильник. Ожидается срабатывание в {0}", _clockState.AlarmTime.ToShortTimeString());
                DisableAlarmButton.Enabled = true;
            }
            else
            {
                Text = "Будильник";
                DisableAlarmButton.Enabled = false;
            }
        }

        /// <summary>
        /// Повторно применить тему после закрытия дочерней формы
        /// </summary>
        private void ApplyCurrentTheme()
        {
            var theme = ThemeManager.LoadTheme();
            ThemeManager.ApplyTheme(this, theme);
        }
    }
}
