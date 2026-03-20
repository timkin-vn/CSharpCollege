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

        private void AwakeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
           
            if (AlarmState.IsAwakeActivated)
            {
                AlarmState.IsAlarmActive = false;
                AlarmState.IsAwakeActivated = false;
            }

        }
      
        private void AwakeButton_Click(object sender, EventArgs e)
        {

        }

        private void More_time_Click(object sender, EventArgs e)
        {
            // Устанавливает новое время
            AlarmState.AlarmTime = DateTime.Now.AddMinutes(5);


            AlarmState.IsAwakeActivated = false;

            this.Close();
        }
    }
}
