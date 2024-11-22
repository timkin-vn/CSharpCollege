using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace БомбитьКиев2014
{
    public partial class PictureForm : Form
    {
        public PictureForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var g = CreateGraphics();

            var trainBodyBrush = new LinearGradientBrush(new Point(100, 200), new Point(400, 300), Color.Red, Color.Yellow);
            g.FillRectangle(trainBodyBrush, 100, 200, 300, 100); 

            var chimneyBrush = new LinearGradientBrush(new Point(350, 150), new Point(380, 200), Color.Gray, Color.Black);
            g.FillRectangle(chimneyBrush, 350, 150, 30, 50); 

            g.FillRectangle(trainBodyBrush, 400, 200, 50, 50); 

            var wheelBrush = new HatchBrush(HatchStyle.Cross, Color.Black, Color.Gray);
            g.FillEllipse(wheelBrush, 120, 280, 50, 50); 
            g.FillEllipse(wheelBrush, 250, 280, 50, 50); 
            g.FillEllipse(wheelBrush, 380, 280, 50, 50); 
            
            var windowBrush = new LinearGradientBrush(new Point(150, 220), new Point(200, 250), Color.LightBlue, Color.White);
            g.FillRectangle(windowBrush, 150, 220, 50, 30);
            g.FillRectangle(windowBrush, 250, 220, 50, 30); 
            g.FillRectangle(windowBrush, 400, 220, 30, 20); 
        }
    }
}