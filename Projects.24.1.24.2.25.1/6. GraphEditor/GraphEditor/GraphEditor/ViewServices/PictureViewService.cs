using GraphEditor.Business.Models;
using GraphEditor.Business.Services;
using GraphEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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

        public PictureViewService()
        {
            LoadViewModel();
        }

        public bool CreateMode { get; set; }

        public bool CanDelete => !CreateMode && _businessService.PictureModel.SelectedShape != null;

        public string FileName { get; set; }

        public ShapeType CurrentShapeType
        {
            get => _businessService.CurrentShapeType;
            set => _businessService.CurrentShapeType = value;
        }

        public void CreateButtonClicked()
        {
            CreateMode = !CreateMode;
        }

        public void CreateNewPicture()
        {
            _businessService.CreateNewPicture();
            FileName = string.Empty;
            LoadViewModel();
        }

        public void DeleteButtonClicked()
        {
            _businessService.DeleteShape();
            LoadViewModel();
        }

        public void Export(string fileName, Rectangle size, Color backColor)
        {
            using (var bmp = new Bitmap(size.Width, size.Height))
            {
                using (var g = Graphics.FromImage(bmp))
                {
                    g.Clear(backColor);
                    new Painter().Paint(g, _viewModel, false);
                }
                bmp.Save(fileName, ImageFormat.Png);
            }
        }

        public Color GetCurrentFillColor()
        {
            return _businessService.PictureModel.SelectedShape?.FillColor ?? PictureService.DefaultFillColor;
        }

        public Cursor GetCursor(Point loc)
        {
            if (_viewModel == null) return Cursors.Default;
            if (CreateMode) return Cursors.Cross;

            var activeMarker = _viewModel.Markers.FirstOrDefault(m => m.IsActive && IsInside(loc, m.Rectangle));
            if (activeMarker != null) return activeMarker.Cursor;

            var activeRect = _viewModel.Rectangles.FirstOrDefault(r => IsInside(loc, r.Rectangle));
            if (activeRect != null) return Cursors.Hand;

            return Cursors.Default;
        }

        public void MouseDown(Point loc)
        {
            if (CreateMode)
            {
                _businessService.CreateAndSetCreateMode(ToModel(loc));
                LoadViewModel();
                return;
            }

            if (_viewModel != null)
            {
                var activeMarker = _viewModel.Markers.FirstOrDefault(m => m.IsActive && IsInside(loc, m.Rectangle));
                if (activeMarker != null)
                {
                    _businessService.SetResizeMode(activeMarker.EditMode);
                    LoadViewModel();
                    return;
                }
            }

            _businessService.SelectAndSetMoveMode(ToModel(loc));
            LoadViewModel();
        }

        public void MouseMove(Point loc)
        {
            bool isShiftPressed = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
            _businessService.UpdateMovingPoint(ToModel(loc), isShiftPressed);
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

        public void MoveBackward()
        {
            _businessService.MoveBackward();
            LoadViewModel();
        }

        public void Open(string fileName)
        {
            _businessService.Open(fileName);
            FileName = fileName;
            LoadViewModel();
        }

        public void Paint(Graphics g)
        {
            new Painter().Paint(g, _viewModel, true);
        }

        public void Save() => _businessService.Save(FileName);

        public void Save(string fileName)
        {
            _businessService.Save(fileName);
            FileName = fileName;
        }

        public void SetFillColor(Color color)
        {
            if (_businessService.PictureModel.SelectedShape == null) return;

            _businessService.SetFillColor(color);
            LoadViewModel();
        }

        private bool IsInside(Point loc, Rectangle rect) =>
            loc.X >= rect.X && loc.X <= rect.Right && loc.Y >= rect.Y && loc.Y <= rect.Bottom;

        private void LoadViewModel()
        {
            var model = _businessService.PictureModel;

            _viewModel = new PictureViewModel
            {
                Rectangles = model.Shapes
                    .Select(r => RectangleViewModel.FromBusiness(r))
                    .ToList(),
            };

            if (model.SelectedShape != null)
            {
                var index = model.Shapes.IndexOf(model.SelectedShape);
                if (index >= 0 && index < _viewModel.Rectangles.Count)
                {
                    _viewModel.SelectedRectangle = _viewModel.Rectangles[index];
                }
            }
        }

        private PointModel ToModel(Point point) => new PointModel { X = point.X, Y = point.Y };

    }
}
