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
            };

            if (model.SelectedRectangle != null)
            {
                var index = model.Rectangles.IndexOf(model.SelectedRectangle);
                if (index >= 0)
                {
                    result.SelectedRectangle = result.Rectangles[index];
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
                _businessService.CreateRectangle(ToModel(loc));
                _viewModel = FromBusiness(_businessService.PictureModel);
                return;
            }

            if (_viewModel != null)
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
            return _viewModel?.SelectedRectangle?.FillColor ?? PictureService.DefaultFillColor;
        }

        public void SetFillColor(Color color)
        {
            _businessService.SetFillColor(color);
            _viewModel = FromBusiness(_businessService.PictureModel);
        }
        public Color GetBorderColor()
        {
            return _viewModel?.SelectedRectangle?.BorderColor ?? PictureService.DefaultBorderColor;
        }

        public void SetBorderColor(Color color)
        {
            _businessService.SetBorderColor(color);
            _viewModel = FromBusiness(_businessService.PictureModel);
        }

        public bool MoveToFront()
        {
            var result = _businessService.MoveToFront();
            _viewModel = FromBusiness(_businessService.PictureModel);
            return result;
        }

        public bool MoveToBack()
        {
            var result = _businessService.MoveToBack();
            _viewModel = FromBusiness(_businessService.PictureModel);
            return result;
        }

        public bool MoveForward()
        {
            var result = _businessService.MoveForward();
            _viewModel = FromBusiness(_businessService.PictureModel);
            return result;
        }

        public bool MoveBackward()
        {
            var result = _businessService.MoveBackward();
            _viewModel = FromBusiness(_businessService.PictureModel);
            return result;
        }

        public Color GetGradientColor1()
        {
            return _viewModel?.SelectedRectangle?.GradientColor1 ?? Color.Yellow;
        }

        public Color GetGradientColor2()
        {
            return _viewModel?.SelectedRectangle?.GradientColor2 ?? Color.Red;
        }

        public void SetGradientColors(Color color1, Color color2)
{
    try
    {
        if (color1 == null || color2 == null)
        {
            MessageBox.Show("Некорректные цвета градиента!");
            return;
        }

        if (_businessService == null)
        {
            MessageBox.Show("Сервис не инициализирован!");
            return;
        }

        _businessService.SetGradientColors(color1, color2);
        
        if (_businessService.PictureModel == null)
        {
            MessageBox.Show("Модель данных не найдена!");
            return;
        }

        var newViewModel = FromBusiness(_businessService.PictureModel);
        if (newViewModel != null)
        {
            _viewModel = newViewModel;
        }
        else
        {
            MessageBox.Show("Ошибка преобразования данных!");
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Ошибка установки градиента: {ex.Message}");
    }
}
        public FillType GetFillType()
        {
            return _viewModel?.SelectedRectangle?.FillType ?? FillType.SolidColor;
        }

        public void SetFillType(FillType fillType)
        {
            _businessService.SetFillType(fillType);
            _viewModel = FromBusiness(_businessService.PictureModel);
        }

        public PictureModel PictureModel { get; set; } = new PictureModel();
        public RectangleModel SelectedRectangle
        {
            get => PictureModel.SelectedRectangle;
            set => PictureModel.SelectedRectangle = value;
        }
        public void SetTexturePath(string texturePath)
        {
            try
            {
                if (_businessService == null)
                {
                    MessageBox.Show("BusinessService не инициализирован!");
                    return;
                }
                _businessService.SetTexturePath(texturePath);
                _viewModel = FromBusiness(_businessService.PictureModel);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка установки текстуры: {ex.Message}");
            }
        }

        private bool IsInside(Point p, Rectangle rect) =>
            p.X >= rect.X && p.X <= rect.Right &&
            p.Y >= rect.Y && p.Y <= rect.Bottom;

        private PointModel ToModel(Point loc) => new PointModel { X =  loc.X, Y = loc.Y, };
    }
}
