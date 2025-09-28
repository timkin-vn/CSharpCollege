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

        public void SetBorderColor(Color color)
        {
            if (PictureModel.SelectedRectangle != null)
            {
                PictureModel.SelectedRectangle.BorderColor = color;
            }
        }

        public void SetBorderWidth(int width)
        {
            if (PictureModel.SelectedRectangle != null)
            {
                PictureModel.SelectedRectangle.BorderWidth = width;
            }
        }

        public PictureService()
        {
            PictureModel.Rectangles.Add(new RectangleModel { Left = 150, Top = 50, Width = 200, Height = 100, FillColor = Color.LightSkyBlue, });
            var newRectangle = new RectangleModel { Left = 200, Top = 100, Width = 200, Height = 150, };
            PictureModel.Rectangles.Add(newRectangle);
            PictureModel.SelectedRectangle = newRectangle;
            PictureModel.Rectangles.Add(new RectangleModel { Left = 250, Top = 150, Width = 200, Height = 200, FillColor = Color.DarkMagenta, });
        }

        public void SetMoveMode(PointModel point)
        {
            var selectedRect = PictureModel.Rectangles.LastOrDefault(r => r.IsInside(point));
            PictureModel.SelectedRectangle = selectedRect;

            if (selectedRect == null)
            {
                return;
            }

            selectedRect.EditMode = EditMode.Moving;
            selectedRect.Dx = point.X - selectedRect.Left;
            selectedRect.Dy = point.Y - selectedRect.Top;
        }

        public void SetResizeMode(EditMode mode)
        {
            PictureModel.SelectedRectangle.EditMode = mode;
        }

        public void SetCreateMode(PointModel point)
        {
            var newRectangle = new RectangleModel
            {
                Left = point.X,
                Top = point.Y,
                Width = 0,
                Height = 0,
                FillColor = DefaultFillColor,
                BorderColor = DefaultBorderColor,
                BorderWidth = 3
            };

            PictureModel.Rectangles.Add(newRectangle);
            PictureModel.SelectedRectangle = newRectangle;
            newRectangle.EditMode = EditMode.Creating;
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
                    selectedRect.Width = loc.X - selectedRect.Left;
                    selectedRect.Height = loc.Y - selectedRect.Top;
                    break;
                case EditMode.ResizeR:
                    selectedRect.Width = loc.X - selectedRect.Left;
                    break;
                case EditMode.ResizeB:
                    selectedRect.Height = loc.Y - selectedRect.Top;
                    break;
                case EditMode.ResizeL:
                    selectedRect.Width += selectedRect.Left - loc.X;
                    selectedRect.Left = loc.X;
                    break;
                case EditMode.ResizeT:
                    selectedRect.Height += selectedRect.Top - loc.Y;
                    selectedRect.Top = loc.Y;
                    break;
                case EditMode.ResizeTL:
                    selectedRect.Width += selectedRect.Left - loc.X;
                    selectedRect.Height += selectedRect.Top - loc.Y;
                    selectedRect.Left = loc.X;
                    selectedRect.Top = loc.Y;
                    break;
                case EditMode.ResizeTR:
                    selectedRect.Width = loc.X - selectedRect.Left;
                    selectedRect.Height += selectedRect.Top - loc.Y;
                    selectedRect.Top = loc.Y;
                    break;
                case EditMode.ResizeBL:
                    selectedRect.Width += selectedRect.Left - loc.X;
                    selectedRect.Height = loc.Y - selectedRect.Top;
                    selectedRect.Left = loc.X;
                    break;
                case EditMode.Moving:
                    selectedRect.Left = loc.X - selectedRect.Dx;
                    selectedRect.Top = loc.Y - selectedRect.Dy;
                    break;
            }
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

        public void Save(string fileName)
        {
            new FileService().Save(fileName, PictureModel);
        }

        public void Open(string fileName)
        {
            PictureModel = new FileService().Open(fileName);
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

            var rect = PictureModel.Rectangles[index];
            PictureModel.Rectangles[index] = PictureModel.Rectangles[index + 1];
            PictureModel.Rectangles[index + 1] = rect;
        }
    }
}
