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
        private bool _isPressed;

        private RectangleModel _rect = new RectangleModel { X = 100, Y = 100, Width = 200, Height = 150, };

        public EditorForm()
        {
            InitializeComponent();
        }

        private void EditorForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var marker = _rect.GetMarker(e.Location);

                if (marker != null)
                {
                    _rect.SetResizeMode(marker.Mode);
                }
                else if (_rect.IsInside(e.Location))
                {
                    _rect.SetMoveMode(e.Location);
                }
                else
                {
                    _rect.SetCreateMode(e.Location);
                }

                Refresh();
                _isPressed = true;
            }
        }

        private void EditorForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isPressed = false;
                _rect.Normalize();
                _rect.ResetMode();
                Refresh();
            }
        }

        private void EditorForm_MouseMove(object sender, MouseEventArgs e)
        {
            var marker = _rect.GetMarker(e.Location);

            if (marker != null && marker.IsActive)
            {
                Cursor = marker.Cursor;
            }
            else if (_rect.IsInside(e.Location))
            {
                Cursor = Cursors.SizeAll;
            }
            else
            {
                Cursor = Cursors.Default;
            }

            if (_isPressed)
            {
                _rect.Move(e.Location);
                Refresh();
            }
        }

        private void EditorForm_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.FillRectangle(Brushes.White, ClientRectangle);
            var brush = new SolidBrush(Color.Yellow);
            var pen = new Pen(Color.Blue, 3);

            g.FillRectangle(brush, _rect.Rectangle);
            g.DrawRectangle(pen, _rect.Rectangle);

            DrawMarkers(g);
        }

        private void DrawMarkers(Graphics g)
        {
            var activeBrush = Brushes.Black;
            var inactiveBrush = Brushes.White;
            var pen = Pens.Black;

            foreach (var marker in _rect.Markers)
            {
                var brush = marker.IsActive ? activeBrush : inactiveBrush;
                g.FillRectangle(brush, marker.Rectangle);

                g.DrawRectangle(pen, marker.Rectangle);
            }
        }
    }
}
