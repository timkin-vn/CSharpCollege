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
    public partial class RectangleEditorForm : Form
    {
        private RectangleModel _model = new RectangleModel
        {
            Left = 200,
            Top = 100,
            Width = 200,
            Height = 150,
        };

        private bool _isMouseDown;

        public RectangleEditorForm()
        {
            InitializeComponent();
        }

        private void RectangleEditorForm_Paint(object sender, PaintEventArgs e)
        {
            var pen = Pens.Blue;
            var brush = Brushes.Yellow;

            var rect = _model.GetRectangle();
            e.Graphics.FillRectangle(brush, rect);
            e.Graphics.DrawRectangle(pen, rect);

            DrawMarkers(e.Graphics);
        }

        private void RectangleEditorForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            _isMouseDown = true;

            var currentMarker = _model.GetMarkerUnderCursor(e.Location);
            if (currentMarker != null && currentMarker.IsActive)
            {
                _model.SetResizeMode(currentMarker.EditMode);
            }
            else if (_model.IsInside(e.Location))
            {
                _model.SetMovingMode(e.Location);
            }
            else
            {
                _model.SetCreatingMode(e.Location);
            }

            Refresh();
        }

        private void RectangleEditorForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_isMouseDown || e.Button != MouseButtons.Left)
            {
                return;
            }

            _isMouseDown = false;
            _model.ResetMode();
            _model.Normalize();
            Refresh();
        }

        private void RectangleEditorForm_MouseMove(object sender, MouseEventArgs e)
        {
            var currentMarker = _model.GetMarkerUnderCursor(e.Location);

            if (currentMarker != null && currentMarker.IsActive)
            {
                Cursor = currentMarker.Cursor;
            }
            else if (_model.IsInside(e.Location))
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

            _model.UpdateMovingPoint(e.Location);
            Refresh();
        }

        private void DrawMarkers(Graphics g)
        {
            var pen = Pens.Black;
            var activeBrush = Brushes.Black;
            var inactiveBrush = Brushes.White;

            foreach (var marker in _model.Markers)
            {
                var brush = marker.IsActive ? activeBrush : inactiveBrush;

                g.FillRectangle(brush, marker.Rectangle);
                g.DrawRectangle(pen, marker.Rectangle);
            }
        }
    }
}
