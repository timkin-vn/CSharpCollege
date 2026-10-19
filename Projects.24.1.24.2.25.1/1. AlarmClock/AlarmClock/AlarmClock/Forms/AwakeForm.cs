using AlarmClock.Models;
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

        private bool _snoozed;

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
            _imageFileNames = Directory.EnumerateFiles(ImageFolderName, "*.jpg").ToArray();
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
            AlarmState.AlarmTime = DateTime.Now.AddMinutes(5);
            AlarmState.IsAlarmActive = true;
            AlarmState.IsAwakeActivated = false;
            _snoozed = true;
            Close();
        }

        private void AwakeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!_snoozed)
            {
                AlarmState.IsAlarmActive = false;
                AlarmState.IsAwakeActivated = false;
            }
        }
    }
}
