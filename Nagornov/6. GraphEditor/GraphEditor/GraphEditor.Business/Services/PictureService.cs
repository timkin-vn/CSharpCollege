using GraphEditor.Business.Models;
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

        public bool CreateRectangleMode { get; set; }
        public bool CreateCircleMode { get; set; }

        public PictureService()
        {
            PictureModel.Rectangles.Add(new RectangleModel { Left = 150, Top = 50, Width = 200, Height = 100, FillColor = Color.LightSkyBlue });
            var newRectangle = new RectangleModel { Left = 200, Top = 100, Width = 200, Height = 150 };
            PictureModel.Rectangles.Add(newRectangle);
            PictureModel.SelectedRectangle = newRectangle;
            PictureModel.Rectangles.Add(new RectangleModel { Left = 250, Top = 150, Width = 200, Height = 200, FillColor = Color.DarkMagenta });

            var newCircle = new CircleModel { CenterX = 400, CenterY = 300, Radius = 80, FillColor = Color.LightGreen };
            PictureModel.Circles.Add(newCircle);
            PictureModel.SelectedCircle = newCircle;
        }

        public void SetMoveMode(PointModel point)
        {
            PictureModel.SelectedRectangle = null;
            PictureModel.SelectedCircle = null;


            var selectedCircle = PictureModel.Circles.LastOrDefault(r => r.IsInside(point));
            if (selectedCircle != null)
            {
                PictureModel.SelectedCircle = selectedCircle;
                selectedCircle.EditMode = EditMode.Moving;
                selectedCircle.Dx = point.X - selectedCircle.CenterX;
                selectedCircle.Dy = point.Y - selectedCircle.CenterY;
                return;
            }

            var selectedRect = PictureModel.Rectangles.LastOrDefault(r => r.IsInside(point));
            if (selectedRect != null)
            {
                PictureModel.SelectedRectangle = selectedRect;
                selectedRect.EditMode = EditMode.Moving;
                selectedRect.Dx = point.X - selectedRect.Left;
                selectedRect.Dy = point.Y - selectedRect.Top;
            }
        }

        public void SetResizeMode(EditMode mode)
        {
            if (PictureModel.SelectedRectangle != null)
                PictureModel.SelectedRectangle.EditMode = mode;
            else if (PictureModel.SelectedCircle != null)
                PictureModel.SelectedCircle.EditMode = mode;
        }

        public void SetCreateRectangleMode(PointModel point)
        {
            var newRectangle = new RectangleModel
            {
                Left = point.X,
                Top = point.Y,
                Width = 0,
                Height = 0,
                EditMode = EditMode.Creating
            };

            PictureModel.Rectangles.Add(newRectangle);
            PictureModel.SelectedRectangle = newRectangle;
            PictureModel.SelectedCircle = null;
            CreateCircleMode = false;
        }

        public void SetCreateCircleMode(PointModel point)
        {
            var newCircle = new CircleModel
            {
                CenterX = point.X,
                CenterY = point.Y,
                Radius = 0,
                EditMode = EditMode.CreatingCircle
            };

            PictureModel.Circles.Add(newCircle);
            PictureModel.SelectedCircle = newCircle;
            PictureModel.SelectedRectangle = null;

            CreateRectangleMode = false;
        }

        public void ResetMode()
        {
            if (PictureModel.SelectedRectangle != null)
            {
                PictureModel.SelectedRectangle.EditMode = EditMode.None;
                PictureModel.SelectedRectangle.Normalize();
            }
            if (PictureModel.SelectedCircle != null)
            {
                PictureModel.SelectedCircle.EditMode = EditMode.None;
                PictureModel.SelectedCircle.Normalize();
            }
        }

        public void UpdateMovingPoint(PointModel currentPoint)
        {
            if (PictureModel.SelectedRectangle != null)
            {
                UpdateRectangleMovingPoint(currentPoint);
            }
            else if (PictureModel.SelectedCircle != null)
            {
                UpdateCircleMovingPoint(currentPoint);
            }
        }

        private void UpdateRectangleMovingPoint(PointModel currentPoint)
        {
            var rect = PictureModel.SelectedRectangle;

            switch (rect.EditMode)
            {
                case EditMode.Creating:
                    rect.Width = currentPoint.X - rect.Left;
                    rect.Height = currentPoint.Y - rect.Top;
                    break;

                case EditMode.Moving:
                    rect.Left = currentPoint.X - rect.Dx;
                    rect.Top = currentPoint.Y - rect.Dy;
                    break;

                case EditMode.ResizeT:
                    rect.Height += rect.Top - currentPoint.Y;
                    rect.Top = currentPoint.Y;
                    break;

                case EditMode.ResizeB:
                    rect.Height = currentPoint.Y - rect.Top;
                    break;

                case EditMode.ResizeL:
                    rect.Width += rect.Left - currentPoint.X;
                    rect.Left = currentPoint.X;
                    break;

                case EditMode.ResizeR:
                    rect.Width = currentPoint.X - rect.Left;
                    break;

                case EditMode.ResizeTL:
                    rect.Width += rect.Left - currentPoint.X;
                    rect.Height += rect.Top - currentPoint.Y;
                    rect.Left = currentPoint.X;
                    rect.Top = currentPoint.Y;
                    break;

                case EditMode.ResizeTR:
                    rect.Width = currentPoint.X - rect.Left;
                    rect.Height += rect.Top - currentPoint.Y;
                    rect.Top = currentPoint.Y;
                    break;

                case EditMode.ResizeBL:
                    rect.Width += rect.Left - currentPoint.X;
                    rect.Height = currentPoint.Y - rect.Top;
                    rect.Left = currentPoint.X;
                    break;

                case EditMode.ResizeBR:
                    rect.Width = currentPoint.X - rect.Left;
                    rect.Height = currentPoint.Y - rect.Top;
                    break;
            }

            if (rect.EditMode != EditMode.None && rect.EditMode != EditMode.Moving)
            {
                rect.Normalize();
            }
        }

        private void UpdateCircleMovingPoint(PointModel currentPoint)
        {
            var circle = PictureModel.SelectedCircle;

            switch (circle.EditMode)
            {
                case EditMode.CreatingCircle:
                    int dx = currentPoint.X - circle.CenterX;
                    int dy = currentPoint.Y - circle.CenterY;
                    circle.Radius = (int)Math.Sqrt(dx * dx + dy * dy);
                    break;

                case EditMode.Moving:
                    circle.CenterX = currentPoint.X - circle.Dx;
                    circle.CenterY = currentPoint.Y - circle.Dy;
                    break;

                case EditMode.ResizeT:
                    circle.Radius = circle.CenterY - currentPoint.Y;
                    break;

                case EditMode.ResizeB:
                    circle.Radius = currentPoint.Y - circle.CenterY;
                    break;

                case EditMode.ResizeL:
                    circle.Radius = circle.CenterX - currentPoint.X;
                    break;

                case EditMode.ResizeR:
                    circle.Radius = currentPoint.X - circle.CenterX;
                    break;

                case EditMode.ResizeTL:
                    circle.Radius = Math.Min(circle.CenterX - currentPoint.X, circle.CenterY - currentPoint.Y);
                    break;

                case EditMode.ResizeTR:
                    circle.Radius = Math.Min(currentPoint.X - circle.CenterX, circle.CenterY - currentPoint.Y);
                    break;

                case EditMode.ResizeBL:
                    circle.Radius = Math.Min(circle.CenterX - currentPoint.X, currentPoint.Y - circle.CenterY);
                    break;

                case EditMode.ResizeBR:
                    circle.Radius = Math.Min(currentPoint.X - circle.CenterX, currentPoint.Y - circle.CenterY);
                    break;
            }

            if (circle.EditMode != EditMode.None && circle.EditMode != EditMode.Moving)
            {
                circle.Normalize();
            }
        }

        public void DeleteRectangle()
        {
            if (PictureModel.SelectedRectangle != null)
            {
                PictureModel.Rectangles.Remove(PictureModel.SelectedRectangle);
                PictureModel.SelectedRectangle = null;
            }
            else if (PictureModel.SelectedCircle != null)
            {
                PictureModel.Circles.Remove(PictureModel.SelectedCircle);
                PictureModel.SelectedCircle = null;
            }
        }

        public void ChangeColor(Color newColor)
        {
            if (PictureModel.SelectedRectangle != null)
            {
                PictureModel.SelectedRectangle.FillColor = newColor;
            }
            else if (PictureModel.SelectedCircle != null)
            {
                PictureModel.SelectedCircle.FillColor = newColor;
            }
        }

        public void ToggleCreateRectangleMode()
        {
            CreateRectangleMode = !CreateRectangleMode;
            if (CreateRectangleMode)
            {
                CreateCircleMode = false; 
            }
        }

        public void ToggleCreateCircleMode()
        {
            CreateCircleMode = !CreateCircleMode;
            if (CreateCircleMode)
            {
                CreateRectangleMode = false; 
            }
        }

        public bool IsInCreateMode()
        {
            return CreateRectangleMode || CreateCircleMode;
        }
    }
}