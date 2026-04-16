using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ColorBlastRect.BusinessModels;

namespace ColorBlastRect
{
    public partial class RectangleEditorForm : Form
    {
        private bool _isMouseDown;
        private RectangleModel _rectangle = new RectangleModel
        {
            Left = 100,
            Top = 100,
            Width = 200,
            Height = 150,
        };
        public RectangleEditorForm()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var pen = new Pen(Color.Green, 3);
            var brush = Brushes.Red;
            var _rect = _rectangle.GetRectangle();
            g.FillRectangle(brush, _rect);
            g.DrawRectangle(pen, _rect);
            DrawMarkers(g);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            _isMouseDown = true;

            var marker = _rectangle.GetCurrentMarker(e.Location);

            if (marker != null && marker.IsActive)
            {
                _rectangle.SetResizeMode(marker.EditMode);
            }
            else if (_rectangle.IsInside(e.Location))
            {
                _rectangle.SetMovingMode(e.Location);
            }
            else
            {
                _rectangle.SetCreatingMode(e.Location);
            }

            Refresh();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            var marker = _rectangle.GetCurrentMarker(e.Location);

            if (marker != null && marker.IsActive)
            {
                Cursor = marker.Cursor;
            }
            else if (_rectangle.IsInside(e.Location))
            {
                Cursor = Cursors.SizeAll;
            }
            else
            {
                Cursor = Cursors.Default;
            }

            if (!_isMouseDown)
            {
                return;
            }

            _rectangle.UpdateMovingPoint(e.Location);

            Refresh();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_isMouseDown || e.Button != MouseButtons.Left)
            {
                return;
            }

            _rectangle.ResetMode();
            _rectangle.Normalize();
            _isMouseDown = false;
            Refresh();
        }
        private void DrawMarkers(Graphics g)
        {
            var activeBrush = Brushes.Black;
            var inactiveBrush = Brushes.White;
            var pen = Pens.Black;

            foreach (var marker in _rectangle.Markers)
            {
                var brush = marker.IsActive ? activeBrush : inactiveBrush;
                g.FillRectangle(brush, marker.Rectangle);

                g.DrawRectangle(pen, marker.Rectangle);
            }
        }
    }
}
