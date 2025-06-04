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

        private int _imageIndex;

        private List<string> _imageFileNames = new List<string>();

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
            Settings.IsAlarmActive = false;
            Settings.IsAwakeActivated = false;

            DialogResult = DialogResult.OK;
        }

        private void AwakeTimer_Tick(object sender, EventArgs e)
        {
          
        }

        private void InitializeImages()
        {
            _imageIndex = 0;
            _imageFileNames.Clear();

            if (!string.IsNullOrEmpty(Settings.ImageFolderPath) && Directory.Exists(Settings.ImageFolderPath))
            {
                _imageFileNames.AddRange(Directory.EnumerateFiles(Settings.ImageFolderPath, "*.jpg"));
            }

            if (_imageFileNames.Count > 0)
            {
                _imageIndex = Settings.SelectedImageIndex >= 0 && Settings.SelectedImageIndex < _imageFileNames.Count
                    ? Settings.SelectedImageIndex
                    : 0;

                AwakePictureBox.Load(_imageFileNames[_imageIndex]);
            }
            else
            {
                MessageBox.Show("Нет доступных изображений!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
