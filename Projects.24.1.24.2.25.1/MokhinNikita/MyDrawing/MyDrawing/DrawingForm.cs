using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DrawingWindows.Services;

namespace MyDrawing
{
    public partial class DrawingForm : Form
    {
        public DrawingForm()
        {
            InitializeComponent();
        }
        private void UpdateResize(object sender, EventArgs e)
        {
            Refresh();
        }
        private void PaintPictures(object sender, PaintEventArgs e)
        {
            var painter = new Painter();
            painter.Initialize(ClientRectangle);
            painter.Paint(e.Graphics);
        }
    }
}
