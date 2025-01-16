using MouseEvents.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseEvents
{
    public partial class MouseEventsForm : Form
    {
        private bool _isPressed;

        //private Point _anchor;

        private LineModel _line = new LineModel();

        private RectangleModel _rect = new RectangleModel();

        public MouseEventsForm()
        {
            InitializeComponent();
        }

        private void MouseEventsForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //var g = CreateGraphics();
                //g.DrawRectangle(Pens.Black, e.X - 10, e.Y - 10, 20, 20);
                //_line.Point1 = e.Location;
                //_line.Point2 = e.Location;
                _rect.Point1 = e.Location;
                _rect.Point2 = e.Location;
                _isPressed = true;
                Refresh();
            }
        }

        private void MouseEventsForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //var g = CreateGraphics();
                //g.DrawEllipse(Pens.Black, e.X - 10, e.Y - 10, 20, 20);
                _isPressed = false;
                _rect.Normalize();
                Refresh();
            }
        }

        private void MouseEventsForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isPressed)
            {
                //var g = CreateGraphics();
                //g.DrawLine(Pens.Black, e.X - 12, e.Y, e.X + 12, e.Y);
                //g.DrawLine(Pens.Black, e.X, e.Y - 12, e.X, e.Y + 12);
                //g.DrawLine(Pens.Black, _anchor, e.Location);
                //_line.Point2 = e.Location;
                _rect.Point2 = e.Location;
                Refresh();
            }
        }

        private void MouseEventsForm_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.FillRectangle(Brushes.White, ClientRectangle);
            //g.DrawLine(Pens.Black, _line.Point1, _line.Point2);
            g.DrawRectangle(Pens.Black, _rect.Rectangle);
        }
    }
}
