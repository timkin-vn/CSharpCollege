using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace AlarmClock.Forms
{
    public partial class StopwatchForm : Form
    {
        private DateTime starttime;
        private DateTime pauseTime;
        public StopwatchForm()
        {
            InitializeComponent();
        }

        private void StopwatchTimer_Tick(object sender, EventArgs e)
        {
            DateTime time = new DateTime(0, 0);
            time=time.Add(DateTime.Now.Subtract(starttime));
            DisplayLabel.Text = time.ToString("mm:ss:ff");
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            starttime = DateTime.Now;
            DisplayLabel.Text = (starttime - DateTime.Now.TimeOfDay).ToString("HH:mm:ss");
            StopwatchTimer.Start();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            starttime = new DateTime(0, 0);
            StopwatchTimer.Stop();
            DisplayLabel.Text = starttime.ToString("HH:mm:ss");
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            if (StopwatchTimer.Enabled)
            {
                pauseTime = DateTime.Now;
                StopwatchTimer.Stop();
                return;
            }
            starttime = starttime.Add(DateTime.Now.Subtract(pauseTime));
            StopwatchTimer.Start();
        }
    }
}
