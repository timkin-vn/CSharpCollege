using AlarmClock.Models;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace AlarmClock.Forms
{
    public partial class SettingsForm : Form
    {
        internal ClockSettings Settings { get; set; }

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            AlarmListBox.Items.Clear();
            for (int i = 0; i < Settings.AlarmTimes.Count; i++)
            {
                AlarmListBox.Items.Add(Settings.GetAlarmDisplayString(i));
            }

            IsSoundActiveCheckBox.Checked = Settings.IsSoundActive;
        }

        private void AddAlarmButton_Click(object sender, EventArgs e)
        {
            if (DateTime.TryParseExact(NewAlarmTimeTextBox.Text, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var alarmTime))
            {
                Settings.AlarmTimes.Add(alarmTime);
                Settings.AlarmTriggered.Add(false);
                Settings.AlarmMessages.Add(AlarmMessageTextBox.Text);

                AlarmListBox.Items.Add(Settings.GetAlarmDisplayString(Settings.AlarmTimes.Count - 1));

                NewAlarmTimeTextBox.Clear();
                AlarmMessageTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Неверно указано время срабатывания! Формат: HH:mm", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NewAlarmTimeTextBox.Focus();
                NewAlarmTimeTextBox.SelectAll();
            }
        }

        private void RemoveAlarmButton_Click(object sender, EventArgs e)
        {
            if (AlarmListBox.SelectedIndex != -1)
            {
                int selectedIndex = AlarmListBox.SelectedIndex;
                Settings.AlarmTimes.RemoveAt(selectedIndex);
                Settings.AlarmTriggered.RemoveAt(selectedIndex);
                Settings.AlarmMessages.RemoveAt(selectedIndex);
                AlarmListBox.Items.RemoveAt(selectedIndex);
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Settings.IsSoundActive = IsSoundActiveCheckBox.Checked;
            DialogResult = DialogResult.OK;
        }
    }
}