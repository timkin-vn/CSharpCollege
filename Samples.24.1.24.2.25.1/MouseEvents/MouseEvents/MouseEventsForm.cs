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

        private Point _currentPoint;

        private Point _startPoint;

        public MouseEventsForm()
        {
            InitializeComponent();
        }

        private void MouseEventsForm_MouseDown(object sender, MouseEventArgs e)
        {
            //using (var g = CreateGraphics())
            //{
            //    g.DrawRectangle(Pens.Black, e.X - 10, e.Y - 10, 20, 20);
            //}

            _isMouseDown = true;
            _currentPoint = e.Location;
            _startPoint = e.Location;
            Refresh();
        }

        private void MouseEventsForm_MouseUp(object sender, MouseEventArgs e)
        {
            //using (var g = CreateGraphics())
            //{
            //    g.DrawEllipse(Pens.Black, e.X - 10, e.Y - 10, 20, 20);
            //}

            _isMouseDown = false;
        }

        private void MouseEventsForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isMouseDown)
            {
                return;
            }

            //using (var g = CreateGraphics())
            //{
            //    //g.DrawLine(Pens.Black, e.X - 15, e.Y, e.X + 15, e.Y);
            //    //g.DrawLine(Pens.Black, e.X, e.Y - 15, e.X, e.Y + 15);
            //    g.DrawLine(Pens.Black, _startPoint, e.Location);
            //}

            _currentPoint = e.Location;
            Refresh();
        }

        private void MouseEventsForm_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            //g.DrawLine(Pens.Black, _startPoint, _currentPoint);

            var rect = new Rectangle();
            if (_startPoint.X < _currentPoint.X)
            {
                rect.X = _startPoint.X;
                rect.Width = _currentPoint.X - _startPoint.X;
            }
            else
            {
                rect.X = _currentPoint.X;
                rect.Width = _startPoint.X - _currentPoint.X;
            }

            if (_startPoint.Y < _currentPoint.Y)
            {
                rect.Y = _startPoint.Y;
                rect.Height = _currentPoint.Y - _startPoint.Y;
            }
            else
            {
                rect.Y = _currentPoint.Y;
                rect.Height = _startPoint.Y - _currentPoint.Y;
            }

            g.DrawRectangle(Pens.Black, rect);
        }
    }
}
