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
        private int ImageIndex;

        private const string ImageFolderName = "Images";

        private List<string> ImageFileNames = new List<string>();

        internal ClockSettings Settings { get; set; }
        public AwakeForm()
        {
            InitializeComponent();
        }

        private void AwakeForm_Load(object sender, EventArgs e)
        {
            AwakeMessage.Text = Settings.AlarmMessage;
            InitializeImages();
        }

        private void AwakeButton_Click(object sender, EventArgs e)
        {
            Settings.IsAwakeActivated = false;
            Settings.IsAlarmON = false;

            DialogResult = DialogResult.OK;
        }

        private void AwakeTimer_Tick(object sender, EventArgs e)
        {
            ImageIndex++;
            if (ImageIndex == ImageFileNames.Count)
            {
                ImageIndex = 0;
            }
            AwakePictureBox.Load(ImageFileNames[ImageIndex]);

        }

        private void InitializeImages()
        {
            ImageIndex = 0;
            ImageFileNames.Clear();
            ImageFileNames.AddRange(Directory.EnumerateFiles(ImageFolderName));
            AwakePictureBox.Load(ImageFileNames[ImageIndex]);
        }

        private void PostponeButton_Click(object sender, EventArgs e)
        {
            Settings.IsAwakeActivated = false;
            Settings.AlarmTime = Settings.AlarmTime.Add(new TimeSpan(0, 5, 0));

            DialogResult = DialogResult.OK;
        }
    }
}   
