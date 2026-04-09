using AlarmClock.Model;
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

        internal AlarmClockState ClockState { get; set; }

        public AwakeForm()
        {
            InitializeComponent();
        }

        private void AwakeForm_Load(object sender, EventArgs e)
        {
            AlarmMessageLabel.Text = string.IsNullOrWhiteSpace(ClockState.AlarmMessage)
                ? "Будильник сработал!"
                : ClockState.AlarmMessage;

            SnoozeButton.Text = $"Отложить на {ClockState.SnoozeMinutes} мин.";
            InitializeImages();
        }

        private void InitializeImages()
        {
            _imageFileNames = Directory.EnumerateFiles(ImageFolderName).ToArray();
            _imageIndex = 0;

            if (_imageFileNames.Length > 0)
            {
                AwakePictureBox.Load(_imageFileNames[_imageIndex]);
            }
        }

        private void AwakeTimer_Tick(object sender, EventArgs e)
        {
            if (_imageFileNames == null || _imageFileNames.Length == 0)
            {
                return;
            }

            _imageIndex++;
            if (_imageIndex >= _imageFileNames.Length)
            {
                _imageIndex = 0;
            }

            AwakePictureBox.Load(_imageFileNames[_imageIndex]);
        }

        private void SnoozeButton_Click(object sender, EventArgs e)
        {
            ClockState.IsSnoozeRequested = true;
            Close();
        }
    }
}
