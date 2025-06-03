    using AlarmClock.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AlarmClock.Forms
{
    public partial class AwakeForm : Form
    {
        private const string ImageFolderName = "Images";

        private int _imageIndex;
        private List<string> _imageFileNames = new List<string>();

        private Timer _countdownTimer = new Timer(); // таймер обратного отсчёта
        private int _timeLeftInSeconds = 10; // 10 секунд отсчёта

        private Label CountdownLabel;

        internal ClockSettings Settings { get; set; }

        public AwakeForm()
        {
            InitializeComponent();

            // Создаем метку таймера
            CountdownLabel = new Label
            {
                Location = new Point(20, 250),
                Size = new Size(300, 30),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Text = "Осталось: 10 сек."
            };
            Controls.Add(CountdownLabel);

            // Настройка таймера
            _countdownTimer.Interval = 1000; // 1 секунда
            _countdownTimer.Tick += CountdownTimer_Tick;
        }

        private void AwakeForm_Load(object sender, EventArgs e)
        {
            AwakeMessage.Text = Settings.AlarmMessage;
            InitializeImages();

            _countdownTimer.Start(); // запускаем обратный отсчёт
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            _timeLeftInSeconds--;

            CountdownLabel.Text = $"Осталось: {_timeLeftInSeconds} сек.";

            if (_timeLeftInSeconds <= 0)
            {
                _countdownTimer.Stop();

                Settings.IsAlarmActive = false;
                Settings.IsAwakeActivated = false;

                DialogResult = DialogResult.OK; // закрыть форму
            }
        }

        private void AwakeButton_Click(object sender, EventArgs e)
        {
            _countdownTimer.Stop();

            Settings.IsAlarmActive = false;
            Settings.IsAwakeActivated = false;

            DialogResult = DialogResult.OK;
        }

        private void AwakeTimer_Tick(object sender, EventArgs e)
        {
            _imageIndex++;
            if (_imageIndex >= _imageFileNames.Count)
            {
                _imageIndex = 0;
            }

            if (_imageFileNames.Count > 0)
                AwakePictureBox.Load(_imageFileNames[_imageIndex]);
        }

        private void InitializeImages()
        {
            _imageIndex = 0;
            _imageFileNames.Clear();

            if (Directory.Exists(ImageFolderName))
            {
                _imageFileNames.AddRange(Directory.EnumerateFiles(ImageFolderName));
            }

            if (_imageFileNames.Count > 0)
            {
                AwakePictureBox.Load(_imageFileNames[_imageIndex]);
            }
        }
    }
}
