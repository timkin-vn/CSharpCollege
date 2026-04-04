using System;
using System.Media;
using System.Windows.Forms;
using System.Drawing;

namespace MyAlarmApp
{
    public partial class Form1 : Form
    {
        private DateTimePicker timePicker;
        private Button btnSetAlarm;
        private Label lblStatus;
        private System.Windows.Forms.Timer checkTimer;
        private DateTime alarmTime;

        public Form1()
        {
            // Настройка окна
            this.Text = "C# Будильник";
            this.Size = new Size(1000, 800);
            this.StartPosition = FormStartPosition.CenterScreen;

            //ПНГ картинка
            this.Text = "Демократичный будильник";
            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
    try 
    {
        this.BackgroundImage = Image.FromFile("Helldivers 2.jpg");
        this.BackgroundImageLayout = ImageLayout.Stretch; 
    }
    catch (Exception ex)
    {
        Console.WriteLine("Ошибка загрузки фона: " + ex.Message);
    }
    lblStatus = new Label();
    lblStatus.BackColor = Color.Transparent;

            // Выбор времени
            timePicker = new DateTimePicker();
            timePicker.Format = DateTimePickerFormat.Time;
            timePicker.ShowUpDown = true;
            timePicker.Location = new Point(60, 250);
            timePicker.Size = new Size(180, 30);

            // Кнопка
            btnSetAlarm = new Button();
            btnSetAlarm.Text = "Установить будильник";
            btnSetAlarm.Location = new Point(60, 300);
            btnSetAlarm.Size = new Size(180, 30);
            btnSetAlarm.Click += BtnSetAlarm_Click;

            // Статус
            lblStatus = new Label();
            lblStatus.Text = "Будильник не установлен";
            lblStatus.Location = new Point(25, 350);
            lblStatus.Size = new Size(250, 30);
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;

            // Таймер проверки (тикает раз в секунду)
            checkTimer = new System.Windows.Forms.Timer();
            checkTimer.Interval = 1000;
            checkTimer.Tick += CheckTimer_Tick;
            // Добавляем элементы на форму
            this.Controls.Add(timePicker);
            this.Controls.Add(btnSetAlarm);
            this.Controls.Add(lblStatus);
        }

        private void BtnSetAlarm_Click(object sender, EventArgs e)
        {
            alarmTime = timePicker.Value;
            
            if (DateTime.Now > alarmTime)
                alarmTime = alarmTime.AddDays(1);

            lblStatus.Text = $"Установлено на: {alarmTime:HH:mm}";
            checkTimer.Start();
            MessageBox.Show("Будильник запущен!");
        }

        private void CheckTimer_Tick(object sender, EventArgs e)
{
    if (DateTime.Now >= alarmTime)
    {
        checkTimer.Stop();
        lblStatus.Text = "ВРЕМЯ ВЫШЛО!";

        try 
        {
            SoundPlayer player = new SoundPlayer("Muz.wav");
            player.PlayLooping();
            
            DialogResult result = MessageBox.Show("ПОДЪЕМ!", "Будильник", MessageBoxButtons.OK);
            
            if (result == DialogResult.OK)
            {
                player.Stop();
            }
        }
        catch (Exception ex)
        {
            Console.Beep(1000, 2000);
            MessageBox.Show("Ошибка звука: " + ex.Message);
        }
    }
}
    }
}