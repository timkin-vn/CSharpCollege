using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlarmClock
{
    public partial class StopwatchForm : Form
    {
        private Stopwatch stopwatch = new Stopwatch();
        private Timer timer = new Timer();

        public StopwatchForm()
        {
            InitializeComponent();
            timer.Interval = 1000; // 1 секунда
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Обновляем Label с прошедшим временем
            labelTime.Text = stopwatch.Elapsed.ToString(@"hh\:mm\:ss");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            stopwatch.Start();
            timer.Start();
        }

        private void btnLap_Click(object sender, EventArgs e)
        {
            // Здесь можно добавить логику для промежуточных итогов
            MessageBox.Show($"Промежуточный итог: {labelTime.Text}");
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            stopwatch.Stop();
            timer.Stop();
        }
        private void btnBackToClock_Click(object sender, EventArgs e)
        {
            ClockForm clockForm = new ClockForm();
            clockForm.Show();
            this.Close(); // Закрывают текущую форму
        }
    }
}
