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
        public int TriggeredIndex { get; set; } = -1;
        private const string ImageFolderName = "Images";

        private int _imageIndex;

        private List<string> _imageFileNames = new List<string>();

        internal ClockSettings Settings { get; set; }
        public string AlarmMessage { get; set; }

        public AwakeForm()
        {
            InitializeComponent();
        }

        private void AwakeForm_Load(object sender, EventArgs e)
        {
            AwakeMessageLabel.Text = AlarmMessage;
            InitializeImages();
        }

        private void AwakeButton_Click(object sender, EventArgs e)
        {

            DialogResult = DialogResult.OK;
        }

        private void AwakeTimer_Tick(object sender, EventArgs e)
        {
            _imageIndex++;
            if (_imageIndex >= _imageFileNames.Count)
            {
                _imageIndex = 0;
            }

            AwakePictureBox.Load(_imageFileNames[_imageIndex]);
        }

        private void InitializeImages()
        {
            _imageIndex = 0;
            _imageFileNames.Clear();
            _imageFileNames.AddRange(Directory.EnumerateFiles(ImageFolderName));

            AwakePictureBox.Load(_imageFileNames[_imageIndex]);
        }
    }
}