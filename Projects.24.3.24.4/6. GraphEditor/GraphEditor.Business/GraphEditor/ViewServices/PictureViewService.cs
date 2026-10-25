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

        public PictureViewService() => LoadViewModel();

        public bool CreateMode { get; set; }
        public PictureService.FigureType CurrentCreationType { get; set; } = PictureService.FigureType.Rectangle;
        public string FileName { get; set; }

        public void Paint(Graphics g) => new Painter().Paint(g, _viewModel, true);

        public void MouseDown(Point loc)
        {
            if (CreateMode)
            {
                _businessService.CreateFigureAndSetCreateMode(ToModel(loc), CurrentCreationType);
                LoadViewModel();
                return;
            }

            if (_viewModel?.SelectedFigure != null)
            {
                var marker = _viewModel.Markers.FirstOrDefault(m => m.IsActive && IsInside(loc, m.Rectangle));
                if (marker != null)
                {
                    _businessService.SetResizeMode(marker.EditMode);
                    LoadViewModel();
                    return;
                }
            }

            _businessService.FindAndSetMoveMode(ToModel(loc));
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
            if (CreateMode) return Cursors.Cross;
            if (_viewModel?.SelectedFigure != null)
            {
                var marker = _viewModel.Markers.FirstOrDefault(m => m.IsActive && IsInside(loc, m.Rectangle));
                if (marker != null) return marker.Cursor;
                if (IsInside(loc, _viewModel.SelectedFigure.Bounds)) return Cursors.Hand;
            }
            return Cursors.Default;
        }

        public void DeleteSelected() { _businessService.DeleteFigure(); LoadViewModel(); }
        public void MoveForward() { _businessService.MoveForward(); LoadViewModel(); }
        public void MoveBackward() { _businessService.MoveBackward(); LoadViewModel(); }
        public void SetColor(Color color) { _businessService.SetFillColor(color); LoadViewModel(); }

        public void Save(string path) { _businessService.Save(path); FileName = path; }
        public void Open(string path) { _businessService.Open(path); FileName = path; LoadViewModel(); }
        public void CreateNew() { _businessService.CreateNewPicture(); FileName = null; LoadViewModel(); }

        public void ExportPng(string path, Rectangle bounds)
        {
            using (var bmp = new Bitmap(bounds.Width, bounds.Height))
            {
                using (var g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.White);
                    new Painter().Paint(g, _viewModel, false); // без маркеров выделения
                }
                bmp.Save(path, ImageFormat.Png);
            }
        }

        private void LoadViewModel()
        {
            var model = _businessService.PictureModel;
            _viewModel = new PictureViewModel
            {
                Figures = model.Figures.Select(f => FigureViewModel.FromBusiness(f)).ToList()
            };
            if (model.SelectedFigure != null)
            {
                int idx = model.Figures.IndexOf(model.SelectedFigure);
                if (idx >= 0) _viewModel.SelectedFigure = _viewModel.Figures[idx];
            }
        }

        private PointModel ToModel(Point p) => new PointModel { X = p.X, Y = p.Y };
        private bool IsInside(Point loc, Rectangle r) => loc.X >= r.X && loc.X <= r.Right && loc.Y >= r.Y && loc.Y <= r.Bottom;
    }
}