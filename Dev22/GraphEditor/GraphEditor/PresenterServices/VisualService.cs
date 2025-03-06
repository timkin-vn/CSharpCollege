using GraphEditor.Business.Models;
using GraphEditor.Business.Services;
using GraphEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphEditor.PresenterServices
{
    internal class VisualService
    {
        private PictureService _businessService = new PictureService();

        private PictureViewModel _viewModel;

        public bool CreateMode { get; set; }

        public bool DeleteButtonEnabled => _businessService.PictureModel.SelectedRectangle != null;

        public string FileName { get; set; }

        public void MouseDown(Point location)
        {
            if (CreateMode)
            {
                _businessService.CreateRectangle(ToModel(location));
                _viewModel = FromModel(_businessService.PictureModel);
                return;
            }

            var activeMarker = (_viewModel != null && _viewModel.Markers.Any()) ?
                _viewModel.Markers.FirstOrDefault(m => m.IsActive && IsInside(location, m.Rectangle)) :
                null;
            if (activeMarker != null)
            {
                _businessService.SetResizeMode(activeMarker.Mode);
                _viewModel = FromModel(_businessService.PictureModel);
                return;
            }

            _businessService.SetMoveMode(ToModel(location));
            _viewModel = FromModel(_businessService.PictureModel);
        }

        public void MouseMove(Point location)
        {
            _businessService.Move(ToModel(location));
            _viewModel = FromModel(_businessService.PictureModel);
        }

        public void MouseUp()
        {
            _businessService.ResetMode();
            CreateMode = false;
            _viewModel = FromModel(_businessService.PictureModel);
        }

        public void Paint(Graphics g)
        {
            var painter = new Painter();
            painter.Paint(g, _viewModel, true);
        }

        public Cursor GetCursor(Point location)
        {
            if (_viewModel == null)
            {
                return Cursors.Default;
            }

            var activeMarker = _viewModel.Markers.FirstOrDefault(m => m.IsActive && IsInside(location, m.Rectangle));
            if (activeMarker != null)
            {
                return activeMarker.Cursor;
            }

            var activeRect = _viewModel.Rectangles.FirstOrDefault(r => IsInside(location, r.Rectangle));
            if (activeRect != null && activeRect.Rectangle.Width > 0)
            {
                return Cursors.SizeAll;
            }

            return Cursors.Default;
        }

        public void Delete()
        {
            _businessService.Delete();
            _viewModel = FromModel(_businessService.PictureModel);
        }

        public Color GetSelectedFillColor()
        {
            var rect = _businessService.PictureModel.SelectedRectangle;
            if (rect == null)
            {
                return Color.Black;
            }

            return rect.FillColor;
        }

        public void SetSelectedFillColor(Color color)
        {
            _businessService.SetSelectedFillColor(color);
            _viewModel = FromModel(_businessService.PictureModel);
        }
        public void SetSelectedDrawColor(Color color)
        {
            _businessService.SetSelectedDrawColor(color);
            _viewModel = FromModel(_businessService.PictureModel);
        }
        public void SetSelectedBackgroud(Image image)
        {
            _businessService.SetSelectedImage(image);
            _viewModel = FromModel(_businessService.PictureModel);
        }
        public void MoveForward()
        {
            _businessService.MoveForward();
            _viewModel = FromModel(_businessService.PictureModel);
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

        public void Save(string fileName)
        {
            FileName = fileName;
            _businessService.Save(FileName);
        }

        public void Save()
        {
            Save(FileName);
        }

        public void Open(string fileName)
        {
            FileName = fileName;
            _businessService.Open(FileName);
            _viewModel = FromModel(_businessService.PictureModel);
        }

        public PointModel ToModel(Point p)
        {
            return new PointModel { X = p.X, Y = p.Y, };
        }

        public Rectangle FromModel(RectangleModel rect)
        {
            return new Rectangle
            {
                X = rect.Width > 0 ? rect.X : rect.X + rect.Width,
                Y = rect.Height > 0 ? rect.Y : rect.Y + rect.Height,
                Width = rect.Width > 0 ? rect.Width : -rect.Width,
                Height = rect.Height > 0 ? rect.Height : -rect.Height,
            };
        }

        public MarkerViewModel FromModel(Marker m)
        {
            var viewModel = new MarkerViewModel
            {
                Rectangle = FromModel(m.Rectangle),
                IsActive = m.IsActive,
                Mode = m.Mode,
            };

            switch (m.Cursor)
            {
                case PictureCursor.Default:
                    viewModel.Cursor = Cursors.Default;
                    break;
                case PictureCursor.Hand:
                    viewModel.Cursor = Cursors.SizeAll;
                    break;
                case PictureCursor.SizeWE:
                    viewModel.Cursor = Cursors.SizeWE;
                    break;
                case PictureCursor.SizeNS:
                    viewModel.Cursor = Cursors.SizeNS;
                    break;
                case PictureCursor.SizeNWSE:
                    viewModel.Cursor = Cursors.SizeNWSE;
                    break;
                case PictureCursor.SizeNESW:
                    viewModel.Cursor = Cursors.SizeNESW;
                    break;
                default:
                    viewModel.Cursor = Cursors.Default;
                    break;
            }

            return viewModel;
        }

        public PictureViewModel FromModel(PictureModel pic)
        {
            return new PictureViewModel
            {
                Rectangles = pic.Rectangles.Select(r => new RectangleViewModel
                {
                    Rectangle = FromModel(r),
                    FillColor = r.FillColor,
                    DrawColor = r.DrawColor,
                    ImageRect = r.backGroub,
                }).ToList(),
                Markers = pic.SelectedRectangle == null ?
                    Enumerable.Empty<MarkerViewModel>() :
                    pic.SelectedRectangle.Markers.Select(m => FromModel(m)).ToList(),
            };
        }

        private bool IsInside(Point p, Rectangle rect) =>
            p.X >= rect.X && p.X <= rect.X + rect.Width &&
            p.Y >= rect.Y && p.Y <= rect.Y + rect.Height;
    }
}
