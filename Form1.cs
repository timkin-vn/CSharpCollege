using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text.Json;
using System.Windows.Forms;

namespace AlarmClockApp {
    public partial class Form1 : Form {
        private DateTime alarmDateTime;
        private Timer timer;
        private string soundPath = "";
        private HashSet<DayOfWeek> repeatDays = new HashSet<DayOfWeek>();
        private bool darkTheme = false;
        private NotifyIcon trayIcon;
        private string configPath = "settings.json";
        private Timer timerUpdateTime;

        public Form1()
        {
            InitializeComponent();
            this.button1.Click += new System.EventHandler(this.SaveSettings);
            InitializeAlarmControls();
            LoadSettings();
            SetupTray();

            timerUpdateTime = new Timer(); // Инициализация таймера
            timerUpdateTime.Interval = 1000;
            timerUpdateTime.Tick += UpdateTimeLabel;
            timerUpdateTime.Start();
        }

        private void UpdateTimeLabel(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private DateTimePicker dateTimePicker;
        private ComboBox soundComboBox;
        private CheckBox[] dayCheckboxes;
        private Button saveButton, themeButton, customSoundButton;

        private void InitializeAlarmControls() {
            this.Text = "Будильник";
            this.Size = new Size(350, 450);

            dateTimePicker = new DateTimePicker {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd.MM.yyyy HH:mm",
                Location = new Point(20, 20),
                Width = 200
            };
            this.Controls.Add(dateTimePicker);

            dayCheckboxes = new CheckBox[7];
            for (int i = 0; i < 7; i++) {
                dayCheckboxes[i] = new CheckBox {
                    Text = ((DayOfWeek)i).ToString(),
                    Location = new Point(20, 60 + i * 25),
                    Width = 150
                };
                this.Controls.Add(dayCheckboxes[i]);
            }

            soundComboBox = new ComboBox {
                Location = new Point(20, 250),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            soundComboBox.Items.AddRange(new string[] { "alarm.wav", "ring.wav" });
            this.Controls.Add(soundComboBox);

            customSoundButton = new Button {
                Text = "Выбрать звук",
                Location = new Point(230, 250),
                Width = 90
            };
            customSoundButton.Click += ChooseCustomSound;
            this.Controls.Add(customSoundButton);

            saveButton = new Button {
                Text = "Установить будильник",
                Location = new Point(20, 300)
            };
            saveButton.Click += SaveSettings;
            this.Controls.Add(saveButton);

            themeButton = new Button
            {
                Text = "Тема",
                Location = new Point(120, 300)
            };
            themeButton.Click += ToggleTheme;
            this.Controls.Add(themeButton);
        }

        private void ChooseCustomSound(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "WAV files|*.wav"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                soundPath = ofd.FileName;
            }
        }

        private void StartTimer()
        {
            if (timer != null)
                timer.Stop();

            alarmDateTime = CalculateNextAlarmTime(dateTimePicker.Value, repeatDays);

            timer = new Timer { Interval = 1000 };
            timer.Tick += (s, e) =>
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() =>
                    {
                        if (DateTime.Now >= alarmDateTime && (!repeatDays.Any() || repeatDays.Contains(DateTime.Now.DayOfWeek)))
                        {
                            TriggerAlarm();
                        }
                    }));
                }
            };
            timer.Start();
        }

        private void TriggerAlarm()
        {
            AlarmPopupForm popup = new AlarmPopupForm();
            popup.Show();

            try
            {
                SoundPlayer player = string.IsNullOrEmpty(soundPath)
                    ? new SoundPlayer(soundComboBox.SelectedItem?.ToString() ?? "alarm.wav")
                    : new SoundPlayer(soundPath);
                player.PlayLooping();
            }
            catch { }

            if (repeatDays.Any())
            {
                alarmDateTime = CalculateNextAlarmTime(alarmDateTime.AddSeconds(1), repeatDays);
                timer.Start();
            }
        }

        private void SaveSettings(object sender, EventArgs e)
        {
            MessageBox.Show("Метод SaveSettings вызван!"); // Для отладки

            alarmDateTime = CalculateNextAlarmTime(dateTimePicker.Value, repeatDays);
            repeatDays.Clear();
            foreach (CheckBox cb in dayCheckboxes)
            {
                if (cb.Checked)
                    repeatDays.Add((DayOfWeek)Enum.Parse(typeof(DayOfWeek), cb.Text));
            }

            var config = new
            {
                AlarmTime = alarmDateTime,
                RepeatDays = repeatDays,
                Sound = soundPath,
                DarkTheme = darkTheme
            };
            File.WriteAllText(configPath, JsonSerializer.Serialize(config));

            if (timer != null)
                timer.Stop();
            StartTimer();
        }

        private DateTime CalculateNextAlarmTime(DateTime current, HashSet<DayOfWeek> repeatDays)
        {
            if (!repeatDays.Any())
            {
                return current < DateTime.Now ? current.AddDays(1) : current;
            }

            DateTime next = current;
            while (true)
            {
                if (next > DateTime.Now && repeatDays.Contains(next.DayOfWeek))
                {
                    return next;
                }
                next = next.AddDays(1);
            }
        }

        private void LoadSettings()
        {
            if (!File.Exists(configPath)) return;
            try {
                var configText = File.ReadAllText(configPath);
                var config = JsonSerializer.Deserialize<JsonElement>(configText);

                alarmDateTime = CalculateNextAlarmTime(alarmDateTime, repeatDays);
                dateTimePicker.Value = alarmDateTime;

                repeatDays = new HashSet<DayOfWeek>();
                foreach (var day in config.GetProperty("RepeatDays").EnumerateArray()) {
                    if (Enum.TryParse(day.GetString(), out DayOfWeek d))
                        repeatDays.Add(d);
                }
                foreach (CheckBox cb in dayCheckboxes)
                    cb.Checked = repeatDays.Contains((DayOfWeek)Enum.Parse(typeof(DayOfWeek), cb.Text));

                soundPath = config.GetProperty("Sound").GetString();
                darkTheme = config.GetProperty("DarkTheme").GetBoolean();

                if (darkTheme) SetDarkTheme(true);
                StartTimer();
            }
            catch { }
        }

        private void ToggleTheme(object sender, EventArgs e) {
            darkTheme = !darkTheme;
            SetDarkTheme(darkTheme);
        }

        private void SetDarkTheme(bool enabled) {
            this.BackColor = enabled ? Color.FromArgb(30, 30, 30) : SystemColors.Control;
            foreach (Control c in this.Controls) {
                c.ForeColor = enabled ? Color.White : Color.Black;
                c.BackColor = enabled ? Color.FromArgb(30, 30, 30) : SystemColors.Control;
            }
        }

        private void SetupTray() {
            trayIcon = new NotifyIcon {
                Icon = SystemIcons.Information,
                Text = "Будильник",
                Visible = true
            };

            trayIcon.DoubleClick += (s, e) => this.Show();
            this.Resize += (s, e) => {
                if (this.WindowState == FormWindowState.Minimized) {
                    this.Hide();
                    trayIcon.ShowBalloonTip(1000, "Будильник", "Приложение свернуто в трей", ToolTipIcon.Info);
                }
            };
        }
    }
}