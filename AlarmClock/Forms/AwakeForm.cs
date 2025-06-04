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
            label2.Text = DateTime.Now.ToLongTimeString();
        }

        private void AwakeButton_Click(object sender, EventArgs e)
        {
            Settings.IsAlarmActive = false;
            Settings.IsAwakeActivated = false;

            DialogResult = DialogResult.OK;
        }

        private void AwakeMessage_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings.AlarmTime = DateTime.Now.AddMinutes(5).TimeOfDay;
            Settings.IsAwakeActivated = false;

            DialogResult = DialogResult.OK;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

