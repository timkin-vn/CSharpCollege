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

        private Point _startPoint;

        private Point _currentPoint;

        public MouseEventsForm()
        {
            InitializeComponent();
        }

        private void MouseEventsForm_MouseDown(object sender, MouseEventArgs e)
        {
            using (var g = CreateGraphics())
            {
                //g.DrawRectangle(Pens.Black, e.X - 10, e.Y - 10, 20, 20);
                _isMouseDown = true;
                _startPoint = e.Location;
                _currentPoint = e.Location;
                Refresh();
            }
        }

        private void MouseEventsForm_MouseUp(object sender, MouseEventArgs e)
        {
            using (var g = CreateGraphics())
            {
                //g.DrawEllipse(Pens.Black, e.X - 10, e.Y - 10, 20, 20);
                _isMouseDown = false;
            }
        }

        private void MouseEventsForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMouseDown)
            {
                using (var g = CreateGraphics())
                {
                    //g.DrawLine(Pens.Black, e.X - 12, e.Y, e.X + 12, e.Y);
                    //g.DrawLine(Pens.Black, e.X, e.Y - 12, e.X, e.Y + 12);
                    //g.DrawLine(Pens.Black, _startPoint, e.Location);
                    //_prevPoint = e.Location;
                    _currentPoint = e.Location;
                    Refresh();
                }
            }
        }

        private void MouseEventsForm_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.DrawLine(Pens.Black, _startPoint, _currentPoint);

            var rect = new Rectangle { Location = _startPoint, };
            if (_currentPoint.X < _startPoint.X)
            {
                rect.X = _currentPoint.X;
                rect.Width = _startPoint.X - _currentPoint.X;
            }
            else
            {
                rect.Width = _currentPoint.X - _startPoint.X;
            }

            if (_currentPoint.Y < _startPoint.Y)
            {
                rect.Y = _currentPoint.Y;
                rect.Height = _startPoint.Y - _currentPoint.Y;
            }
            else
            {
                rect.Height = _currentPoint.Y - _startPoint.Y;
            }

            e.Graphics.DrawRectangle(Pens.Black, rect);
        }
    }
}
