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

namespace Picture
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

            //var rect = new Rectangle
            //{
            //    X = 100,
            //    Y = 150,
            //    Width = 200,
            //    Height = 50,
            //};

            //var pen = new Pen(Color.FromArgb(0, 68, 153), 3);
            ////pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            ////pen.DashPattern = new float[] { 1, 2, 5, 3, 3, 2, };

            ////var brush = new SolidBrush(Color.FromArgb(255, 204, 17));
            ////var brush = new HatchBrush(HatchStyle.DiagonalCross, Color.FromArgb(255, 204, 17), Color.FromArgb(119, 17, 255));
            //var brush = new LinearGradientBrush(rect, Color.FromArgb(255, 204, 17), Color.FromArgb(119, 17, 255), LinearGradientMode.ForwardDiagonal);

            //g.FillRectangle(brush, rect);
            //g.DrawRectangle(pen, rect);

            //var clientRect = ClientRectangle;
            //clientRect.Inflate(-2, -2);
            //g.DrawRectangle(Pens.Black, clientRect);
            //g.DrawLine(Pens.Black, clientRect.Location, clientRect.Location + clientRect.Size);
            //g.DrawLine(Pens.Black, clientRect.Left, clientRect.Bottom, clientRect.Right, clientRect.Top);

            var pen = new Pen(Color.DarkGreen, 3);
            var brush = Brushes.LightGoldenrodYellow;

            var headRect = new Rectangle
            {
                X = ClientRectangle.Width * 2 / 5,
                Y = ClientRectangle.Height / 5,
                Width = ClientRectangle.Width / 5,
                Height = ClientRectangle.Height / 5,
            };

            g.FillEllipse(brush, headRect);
            g.DrawEllipse(pen, headRect);

            var bodyRect = new Rectangle
            {
                X = ClientRectangle.Width * 2 / 5,
                Y = ClientRectangle.Height * 2 / 5,
                Width = ClientRectangle.Width / 5,
                Height = ClientRectangle.Height / 5,
            };

            g.FillRectangle(brush, bodyRect);
            g.DrawRectangle(pen, bodyRect);

            g.DrawLine(pen, ClientRectangle.Width * 2 / 5, ClientRectangle.Height * 2 / 5, 
                ClientRectangle.Width / 5, ClientRectangle.Height * 3 / 5);
            g.DrawLine(pen, ClientRectangle.Width * 3 / 5, ClientRectangle.Height * 2 / 5,
                ClientRectangle.Width * 4 / 5, ClientRectangle.Height * 3 / 5);

            g.DrawLine(pen, ClientRectangle.Width * 2 / 5, ClientRectangle.Height * 3 / 5,
                ClientRectangle.Width * 2 / 5, ClientRectangle.Height * 4 / 5);
            g.DrawLine(pen, ClientRectangle.Width * 3 / 5, ClientRectangle.Height * 3 / 5,
                ClientRectangle.Width * 3 / 5, ClientRectangle.Height * 4 / 5);
        }

        private void PictureForm_Resize(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
