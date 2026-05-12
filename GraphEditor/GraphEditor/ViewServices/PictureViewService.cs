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

        public bool CanDelete => !CreateMode && _businessService.PictureModel.SelectedRectangle != null;

        public string FileName { get; set; }

        // Свойства для сетки
        public bool ShowGrid { get; set; } = false;
        public int GridSize { get; set; } = 20;
        public bool SnapToGridEnabled { get; set; } = true;

        // Свойства для масштабирования
        public float ZoomFactor { get; private set; } = 1.0f;
        private const float MinZoom = 0.1f;
        private const float MaxZoom = 5.0f;
        private const float ZoomStep = 0.1f;

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
            _businessService.DeleteRectangle();
            LoadViewModel();
        }

        // Дублирование выделенной фигуры
        public void DuplicateSelected()
        {
            var selected = _businessService.PictureModel.SelectedRectangle;
            if (selected == null)
                return;

            var duplicate = new RectangleModel
            {
                Left = selected.Left + 20,
                Top = selected.Top + 20,
                Width = selected.Width,
                Height = selected.Height,
                FillColor = selected.FillColor,
                BorderColor = selected.BorderColor,
                Opacity = selected.Opacity,
                RotationAngle = selected.RotationAngle,
                ShowShadow = selected.ShowShadow,
                ShadowOffsetX = selected.ShadowOffsetX,
                ShadowOffsetY = selected.ShadowOffsetY,
                ShadowColor = selected.ShadowColor,
                GradientType = selected.GradientType,
                FillColor2 = selected.FillColor2,
                GradientAngle = selected.GradientAngle
            };

            _businessService.PictureModel.Rectangles.Add(duplicate);
            _businessService.PictureModel.SelectedRectangle = duplicate;
            LoadViewModel();
        }

        // Получить выделенный прямоугольник (ViewModel)
        public RectangleViewModel GetSelectedRectangle()
        {
            return _viewModel?.SelectedRectangle;
        }

        public void Export(string fileName, Rectangle size, Color backColor)
        {
            using (var bmp = new Bitmap(size.Width, size.Height))
            {
                using (var g = Graphics.FromImage(bmp))
                {
                    g.Clear(backColor);
                    var painter = new Painter();
                    painter.ShowGrid = false;
                    painter.Paint(g, _viewModel, false);
                }
                bmp.Save(fileName, ImageFormat.Png);
            }
        }

        public Color GetCurrentFillColor()
        {
            return _viewModel?.SelectedRectangle?.FillColor ?? PictureService.DefaultFillColor;
        }

        public Color GetCurrentBorderColor()
        {
            return _viewModel?.SelectedRectangle?.BorderColor ?? PictureService.DefaultBorderColor;
        }

        public Cursor GetCursor(Point loc)
        {
            if (_viewModel == null)
                return Cursors.Default;

            if (CreateMode)
                return Cursors.Cross;

            var activeMarker = _viewModel.Markers.FirstOrDefault(m => m.IsActive && IsInside(loc, m.Rectangle));
            if (activeMarker != null)
                return activeMarker.Cursor;

            var activeRect = _viewModel.Rectangles.FirstOrDefault(r => IsInside(loc, r.Rectangle));
            if (activeRect != null)
                return Cursors.Hand;

            return Cursors.Default;
        }

        public bool HasRectangles()
        {
            return _viewModel != null && _viewModel.Rectangles != null && _viewModel.Rectangles.Any();
        }

        public void MouseDown(Point loc)
        {
            Point snappedLoc = SnapToGridEnabled ? SnapToGrid(loc) : loc;

            if (CreateMode)
            {
                _businessService.CreateAndSetCreateMode(ToModel(snappedLoc));
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

            _businessService.SelectAndSetMoveMode(ToModel(snappedLoc));
            LoadViewModel();
        }

        public void MouseMove(Point loc)
        {
            Point snappedLoc = SnapToGridEnabled ? SnapToGrid(loc) : loc;
            _businessService.UpdateMovingPoint(ToModel(snappedLoc));
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

        // На передний план
        public void MoveToFront()
        {
            _businessService.MoveToFront();
            LoadViewModel();
        }

        // На задний план
        public void MoveToBack()
        {
            _businessService.MoveToBack();
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
            var painter = new Painter();
            painter.ShowGrid = ShowGrid;
            painter.GridSize = GridSize;
            painter.ZoomFactor = ZoomFactor;
            painter.Paint(g, _viewModel, true);
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
                return;

            _businessService.SetFillColor(color);
            LoadViewModel();
        }

        public void SetBorderColor(Color color)
        {
            if (_viewModel?.SelectedRectangle == null)
                return;

            _businessService.SetBorderColor(color);
            LoadViewModel();
        }

        // Установка прозрачности
        public void SetOpacity(byte opacity)
        {
            if (_viewModel?.SelectedRectangle == null)
                return;

            _businessService.SetOpacity(opacity);
            LoadViewModel();
        }

        // Переключение тени
        public void ToggleShadow()
        {
            var selected = _viewModel?.SelectedRectangle;
            if (selected == null)
                return;

            _businessService.SetShadow(!selected.ShowShadow);
            LoadViewModel();
        }

        // Приблизить
        public void ZoomIn()
        {
            ZoomFactor = Math.Min(ZoomFactor + ZoomStep, MaxZoom);
        }

        // Отдалить
        public void ZoomOut()
        {
            ZoomFactor = Math.Max(ZoomFactor - ZoomStep, MinZoom);
        }

        // Сброс масштаба
        public void ZoomReset()
        {
            ZoomFactor = 1.0f;
        }

        private bool IsInside(Point loc, Rectangle rect) =>
            loc.X >= rect.X && loc.X <= rect.Right &&
            loc.Y >= rect.Y && loc.Y <= rect.Bottom;

        private Point SnapToGrid(Point point)
        {
            int snappedX = (int)(Math.Round((double)point.X / GridSize) * GridSize);
            int snappedY = (int)(Math.Round((double)point.Y / GridSize) * GridSize);
            return new Point(snappedX, snappedY);
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
                if (index >= 0 && index < _viewModel.Rectangles.Count)
                    _viewModel.SelectedRectangle = _viewModel.Rectangles[index];
            }
        }

        private PointModel ToModel(Point point) => new PointModel { X = point.X, Y = point.Y };
    }
}