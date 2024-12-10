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

namespace risunok
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
            g.Clear(this.BackColor);


            var bodyBrush = new SolidBrush(Color.LightBlue); 
            g.FillRectangle(bodyBrush, 100, 200, 400, 100);

 
            g.FillRectangle(bodyBrush, 100, 170, 400, 30);

          
            var windowBrush = new SolidBrush(Color.White); 
            g.FillRectangle(windowBrush, 130, 220, 80, 40);
            g.FillRectangle(windowBrush, 230, 220, 80, 40); 
            g.FillRectangle(windowBrush, 330, 220, 80, 40); 

            
            var wheelBrush = new SolidBrush(Color.Black); 
            g.FillEllipse(wheelBrush, 120, 290, 50, 50); 
            g.FillEllipse(wheelBrush, 320, 290, 50, 50); 

   
            var rimBrush = new SolidBrush(Color.Gray); 
            g.FillEllipse(rimBrush, 130, 300, 20, 20); 
            g.FillEllipse(rimBrush, 330, 300, 20, 20); 

            
            var gradientBrush = new LinearGradientBrush(new Rectangle(130, 220, 80, 40), Color.LightSkyBlue, Color.Blue, 45f);
            g.FillRectangle(gradientBrush, 130, 220, 80, 40); 

            
            var frontBrush = new SolidBrush(Color.DarkRed);
            g.FillRectangle(frontBrush, 95, 210, 5, 70); 
            g.FillRectangle(frontBrush, 495, 210, 5, 70); 

            
            using (var pen = new Pen(Color.Yellow, 2))
            {
                g.DrawLine(pen, 50, 400, 600, 400); 
            }

            
            using (var grassBrush = new SolidBrush(Color.Green))
            {
                g.FillRectangle(grassBrush, 0, 350, this.ClientSize.Width, 50); 
            }

            bodyBrush.Dispose();
            windowBrush.Dispose();
            wheelBrush.Dispose();
            rimBrush.Dispose();
            gradientBrush.Dispose();
            frontBrush.Dispose();
        }
    }
}