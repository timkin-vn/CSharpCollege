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
        }
        public void SetBorderColor(Color color)
        {
            if (PictureModel.SelectedRectangle != null)
            {
                PictureModel.SelectedRectangle.BorderColor = color;
            }
        }

        public bool MoveToFront()
        {
            if (PictureModel.SelectedRectangle == null)
            {
                return false;
            }

            var index = PictureModel.Rectangles.IndexOf(PictureModel.SelectedRectangle);
            if (index < 0 || index == PictureModel.Rectangles.Count - 1)
            {
                return false;
            }

            var selectedRect = PictureModel.SelectedRectangle;
            PictureModel.Rectangles.RemoveAt(index);
            PictureModel.Rectangles.Add(selectedRect);
            PictureModel.SelectedRectangle = selectedRect;

            return true;
        }

        public bool MoveToBack()
        {
            if (PictureModel.SelectedRectangle == null)
            {
                return false;
            }

            var index = PictureModel.Rectangles.IndexOf(PictureModel.SelectedRectangle);
            if (index <= 0)
            {
                return false;
            }

            var selectedRect = PictureModel.SelectedRectangle;
            PictureModel.Rectangles.RemoveAt(index);
            PictureModel.Rectangles.Insert(0, selectedRect);
            PictureModel.SelectedRectangle = selectedRect;

            return true;
        }
        public bool MoveForward()
        {
            if (PictureModel.SelectedRectangle == null)
            {
                return false;
            }

            var index = PictureModel.Rectangles.IndexOf(PictureModel.SelectedRectangle);
            if (index < 0 || index == PictureModel.Rectangles.Count - 1)
            {
                return false;
            }

            var rect = PictureModel.Rectangles[index + 1];
            PictureModel.Rectangles[index + 1] = PictureModel.Rectangles[index];
            PictureModel.Rectangles[index] = rect;

            return true;
        }

        public bool MoveBackward()
        {
            if (PictureModel.SelectedRectangle == null)
            {
                return false;
            }

            var index = PictureModel.Rectangles.IndexOf(PictureModel.SelectedRectangle);
            if (index <= 0)
            {
                return false;
            }

            var selectedRect = PictureModel.SelectedRectangle;
            PictureModel.Rectangles[index] = PictureModel.Rectangles[index - 1];
            PictureModel.Rectangles[index - 1] = selectedRect;
            PictureModel.SelectedRectangle = selectedRect;

            return true;
        }
        public Color GetGradientColor1()
        {
            return PictureModel.SelectedRectangle?.GradientColor1 ?? Color.Yellow;
        }

        public Color GetGradientColor2()
        {
            return PictureModel.SelectedRectangle?.GradientColor2 ?? Color.Red;
        }

        public void SetGradientColors(Color color1, Color color2)
        {
            if (PictureModel.SelectedRectangle != null)
            {
                PictureModel.SelectedRectangle.GradientColor1 = color1;
                PictureModel.SelectedRectangle.GradientColor2 = color2;
            }
        }
        public void SetFillType(FillType fillType)
        {
            if (PictureModel.SelectedRectangle != null)
            {
                PictureModel.SelectedRectangle.FillType = fillType;
            }

        }
        public void SetTexturePath(string texturePath)
        {
            if (PictureModel?.SelectedRectangle != null)
            {
                PictureModel.SelectedRectangle.TexturePath = texturePath ?? string.Empty;
                PictureModel.SelectedRectangle.FillType = FillType.Texture;
            }
        }
    }
}