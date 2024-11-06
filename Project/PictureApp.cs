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

namespace Risunok
{
    public partial class PictureForm : Form
    {
        LinearGradientBrush GradBrush = new LinearGradientBrush(
            new Point(0, 10),
            new Point(0, 160),
            Color.FromArgb(255, 0, 0, 255), 
            Color.FromArgb(255, 255, 0, 0) 
        );

        Graphics q;

        public PictureForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            q = CreateGraphics();
            q.Clear(Color.Azure);
            q.FillRectangle(GradBrush, 50, 50, 150, 150);
            q.FillEllipse(GradBrush, 300, 50, 150, 150);
            q.FillPolygon(GradBrush, new Point[] {
                new Point(500, 50),
                new Point(650, 150),
                new Point(450, 150)
            });
        }
    }
}