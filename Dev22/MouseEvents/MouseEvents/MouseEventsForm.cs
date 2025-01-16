using MouseEvents.Models;
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
        private bool _isMouseDown;

        private RectangleModel _rectangle = new RectangleModel();

        //private Point _start;

        //private Point _current;

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
                //_start = e.Location;
                //_current = e.Location;
                _rectangle.Left = e.X;
                _rectangle.Top = e.Y;
                _rectangle.Width = 0;
                _rectangle.Height = 0;

                _isMouseDown = true;
                Refresh();
            }
        }

        private void MouseEventsForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isMouseDown && e.Button == MouseButtons.Left)
            {
                //var g = CreateGraphics();
                //g.DrawEllipse(Pens.Black, e.X - 10, e.Y - 10, 20, 20);
                _rectangle.Normalize();
                _isMouseDown = false;
            }
        }

        private void MouseEventsForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMouseDown)
            {
                //var g = CreateGraphics();
                //g.DrawLine(Pens.Black, e.X - 12, e.Y, e.X + 12, e.Y);
                //g.DrawLine(Pens.Black, e.X, e.Y - 12, e.X, e.Y + 12);
                //g.DrawLine(Pens.Black, _start, e.Location);
                //_start = e.Location;
                //_current = e.Location;

                _rectangle.Right = e.X;
                _rectangle.Bottom = e.Y;
                Refresh();
            }
        }

        private void MouseEventsForm_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            //g.DrawLine(Pens.Black, _start, _current);
            //g.DrawRectangle(Pens.Black, new Rectangle { Location = _start, Width = _current.X - _start.X, Height = _current.Y - _start.Y });
            g.DrawRectangle(Pens.Black, _rectangle.GetRectangle());
        }
    }
}
