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

        public enum AppTheme
        {
            Light,
            Dark
        }

        private AppTheme _currentTheme = AppTheme.Light;

        public AwakeForm()
        {
            InitializeComponent();
        }

        private void AwakeForm_Load(object sender, EventArgs e)
        {
            AwakeLabel.Text = Settings.AlarmMessage;
            InitializeImages();
            ApplyTheme();
        }

        private void AwakeButton_Click(object sender, EventArgs e)
        {
            Settings.IsAwakeActivated = false;
            Settings.IsAlarmActive = false;

            DialogResult = DialogResult.OK;
        }

        private void SnoozeButton_Click(object sender, EventArgs e)
        {
            var newAlarmTime = DateTime.Now.AddMinutes(5).TimeOfDay;
            Settings.AlarmTime = new TimeSpan(newAlarmTime.Hours, newAlarmTime.Minutes, 0);
            Settings.IsAwakeActivated = false;
            Settings.IsAlarmActive = true;
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

        private void ThemeButton_Click(object sender, EventArgs e)
        {
            _currentTheme = _currentTheme == AppTheme.Light ? AppTheme.Dark : AppTheme.Light;
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            if (_currentTheme == AppTheme.Dark)
            {
                this.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
                this.ForeColor = System.Drawing.Color.White;
                AwakeLabel.ForeColor = System.Drawing.Color.White;
                AwakeButton.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
                AwakeButton.ForeColor = System.Drawing.Color.White;
                SnoozeButton.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
                SnoozeButton.ForeColor = System.Drawing.Color.White;

            }
            else
            {
                this.BackColor = System.Drawing.SystemColors.Control;
                this.ForeColor = System.Drawing.Color.Black;
                AwakeLabel.ForeColor = System.Drawing.Color.Black;
                AwakeButton.BackColor = System.Drawing.SystemColors.Control;
                AwakeButton.ForeColor = System.Drawing.Color.Black;
                SnoozeButton.BackColor = System.Drawing.SystemColors.Control;
                SnoozeButton.ForeColor = System.Drawing.Color.Black;

            }
        }
    }
}
