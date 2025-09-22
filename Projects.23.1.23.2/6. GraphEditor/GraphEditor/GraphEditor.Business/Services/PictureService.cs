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
            var newRectangle = new RectangleModel { Left = loc.X, Top = loc.Y, Width = 0, Height = 0, };
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
                    selectedRect.Bottom = loc.Y;
                    selectedRect.Right = loc.X;
                    break;

                case EditMode.ResizeR:
                    selectedRect.Right = loc.X;
                    break;

                case EditMode.ResizeB:
                    selectedRect.Bottom = loc.Y;
                    break;

                case EditMode.ResizeBL:
                    {
                        int oldRight = selectedRect.Right;
                        selectedRect.Bottom = loc.Y;
                        selectedRect.Left = loc.X;
                        selectedRect.Width = oldRight - selectedRect.Left;
                        break;
                    }

                case EditMode.ResizeL:
                    {
                        int oldRight = selectedRect.Right;
                        selectedRect.Left = loc.X;
                        selectedRect.Width = oldRight - selectedRect.Left;
                        break;
                    }

                case EditMode.ResizeTL:
                    {
                        int oldBottom = selectedRect.Bottom;
                        int oldRight = selectedRect.Right;
                        selectedRect.Top = loc.Y;
                        selectedRect.Left = loc.X;
                        selectedRect.Height = oldBottom - selectedRect.Top;
                        selectedRect.Width = oldRight - selectedRect.Left;
                        break;
                    }

                case EditMode.ResizeT:
                    {
                        int oldBottom = selectedRect.Bottom;
                        selectedRect.Top = loc.Y;
                        selectedRect.Height = oldBottom - selectedRect.Top;
                        break;
                    }

                case EditMode.ResizeTR:
                    {
                        int oldBottom = selectedRect.Bottom;
                        selectedRect.Top = loc.Y;
                        selectedRect.Right = loc.X;
                        selectedRect.Height = oldBottom - selectedRect.Top;
                        break;
                    }

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
    }
}