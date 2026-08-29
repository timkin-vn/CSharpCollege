using GraphEditor.Business.Models;
using GraphEditor.Business.Services;
using GraphEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        public FigureType CurrentFigureType { get; set; } = FigureType.Rectangle;

        public bool CanDelete => !CreateMode && _businessService.PictureModel.SelectedFigure != null;
        public string FileName { get; set; }

        public void Paint(Graphics g)
        {
            new Painter().Paint(g, _viewModel, true);
        }

        private PictureViewModel FromBusiness(PictureModel model)
        {
            var result = new PictureViewModel
            {
                Figures = model.Figures.Select(f => ConvertToViewModel(f)).ToList(),
            };

            if (model.SelectedFigure != null)
            {
                var index = model.Figures.IndexOf(model.SelectedFigure);
                if (index >= 0 && index < result.Figures.Count)
                {
                    result.SelectedFigure = result.Figures[index];
                }
            }

            return result;
        }

        private FigureViewModel ConvertToViewModel(FigureModel model)
        {
            if (model is RectangleModel rect)
                return RectangleViewModel.FromBusiness(rect);
            else if (model is CircleModel circle)
                return CircleViewModel.FromBusiness(circle);
            else if (model is TriangleModel triangle)
                return TriangleViewModel.FromBusiness(triangle);
            else
                return RectangleViewModel.FromBusiness((RectangleModel)model);
        }

        public Cursor GetCursor(Point loc)
        {
            if (_viewModel == null)
            {
                return Cursors.Default;
            }

            var activeMarker = _viewModel.Markers.FirstOrDefault(m => IsInside(loc, m.Rectangle));
            if (activeMarker != null)
            {
                return activeMarker.Cursor;
            }

            foreach (var figure in _viewModel.Figures)
            {
                if (IsInside(loc, figure.Rectangle))
                {
                    return Cursors.SizeAll;
                }
            }

            return Cursors.Default;
        }

        public void MouseDown(Point loc)
        {
            if (CreateMode)
            {
                _businessService.SetFigureType(CurrentFigureType);
                _businessService.CreateFigure(ToModel(loc));
                _viewModel = FromBusiness(_businessService.PictureModel);
                return;
            }

            if (_viewModel.SelectedFigure != null)
            {
                foreach (var marker in _viewModel.Markers)
                {
                    if (IsInside(loc, marker.Rectangle))
                    {
                        _businessService.SetResizeMode(marker.EditMode);
                        return;
                    }
                }
            }

            var previouslySelected = _businessService.PictureModel.SelectedFigure;
            _businessService.SetMoveMode(ToModel(loc));

            if (_businessService.PictureModel.SelectedFigure != previouslySelected)
            {
                _viewModel = FromBusiness(_businessService.PictureModel);
            }
        }

        public void ChangeSelectedFillColor(Color newColor)
        {
            if (_businessService.PictureModel.SelectedFigure == null) return;

            _businessService.PictureModel.SelectedFigure.FillColor = newColor;
            Refresh();
        }

        public void ChangeSelectedBorderColor(Color newColor)
        {
            if (_businessService.PictureModel.SelectedFigure == null) return;

            _businessService.PictureModel.SelectedFigure.BorderColor = newColor;
            Refresh();
        }

        public void SetFigureType(FigureType figureType)
        {
            CurrentFigureType = figureType;
            _businessService.SetFigureType(figureType);
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
            _businessService.DeleteFigure();
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

        private bool IsInside(Point p, Rectangle rect) =>
            p.X >= rect.X && p.X <= rect.Right &&
            p.Y >= rect.Y && p.Y <= rect.Bottom;

        private PointModel ToModel(Point loc) => new PointModel { X = loc.X, Y = loc.Y };

        public int GetFiguresCount()
        {
            return _businessService.PictureModel.Figures.Count;
        }

        public void Refresh()
        {
            _viewModel = FromBusiness(_businessService.PictureModel);
        }

    }

}