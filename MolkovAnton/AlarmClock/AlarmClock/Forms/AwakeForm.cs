using AlarmClock.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlarmClock.Forms
{
    public partial class AwakeForm : Form
    {
        public AlarmSettings Settings { get; set; }

        private int _imageIndex = 0;

        private Bitmap[] _images = new[]
        {
            Properties.Resources.Image1,
            Properties.Resources.Image3,
            Properties.Resources.Image4,
            Properties.Resources.Image5,
            Properties.Resources.Image7,
        };

        public AwakeForm()
        {
            InitializeComponent();
        }

        private void AwakeButton_Click(object sender, EventArgs e)
        {
            Settings.IsAlarmActive = false;
            Hide();
        }

        private void AwakeTimer_Tick(object sender, EventArgs e)
        {
            _imageIndex++;
            if (_imageIndex >= _images.Length)
            {
                _imageIndex = 0;
            }

            AwakePictureBox.Image = _images[_imageIndex];
        }

        private void AwakeForm_Load(object sender, EventArgs e)
        {
            AwakeTimer.Enabled = true;
            AwakePictureBox.Image = Properties.Resources.Image1;
        }

        private void PostponeButton_Click(object sender, EventArgs e)
        {
            Settings.AlarmTime = DateTime.Now.AddMinutes(9);
            Hide(); 
        }
    }
}
