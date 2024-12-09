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
    public partial class TimerForm : Form
    {
        public TimerForm()
        {
            InitializeComponent();
        }
        int i = 0;
        private void label1_Click(object sender, EventArgs e)
        {
          
        }
    
        private void timer1_Tick(object sender, EventArgs e)
        {
            i++;
            DisplayLabelSecundomer.Text = i.ToString();
        }

        private void TimerOnOff_Click(object sender, EventArgs e)
        {
            Secundomer.Enabled = true; 
        }

        private void Back_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Secundomer.Enabled = false;
        }
    }
}
