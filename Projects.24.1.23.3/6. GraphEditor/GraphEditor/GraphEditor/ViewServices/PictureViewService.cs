using GraphEditor.Business.Models;
using GraphEditor.Business.Services;
using GraphEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
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

        public bool CanDelete => !CreateMode && _businessService.PictureModel.SelectedRectangle != null;

        public void Paint(Graphics g)
        {
            new Painter().Paint(g, _viewModel, true);
        }

        public Cursor GetCursor(Point loc)
        {
            if (_viewModel == null)
            {
                return Cursors.Default;
            }

            var activeMarker = _viewModel.Markers.FirstOrDefault(m => m.IsActive && IsInside(loc, m.Rectangle));
            if (activeMarker != null)
            {
                return activeMarker.Cursor;
            }

            var activeRect = _viewModel.Rectangles.FirstOrDefault(r => IsInside(loc, r.Rectangle));
            if (activeRect != null)
            {
                return Cursors.SizeAll;
            }

            return Cursors.Default;
        }

        public void MouseDown(Point loc)
        {
            if (CreateMode)
            {
                _businessService.SetCreateMode(ToModel(loc));
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

            _businessService.SetMoveMode(ToModel(loc));
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

        public void CreateButtonClicked()
        {
            CreateMode = !CreateMode;
        }

        public void DeleteButtonClicked()
        {
            _businessService.DeleteRectangle();
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
                _viewModel.SelectedRectangle = _viewModel.Rectangles[index];
            }
        }

        private PointModel ToModel(Point point) => new PointModel { X = point.X, Y = point.Y };

        private bool IsInside(Point loc, Rectangle rect) =>
            loc.X >= rect.X && loc.X <= rect.Right &&
            loc.Y >= rect.Y && loc.Y <= rect.Bottom;
    }
}
