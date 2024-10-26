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
    public partial class main_menu : Form
    {
        public main_menu()
        {
            InitializeComponent();
        }

        private void alarm_Click(object sender, EventArgs e)
        {
            new ClockForm().ShowDialog();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Aboutt().ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Timers().ShowDialog();
        }
    }
}
