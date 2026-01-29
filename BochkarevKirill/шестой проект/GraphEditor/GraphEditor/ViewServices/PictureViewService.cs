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

        public bool CreateCircleMode { get; private set; }

        public bool CanDeleteCircle => !CreateCircleMode && _businessService.PictureModel.SelectedCircle != null;

        public bool CanDelete => !CreateMode && _businessService.PictureModel.SelectedRectangle != null;

        public string FileName { get; set; }

        public void Paint(Graphics g)
        {
            new Painter().Paint(g, _viewModel, true);
        }

        private PictureViewModel FromBusiness(PictureModel model)
        {
            var result = new PictureViewModel
            {
                Rectangles = model.Rectangles.Select(r => RectangleViewModel.FromBusiness(r)).ToList(),
                Circles = model.Circles.Select(c => CircleViewModel.FromBusiness(c)).ToList(),
            };

            if (model.SelectedRectangle != null)
            {
                var index = model.Rectangles.IndexOf(model.SelectedRectangle);
                if (index >= 0)
                {
                    result.SelectedRectangle = result.Rectangles[index];
                }
            }

            if (model.SelectedCircle != null)
            {
                var index = model.Circles.IndexOf(model.SelectedCircle);
                if (index >= 0)
                {
                    result.SelectedCircle = result.Circles[index];
                }
            }
            if (model.SelectedCircle != null)
            {
                var index = model.Circles.IndexOf(model.SelectedCircle);
                if (index >= 0)
                {
                    result.SelectedCircle = result.Circles[index];
                }
            }
            return result;
        }

        public Cursor GetCursor(Point loc)
        {
            if (_viewModel == null)
            {
                return Cursors.Default;
            }

            var activeMarker = _viewModel.Markers
                .FirstOrDefault(m => m.IsActive && IsInside(loc, m.Rectangle));

            if (activeMarker != null)
            {
                return activeMarker.Cursor;
            }

            var activeRect = _viewModel.Rectangles
                .FirstOrDefault(r => IsInside(loc, r.Rectangle));

            if (activeRect != null)
            {
                return Cursors.SizeAll;
            }

            var activeCircle = _viewModel.Circles
                .FirstOrDefault(c => IsInside(loc, c.Rectangle));

            if (activeCircle != null)
            {
                return Cursors.SizeAll;
            }

            return Cursors.Default;
        }


        public void MouseDown(Point loc)
        {
            if (CreateCircleMode)
            {
                _businessService.CreateCircle(ToModel(loc));
                _viewModel = FromBusiness(_businessService.PictureModel);
                return;
            }

            if (CreateMode)
            {
                _businessService.CreateRectangle(ToModel(loc));
                _viewModel = FromBusiness(_businessService.PictureModel);
                return;
            }

            if (_viewModel != null)
            {
                var activeMarker = _viewModel.Markers.FirstOrDefault(m => m.IsActive && IsInside(loc, m.Rectangle));
                if (activeMarker != null)
                {
                    if (_businessService.PictureModel.SelectedCircle != null)
                    {
                        _businessService.SetResizeModeCircle(activeMarker.EditMode);
                        _viewModel = FromBusiness(_businessService.PictureModel);
                        return;
                    }
                    _businessService.SetResizeMode(activeMarker.EditMode);
                    _viewModel = FromBusiness(_businessService.PictureModel);
                    return;
                }

            }

            _businessService.SetMoveModeCircle(ToModel(loc));
            _viewModel = FromBusiness(_businessService.PictureModel);

            if (_businessService.PictureModel.SelectedCircle != null)
            {
                return;
            }
            _businessService.SetMoveMode(ToModel(loc));
            _viewModel = FromBusiness(_businessService.PictureModel);

        }

        public void MouseMove(Point loc)
        {
            _businessService.UpdateMovingPoint(ToModel(loc));
            _businessService.UpdateMovingPointCircle(ToModel(loc));
            _viewModel = FromBusiness(_businessService.PictureModel);
        }

        public void MouseUp()
        {
            _businessService.ResetMode();
            _businessService.ResetModeCircle();
            CreateCircleMode = false;
            CreateMode = false;
            _viewModel = FromBusiness(_businessService.PictureModel);
        }

        public void CreateCircleButtonClick()
        {
            CreateCircleMode = !CreateCircleMode;
        }

        public void DeleteCircleButtonClick()
        {
            _businessService.DeleteCircle();
            _viewModel = FromBusiness(_businessService.PictureModel);
        }

        public void CreateButtonClick()
        {
            CreateMode = !CreateMode;
        }

        public void DeleteButtonClick()
        {
            _businessService.DeleteRectangle();
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
            _businessService.SaveToFile(FileName);
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
            return _viewModel?.SelectedRectangle?.FillColor
                ?? _viewModel?.SelectedCircle?.FillColor
                ?? PictureService.DefaultFillColor;
        }


        public void SetFillColor(Color color)
        {
            _businessService.SetFillColor(color);
            _viewModel = FromBusiness(_businessService.PictureModel);
        }

        public Color GetBorderColor()
        {
            return _viewModel?.SelectedRectangle?.BorderColor
                ?? _viewModel?.SelectedCircle?.BorderColor
                ?? PictureService.DefaultBorderColor;
        }

        public void SetBorderColor(Color color)
        {
            _businessService.SetBorderColor(color);
            _viewModel = FromBusiness(_businessService.PictureModel);
        }


        public void MoveForward()
        {
            if (_businessService.PictureModel.SelectedCircle != null)
            {
                _businessService.MoveForwardCircle();
            }
            else
            {
                _businessService.MoveForward();
            }

            _viewModel = FromBusiness(_businessService.PictureModel);
        }

        private bool IsInside(Point p, Rectangle rect) =>
            p.X >= rect.X && p.X <= rect.Right &&
            p.Y >= rect.Y && p.Y <= rect.Bottom;

        private PointModel ToModel(Point loc) => new PointModel { X =  loc.X, Y = loc.Y, };
    }
}
