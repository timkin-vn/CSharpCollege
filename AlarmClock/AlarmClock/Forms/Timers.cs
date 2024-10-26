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
    public partial class Timers : Form
    {

        public Timers()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (hour.Value == 0 && Minute.Value == 0 && SEcond.Value == 0)
            {
                MessageBox.Show("time off");

            }
            else {
                if (SEcond.Value < 0)
                {
                    Minute.Value--;
                    SEcond.Value = 59;

                }
                else if(Minute.Value==0){
                    hour.Value--;
                    Minute.Value = 59;

                }
            }
        }
    }
}
