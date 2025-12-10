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

                case EditMode.ResizeL:
                    var deltaL = loc.X - selectedRect.Left;
                    selectedRect.Left += deltaL;
                    selectedRect.Width -= deltaL;
                    break;

                case EditMode.ResizeB:
                    selectedRect.Height = loc.Y - selectedRect.Top;
                    break;

                case EditMode.ResizeT:
                    var deltaT = loc.Y - selectedRect.Top;
                    selectedRect.Top += deltaT;
                    selectedRect.Height -= deltaT;
                    break;

                case EditMode.ResizeTL:
                    deltaL = loc.X - selectedRect.Left;
                    deltaT = loc.Y - selectedRect.Top;
                    selectedRect.Left += deltaL;
                    selectedRect.Top += deltaT;
                    selectedRect.Width -= deltaL;
                    selectedRect.Height -= deltaT;
                    break;

                case EditMode.ResizeTR:
                    deltaT = loc.Y - selectedRect.Top;
                    selectedRect.Top += deltaT;
                    selectedRect.Width = loc.X - selectedRect.Left;
                    selectedRect.Height -= deltaT;
                    break;

                case EditMode.ResizeBL:
                    deltaL = loc.X - selectedRect.Left;
                    selectedRect.Left += deltaL;
                    selectedRect.Width -= deltaL;
                    selectedRect.Height = loc.Y - selectedRect.Top;
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

        public void MoveBehaind()
        {
            if (PictureModel.SelectedRectangle == null)
            {
                return;
            }

            var index = PictureModel.Rectangles.IndexOf(PictureModel.SelectedRectangle);
            if (index <= 0) return;

            PictureModel.Rectangles[index] = PictureModel.Rectangles[index - 1];
            PictureModel.Rectangles[index - 1] = PictureModel.SelectedRectangle;
        }

        public void BringToFront()
        {
            if (PictureModel.SelectedRectangle == null)
            {
                return;
            }

            var index = PictureModel.Rectangles.IndexOf(PictureModel.SelectedRectangle);
            if (index < 0 || index == PictureModel.Rectangles.Count - 1)
            {
                return; // уже самый верхний
            }

            PictureModel.Rectangles.RemoveAt(index);
            PictureModel.Rectangles.Add(PictureModel.SelectedRectangle);
        }

        public void SendToBack()
        {
            if (PictureModel.SelectedRectangle == null)
            {
                return;
            }

            var index = PictureModel.Rectangles.IndexOf(PictureModel.SelectedRectangle);
            if (index < 0) return;

            PictureModel.Rectangles.RemoveAt(index);
            PictureModel.Rectangles.Insert(0, PictureModel.SelectedRectangle);
        }

    }
}
