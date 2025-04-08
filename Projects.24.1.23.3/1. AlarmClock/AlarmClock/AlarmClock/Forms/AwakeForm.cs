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

        private List<string> _fileNames = new List<string>();

        private int _imageIndex;

        internal AlarmSettings Settings { get; set; }

        public AwakeForm()
        {
            InitializeComponent();
        }

        private void AwakeForm_Load(object sender, EventArgs e)
        {
            AwakeLabel.Text = Settings.AlarmMessage;
            InitializeImages();
        }

        private void AwakeButton_Click(object sender, EventArgs e)
        {
            Settings.IsAwakeActivated = false;
            Settings.IsAlarmActive = false;
            DialogResult = DialogResult.OK;
        }

        private void AwakeTimer_Tick(object sender, EventArgs e)
        {
            _imageIndex++;
            if (_imageIndex >= _fileNames.Count)
            {
                _imageIndex = 0;
            }

            AwakePicture.Load(_fileNames[_imageIndex]);
        }

        private void InitializeImages()
        {
            _imageIndex = 0;
            _fileNames.Clear();
            _fileNames.AddRange(Directory.EnumerateFiles(ImageFolderName));
            AwakePicture.Load(_fileNames[_imageIndex]);
        }
    }
}
