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

namespace PictureApp
{
    public partial class PictureForm : Form
    {
        public PictureForm()
        {
            InitializeComponent();
        }

        private void PictureForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.Azure);

            
            float scaleX = ClientRectangle.Width / 400f; 
            float scaleY = ClientRectangle.Height / 400f; 
            float scale = Math.Min(scaleX, scaleY); 

            
            using (LinearGradientBrush headBrush = new LinearGradientBrush(
                new Rectangle((int)(200 * scale), (int)(50 * scale), (int)(100 * scale), (int)(100 * scale)),
                Color.FromArgb(255, 224, 189),
                Color.FromArgb(255, 153, 102),
                LinearGradientMode.Vertical))
            {
                g.FillEllipse(headBrush, (int)(200 * scale), (int)(50 * scale), (int)(100 * scale), (int)(100 * scale));
            }

            
            using (SolidBrush bodyBrush = new SolidBrush(Color.FromArgb(0, 102, 204)))
            {
                g.FillRectangle(bodyBrush, (int)(225 * scale), (int)(150 * scale), (int)(50 * scale), (int)(100 * scale));
            }

            
            using (SolidBrush armBrush = new SolidBrush(Color.FromArgb(255, 224, 189)))
            {
                g.FillRectangle(armBrush, (int)(175 * scale), (int)(150 * scale), (int)(50 * scale), (int)(20 * scale));
                g.FillRectangle(armBrush, (int)(275 * scale), (int)(150 * scale), (int)(50 * scale), (int)(20 * scale));
            }

            
            using (SolidBrush legBrush = new SolidBrush(Color.FromArgb(0, 51, 102)))
            {
                g.FillRectangle(legBrush, (int)(225 * scale), (int)(250 * scale), (int)(20 * scale), (int)(100 * scale));
                g.FillRectangle(legBrush, (int)(255 * scale), (int)(250 * scale), (int)(20 * scale), (int)(100 * scale));
            }

            
            using (SolidBrush footBrush = new SolidBrush(Color.FromArgb(255, 224, 189)))
            {
                g.FillEllipse(footBrush, (int)(225 * scale), (int)(350 * scale), (int)(20 * scale), (int)(10 * scale));
                g.FillEllipse(footBrush, (int)(255 * scale), (int)(350 * scale), (int)(20 * scale), (int)(10 * scale));
            }

            
            g.FillEllipse(Brushes.White, (int)(230 * scale), (int)(80 * scale), (int)(20 * scale), (int)(20 * scale));
            g.FillEllipse(Brushes.White, (int)(250 * scale), (int)(80 * scale), (int)(20 * scale), (int)(20 * scale));
            g.FillEllipse(Brushes.Black, (int)(235 * scale), (int)(85 * scale), (int)(10 * scale), (int)(10 * scale));
            g.FillEllipse(Brushes.Black, (int)(255 * scale), (int)(85 * scale), (int)(10 * scale), (int)(10 * scale));

            
            using (Pen mouthPen = new Pen(Color.Black, 2))
            {
                g.DrawArc(mouthPen, (int)(230 * scale), (int)(100 * scale), (int)(40 * scale), (int)(20 * scale), 0, 180);
            }

            
            using (SolidBrush hairBrush = new SolidBrush(Color.FromArgb(139, 69, 19)))
            {
                Point[] hairPoints = {
            new Point((int)(200 * scale), (int)(50 * scale)),
            new Point((int)(250 * scale), (int)(10 * scale)),
            new Point((int)(300 * scale), (int)(50 * scale))
        };
                g.FillPolygon(hairBrush, hairPoints);
            }

            using (Pen outlinePen = new Pen(Color.Black, 2))
            {
                g.DrawRectangle(outlinePen, (int)(225 * scale), (int)(150 * scale), (int)(50 * scale), (int)(100 * scale));
                g.DrawRectangle(outlinePen, (int)(175 * scale), (int)(150 * scale), (int)(50 * scale), (int)(20 * scale));
                g.DrawRectangle(outlinePen, (int)(275 * scale), (int)(150 * scale), (int)(50 * scale), (int)(20 * scale));
                g.DrawRectangle(outlinePen, (int)(225 * scale), (int)(250 * scale), (int)(20 * scale), (int)(100 * scale));
                g.DrawRectangle(outlinePen, (int)(255 * scale), (int)(250 * scale), (int)(20 * scale), (int)(100 * scale));
            }
        }

        private void PictureForm_Resize(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
