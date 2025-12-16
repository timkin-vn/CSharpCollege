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
            _viewModel = FromBusiness(_businessService.PictureModel);
        }

        public bool CreateMode { get; private set; }
        public ShapeType CurrentShapeType
        {
            get => _businessService.CurrentShapeType;
            set => _businessService.CurrentShapeType = value;
        }

        public bool CanDelete => !CreateMode && _businessService.PictureModel.SelectedShape != null;
        public string FileName { get; set; }

        public void Paint(Graphics g)
        {
            new Painter().Paint(g, _viewModel, true);
        }

        private PictureViewModel FromBusiness(PictureModel model)
        {
            var result = new PictureViewModel
            {
                Shapes = model.Shapes.Select(s => ShapeViewModel.FromBusiness(s)).ToList(),
            };

            if (model.SelectedShape != null)
            {
                var index = model.Shapes.IndexOf(model.SelectedShape);
                if (index >= 0)
                {
                    result.SelectedShape = result.Shapes[index];
                }
            }

            return result;
        }

        public Cursor GetCursor(Point loc)
        {
            if (_viewModel == null || _viewModel.Shapes == null)
            {
                return Cursors.Default;
            }

            var activeMarker = _viewModel.Markers?.FirstOrDefault(m => m.IsActive && IsInside(loc, m.Rectangle));
            if (activeMarker != null)
            {
                return activeMarker.Cursor;
            }

            var activeShape = _viewModel.Shapes.LastOrDefault(s => IsInside(loc, s.Rectangle));
            if (activeShape != null)
            {
                return Cursors.SizeAll;
            }

            return Cursors.Default;
        }

        public void MouseDown(Point loc)
        {
            if (CreateMode)
            {
                _businessService.CreateShape(ToModel(loc));
                _viewModel = FromBusiness(_businessService.PictureModel);
                return;
            }

            if (_viewModel != null && _viewModel.Markers != null)
            {
                var activeMarker = _viewModel.Markers.FirstOrDefault(m => m.IsActive && IsInside(loc, m.Rectangle));
                if (activeMarker != null)
                {
                    _businessService.SetResizeMode(activeMarker.EditMode);
                    _viewModel = FromBusiness(_businessService.PictureModel);
                    return;
                }
            }

            _businessService.SetMoveMode(ToModel(loc));
            _viewModel = FromBusiness(_businessService.PictureModel);
        }

        public void MouseMove(Point loc)
        {
            _businessService.UpdateMovingPoint(ToModel(loc));
            _viewModel = FromBusiness(_businessService.PictureModel);
        }

        public void MouseUp()
        {
            _businessService.ResetMode();
            CreateMode = false;
            _viewModel = FromBusiness(_businessService.PictureModel);
        }

        public void CreateButtonClick()
        {
            CreateMode = !CreateMode;
        }

        public void DeleteButtonClick()
        {
            _businessService.DeleteShape();
            _viewModel = FromBusiness(_businessService.PictureModel);
        }

        public void OpenFile(string fileName)
        {
            _businessService.OpenFile(fileName);
            _viewModel = FromBusiness(_businessService.PictureModel);
            FileName = fileName;
        }

        public void SaveToFile(string fileName)
        {
            _businessService.SaveToFile(fileName);
            FileName = fileName;
        }

        public void SaveToFile()
        {
            if (!string.IsNullOrEmpty(FileName))
            {
                _businessService.SaveToFile(FileName);
            }
        }

        public void CreateNewPicture()
        {
            _businessService.CreateNewPicture();
            _viewModel = FromBusiness(_businessService.PictureModel);
            FileName = string.Empty;
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

        public Color GetFillColor()
        {
            return _businessService.GetSelectedFillColor();
        }

        public void SetFillColor(Color color)
        {
            _businessService.SetFillColor(color);
            _viewModel = FromBusiness(_businessService.PictureModel);
        }

        public Color GetBorderColor()
        {
            return _businessService.GetSelectedBorderColor();
        }

        public void SetBorderColor(Color color)
        {
            _businessService.SetBorderColor(color);
            _viewModel = FromBusiness(_businessService.PictureModel);
        }

        public int GetBorderThickness()
        {
            return _businessService.GetSelectedBorderThickness();
        }

        public void SetBorderThickness(int thickness)
        {
            _businessService.SetBorderThickness(thickness);
            _viewModel = FromBusiness(_businessService.PictureModel);
        }

        public void MoveForward()
        {
            _businessService.MoveForward();
            _viewModel = FromBusiness(_businessService.PictureModel);
        }

        private bool IsInside(Point p, Rectangle rect) =>
            p.X >= rect.X && p.X <= rect.Right &&
            p.Y >= rect.Y && p.Y <= rect.Bottom;

        private PointModel ToModel(Point loc) => new PointModel { X = loc.X, Y = loc.Y };
    }
}