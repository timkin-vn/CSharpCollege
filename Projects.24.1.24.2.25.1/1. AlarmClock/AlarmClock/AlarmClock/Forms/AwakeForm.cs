using AlarmClock.Models;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AlarmClock.Forms
{
    public partial class AwakeForm : Form
    {
        private const string ImageFolderName = "Images";

        private string[] _imageFileNames;
        private int _imageIndex;

        private Button _snoozeButton;

        public Models.AlarmState AlarmState { get; set; }

        public AwakeForm()
        {
            InitializeComponent();
        }

        private void AwakeForm_Load(object sender, EventArgs e)
        {
            base.Text = string.IsNullOrEmpty(AlarmState.AlarmMessage) ? "Будильник" : AlarmState.AlarmMessage;
            InitializeImages();
            InitializeSnoozeButton(); // Инициализация кнопки «Отложить»
        }

        private void InitializeImages()
        {
            if (!Directory.Exists(ImageFolderName))
                return;

            _imageFileNames = Directory.EnumerateFiles(ImageFolderName).ToArray();
            if (_imageFileNames.Length == 0)
                return;

            _imageIndex = 0;
            AwakePictureBox.Load(_imageFileNames[_imageIndex]);
        }

        private void AwakeTimer_Tick(object sender, EventArgs e)
        {
            if (_imageFileNames == null || _imageFileNames.Length == 0)
                return;

            _imageIndex++;
            if (_imageIndex >= _imageFileNames.Length)
            {
                _imageIndex = 0;
            }

            AwakePictureBox.Load(_imageFileNames[_imageIndex]);
        }

        private void InitializeSnoozeButton()
        {
            _snoozeButton = new Button
            {
                Text = "Отложить",
                Width = 100,
                Height = 40,
                Top = AwakePictureBox.Bottom + 10,
                Left = 10
            };
            _snoozeButton.Click += SnoozeButton_Click;
            this.Controls.Add(_snoozeButton);
        }

        private void SnoozeButton_Click(object sender, EventArgs e)
        {
            // Переносим будильник на SnoozeDuration
            AlarmState.AlarmTime = DateTime.Now.Add(AlarmState.SnoozeDuration);
            AlarmState.IsAwakeActivated = false;
            AlarmState.IsSnoozed = true;

            this.Close(); // Закрываем форму
        }

        private void AwakeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Если будильник не был отложен — сбрасываем флаги
            if (!AlarmState.IsSnoozed)
            {
                AlarmState.IsAlarmActive = false;
                AlarmState.IsAwakeActivated = false;
            }
        }
    }
}