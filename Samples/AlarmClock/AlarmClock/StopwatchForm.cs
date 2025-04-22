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

namespace AlarmClock
{
    public partial class StopwatchForm : Form
    {
        private List<string> lapTimes = new List<string>();
        private Stopwatch stopwatch = new Stopwatch();
        private Timer timer = new Timer();
        private int lapCount = 0;
        private TimeSpan lastLapTime = TimeSpan.Zero;

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
            // Получаем текущее время
            TimeSpan currentLapTime = stopwatch.Elapsed;

            // Вычисляем время прошедшее с последнего нажатия
            TimeSpan interval = currentLapTime - lastLapTime;

            // Форматируем строку с промежуточным временем
            string lapTime = string.Format("Промежуточное время lap{0}: {1:00}:{2:00}.{3:00}",
                                            ++lapCount,
                                            interval.Minutes,
                                            interval.Seconds,
                                            interval.Milliseconds / 10); // Для отображения сотых секунд

            // Обновляем последнее зафиксированное время
            lastLapTime = currentLapTime;

            // Добавляем промежуточное время в текстовое поле или список
            
            MessageBox.Show(lapTime + Environment.NewLine);
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            stopwatch.Stop();

            // Сбрасываем таймер
            stopwatch.Reset();

            // Обновляем отображение времени на экране
            labelTime.Text = "00:00:00";

            // Сбрасываем промежуточные данные, если нужно
            lapCount = 0;
            lapTimes.Clear(); // Очистка списка промежуточных времён
        }

    }
}
