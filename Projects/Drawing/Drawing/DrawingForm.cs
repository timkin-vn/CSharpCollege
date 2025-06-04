using Drawing.Services;
using Drawing.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drawing
{
    public partial class DrawingForm : Form
    {
        public DrawingForm()
        {
            InitializeComponent();
        }

        private void DrawingForm_Paint(object sender, PaintEventArgs e)
        {
            var painter = new Painter();
            painter.Initialize(ClientRectangle);
            painter.Paint(e.Graphics);
        }

        private void DrawingForm_Resize(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
