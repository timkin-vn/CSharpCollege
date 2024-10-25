using AlarmClock.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace AlarmClock.Forms
{
    public partial class AwakeForm : Form
    {
        public AlarmSettings Settings { get; set; }

        public AwakeForm()
        {
            InitializeComponent();
        }

        private void AwakeButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
