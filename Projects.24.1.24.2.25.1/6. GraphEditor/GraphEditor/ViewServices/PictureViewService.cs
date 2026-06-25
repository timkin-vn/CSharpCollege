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

        public ShapeType CurrentShapeType { get; private set; } = ShapeType.Rectangle;

        public bool CanDelete => !CreateMode && _businessService.PictureModel.SelectedRectangle != null;

        public bool CanMoveForward => CanMoveOrder(1);

        public bool CanMoveBackward => CanMoveOrder(-1);

        public string FileName { get; set; }

        public string CurrentShapeName
        {
            get
            {
                switch (CurrentShapeType)
                {
                    case ShapeType.Square:
                        return "Квадрат";
                    case ShapeType.Circle:
                        return "Круг";
                    case ShapeType.Hexagon:
                        return "Шестиугольник";
                    default:
                        return "Прямоугольник";
                }
            }
        }

        public void CreateButtonClicked()
        {
            CreateMode = !CreateMode;
        }

        public void SetCurrentShapeType(ShapeType shapeType)
        {
            CurrentShapeType = shapeType;
            CreateMode = true;
        }

        public void CreateNewPicture()
        {
            _businessService.CreateNewPicture();
            FileName = string.Empty;
            CreateMode = false;
            LoadViewModel();
        }

        public void DeleteButtonClicked()
        {
            _businessService.DeleteRectangle();
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
            return _viewModel?.SelectedRectangle?.FillColor ?? PictureService.DefaultFillColor;
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

            var activeRect = _viewModel.Rectangles.LastOrDefault(r => IsInside(loc, r.Rectangle));
            if (activeRect != null)
            {
                return Cursors.Hand;
            }

            return Cursors.Default;
        }

        public void MouseDown(Point loc)
        {
            if (CreateMode)
            {
                _businessService.CreateAndSetCreateMode(ToModel(loc), CurrentShapeType);
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
            _businessService.UpdateMovingPoint(ToModel(loc));
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

        public void MoveToFront()
        {
            _businessService.MoveToFront();
            LoadViewModel();
        }

        public void MoveToBack()
        {
            _businessService.MoveToBack();
            LoadViewModel();
        }

        public void Open(string fileName)
        {
            _businessService.Open(fileName);
            FileName = fileName;
            CreateMode = false;
            LoadViewModel();
        }

        public void Paint(Graphics g)
        {
            new Painter().Paint(g, _viewModel, true);
        }

        public void Save()
        {
            _businessService.Save(FileName);
        }

        public void Save(string fileName)
        {
            _businessService.Save(fileName);
            FileName = fileName;
        }

        public void SetFillColor(Color color)
        {
            if (_viewModel?.SelectedRectangle == null)
            {
                return;
            }

            _businessService.SetFillColor(color);
            LoadViewModel();
        }

        private bool CanMoveOrder(int delta)
        {
            if (CreateMode || _viewModel?.SelectedRectangle == null || _viewModel.Rectangles == null)
            {
                return false;
            }

            var index = _viewModel.Rectangles.IndexOf(_viewModel.SelectedRectangle);
            var newIndex = index + delta;
            return newIndex >= 0 && newIndex < _viewModel.Rectangles.Count;
        }

        private bool IsInside(Point loc, Rectangle rect) =>
            loc.X >= rect.X && loc.X <= rect.Right &&
            loc.Y >= rect.Y && loc.Y <= rect.Bottom;

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
                if (index >= 0 && index < _viewModel.Rectangles.Count)
                {
                    _viewModel.SelectedRectangle = _viewModel.Rectangles[index];
                }
            }
        }

        private PointModel ToModel(Point point) => new PointModel { X = point.X, Y = point.Y };
    }
}
