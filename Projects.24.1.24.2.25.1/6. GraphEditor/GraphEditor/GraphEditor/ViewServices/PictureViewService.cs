using GraphEditor.Business.Enums;
using GraphEditor.Business.Models;
using GraphEditor.Business.Services;
using GraphEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace GraphEditor.ViewServices
{
    internal class PictureViewService
    {
        private readonly PictureService _businessService = new PictureService();
        private PictureViewModel _viewModel;

        public PictureViewService()
        {
            LoadViewModel();
        }

        public bool CreateMode { get; set; }
        public ShapeType CurrentShapeType { get; set; } = ShapeType.Rectangle;
        public bool CanDelete => !CreateMode && _businessService.PictureModel.SelectedShape != null;
        public string FileName { get; set; }

        public void CreateButtonClicked() => CreateMode = !CreateMode;

        public void CreateNewPicture()
        {
            _businessService.CreateNewPicture();
            FileName = string.Empty;
            LoadViewModel();
        }

        public void DeleteButtonClicked()
        {
            _businessService.DeleteSelectedShape();
            LoadViewModel();
        }

        public void Export(string fileName, Rectangle size, Color backColor)
        {
            using (var bmp = new Bitmap(size.Width, size.Height))
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(backColor);
                new Painter().Paint(g, _viewModel, false);
                bmp.Save(fileName, ImageFormat.Png);
            }
        }

        public Color GetCurrentFillColor()
        {
            var shape = _businessService.PictureModel.SelectedShape;
            if (shape is RectangleModel r) return r.FillColor;
            if (shape is TriangleModel t) return t.FillColor;
            if (shape is CircleModel c) return c.FillColor;
            if (shape is HilbertCurveModel h) return h.FillColor;
            return PictureService.DefaultFillColor;
        }

        public Cursor GetCursor(Point loc)
        {
            if (_viewModel == null) return Cursors.Default;
            if (CreateMode) return Cursors.Cross;

            var activeMarker = _viewModel.Markers.FirstOrDefault(m => m.IsActive && IsInside(loc, m.Rectangle));
            if (activeMarker != null) return activeMarker.Cursor;

            // Проверяем по DrawOrder (с конца)
            for (int i = _viewModel.DrawOrder.Count - 1; i >= 0; i--)
            {
                var shape = _viewModel.DrawOrder[i];
                var p = new PointModel { X = loc.X, Y = loc.Y };
                if (shape is RectangleModel r && r.IsInside(p)) return Cursors.Hand;
                if (shape is TriangleModel t && t.IsInside(p)) return Cursors.Hand;
                if (shape is CircleModel c && c.IsInside(p)) return Cursors.Hand;
                if (shape is HilbertCurveModel h && h.IsInside(p)) return Cursors.Hand;
            }
            return Cursors.Default;
        }

        public void MouseDown(Point loc)
        {
            if (CreateMode)
            {
                var point = new PointModel { X = loc.X, Y = loc.Y };
                switch (CurrentShapeType)
                {
                    case ShapeType.Rectangle: _businessService.CreateRectangle(point); break;
                    case ShapeType.Triangle: _businessService.CreateTriangle(point); break;
                    case ShapeType.Circle: _businessService.CreateCircle(point); break;
                    case ShapeType.HilbertCurve: _businessService.CreateHilbertCurve(point); break;
                }
                LoadViewModel();
                return;
            }

            var model = _businessService.PictureModel;

            if (model.SelectedShape != null)
            {
                var marker = _viewModel.Markers.FirstOrDefault(m => m.IsActive && IsInside(loc, m.Rectangle));
                if (marker != null)
                {
                    _businessService.SetResizeMode(marker.EditMode);
                    LoadViewModel();
                    return;
                }
            }

            var pointModel = new PointModel { X = loc.X, Y = loc.Y };
            _businessService.SelectAndSetMoveMode(pointModel);
            LoadViewModel();
        }

        public void MouseMove(Point loc)
        {
            _businessService.UpdateMovingPoint(new PointModel { X = loc.X, Y = loc.Y });
            LoadViewModel();
        }

        public void MouseUp()
        {
            _businessService.ResetMode();
            CreateMode = false;
            LoadViewModel();
        }

        public void MoveForward()
        {
            _businessService.MoveForward();
            LoadViewModel();
        }

        public void Open(string fileName)
        {
            _businessService.Open(fileName);
            FileName = fileName;
            LoadViewModel();
        }

        public void Paint(Graphics g) => new Painter().Paint(g, _viewModel, true);

        public void Save() => _businessService.Save(FileName);
        public void Save(string fileName) { _businessService.Save(fileName); FileName = fileName; }

        public void SetFillColor(Color color)
        {
            if (_businessService.PictureModel.SelectedShape == null) return;
            _businessService.SetFillColor(color);
            LoadViewModel();
        }

        private bool IsInside(Point loc, Rectangle rect) =>
            loc.X >= rect.X && loc.X <= rect.Right &&
            loc.Y >= rect.Y && loc.Y <= rect.Bottom;

        private void LoadViewModel()
        {
            var model = _businessService.PictureModel;
            _viewModel = new PictureViewModel
            {
                DrawOrder = model.DrawOrder.ToList(),
                Rectangles = model.Rectangles.Select(r => RectangleViewModel.FromBusiness(r)).ToList(),
                Triangles = model.Triangles.Select(t => TriangleViewModel.FromBusiness(t)).ToList(),
                Circles = model.Circles.Select(c => CircleViewModel.FromBusiness(c)).ToList(),
                HilbertCurves = model.HilbertCurves.Select(h => HilbertCurveViewModel.FromBusiness(h)).ToList(),
                SelectedRectangle = model.SelectedRectangle != null ? RectangleViewModel.FromBusiness(model.SelectedRectangle) : null,
                SelectedTriangle = model.SelectedTriangle != null ? TriangleViewModel.FromBusiness(model.SelectedTriangle) : null,
                SelectedCircle = model.SelectedCircle != null ? CircleViewModel.FromBusiness(model.SelectedCircle) : null,
                SelectedHilbertCurve = model.SelectedHilbertCurve != null ? HilbertCurveViewModel.FromBusiness(model.SelectedHilbertCurve) : null,
            };
        }
    }
}