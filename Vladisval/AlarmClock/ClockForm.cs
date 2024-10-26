using AlarmClock.Forms;
using AlarmClock.Models;
using AlarmClock.Properties;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AlarmClock
{
    public partial class ClockForm : Form
    {
        private AlarmSettings _settings = new AlarmSettings();

        private AwakeForm _awakeForm = null;
        private readonly int totalSeconds;

        public AlarmSettings Settings { get; set; }

        public ClockForm()
        {
            InitializeComponent();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            DisplayLabel.Text = DateTime.Now.ToLongTimeString();
            if (_settings.IsAlarmActive && DateTime.Now.TimeOfDay >= _settings.AlarmTime.TimeOfDay)
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            
            string timeString = time1.Text;
            string timeString2 = time2.Text;
            try
            {
              
                string[] timeParts = timeString.Split(':');

                int hours = int.Parse(timeParts[0]);
                int minutes = int.Parse(timeParts[1]);
                int seconds = int.Parse(timeParts[2]);
                int totalSeconds = (hours * 3600) + (minutes * 60) + seconds;

                string[] timeParts2 = timeString2.Split(':');

                int hours2 = int.Parse(timeParts2[0]);
                int minutes2 = int.Parse(timeParts2[1]);
                int seconds2 = int.Parse(timeParts2[2]);
                int totalSeconds2 = (hours2 * 3600) + (minutes2 * 60) + seconds2;
                if(totalSeconds> totalSeconds2)
                {
                    int res = totalSeconds - totalSeconds2;

                    int hoursResult = res / 3600;
                    int minutesResult = (res % 3600) / 60;
                    int secondsResult = res % 60;

                    resault.Text = $"{hoursResult:D2}:{minutesResult:D2}:{secondsResult:D2}";
                }
                else
                {
                    MessageBox.Show("Неправильный формат ввода! попробуйте точки поменять местами. (Ожидалось конечная точка > начальной точки!)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               
            }
            catch { };


        }

        public void textBox1_TextChanged(object sender, EventArgs e)
        {}

        private void textBox2_TextChanged(object sender, EventArgs e)
        {}

        private void resault_Click(object sender, EventArgs e)
        {

        }

        private void ClockForm_Load(object sender, EventArgs e)
        {

        }
    }
}
