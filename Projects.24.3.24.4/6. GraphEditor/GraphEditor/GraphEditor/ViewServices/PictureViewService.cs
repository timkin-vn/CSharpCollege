using GraphEditor.Business.Models;
using GraphEditor.Business.Services;
using GraphEditor.ViewModels;
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

        public bool CanDelete =>
            !CreateMode &&
            (_businessService.PictureModel.SelectedRectangle != null ||
             _businessService.PictureModel.SelectedRectangles.Any());

        public string FileName { get; set; }

        public void Paint(Graphics g)
        {
            new Painter().Paint(g, _viewModel, true);
        }

        public void MouseDown(Point loc, bool ctrlPressed)
        {
            if (CreateMode)
            {
                _businessService.CreateRectangleAndSetCreateMode(ToModel(loc));
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

            _businessService.FindAndSetMoveMode(ToModel(loc), ctrlPressed);
            LoadViewModel();
        }

        public void MouseMove(Point loc)
        {
            _businessService.UpdateMovingPoint(ToModel(loc));
            LoadViewModel();
        }

        public void MouseUp()
        {
            _businessService.ResetMode();
            CreateMode = false;
            LoadViewModel();
        }

        public Cursor GetCursor(Point loc)
        {
            if (_viewModel == null)
            {
                return Cursors.Default;
            }

            if (CreateMode)
            {
                return Cursors.Cross;
            }

            var activeMarker = _viewModel.Markers.FirstOrDefault(m => m.IsActive && IsInside(loc, m.Rectangle));
            if (activeMarker != null)
            {
                return activeMarker.Cursor;
            }

            var activeRect = _viewModel.Rectangles.FirstOrDefault(m => IsInside(loc, m.Rectangle));
            if (activeRect != null)
            {
                return Cursors.Hand;
            }

            return Cursors.Default;
        }

        public void CreateButtonClicked()
        {
            CreateMode = !CreateMode;
        }

        public void DeleteButtonClicked()
        {
            _businessService.DeleteRectangle();
            LoadViewModel();
        }

        public Color GetCurrentFillColor()
        {
            return _viewModel?.SelectedRectangle?.FillColor ?? PictureService.DefaultFillColor;
        }

        public void SetFillColor(Color color)
        {
            _businessService.SetFillColor(color);
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

        public void Save(string fileName)
        {
            _businessService.Save(fileName);
            FileName = fileName;
        }

        public void Save()
        {
            _businessService.Save(FileName);
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

        public void CreateNewPicture()
        {
            _businessService.CreateNewPicture();
            LoadViewModel();
        }

        private void LoadViewModel()
        {
            var model = _businessService.PictureModel;

            _viewModel = new PictureViewModel
            {
                Rectangles = model.Rectangles.Select(r => RectangleViewModel.FromBusiness(r)).ToList(),
            };

            if (model.SelectedRectangle != null)
            {
                var index = model.Rectangles.IndexOf(model.SelectedRectangle);

                if (index >= 0)
                {
                    _viewModel.SelectedRectangle = _viewModel.Rectangles[index];
                }
            }

            foreach (var selected in model.SelectedRectangles)
            {
                var index = model.Rectangles.IndexOf(selected);

                if (index >= 0)
                {
                    _viewModel.SelectedRectangles.Add(_viewModel.Rectangles[index]);
                }
            }
        }

        private PointModel ToModel(Point point)
        {
            return new PointModel { X = point.X, Y = point.Y };
        }

        private bool IsInside(Point loc, Rectangle rect)
        {
            return loc.X >= rect.X && loc.X <= rect.Right &&
                   loc.Y >= rect.Y && loc.Y <= rect.Bottom;
        }
    }
}