using AlarmClock.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            AlarmMessageLabel.Text = ClockState.AlarmMessage;
            InitializeImages();
        }

        private void InitializeImages()
        {
            _imageFileNames = Directory.EnumerateFiles(ImageFolderName).ToArray();
            _imageIndex = 0;
            AwakePictureBox.Load(_imageFileNames[_imageIndex]);
        }

        private void AwakeTimer_Tick(object sender, EventArgs e)
        {
            _imageIndex++;
            if (_imageIndex >= _imageFileNames.Length)
            {
                _imageIndex = 0;
            }

            AwakePictureBox.Load(_imageFileNames[_imageIndex]);
        }
        private void SnoozeButton_Click(object sender, EventArgs e)
        {
            // Прибавляем 5 минут к текущему времени будильника
            ClockState.AlarmTime = ClockState.AlarmTime.AddMinutes(5);

            // Сбрасываем флаг активации, чтобы ClockForm снова смогла его запустить
            ClockState.IsAwakeActivated = false;

            this.Tag = "Snooze"; // Мы вешаем ярлык на окно перед закрытием

            // Закрываем форму (звук и таймер ClockForm подхватят изменения)
            this.Close();
        }

        private void AwakeButton_Click(object sender, EventArgs e)
        {

        }
    }
}
