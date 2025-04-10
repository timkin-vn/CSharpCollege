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
    public partial class Secundomer : Form
    {


        public int milisec, sec, min;

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            richTextBox1.Text = "Результат! "+ "\nМинут: " +min+ "\nСекунд: " +sec+ "\nМилисекунд: " + milisec;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer2.Enabled = true;

            sec = 0;
            min = 0;
            milisec = 0;
            label3.Text = min + "." + sec + "." + milisec;
            timer2.Enabled = false;

            richTextBox1.Clear();


        }

        public Secundomer()
        {
            InitializeComponent();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            milisec += 1;
            if(milisec == 10)
            {
                milisec = 0;
                sec += 1;
            }
            if(sec == 60)
            {
                sec = 0;
                min += 1;
            }
            if(min == 60)
            {
                min = 0;
                timer1.Stop();
                richTextBox1.Text = "Time out!";
            }
            label3.Text = min +"." + sec + "." + milisec;

        }
    }
}
