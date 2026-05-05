using GraphEditor.Business.Models;
using GraphEditor.Business.Services;
using GraphEditor.ViewModels;
using System;
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
        public bool CanDelete => !CreateMode && _businessService.PictureModel.SelectedFigure != null;
        public string FileName { get; set; }

        public void SetFigureType(FigureType type) => _businessService.SetFigureType(type);

        public void CreateButtonClicked() => CreateMode = !CreateMode;
        public void CreateNewPicture() { _businessService.CreateNewPicture(); FileName = string.Empty; LoadViewModel(); }
        public void DeleteButtonClicked() { _businessService.DeleteRectangle(); LoadViewModel(); }

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

        public Color GetCurrentFillColor() => _viewModel?.SelectedFigure?.FillColor ?? PictureService.DefaultFillColor;
        public Cursor GetCursor(Point loc)
        {
            if (_viewModel == null) return Cursors.Default;
            if (CreateMode) return Cursors.Cross;
            var activeMarker = _viewModel.Markers.FirstOrDefault(m => m.IsActive && IsInside(loc, m.Rectangle));
            if (activeMarker != null) return activeMarker.Cursor;
            var activeFig = _viewModel.Figures.FirstOrDefault(r => IsInside(loc, r.Rectangle));
            return activeFig != null ? Cursors.Hand : Cursors.Default;
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

        public void MouseMove(Point loc) { _businessService.UpdateMovingPoint(ToModel(loc)); LoadViewModel(); }
        public void MouseUp() { _businessService.ResetMode(); CreateMode = false; LoadViewModel(); }
        public void MoveForward() { _businessService.MoveForward(); LoadViewModel(); }
        public void Open(string fileName) { _businessService.Open(fileName); FileName = fileName; LoadViewModel(); }
        public void Paint(Graphics g) => new Painter().Paint(g, _viewModel, true);
        public void Save() => _businessService.Save(FileName);
        public void Save(string fileName) { _businessService.Save(fileName); FileName = fileName; }
        public void SetFillColor(Color color) { if (_viewModel?.SelectedFigure != null) _businessService.SetFillColor(color); LoadViewModel(); }

        private bool IsInside(Point loc, Rectangle rect) =>
            loc.X >= rect.X && loc.X <= rect.Right && loc.Y >= rect.Y && loc.Y <= rect.Bottom;

        private void LoadViewModel()
        {
            var model = _businessService.PictureModel;
            _viewModel = new PictureViewModel
            {
                Figures = model.Figures.Select(r => FigureViewModel.FromBusiness(r)).ToList(),
            };
            if (model.SelectedFigure != null)
            {
                var index = model.Figures.IndexOf(model.SelectedFigure);
                _viewModel.SelectedFigure = _viewModel.Figures[index];
            }
        }

        private PointModel ToModel(Point point) => new PointModel { X = point.X, Y = point.Y };
    }
}