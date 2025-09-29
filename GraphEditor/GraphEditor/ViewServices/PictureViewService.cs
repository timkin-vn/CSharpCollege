﻿using GraphEditor.Business.Models;
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
        public void SetGradientFill(Color color) 
        {
            _businessService.SetGradientFill(color);
            _viewModel = FromBusiness(_businessService.PictureModel);
        }
        public void MoveForward()
        {
            _businessService.MoveForward();
            _viewModel = FromBusiness(_businessService.PictureModel);
        }
        public void BackToFront() 
        {
            _businessService.BackToFront();
            _viewModel = FromBusiness(_businessService.PictureModel);
        }
        public void LastLayer() 
        {
            _businessService.LastLayer();
            _viewModel = FromBusiness(_businessService.PictureModel);
        }
        public void FrontLayer() 
        {
            _businessService.FrontLayer();
            _viewModel = FromBusiness(_businessService.PictureModel);
        }
        private bool IsInside(Point p, Rectangle rect) =>
            p.X >= rect.X && p.X <= rect.Right &&
            p.Y >= rect.Y && p.Y <= rect.Bottom;

        private PointModel ToModel(Point loc) => new PointModel { X =  loc.X, Y = loc.Y, };
    }
}
