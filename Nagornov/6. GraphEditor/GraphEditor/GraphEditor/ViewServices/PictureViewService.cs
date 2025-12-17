using GraphEditor.Business.Models;
using GraphEditor.Business.Services;
using GraphEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphEditor.ViewServices
{
    internal class PictureViewService
    {
        private readonly PictureService _businessService = new PictureService();
        private PictureViewModel _viewModel;

        private float _zoom = 1.0f;
        private PointF _panOffset = new PointF(0, 0);
        private bool _isPanning = false;
        private Point _lastPanPoint;

        public PictureViewService()
        {
            LoadViewModel();
        }

        public bool CreateRectangleMode => _businessService.CreateRectangleMode;
        public bool CreateCircleMode => _businessService.CreateCircleMode;
        public bool CanDelete => !_businessService.IsInCreateMode() &&
                               (_businessService.PictureModel.SelectedRectangle != null ||
                                _businessService.PictureModel.SelectedCircle != null);

        public float Zoom => _zoom;
        public PointF PanOffset => _panOffset;

        public void Paint(Graphics g)
        {
            var originalState = g.Save();

            try
            {
                g.TranslateTransform(_panOffset.X, _panOffset.Y);
                g.ScaleTransform(_zoom, _zoom);

                new Painter().Paint(g, _viewModel, true);
            }
            finally
            {
                g.Restore(originalState);
            }
            DrawZoomInfo(g);
        }

        private void DrawZoomInfo(Graphics g)
        {
            var zoomText = $"Zoom: {_zoom * 100:0}%";
            using (var font = new Font("Arial", 10))
            using (var brush = new SolidBrush(Color.DarkBlue))
            using (var bgBrush = new SolidBrush(Color.FromArgb(200, Color.White)))
            {
                var textSize = g.MeasureString(zoomText, font);
                var rect = new RectangleF(10, 10, textSize.Width + 10, textSize.Height + 5);

                g.FillRectangle(bgBrush, rect);
                g.DrawRectangle(Pens.DarkBlue, rect.X, rect.Y, rect.Width, rect.Height);
                g.DrawString(zoomText, font, brush, 15, 12);
            }
        }

        public Cursor GetCursor(Point loc)
        {
            if (_isPanning) return Cursors.Hand;

            var transformedLoc = ApplyTransform(loc);
            if (_viewModel == null) return Cursors.Default;

            var activeMarker = _viewModel.Markers.FirstOrDefault(m => IsInside(transformedLoc, m.Rectangle));
            if (activeMarker != null) return activeMarker.Cursor;

            var activeRect = _viewModel.Rectangles?.FirstOrDefault(r => IsInside(transformedLoc, r.Rectangle));
            if (activeRect != null) return Cursors.SizeAll;

            var activeCircle = _viewModel.Circles?.FirstOrDefault(c => IsInsideCircle(transformedLoc, c));
            if (activeCircle != null) return Cursors.SizeAll;

            return Cursors.Default;
        }

        public void MouseDown(Point loc)
        {
            if (Control.ModifierKeys == Keys.Shift) 
            {
                StartPan(loc);
                return;
            }

            var transformedPoint = ApplyTransform(loc); 

            if (_businessService.CreateRectangleMode)
            {
                _businessService.SetCreateRectangleMode(ToModel(transformedPoint));
                LoadViewModel();
                return;
            }

            if (_businessService.CreateCircleMode)
            {
                _businessService.SetCreateCircleMode(ToModel(transformedPoint));
                LoadViewModel();
                return;
            }

            if (_viewModel != null)
            {
                var activeMarker = _viewModel.Markers.FirstOrDefault(m => IsInside(transformedPoint, m.Rectangle));
                if (activeMarker != null)
                {
                    _businessService.SetResizeMode(activeMarker.EditMode);
                    LoadViewModel();
                    return;
                }
            }

            _businessService.SetMoveMode(ToModel(transformedPoint));
            LoadViewModel();
        }

        public void MouseMove(Point loc)
        {
            if (_isPanning)
            {
                UpdatePan(loc);
                return;
            }

            var transformedPoint = ApplyTransform(loc); 
            _businessService.UpdateMovingPoint(ToModel(transformedPoint));
            LoadViewModel();
        }

        public void MouseUp()
        {
            if (_isPanning)
            {
                StopPan();
                return;
            }

            _businessService.ResetMode();
            LoadViewModel();
        }
        public void ZoomIn()
        {
            _zoom *= 1.2f;
            _zoom = Math.Min(_zoom, 5.0f);
        }

        public void ZoomOut()
        {
            _zoom /= 1.2f;
            _zoom = Math.Max(_zoom, 0.1f);
        }

        public void ResetView()
        {
            _zoom = 1.0f;
            _panOffset = new PointF(0, 0);
        }

        public void StartPan(Point startPoint)
        {
            _isPanning = true;
            _lastPanPoint = startPoint;
        }

        public void UpdatePan(Point currentPoint)
        {
            if (_isPanning)
            {
                _panOffset.X += currentPoint.X - _lastPanPoint.X;
                _panOffset.Y += currentPoint.Y - _lastPanPoint.Y;
                _lastPanPoint = currentPoint;
            }
        }

        public void StopPan()
        {
            _isPanning = false;
        }
        public void CreateRectangleButtonClicked()
        {
            _businessService.ToggleCreateRectangleMode();
        }

        public void CreateCircleButtonClicked()
        {
            _businessService.ToggleCreateCircleMode();
        }

        public void DeleteButtonClicked()
        {
            _businessService.DeleteRectangle();
            LoadViewModel();
        }

        public void ChangeColor(Color newColor)
        {
            _businessService.ChangeColor(newColor);
            LoadViewModel();
        }

        private void LoadViewModel()
        {
            var model = _businessService.PictureModel;
            _viewModel = new PictureViewModel
            {
                Rectangles = model.Rectangles?.Select(r => RectangleViewModel.FromBusiness(r)).ToList() ?? new List<RectangleViewModel>(),
                Circles = model.Circles?.Select(c => CircleViewModel.FromBusiness(c)).ToList() ?? new List<CircleViewModel>()
            };

            if (model.SelectedRectangle != null)
            {
                var index = model.Rectangles.IndexOf(model.SelectedRectangle);
                _viewModel.SelectedRectangle = _viewModel.Rectangles[index];
            }
            else
            {
                _viewModel.SelectedRectangle = null;
            }

            if (model.SelectedCircle != null)
            {
                var index = model.Circles.IndexOf(model.SelectedCircle);
                _viewModel.SelectedCircle = _viewModel.Circles[index];
            }
            else
            {
                _viewModel.SelectedCircle = null;
            }
        }

        private Point ApplyTransform(Point point)
        {
            return new Point(
                (int)((point.X - _panOffset.X) / _zoom),
                (int)((point.Y - _panOffset.Y) / _zoom)
            );
        }

        private PointModel ToModel(Point point) => new PointModel { X = point.X, Y = point.Y };

        private bool IsInside(Point loc, Rectangle rect) =>
            loc.X >= rect.X && loc.X <= rect.Right &&
            loc.Y >= rect.Y && loc.Y <= rect.Bottom;

        private bool IsInsideCircle(Point loc, CircleViewModel circle)
        {
            int dx = loc.X - circle.CenterX;
            int dy = loc.Y - circle.CenterY;
            return dx * dx + dy * dy <= circle.Radius * circle.Radius;
        }
    }
}