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
            g.DrawRectangle(pen, dx * 2, dy * 3, dx * 2, dy * 2);
            g.DrawLine(pen, dx * 2, dy * 1.5f, dx * 4, dy * 1.5f);
            g.DrawLine(pen, dx * 3, dy * 1.5f, dx * 3, dy * 3);
            g.DrawEllipse(pen, dx, dy, dx, dy);
            g.DrawEllipse(pen, dx * 4, dy, dx, dy);
        }

        private void PictureForm_Resize(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
