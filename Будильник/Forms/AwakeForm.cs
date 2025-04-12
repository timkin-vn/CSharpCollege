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
        internal ClockSettings Settings { get; set; }

        public AwakeForm()
        {
            InitializeComponent();
        }

        private void AwakeForm_Load(object sender, EventArgs e)
        {
            AwakeMessage.Text = Settings.AlarmMessage;
            AwakePictureBox.Load("Images\\Часы.png");
        }

        private void AwakeButton_Click(object sender, EventArgs e)
        {
            Settings.IsAlarmActive = false;
            Settings.IsAwakeActivated = false;

            DialogResult = DialogResult.OK;
        }
    }
}
