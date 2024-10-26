using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlarmClock.Forms
{
    public partial class Form1 : Form
    {
        public int mill, cek, min, w;
        private List<string> lapTimes = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer3.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer3.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string currentTime = $"Минуты: {min}, Секунды: {cek}, Миллисекунды: {mill}";
            lapTimes.Add(currentTime);

            richTextBox1.Clear(); 
            foreach (var time in lapTimes)
            {
                richTextBox1.AppendText(time + Environment.NewLine);
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            mill = 0;
            cek = 0;
            min = 0;
            lapTimes.Clear();
            //richTextBox1.Clear();
            label1.Text = "время : минуты 0 секунды 0 миллисекунды 0";
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            mill += 10;
            if (mill == 100)
            {
                mill = 0;
                cek += 1;
            }
            if (cek == 60)
            {
                cek = 0;
                min += 1;
                if (w > 1)
                {
                    richTextBox1.Text = "прошло" + w + "минут\n";
                }
                else
                    richTextBox1.Text = "прошла" + w + "минута\n";
            }
            if (min == 60)
            {
                min = 0;
            }
            label1.Text = "время : минуты " + min + " секунды " + cek + " миллисекунды " + mill;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }
    }
}

