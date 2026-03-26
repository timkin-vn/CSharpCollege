using AlarmClock.Forms;
using AlarmClock.Model;
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
    public partial class ClockForm : Form
    {
        private AlarmClockState _clockState = new AlarmClockState();

        public ClockForm()
        {
            InitializeComponent();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            // 1. ЛОГИКА МИРОВОГО ВРЕМЕНИ
            // Проверяем: если галочка стоит, берем UtcNow, если нет - обычный Now
            DateTime timeToShow = UtcCheckBox.Checked ? DateTime.UtcNow : DateTime.Now;

            // Выводим время на экран
            DisplayLabel.Text = timeToShow.ToLongTimeString();

            // 2. ЛОГИКА БУДИЛЬНИКА (оставляем твою рабочую версию)
            if (!_clockState.IsAlarmActive) return;

            // Сравниваем всегда с местным временем (DateTime.Now), 
            // чтобы будильник не "прыгал" при переключении режима отображения
            if (!_clockState.IsAwakeActivated &&
                DateTime.Now.Minute == _clockState.AlarmTime.Minute &&
                DateTime.Now.Hour == _clockState.AlarmTime.Hour)
            {
                _clockState.IsAwakeActivated = true;

                var awakeForm = new AwakeForm { ClockState = _clockState };
                awakeForm.FormClosed += AwakeForm_FormClosed;
                awakeForm.ShowDialog();
            }

            // Звук
            if (_clockState.IsSoundActive && _clockState.IsAwakeActivated)
            {
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void AwakeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form awakeForm = (Form)sender;
            awakeForm.FormClosed -= AwakeForm_FormClosed;

            // ПРОВЕРКА: Если в Tag написано "Snooze", мы НЕ выключаем будильник
            if (awakeForm.Tag?.ToString() != "Snooze")
            {
                _clockState.IsAlarmActive = false;
            }

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

        private void UpdateView()
        {
            if (_clockState.IsAlarmActive)
            {
                Text = $"Будильник. Ожидается срабатывание в {_clockState.AlarmTime.ToShortTimeString()}";
            }
            else
            {
                Text = "Будильник";
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
