using RectangleEditor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RectangleEditor
{
    public partial class EditorForm : Form
    {
        private bool _isMouseDown;

        private RectangleModel _rectangle = new RectangleModel { Left = 100, Top = 100, Width = 200, Height = 150, };

        public EditorForm()
        {
            InitializeComponent();
        }

        private void EditorForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Создание прямоугольника
                //_rectangle.Left = e.X;
                //_rectangle.Top = e.Y;
                //_rectangle.Width = 0;
                //_rectangle.Height = 0;

                _isMouseDown = true;

                var marker = _rectangle.GetActiveMarker(e.Location);

                if (marker != null && marker.IsActive)
                {
                    _rectangle.SetResizeMode(marker.EditMode);
                }
                else if (_rectangle.IsInside(e.Location))
                {
                    _rectangle.SetMovingMode(e.Location);
                    Refresh();
                }
                else
                {
                    _rectangle.SetCreateMode(e.Location);
                    Refresh();
                }
            }
        }

        private void EditorForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isMouseDown && e.Button == MouseButtons.Left)
            {
                // Создание прямоугольника

                _rectangle.ResetMode();
                _rectangle.Normalize();
                Refresh();
                _isMouseDown = false;
            }
        }

        private void EditorForm_MouseMove(object sender, MouseEventArgs e)
        {
            var marker = _rectangle.GetActiveMarker(e.Location);

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

            if (_isMouseDown)
            {
                // Создание прямоугольника
                //_rectangle.Right = e.X;
                //_rectangle.Bottom = e.Y;

                // Перемещение прямоугольника
                _rectangle.SetNewLocation(e.Location);
                Refresh();
            }
        }

        private void EditorForm_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var rect = _rectangle.GetRectangle();
            g.FillRectangle(Brushes.Yellow, rect);
            g.DrawRectangle(Pens.Blue, rect);
            DrawMarkers(g);
        }

        private void DrawMarkers(Graphics g)
        {
            var activeBrush = Brushes.Black;
            var inactiveBrush = Brushes.White;
            var markerPen = Pens.Black;

            foreach (var marker in _rectangle.Markers)
            {
                var brush = marker.IsActive ? activeBrush : inactiveBrush;
                g.FillRectangle(brush, marker.Rectangle);

                g.DrawRectangle(markerPen, marker.Rectangle);
            }
        }
    }
}
