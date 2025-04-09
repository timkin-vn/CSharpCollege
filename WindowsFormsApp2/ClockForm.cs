using AlarmClock.Forms;
using AlarmClock.Models;
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using System.Drawing.Drawing2D;


namespace AlarmClock
{
    public partial class ClockForm : Form
    {
        private ClockSettings _settings = new ClockSettings();
        private readonly Timer _clockTimer = new Timer();
        private readonly Pen _hourHandPen = new Pen(Color.Black, 6);
        private readonly Pen _minuteHandPen = new Pen(Color.Blue, 4);
        private readonly Pen _secondHandPen = new Pen(Color.Red, 2);
        private readonly Pen _alarmHandPen = new Pen(Color.Green, 3);
        private readonly Brush _clockFaceBrush = new SolidBrush(Color.WhiteSmoke);
        private readonly Brush _hourMarkBrush = new SolidBrush(Color.Black);
        private readonly Font _hourFont = new Font("Arial", 10, FontStyle.Bold);

        public ClockForm()
        {
            InitializeComponent();
            

            var initialTime = _settings.AlarmTime;
            _settings.AlarmTime = new TimeSpan(initialTime.Hours, initialTime.Minutes, 0);

            // Настройка таймера
            _clockTimer.Interval = 1000; // Обновление каждую секунду
            _clockTimer.Tick += ClockTimer_Tick;
            _clockTimer.Start();

            // Настройка формы
            this.DoubleBuffered = true;
            this.Resize += (s, e) => this.Invalidate();
        }



        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawClock(e.Graphics);
        }

        private void DrawClock(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int width = this.ClientSize.Width - 150; // Оставляем место для кнопок
            int height = this.ClientSize.Height;
            int centerX = width / 2;
            int centerY = height / 2;
            int radius = Math.Min(centerX, centerY) - 20;

            // Рисуем циферблат
            g.FillEllipse(_clockFaceBrush, centerX - radius, centerY - radius, radius * 2, radius * 2);
            g.DrawEllipse(Pens.Black, centerX - radius, centerY - radius, radius * 2, radius * 2);

            // Рисуем цифры и деления
            for (int i = 1; i <= 12; i++)
            {
                double angle = i * 30 - 90;
                double rad = angle * Math.PI / 180;

                // Большие деления для часов
                int x1 = centerX + (int)((radius - 15) * Math.Cos(rad));
                int y1 = centerY + (int)((radius - 15) * Math.Sin(rad));
                int x2 = centerX + (int)(radius * Math.Cos(rad));
                int y2 = centerY + (int)(radius * Math.Sin(rad));
                g.DrawLine(Pens.Black, x1, y1, x2, y2);

                // Цифры
                SizeF textSize = g.MeasureString(i.ToString(), _hourFont);
                int xText = centerX + (int)((radius - 30) * Math.Cos(rad)) - (int)(textSize.Width / 2);
                int yText = centerY + (int)((radius - 30) * Math.Sin(rad)) - (int)(textSize.Height / 2);
                g.DrawString(i.ToString(), _hourFont, _hourMarkBrush, xText, yText);
            }

            DateTime now = DateTime.Now;

            // Чс
            double hourAngle = (now.Hour % 12) * 30 + now.Minute * 0.5 - 90;
            DrawHand(g, _hourHandPen, centerX, centerY, hourAngle, radius * 0.5);

            // Мс
            double minuteAngle = now.Minute * 6 - 90;
            DrawHand(g, _minuteHandPen, centerX, centerY, minuteAngle, radius * 0.7);

            // Сс
            double secondAngle = now.Second * 6 - 90;
            DrawHand(g, _secondHandPen, centerX, centerY, secondAngle, radius * 0.8);

            // Стрелка будильника
            if (_settings.IsAlarmActive)
            {
                double alarmAngle = (_settings.AlarmTime.Hours % 12) * 30 +
                                  _settings.AlarmTime.Minutes * 0.5 - 90;
                DrawHand(g, _alarmHandPen, centerX, centerY, alarmAngle, radius * 0.4);
            }

            // Центр циферблата
            g.FillEllipse(Brushes.Black, centerX - 5, centerY - 5, 10, 10);
        }

        private void DrawHand(Graphics g, Pen pen, int x, int y, double angle, double length)
        {
            double rad = angle * Math.PI / 180;
            int x2 = x + (int)(length * Math.Cos(rad));
            int y2 = y + (int)(length * Math.Sin(rad));
            g.DrawLine(pen, x, y, x2, y2);
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            this.Invalidate(); // Перерисовываем форму

            if (!_settings.IsAlarmActive) return;

            if (!_settings.IsAwakeActivated &&
                DateTime.Now.Hour == _settings.AlarmTime.Hours &&
                DateTime.Now.Minute == _settings.AlarmTime.Minutes &&
                DateTime.Now.Second == 0)
            {
                _settings.IsAwakeActivated = true;
                var awakeForm = new AwakeForm();
                awakeForm.Settings = _settings;
                awakeForm.FormClosed += AwakeForm_FormClosed;
                awakeForm.ShowDialog();
            }

            if (_settings.IsSoundActive && _settings.IsAwakeActivated)
            {
                SystemSounds.Beep.Play();
            }
        }

        private void AwakeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form)sender).FormClosed -= AwakeForm_FormClosed;
            UpdateView();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            var aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm();
            settingsForm.Settings = _settings;

            if (settingsForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            UpdateView();
            this.Invalidate(); // Обновляем циферблат после изменения настроек
        }

        private void UpdateView()
        {
            this.Text = _settings.IsAlarmActive ? $"Будильник (на {_settings.AlarmTime:hh\\:mm})" : "Будильник";
        }//ддддддддддддддддддд



    }
}