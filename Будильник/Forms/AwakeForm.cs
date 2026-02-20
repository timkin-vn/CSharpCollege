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
using Будильник.Models;

namespace Будильник.Forms
{
    public partial class AwakeForm : Form
    {
        private const string ImageFolderName = "Images";

        private string[] _imageFileNames;
        private int _imageIndex;
        public AlarmState AlarmState {  get; set; }

        public AwakeForm()
        {
            InitializeComponent();
        }

        private void AwakePictureBox_Click(object sender, EventArgs e)
        {

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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void AwakeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!AlarmState.IsSnoozed)
            {
                AlarmState.IsAlarmActive = false;
            }

            AlarmState.IsAwakeActivated = false;
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            AlarmState.AlarmTime = AlarmState.AlarmTime.AddMinutes(5);
            AlarmState.IsSnoozed = true;
            AlarmState.IsAwakeActivated = false;

            this.Close();
        }
    }
}