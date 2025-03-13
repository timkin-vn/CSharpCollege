using GraphEditor.Business.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.Business.Services
{
    public class PictureService
    {
        private PictureModel _model = new PictureModel();

        public PictureModel PictureModel => _model;

        public void CreateRectangle(PointModel location)
        {
            var newRectangle = new RectangleModel { Point1 = location, Point2 = location };
        
            _model.RectangleList.Add(newRectangle);
            _model.SelectedRectangle = newRectangle;
            _model.Mode = PictureMode.Creating;
        }

        public void SetMoveMode(PointModel location)
        {
            var selectedRect = _model.RectangleList.LastOrDefault(r => r.IsInside(location));
            _model.SelectedRectangle = selectedRect;

            if (selectedRect == null)
            {
                return;
            }

            _model.Dx = location.X - selectedRect.X;
            _model.Dy = location.Y - selectedRect.Y;
            _model.Mode = PictureMode.Moving;
        }

        public void SetResizeMode(PictureMode mode)
        {
            _model.Mode = mode;
        }

        public void Move(PointModel location)
        {
            var rect = _model.SelectedRectangle;

            if (rect == null)
            {
                return;
            }

            switch (_model.Mode)
            {
                case PictureMode.Creating:
                case PictureMode.ResizeBR:
                    rect.Point2 = location;
                    break;
                case PictureMode.Moving:
                    rect.X = location.X - _model.Dx;
                    rect.Y = location.Y - _model.Dy;
                    break;
            }
        }

        public void ResetMode()
        {
            _model.SelectedRectangle?.Normalize();
            _model.Mode = PictureMode.None;
        }

        public void Delete()
        {
            if (_model.SelectedRectangle == null)
            {
                return;
            }

            _model.RectangleList.Remove(_model.SelectedRectangle);
            _model.SelectedRectangle = null;
        }

        public void SetSelectedFillColor(Color color)
        {
            if (_model.SelectedRectangle != null)
            {
                _model.SelectedRectangle.FillColor = color;  
                
                _model.IsNewFillColor = true;
                
            }
        }   
        public void SetSelectedDrawColor(Color color)
        {
            if (_model.SelectedRectangle != null)
            {
                _model.SelectedRectangle.DrawColor = color;
                _model.IsNewDrawColor = true;
            }
        }

        public void MoveForward()
        {
            if (_model.SelectedRectangle == null)
            {
                return;
            }

            var index = _model.RectangleList.IndexOf(_model.SelectedRectangle);
            if (index == _model.RectangleList.Count - 1)
            {
                return;
            }

            var r = _model.RectangleList[index];
            _model.RectangleList[index] = _model.RectangleList[index + 1];
            _model.RectangleList[index + 1] = r;
        }  
        public void MoveFurther()
        {
            if (_model.SelectedRectangle == null)
            {
                return;
            }

            var index = _model.RectangleList.IndexOf(_model.SelectedRectangle);
            if (index == 0 || index<0)
            {
                return;
            }

            var r = _model.RectangleList[index];
            _model.RectangleList[index] = _model.RectangleList[index - 1];
            _model.RectangleList[index - 1] = r;
        }

        public void Save(string fileName)
        {
            var fileService = new FileService();
            fileService.SaveFile(fileName, _model);
        }

        public void Open(string fileName)
        {
            var fileService = new FileService();
            _model = fileService.OpenFile(fileName);
        }
    }
}
