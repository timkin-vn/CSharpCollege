using AlarmClock.Forms;
using AlarmClock.Models;
using System;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace AlarmClock
{
    public partial class ClockForm : Form
    {
        public AlarmSettings _settings = new AlarmSettings();

        private AwakeForm _awakeForm = null;

        private AlarmTime _settingsAlarm = new AlarmTime();

        private EditForm _editForm = null;
        
        public ClockForm()
        {
            InitializeComponent();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            DisplayLabel.Text = DateTime.Now.ToLongTimeString();
            var timeSettings = _settings.TimeSettings
                .FirstOrDefault(ts => ts.IsAlarmActive && DateTime.Now.TimeOfDay >= ts.Times.TimeOfDay);
            //if (_settings.IsAlarmActive && DateTime.Now.TimeOfDay >= _settings.AlarmTime.TimeOfDay)
            if (timeSettings != null)
            {
                if (_awakeForm == null || _awakeForm.IsDisposed)
                {
                    _awakeForm = new AwakeForm();
                    _awakeForm.Settings = _settings;
                }

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

        private void AddItem_Click(object sender, EventArgs e)
        {
            var form = new SettingsForm();
            form.Settings = _settings;
            var newSetting = new AlarmTime();
            form.SettingsAlarm = newSetting;
            if (form.ShowDialog() == DialogResult.OK)
            {
                _settings.TimeSettings.Add(newSetting);
                RefreshGrid(_settings);
            }
        }
        private void ClockForm_Load(object sender, EventArgs e)
        {
            
        }

        private void RefreshGrid(AlarmSettings settings)
        {
            ListAlarmGrid.Rows.Clear();

            foreach (var u in settings.TimeSettings)
            {
                ListAlarmGrid.Rows.Add(u.Times, u.AlarmMessage, u.IsAlarmActive);
            }
        }


        private void DeleteItems_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in ListAlarmGrid.SelectedRows)
            {
                ListAlarmGrid.Rows.RemoveAt(row.Index);
                _settings.TimeSettings.RemoveAt(row.Index+1);
            }
            ListAlarmGrid.Refresh();
        }

        private void ChangeItems_Click(object sender, EventArgs e)
        {
            if (ListAlarmGrid.SelectedRows.Count == 0)
            {
                return;
            }

            int index = ListAlarmGrid.SelectedRows[0].Index;
            var currentSetting = _settings.TimeSettings[index];

            var editForm = new EditForm();
            editForm.Setting = currentSetting;

            if (editForm.ShowDialog() == DialogResult.OK)
            {
                _settings.TimeSettings[index] = editForm.Setting;
                RefreshGrid(_settings);
            }
        }
    }
}
