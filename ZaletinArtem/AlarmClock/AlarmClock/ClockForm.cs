using AlarmClock.Forms;
using AlarmClock.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using System.Linq;

namespace AlarmClock
{
    public partial class ClockForm : Form
    {
        private AlarmSettings _settings = new AlarmSettings();

        private AwakeForm _awakeForm;

        //Timer
        private List<DateTime> _alarmTimes = new List<DateTime>();
        private List<Panel> _panels = new List<Panel>();
        private Label _timerText;
        private int _xPosition = 102;

        public ClockForm()
        {
            InitializeComponent();
        }

        private void AlarmTimer_Tick(object sender, EventArgs e)
        {
            DisplayLabel.Text = DateTime.Now.ToLongTimeString();

            foreach (var alarmTime in _alarmTimes)
            {
                if (DateTime.Now.TimeOfDay >= alarmTime.TimeOfDay &&
                    DateTime.Now.TimeOfDay < alarmTime.TimeOfDay.Add(TimeSpan.FromSeconds(1)))
                {
                    if (_awakeForm == null || _awakeForm.IsDisposed)
                    {
                        _awakeForm = new AwakeForm();
                    }

                    _awakeForm.Settings = _settings;
                    _awakeForm.Show();

                    SystemSounds.Beep.Play();

                    break;
                }
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            var panel = new Panel();
            panel.Location = new Point(6, _xPosition + 80);
            panel.Parent = flowLayoutPanel1;
            panel.Size = new Size(355, 75);
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.BackColor = Color.Black;

            _panels.Add(panel);

            var timeText = new Label();
            timeText.Parent = panel;
            timeText.Size = new Size(181, 61);
            timeText.Location = new Point(11, 7);
            timeText.BackColor = Color.Black;
            timeText.ForeColor = Color.White;
            timeText.Text = "00:00";
            timeText.Font = new Font("Verdana", 36, FontStyle.Bold);

            _timerText = timeText;

            var editButton = new Button();
            editButton.Parent = panel;
            editButton.Size = new Size(136, 28);
            editButton.Location = new Point(208, 7);
            editButton.BackColor = Color.Black;
            editButton.ForeColor = Color.White;
            editButton.Font = new Font("Verdana", 9, FontStyle.Bold);
            editButton.Text = "Редактировать";
            editButton.Click += (s, eventArgs) => EditButton_Click(s, e, panel);

            var delButton = new Button();
            delButton.Parent = panel;
            delButton.Size = new Size(136, 28);
            delButton.Location = new Point(208, 39);
            delButton.BackColor = Color.Black;
            delButton.ForeColor = Color.White;
            delButton.Font = new Font("Verdana", 9, FontStyle.Bold);
            delButton.Text = "Удалить";
            delButton.Click += new EventHandler(DeleteButton_Click);

            var form = new SettingsForm();
            form.Settings = _settings;
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                _alarmTimes.Add(_settings.AlarmTime);
                _timerText.Text = _settings.AlarmTime.TimeOfDay.ToString();
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var button = sender as Button;

            if (button != null)
            {
                var panelToRemove = button.Parent as Panel;

                if (panelToRemove != null)
                {
                    int panelIndex = _panels.IndexOf(panelToRemove);

                    _panels.RemoveAt(panelIndex);
                    flowLayoutPanel1.Controls.Remove(panelToRemove);

                    if (panelIndex < _alarmTimes.Count)
                    {
                        _alarmTimes.RemoveAt(panelIndex);
                    }

                    int currentYPosition = 102;
                    foreach (var panel in _panels)
                    {
                        panel.Location = new Point(panel.Location.X, currentYPosition);
                        currentYPosition += 80;
                    }
                }
            }
        }

        private void EditButton_Click(object sender, EventArgs eventArgs, Panel panel)
        {
            int panelIndex = _panels.IndexOf(panel);
            if (panelIndex == -1 || panelIndex >= _alarmTimes.Count)
                return;

            DateTime currentAlarmTime = _alarmTimes[panelIndex];

            var form = new SettingsForm();
            form.Settings = new AlarmSettings
            {
                AlarmTime = currentAlarmTime
            };

            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                _alarmTimes[panelIndex] = form.Settings.AlarmTime;
                var label = panel.Controls.OfType<Label>().FirstOrDefault();
                if (label != null)
                {
                    label.Text = form.Settings.AlarmTime.TimeOfDay.ToString();
                }
            }

        }
        private void About_Click(object sender, EventArgs e)
        {
            var alarm = new AboutForm();
            alarm.ShowDialog();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
