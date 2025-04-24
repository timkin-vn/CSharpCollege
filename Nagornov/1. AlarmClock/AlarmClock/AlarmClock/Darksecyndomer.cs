using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlarmClock
{
    public partial class Darksecyndomer : Form
    {
        public Darksecyndomer()
        {
            InitializeComponent();
        }

        private void Darksecyndomer_Load(object sender, EventArgs e)
        {
            var darkteam = new Darkteam();
            darkteam.ShowDialog();
        }

        private void buttonStar_Click(object sender, EventArgs e)
        {
            timerSec.Start();
        }

        private void buttonStop_Click_2(object sender, EventArgs e)
        {
            timerSec.Stop();
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void timerSec_Tick_2(object sender, EventArgs e)
        {
           
        }

        private void laiblSec_Click(object sender, EventArgs e)
        {

        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            timerSec.Stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
