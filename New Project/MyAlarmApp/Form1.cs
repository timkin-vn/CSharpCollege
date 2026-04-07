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
        private Label lblCurrentTime;
        private System.Windows.Forms.Timer checkTimer;
        private DateTime alarmTime;
        private bool isAlarmActive = false;

        public Form1()
        {
            // Настройка окна
            this.Text = "Демократичный будильник";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            try {
                this.BackgroundImage = Image.FromFile("Helldivers 2.jpg");
                this.BackgroundImageLayout = ImageLayout.Stretch;
            } catch {}

            // 1. ЖИВЫЕ ЧАСЫ (Секунды, минуты)
            lblCurrentTime = new Label();
            lblCurrentTime.Text = DateTime.Now.ToString("Ч:М:С");
            lblCurrentTime.Font = new Font("Arial", 28, FontStyle.Bold);
            lblCurrentTime.ForeColor = Color.Yellow;
            lblCurrentTime.BackColor = Color.Transparent;
            lblCurrentTime.TextAlign = ContentAlignment.MiddleCenter;
            lblCurrentTime.Location = new Point(50, 50);
            lblCurrentTime.Size = new Size(300, 60);

            // 2. ВЫБОР ВРЕМЕНИ
            timePicker = new DateTimePicker();
            timePicker.Format = DateTimePickerFormat.Time;
            timePicker.ShowUpDown = true;
            timePicker.Location = new Point(100, 150);
            timePicker.Size = new Size(200, 30);

            // 3. КНОПКА
            btnSetAlarm = new Button();
            btnSetAlarm.Text = "УСТАНОВИТЬ";
            btnSetAlarm.Font = new Font("Arial", 10, FontStyle.Bold);
            btnSetAlarm.Location = new Point(100, 200);
            btnSetAlarm.Size = new Size(200, 40);
            btnSetAlarm.Click += BtnSetAlarm_Click;

            // 4. СТАТУС
            lblStatus = new Label();
            lblStatus.Text = "Будильник не установлен";
            lblStatus.ForeColor = Color.White;
            lblStatus.BackColor = Color.Black;
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            lblStatus.Location = new Point(50, 260);
            lblStatus.Size = new Size(300, 30);

            // 5. ТАЙМЕР (Запускаем сразу для часов)
            checkTimer = new System.Windows.Forms.Timer();
            checkTimer.Interval = 1000;
            checkTimer.Tick += CheckTimer_Tick;
            checkTimer.Start();

            // Добавляем на форму
            this.Controls.Add(lblCurrentTime);
            this.Controls.Add(timePicker);
            this.Controls.Add(btnSetAlarm);
            this.Controls.Add(lblStatus);
        }

        private void BtnSetAlarm_Click(object sender, EventArgs e)
        {
            alarmTime = timePicker.Value;
            if (DateTime.Now > alarmTime) alarmTime = alarmTime.AddDays(1);

            isAlarmActive = true; 
            lblStatus.Text = $"Ждем: {alarmTime:HH:mm:ss}";
            MessageBox.Show("Будильник запущен!");
        }

        private void CheckTimer_Tick(object sender, EventArgs e)
        {
            // Обновляем время каждую секунду
            lblCurrentTime.Text = DateTime.Now.ToString("HH:mm:ss");

            // Проверяем будильник
            if (isAlarmActive && DateTime.Now >= alarmTime)
            {
                isAlarmActive = false;
                lblStatus.Text = "ВРЕМЯ ВЫШЛО!";
                
                try {
                    SoundPlayer player = new SoundPlayer("Muz.wav");
                    player.PlayLooping();
                    if (MessageBox.Show("ПОДЪЕМ!", "Будильник", MessageBoxButtons.OK) == DialogResult.OK)
                        player.Stop();
                } catch {
                    Console.Beep(1000, 2000);
                }
            }
        }
    }
}
