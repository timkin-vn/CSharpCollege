using Drawing.ViewServices;
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
        private int _currentPicture = 1;

        public DrawingForm()
        {
            InitializeComponent();
        }

        private void DrawingForm_Paint(object sender, PaintEventArgs e)
        {
            var painter = new Painter();
            painter.Paint(ClientRectangle, e.Graphics, _currentPicture);
        }

        private void DrawingForm_Resize(object sender, EventArgs e)
        {
            Refresh();
        }

        private void DrawingForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1)
                _currentPicture = 1;
            else if (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2)
                _currentPicture = 2;
            else if (e.KeyCode == Keys.D3 || e.KeyCode == Keys.NumPad3)
                _currentPicture = 3;

            Refresh();
        }
    }
}