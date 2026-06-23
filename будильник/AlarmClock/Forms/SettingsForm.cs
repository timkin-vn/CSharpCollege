using AlarmClock.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;


struct Save
{
    public string Time;
    public string Message;
    public bool IsActive;
    public bool HasSound;

    public Save(string time, string message, bool isActive, bool hasSound)
    {
        Time = time;
        Message = message;
        IsActive = isActive;
        HasSound = hasSound;
    }
    public override string ToString() =>
        $"{Time:hh\\:mm} - {Message} ({(IsActive ? "ON" : "OFF")}) ({(HasSound ? "SoundOn" : "SoundOff")})"; 
}


namespace AlarmClock.Forms
{
    
    public partial class SettingsForm : Form
    {
        private List<Save> savedAlarms = new List<Save>();
        internal AlarmSettings Settings { get; set; }

        public SettingsForm()
        {
            InitializeComponent();
        }
        private const string JsonFilePath = "alarms.json"; 

        
        private void SaveAlarmsToJson()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true }; 
                string json = JsonSerializer.Serialize(savedAlarms, options);
                File.WriteAllText(JsonFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void LoadAlarmsFromJson()
        {
            try
            {
                if (File.Exists(JsonFilePath))
                {
                    string json = File.ReadAllText(JsonFilePath);
                    var deserializedList = JsonSerializer.Deserialize<List<Save>>(json);
                    savedAlarms = deserializedList != null ? deserializedList : new List<Save>();

                    
                    SavedList.Items.Clear();
                    foreach (var alarm in savedAlarms)
                    {
                        SavedList.Items.Add(alarm);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SettingsForm_Load(object sender, EventArgs e)
        {
            AlarmTimeTextBox.Text = Settings.AlarmTime.ToString(@"h\:mm");
            AlarmMessageTextBox.Text = Settings.AlarmMessage;
            IsAlarmActiveCheckBox.Checked = Settings.IsAlarmActive;
            IsSoundActiveCheckBox.Checked = Settings.IsSoundActive;
            LoadAlarmsFromJson(); 
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!TimeSpan.TryParse(AlarmTimeTextBox.Text, out var alarmTime))
            {
                MessageBox.Show("Неверно задано время срабатывания!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AlarmTimeTextBox.Focus();
                AlarmTimeTextBox.SelectAll();
                return;
            }

            Settings.AlarmTime = new TimeSpan(alarmTime.Hours, alarmTime.Minutes, 0);
            Settings.AlarmMessage = AlarmMessageTextBox.Text;
            Settings.IsAlarmActive = IsAlarmActiveCheckBox.Checked;
            Settings.IsSoundActive = IsSoundActiveCheckBox.Checked;
            DialogResult = DialogResult.OK;

        }

        

        private void btnsave_Click(object sender, EventArgs e)
        {
            var alarm = new Save(
            time: AlarmTimeTextBox.Text,
            message: AlarmMessageTextBox.Text,
            isActive: IsAlarmActiveCheckBox.Checked,
            hasSound: IsSoundActiveCheckBox.Checked);
            

            
            savedAlarms.Add(alarm);
            SavedList.Items.Add(alarm);
            SaveAlarmsToJson();
        }
        private void SavedList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = savedAlarms[SavedList.SelectedIndex];
            AlarmTimeTextBox.Text = selected.Time;
            AlarmMessageTextBox.Text = selected.Message;
            IsAlarmActiveCheckBox.Checked = selected.IsActive;
            IsSoundActiveCheckBox.Checked = selected.HasSound;
           
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Очистить все будильники?", "Подтвердите", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            savedAlarms.Clear();
            SavedList.Items.Clear();
            SaveAlarmsToJson(); 
        }
    }
}
