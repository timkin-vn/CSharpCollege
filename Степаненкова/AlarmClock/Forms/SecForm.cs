using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alarm_clock_C_.Forms
{
    public partial class SecForm : Form
    {
        private TimeSpan elapsedTime;
        public SecForm()
        {
            InitializeComponent();
            SecTimer = new Timer();
            SecTimer.Interval = 10;
            SecTimer.Tick += SecTimer_Tick;
        }

        private void StartSec_Click(object sender, EventArgs e)
        {
            SecTimer.Start();
        }

        private void StopSec_Click(object sender, EventArgs e)
        {
            SecTimer.Stop();
        }

        private void ClearSec_Click(object sender, EventArgs e)
        {
            SecTimer.Stop();
            elapsedTime = TimeSpan.Zero;
            UpdateSecLabel();
        }

        private void SecTimer_Tick(object sender, EventArgs e)
        {
            elapsedTime = elapsedTime.Add(TimeSpan.FromSeconds(1));
            UpdateSecLabel();
        }

        private void UpdateSecLabel()
        {
            SecLabel.Text = elapsedTime.ToString(@"hh\:mm\:ss");
        }
    }
}
