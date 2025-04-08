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
    public partial class CoutdownTimerForm : Form
    {
        private DateTime endTime;
        private bool isTimerRunning = false;

        public CoutdownTimerForm()
        {
            InitializeComponent();
        }

        private void CoutndownTimerStartButton_Click(object sender, EventArgs e)
        {
            int hours = int.TryParse(CoutdownTimerTextBoxHours.Text, out hours) ? hours : 0;
            int minutes = int.TryParse(CoutdownTimerTextBoxMinutes.Text, out minutes) ? minutes : 0;
            int seconds = int.TryParse(CoutdownTimerTextBoxSeconds.Text, out seconds) ? seconds : 0;

            TimeSpan duration = new TimeSpan(hours, minutes, seconds);
            if (duration.TotalSeconds <= 0)
            {
                MessageBox.Show("Введите корректное время!");
                return;
            }

            endTime = DateTime.Now.Add(duration);
            isTimerRunning = true;
            CoutdownTimerTimer.Interval = 100; // можно даже 100мс — точно и плавно
            CoutdownTimerTimer.Tick -= CoutdownTimerTimer_Tick;
            CoutdownTimerTimer.Tick += CoutdownTimerTimer_Tick;
            CoutdownTimerTimer.Start();
        }

        private void CoutdownTimerTimer_Tick(object sender, EventArgs e)
        {
            if (!isTimerRunning) return;

            TimeSpan remaining = endTime - DateTime.Now;

            if (remaining.TotalSeconds > 0)
            {
                CoutdownTimerTimeLabel.Text = remaining.ToString(@"hh\:mm\:ss");
            }
            else
            {
                CoutdownTimerTimer.Stop();
                isTimerRunning = false;
                CoutdownTimerTimeLabel.Text = "00:00:00";
                System.Media.SystemSounds.Beep.Play();
                MessageBox.Show("Время вышло!");
            }
        }
    }
}
