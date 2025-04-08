using Alarm_clock_C_.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alarm_clock_C_.Forms
{
    public partial class AwakeForm : Form
    {
        private const string ImageFolderName = "Images";
        private int _indexImages;
        private List<string> _imageFileNames = new List<string>();
        internal ClockSettings Settings { get; set; }
        public AwakeForm()
        {
            InitializeComponent();
        }

        private void AwakeForm_Load(object sender, EventArgs e)
        {
            AwakeMessage.Text = Settings.AlarmMessage;
            initializeImages();
        }

        private void AwakeButton_Click(object sender, EventArgs e)
        {
            Settings.IsAlarmActive = false;
            Settings.IsAwakeActivated = false;

            DialogResult = DialogResult.OK;
        }

        private void AwakeTimer_Tick(object sender, EventArgs e)
        {
            _indexImages++;
            if (_indexImages >= _imageFileNames.Count)
            {
                _indexImages = 0;
            }
            AwakePictureBox.Load(_imageFileNames[_indexImages]);
        }

        private void initializeImages()
        {
            _indexImages = 0;
            _imageFileNames.Clear();
            _imageFileNames.AddRange(Directory.EnumerateFiles(ImageFolderName));
            AwakePictureBox.Load(_imageFileNames[_indexImages]);
        }
    }
}
