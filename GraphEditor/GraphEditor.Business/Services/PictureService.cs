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

        public static Color DefaultFillColor { get; set; } = Color.Yellow;
        public static Color DefaultBorderColor { get; set; } = Color.Blue;

        public PictureService()
        {
        }

        public void CreateAndSetCreateMode(PointModel loc, ShapeType shapeType)
        {
            var newShape = new RectangleModel
            {
                Left = loc.X,
                Top = loc.Y,
                Width = 0,
                Height = 0,
                ShapeType = shapeType,
                FillColor = DefaultFillColor,
                BorderColor = DefaultBorderColor,
            };

            PictureModel.Rectangles.Add(newShape);
            PictureModel.SelectedRectangle = newShape;
            newShape.EditMode = EditMode.Creating;
        }

        public void CreateNewPicture()
        {
            PictureModel.Rectangles.Clear();
            PictureModel.SelectedRectangle = null;
        }

        public void DeleteRectangle()
        {
            if (PictureModel.SelectedRectangle == null)
                return;

            PictureModel.Rectangles.Remove(PictureModel.SelectedRectangle);
            PictureModel.SelectedRectangle = null;
        }

        public void MoveForward()
        {
            if (PictureModel.SelectedRectangle == null)
                return;

            var selectedIndex = PictureModel.Rectangles.IndexOf(PictureModel.SelectedRectangle);
            if (selectedIndex < 0 || selectedIndex == PictureModel.Rectangles.Count - 1)
                return;

            var rect = PictureModel.Rectangles[selectedIndex + 1];
            PictureModel.Rectangles[selectedIndex + 1] = PictureModel.Rectangles[selectedIndex];
            PictureModel.Rectangles[selectedIndex] = rect;
        }

        public void MoveBackward()
        {
            if (PictureModel.SelectedRectangle == null)
                return;

            var selectedIndex = PictureModel.Rectangles.IndexOf(PictureModel.SelectedRectangle);
            if (selectedIndex <= 0)
                return;

            var rect = PictureModel.Rectangles[selectedIndex - 1];
            PictureModel.Rectangles[selectedIndex - 1] = PictureModel.Rectangles[selectedIndex];
            PictureModel.Rectangles[selectedIndex] = rect;
        }

        public void MoveToFront()
        {
            if (PictureModel.SelectedRectangle == null)
                return;

            PictureModel.Rectangles.Remove(PictureModel.SelectedRectangle);
            PictureModel.Rectangles.Add(PictureModel.SelectedRectangle);
        }

        public void MoveToBack()
        {
            if (PictureModel.SelectedRectangle == null)
                return;

            PictureModel.Rectangles.Remove(PictureModel.SelectedRectangle);
            PictureModel.Rectangles.Insert(0, PictureModel.SelectedRectangle);
        }

        public void Open(string fileName)
        {
            PictureModel = new FileService().Open(fileName);
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

        public void Save(string fileName)
        {
            new FileService().Save(fileName, PictureModel);
        }

        public void SelectAndSetMoveMode(PointModel loc)
        {
            var selectedRect = PictureModel.Rectangles.LastOrDefault(r => r.IsInside(loc));
            PictureModel.SelectedRectangle = selectedRect;

            if (selectedRect == null)
                return;

            selectedRect.EditMode = EditMode.Moving;
            selectedRect.Dx = loc.X - selectedRect.Left;
            selectedRect.Dy = loc.Y - selectedRect.Top;
        }

        public void SetFillColor(Color color)
        {
            if (PictureModel.SelectedRectangle != null)
                PictureModel.SelectedRectangle.FillColor = color;
        }

        public void SetBorderColor(Color color)
        {
            if (PictureModel.SelectedRectangle != null)
                PictureModel.SelectedRectangle.BorderColor = color;
        }

        public void SetOpacity(byte opacity)
        {
            if (PictureModel.SelectedRectangle != null)
                PictureModel.SelectedRectangle.Opacity = opacity;
        }

        public void SetRotationAngle(float angle)
        {
            if (PictureModel.SelectedRectangle != null)
                PictureModel.SelectedRectangle.RotationAngle = angle;
        }

        public void SetShadow(bool showShadow, int offsetX = 5, int offsetY = 5, Color? shadowColor = null)
        {
            if (PictureModel.SelectedRectangle != null)
            {
                PictureModel.SelectedRectangle.ShowShadow = showShadow;
                PictureModel.SelectedRectangle.ShadowOffsetX = offsetX;
                PictureModel.SelectedRectangle.ShadowOffsetY = offsetY;
                PictureModel.SelectedRectangle.ShadowColor = shadowColor ?? Color.Gray;
            }
        }

        public void SetGradient(GradientType gradientType, Color color2, float angle = 0)
        {
            if (PictureModel.SelectedRectangle != null)
            {
                PictureModel.SelectedRectangle.GradientType = gradientType;
                PictureModel.SelectedRectangle.FillColor2 = color2;
                PictureModel.SelectedRectangle.GradientAngle = angle;
            }
        }

        public void SetResizeMode(EditMode mode)
        {
            PictureModel.SelectedRectangle.EditMode = mode;
        }

        public void UpdateMovingPoint(PointModel loc)
        {
            var r = PictureModel.SelectedRectangle;
            if (r == null)
                return;

            switch (r.EditMode)
            {
                case EditMode.Creating:
                case EditMode.ResizeBR:
                    r.Width = loc.X - r.Left;
                    r.Height = loc.Y - r.Top;
                    break;

                case EditMode.ResizeR:
                    r.Width = loc.X - r.Left;
                    break;

                case EditMode.ResizeB:
                    r.Height = loc.Y - r.Top;
                    break;

                case EditMode.ResizeL:
                    r.Width = r.Right - loc.X;
                    r.Left = loc.X;
                    break;

                case EditMode.ResizeT:
                    r.Height = r.Bottom - loc.Y;
                    r.Top = loc.Y;
                    break;

                case EditMode.ResizeTL:
                    r.Width = r.Right - loc.X;
                    r.Left = loc.X;
                    r.Height = r.Bottom - loc.Y;
                    r.Top = loc.Y;
                    break;

                case EditMode.ResizeTR:
                    r.Width = loc.X - r.Left;
                    r.Height = r.Bottom - loc.Y;
                    r.Top = loc.Y;
                    break;

                case EditMode.ResizeBL:
                    r.Width = r.Right - loc.X;
                    r.Left = loc.X;
                    r.Height = loc.Y - r.Top;
                    break;

                case EditMode.Moving:
                    r.Left = loc.X - r.Dx;
                    r.Top = loc.Y - r.Dy;
                    break;

                case EditMode.ResizeLineStart:
                    if (r.ShapeType == ShapeType.Line)
                    {
                        int endX = r.EndX;
                        int endY = r.EndY;
                        r.Left = loc.X;
                        r.Top = loc.Y;
                        r.Width = endX - r.Left;
                        r.Height = endY - r.Top;
                    }
                    break;

                case EditMode.ResizeLineEnd:
                    if (r.ShapeType == ShapeType.Line)
                    {
                        r.Width = loc.X - r.Left;
                        r.Height = loc.Y - r.Top;
                    }
                    break;

                case EditMode.Rotating:
                    if (r.ShapeType == ShapeType.Line)
                        break;

                    float centerX = r.Left + r.Width / 2f;
                    float centerY = r.Top + r.Height / 2f;
                    float angle = (float)(Math.Atan2(loc.Y - centerY, loc.X - centerX) * 180 / Math.PI);
                    r.RotationAngle = angle;
                    break;
            }
        }
    }
}