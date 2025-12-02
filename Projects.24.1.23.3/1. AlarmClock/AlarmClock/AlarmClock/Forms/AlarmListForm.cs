using AlarmClock.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AlarmClock.Forms
{
    public partial class AlarmListForm : Form
    {
        internal List<AlarmSettings> Alarms { get; set; }

        public AlarmListForm()
        {
            InitializeComponent();
            this.Shown += (s, e) => RefreshList();
        }

        private void AlarmListForm_Load(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {
            AlarmsListBox.Items.Clear();

            foreach (var alarm in Alarms)
            {
                string text =
                    $"{alarm.AlarmTime:hh\\:mm} | " +
                    (alarm.IsAlarmActive ? "Активен" : "Неактивен") +
                    (string.IsNullOrWhiteSpace(alarm.AlarmMessage) ? "" : $" | {alarm.AlarmMessage}");

                AlarmsListBox.Items.Add(text);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            int index = AlarmsListBox.SelectedIndex;

            if (index < 0)
                return;

            Alarms.RemoveAt(index);
            RefreshList();
        }
    }
}
