using AlarmClock.Models;
using System;
using System.IO;
using System.Windows.Forms;

namespace AlarmClock.Forms
{
    public partial class AwakeForm : Form
    {
        private const string ImageFolderName = "Images";
        private string[] _imageFileNames;
        private int _imageIndex;
        private bool _isSnoozing = false;   // Флаг, что мы откладываем, а не выключаем будильник

        public AlarmState AlarmState { get; set; }

        public AwakeForm()
        {
            InitializeComponent();
        }

        private void AwakeForm_Load(object sender, EventArgs e)
        {
            Text = AlarmState.AlarmMessage;
            InitializeImages();
        }

        private void InitializeImages()
        {
            if (Directory.Exists(ImageFolderName))
            {
                _imageFileNames = Directory.GetFiles(ImageFolderName, "*.*", SearchOption.TopDirectoryOnly);
                if (_imageFileNames.Length > 0)
                {
                    _imageIndex = 0;
                    AwakePictureBox.Load(_imageFileNames[_imageIndex]);
                }
            }
        }

        private void AwakeTimer_Tick(object sender, EventArgs e)
        {
            if (_imageFileNames != null && _imageFileNames.Length > 0)
            {
                _imageIndex++;
                if (_imageIndex >= _imageFileNames.Length)
                    _imageIndex = 0;
                AwakePictureBox.Load(_imageFileNames[_imageIndex]);
            }
        }

        private void AwakeButton_Click(object sender, EventArgs e)
        {
            _isSnoozing = false;
            AlarmState.IsAlarmActive = false;
            AlarmState.IsAwakeActivated = false;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Snooze(int minutes)
        {
            _isSnoozing = true;
            AlarmState.AlarmTime = DateTime.Now.AddMinutes(minutes);
            AlarmState.IsAwakeActivated = false;   // Останавливаем звук
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Snooze5Button_Click(object sender, EventArgs e) => Snooze(5);
        private void Snooze10Button_Click(object sender, EventArgs e) => Snooze(10);
        private void Snooze30Button_Click(object sender, EventArgs e) => Snooze(30);
        private void Snooze60Button_Click(object sender, EventArgs e) => Snooze(60);

        private void AwakeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!_isSnoozing)
            {
                AlarmState.IsAlarmActive = false;
                AlarmState.IsAwakeActivated = false;
            }

        }
    }
}