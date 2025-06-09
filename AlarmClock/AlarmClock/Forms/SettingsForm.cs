﻿using AlarmClock.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlarmClock.Forms
{
    public partial class SettingsForm : Form
    {
        public AlarmSettings Settings { get; set; }

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!DateTime.TryParse(AlarmTimeTextBox.Text, out var alarmTime))
            {
                MessageBox.Show("Неверно задано время срабатывания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Settings.AlarmTime = alarmTime;
            Settings.AlarmMessage = AlarmMessageTextBox.Text;
            Settings.IsAlarmActive = IsAlarmActiveCheckBox.Checked;
            Settings.IsSoundActive = IsSoundActiveCheckBox.Checked;

            DialogResult = DialogResult.OK;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            AlarmTimeTextBox.Text = Settings.AlarmTime.ToString("H:mm");
            AlarmMessageTextBox.Text = Settings.AlarmMessage;
            IsAlarmActiveCheckBox.Checked = Settings.IsAlarmActive;
            IsSoundActiveCheckBox.Checked = Settings.IsSoundActive;
        }
    }
}
