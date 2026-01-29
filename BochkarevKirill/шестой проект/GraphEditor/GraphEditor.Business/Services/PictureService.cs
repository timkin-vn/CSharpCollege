using GraphEditor.Business.Models;
using GraphEditor.Business.Models.Xml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.Business.Services
{
    public class PictureService
    {
        public PictureModel PictureModel { get; private set; } = new PictureModel();

        public static Color DefaultFillColor = Color.Yellow;

        public static Color DefaultBorderColor = Color.Blue;

        public PictureService()
        {
            PictureModel.Rectangles.Add(new RectangleModel { Left = 100, Top = 50, Width = 200, Height = 150, FillColor = Color.LightCyan, });
            var newRectangle = new RectangleModel { Left = 200, Top = 100, Width = 200, Height = 150, };
            PictureModel.Rectangles.Add(newRectangle);
            PictureModel.SelectedRectangle = newRectangle;
            PictureModel.Rectangles.Add(new RectangleModel { Left = 300, Top = 200, Width = 200, Height = 150, FillColor = Color.Pink, });
        }

        public void CreateRectangle(PointModel loc)
        {
            var newRectangle = new RectangleModel 
            { 
                Left = loc.X, 
                Top = loc.Y, 
                Width = 0, 
                Height = 0, 
                FillColor = DefaultFillColor,
                BorderColor = DefaultBorderColor,
            };
            PictureModel.Rectangles.Add(newRectangle);
            PictureModel.SelectedRectangle = newRectangle;
            newRectangle.EditMode = EditMode.Creating;
        }

        public void DeleteRectangle()
        {
            if (PictureModel.SelectedRectangle == null)
            {
                return;
            }

            PictureModel.Rectangles.Remove(PictureModel.SelectedRectangle);
            PictureModel.SelectedRectangle = null;
        }

        public void SetResizeMode(EditMode mode)
        {
            PictureModel.SelectedRectangle.EditMode = mode;
        }

        public void SetMoveMode(PointModel loc)
        {
            var selectedRect = PictureModel.Rectangles.LastOrDefault(r => r.IsInside(loc));
            PictureModel.SelectedRectangle = selectedRect;

            if (selectedRect == null)
            {
                return;
            }

            PictureModel.SelectedCircle = null;

            if (selectedRect == null)
            {
                return;
            }

            selectedRect.Dx = loc.X - selectedRect.Left;
            selectedRect.Dy = loc.Y - selectedRect.Top;
            selectedRect.EditMode = EditMode.Moving;
        }

        public void ResetMode()
        {
            var selectedRect = PictureModel.SelectedRectangle;
            if (selectedRect != null)
            {
                selectedRect.Normalize();
                selectedRect.EditMode = EditMode.None;
            }
        }

        public void UpdateMovingPoint(PointModel loc)
        {
            var selectedRect = PictureModel.SelectedRectangle;
            if (selectedRect == null)
            {
                return;
            }

            switch (selectedRect.EditMode)
            {
                case EditMode.Creating:
                case EditMode.ResizeBR:
                    selectedRect.Right = loc.X;
                    selectedRect.Bottom = loc.Y;
                    break;

                case EditMode.ResizeR:
                    selectedRect.Right = loc.X;
                    break;

                case EditMode.Moving:
                    selectedRect.Left = loc.X - selectedRect.Dx;
                    selectedRect.Top = loc.Y - selectedRect.Dy;
                    break;
            }
        }

        public void OpenFile(string fileName)
        {
            var fileService = new FileService();
            PictureModel = fileService.OpenFile(fileName);
        }

        public void SaveToFile(string fileName)
        {
            var fileService = new FileService();
            fileService.SaveToFile(fileName, PictureModel);
        }

        public void CreateNewPicture()
        {
            PictureModel.Rectangles.Clear();
            PictureModel.SelectedRectangle = null;
        }

        public void SetFillColor(Color color)
        {
            if (PictureModel.SelectedRectangle != null)
            {
                PictureModel.SelectedRectangle.FillColor = color;
            }

            if (PictureModel.SelectedCircle != null)
            {
                PictureModel.SelectedCircle.FillColor = color;
            }
        }

        public void MoveForwardCircle()
        {
            if (PictureModel.SelectedCircle == null)
            {
                return;
            }

            var index = PictureModel.Circles.IndexOf(PictureModel.SelectedCircle);
            if (index < 0 || index == PictureModel.Circles.Count - 1)
            {
                return;
            }

            var circle = PictureModel.Circles[index + 1];
            PictureModel.Circles[index + 1] = PictureModel.Circles[index];
            PictureModel.Circles[index] = circle;
        }


        public void MoveForward()
        {
            if (PictureModel.SelectedRectangle == null)
            {
                return;
            }

            var index = PictureModel.Rectangles.IndexOf(PictureModel.SelectedRectangle);
            if (index < 0 || index == PictureModel.Rectangles.Count - 1)
            {
                return;
            }

            var rect = PictureModel.Rectangles[index + 1];
            PictureModel.Rectangles[index + 1] = PictureModel.Rectangles[index];
            PictureModel.Rectangles[index] = rect;
        }


        public void CreateCircle(PointModel loc)
        {
            var newCircle = new CircleModel
            {
                Left = loc.X,
                Top = loc.Y,
                Diameter = 0,
                FillColor = DefaultFillColor,
                BorderColor = DefaultBorderColor,
            };

            PictureModel.Circles.Add(newCircle);
            PictureModel.SelectedCircle = newCircle;
            newCircle.EditMode = EditMode.Creating;
        }

        public void SetBorderColor(Color color)
        {
            if (PictureModel.SelectedRectangle != null)
            {
                PictureModel.SelectedRectangle.BorderColor = color;
            }

            if (PictureModel.SelectedCircle != null)
            {
                PictureModel.SelectedCircle.BorderColor = color;
            }
        }


        public void DeleteCircle()
        {
            if (PictureModel.SelectedCircle == null)
            {
                return;
            }

            PictureModel.Circles.Remove(PictureModel.SelectedCircle);
            PictureModel.SelectedCircle = null;
        }

        public void SetResizeModeCircle(EditMode mode)
        {
            if (PictureModel.SelectedCircle == null)
            {
                return;
            }

            PictureModel.SelectedCircle.EditMode = mode;
        }

        public void SetMoveModeCircle(PointModel loc)
        {
            var selectedCircle = PictureModel.Circles.LastOrDefault(c => c.IsInside(loc));
            PictureModel.SelectedCircle = selectedCircle;

            if (selectedCircle == null)
            {
                return;
            }

            PictureModel.SelectedRectangle = null;

            if (selectedCircle == null)
            {
                return;
            }

            selectedCircle.Dx = loc.X - selectedCircle.Left;
            selectedCircle.Dy = loc.Y - selectedCircle.Top;
            selectedCircle.EditMode = EditMode.Moving;
        }

        public void ResetModeCircle()
        {
            var selectedCircle = PictureModel.SelectedCircle;
            if (selectedCircle != null)
            {
                selectedCircle.Normalize();
                selectedCircle.EditMode = EditMode.None;
            }
        }

        public void UpdateMovingPointCircle(PointModel loc)
        {
            var selectedCircle = PictureModel.SelectedCircle;
            if (selectedCircle == null)
            {
                return;
            }

            switch (selectedCircle.EditMode)
            {
                case EditMode.Creating:
                case EditMode.ResizeBR:
                    {
                        int dx = loc.X - selectedCircle.Left;
                        int dy = loc.Y - selectedCircle.Top;
                        selectedCircle.Diameter = Math.Max(dx, dy);
                        break;
                    }

                case EditMode.ResizeR:
                    selectedCircle.Diameter = loc.X - selectedCircle.Left;
                    break;

                case EditMode.Moving:
                    selectedCircle.Left = loc.X - selectedCircle.Dx;
                    selectedCircle.Top = loc.Y - selectedCircle.Dy;
                    break;
            }
        }

        public void SetFillColorCircle(Color color)
        {
            if (PictureModel.SelectedCircle != null)
            {
                PictureModel.SelectedCircle.FillColor = color;
            }
        }

        public void ClearCircles()
        {
            PictureModel.Circles.Clear();
            PictureModel.SelectedCircle = null;
        }

    }
}
