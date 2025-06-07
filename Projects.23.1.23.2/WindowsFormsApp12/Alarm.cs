using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace WindowsFormsApp12
{
    public partial class Alarm : Form
    {
        private System.Timers.Timer timer;
        private DateTime alarmTime;
        private bool alarmActive = false;
        private SoundPlayer player;
        public Alarm()
        {
            InitializeComponent();
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += CheckAlarm;
            player = new SoundPlayer("C:/Users/USER/Desktop/alarmp.wav");
        }


        private void CheckAlarm(object sender, ElapsedEventArgs e)
        {
            if (alarmActive && DateTime.Now >= alarmTime)
            {
                alarmActive = false; // Отключаем будильник

                this.Invoke(new Action(() =>
                {
                    try
                    {
                        player.PlaySync(); // Попробуйте PlaySync для тестирования
                        MessageBox.Show("Будильник сработал!");
                        labelStatus.Text = "Будильник сработал!";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка воспроизведения звука: {ex.Message}");
                    }
                }));

                timer.Stop(); // Останавливаем таймер
            }
        }

        private async Task UpdateTime()
        {
            while (true)
            {
                label1.Text = DateTime.Now.ToString("HH:mm:ss");
                await Task.Delay(1000); // Задержка на 1 секунду
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await UpdateTime();
        }

        private void buttonSetAlarm_Click_1(object sender, EventArgs e)
        {
            if (TimeSpan.TryParse(textBoxTime.Text, out TimeSpan time))
            {
                alarmTime = DateTime.Today + time;

                if (alarmTime < DateTime.Now)
                {
                    alarmTime = alarmTime.AddDays(1); // Установка на следующий день
                }

                labelStatus.Text = $"Будильник установлен на {alarmTime.TimeOfDay}";
                alarmActive = true;
                timer.Start();
            }
            else
            {
                MessageBox.Show("Введите время в формате чч:мм.");
            }
        }

        private void buttonSnooze_Click_1(object sender, EventArgs e)
        {
            if (!alarmActive && labelStatus.Text.Contains("сработал"))
            {
                alarmTime = alarmTime.AddMinutes(10); // Отложить на 10 минут
                labelStatus.Text = $"Будильник отложен на 10 минут. Новое время: {alarmTime.TimeOfDay}";
                alarmActive = true; // Активируем будильник снова
                timer.Start(); // Запускаем таймер
            }
        }

        private void labelStatus_Click(object sender, EventArgs e)
        {

        }
    }
}