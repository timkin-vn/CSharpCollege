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
            var g = e.Graphics;
            var dx = ClientRectangle.Width / 6f;
            var dy = ClientRectangle.Height / 6f;
            var pen = new Pen(Color.Blue, 3);
            var pen2 = new Pen(Color.Red, 3);
            var gradientBrushRed= new LinearGradientBrush(
                new Rectangle(100, 50, 100, 100),
                Color.FromArgb(255, 0, 0),
                Color.FromArgb(176, 0, 0),
                LinearGradientMode.Vertical);

            var gradientBrushBlue = new LinearGradientBrush(
              new Rectangle(100, 50, 100, 100),
              Color.FromArgb(128, 166, 255),
              Color.FromArgb(0, 49, 83),
              LinearGradientMode.Vertical);

            var gradientBrushGreen = new LinearGradientBrush(
            new Rectangle(-1000,-1000, 100, 100),
            Color.FromArgb(52, 201, 36),
            Color.FromArgb(0, 69, 36),
            LinearGradientMode.Horizontal);

            var gradientBrushYellow = new LinearGradientBrush(
         new Rectangle(180, 180, 200, 200),
         Color.FromArgb(255, 255, 0),
         Color.FromArgb(205, 164, 52),
         LinearGradientMode.Vertical);

            var gradientBrushBlack = new LinearGradientBrush(
         new Rectangle(200, 200, 200, 200),
         Color.FromArgb(0,0,0),
         Color.FromArgb(255, 255, 255),
         LinearGradientMode.Horizontal);


            g.DrawRectangle(pen2, dx / 28, dy / 28, dx * 5.90f, dy * 5.90f );
            g.FillRectangle(gradientBrushGreen, dx / 28, dy / 28, dx * 5.90f, dy * 5.90f);
            g.DrawRectangle(pen, dx * 2, dy * 3, dx * 2, dy * 2);
            g.FillRectangle(gradientBrushYellow, dx * 2, dy * 3, dx * 2, dy * 2);
            g.DrawLine(pen, dx * 2, dy * 1.5f, dx * 4, dy * 1.5f);
            g.DrawLine(pen, dx * 3, dy * 1.5f, dx * 3, dy * 3);
            g.DrawEllipse(pen, dx, dy, dx * 2, dy * 2);
            g.FillEllipse(gradientBrushRed, dx, dy, dx, dy);
            g.FillEllipse(gradientBrushBlue, dx *2f, dy * 1.9f, dx, dy);
            g.FillEllipse(gradientBrushYellow, dx * 2, dy, dx, dy);
            g.FillEllipse(gradientBrushBlack, dx, dy * 1.9f, dx, dy);
            g.DrawEllipse(pen, dx * 3, dy, dx * 2 , dy * 2);
            g.FillEllipse(gradientBrushBlue, dx * 4, dy, dx, dy);
            g.FillEllipse(gradientBrushRed, dx * 3, dy * 1.9f , dx, dy);
            g.FillEllipse(gradientBrushYellow, dx * 3, dy, dx, dy);
            g.FillEllipse(gradientBrushBlack, dx * 4, dy * 1.9f, dx, dy);


        }

        private void PictureForm_Resize(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
