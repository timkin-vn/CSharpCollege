using AlarmClock.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlarmClock.Forms
{
    public partial class Stopwatch : Form
    {
        private readonly System.Diagnostics.Stopwatch _stopwatch;
        public Stopwatch()
        {
            InitializeComponent();
            _stopwatch = new System.Diagnostics.Stopwatch();
        }       

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Time.Text = _stopwatch.Elapsed.ToString(@"hh\:mm\:ss\.ff");
        }
        private void StartTimer_Click(object sender, EventArgs e)
        {
            if (!Timer1.Enabled) 
            {
                _stopwatch.Start();
                Timer1.Start();
                StartTimer.Text = "Стоп";
            }
            else
            {
                _stopwatch.Stop();
                Timer1.Stop();
                StartTimer.Text = "Старт";
            }
        }

        private void Restar_Click(object sender, EventArgs e)
        {
            if (Timer1.Enabled || !!Time.Enabled)
            {
                _stopwatch.Stop();
                Timer1.Stop();
                StartTimer.Text = "Старт";
                _stopwatch.Reset();
                Time.Text = "00:00:00.00";
            }
        }
    }
}
