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
            PictureModel.Rectangles.Add(new RectangleModel { Left = 150, Top = 50, Width = 200, Height = 100, FillColor = Color.LightSkyBlue });
            var newRectangle = new RectangleModel { Left = 200, Top = 100, Width = 200, Height = 150, CornerRadius = 10 };
            PictureModel.Rectangles.Add(newRectangle);
            PictureModel.SelectedRectangle = newRectangle;
            PictureModel.Rectangles.Add(new RectangleModel { Left = 250, Top = 150, Width = 200, Height = 200, FillColor = Color.DarkMagenta, Opacity = 180 });
        }

        public void CreateAndSetCreateMode(PointModel loc)
        {
            var newRectangle = new RectangleModel
            {
                Left = loc.X,
                Top = loc.Y,
                Width = 0,
                Height = 0,
                FillColor = DefaultFillColor,
                BorderColor = DefaultBorderColor,
                Opacity = 255,
                CornerRadius = 0
            };
            PictureModel.Rectangles.Add(newRectangle);
            PictureModel.SelectedRectangle = newRectangle;
            newRectangle.EditMode = EditMode.Creating;
        }

        public void CreateNewPicture()
        {
            PictureModel.Rectangles.Clear();
            PictureModel.SelectedRectangle = null;
        }

        public void DeleteRectangle()
        {
            if (PictureModel.SelectedRectangle == null) return;
            PictureModel.Rectangles.Remove(PictureModel.SelectedRectangle);
            PictureModel.SelectedRectangle = null;
        }

        public void MoveForward()
        {
            if (PictureModel.SelectedRectangle == null) return;
            var selectedIndex = PictureModel.Rectangles.IndexOf(PictureModel.SelectedRectangle);
            if (selectedIndex < 0 || selectedIndex == PictureModel.Rectangles.Count - 1) return;
            var rect = PictureModel.Rectangles[selectedIndex + 1];
            PictureModel.Rectangles[selectedIndex + 1] = PictureModel.Rectangles[selectedIndex];
            PictureModel.Rectangles[selectedIndex] = rect;
        }

        // Новые методы порядка слоёв
        public void MoveBackward()
        {
            if (PictureModel.SelectedRectangle == null) return;
            var selectedIndex = PictureModel.Rectangles.IndexOf(PictureModel.SelectedRectangle);
            if (selectedIndex <= 0) return;
            var rect = PictureModel.Rectangles[selectedIndex - 1];
            PictureModel.Rectangles[selectedIndex - 1] = PictureModel.Rectangles[selectedIndex];
            PictureModel.Rectangles[selectedIndex] = rect;
        }

        public void MoveToFront()
        {
            if (PictureModel.SelectedRectangle == null) return;
            var rect = PictureModel.SelectedRectangle;
            PictureModel.Rectangles.Remove(rect);
            PictureModel.Rectangles.Add(rect);
        }

        public void MoveToBack()
        {
            if (PictureModel.SelectedRectangle == null) return;
            var rect = PictureModel.SelectedRectangle;
            PictureModel.Rectangles.Remove(rect);
            PictureModel.Rectangles.Insert(0, rect);
        }

        public void SetOpacity(int opacity)
        {
            if (PictureModel.SelectedRectangle != null)
                PictureModel.SelectedRectangle.Opacity = Math.Max(0, Math.Min(255, opacity));
        }

        public void SetCornerRadius(int radius)
        {
            if (PictureModel.SelectedRectangle != null)
                PictureModel.SelectedRectangle.CornerRadius = Math.Max(0, radius);
        }

        public void SetBorderOpacity(int opacity)
        {
            if (PictureModel.SelectedRectangle != null)
                PictureModel.SelectedRectangle.BorderOpacity = Math.Max(0, Math.Min(255, opacity));
        }


        // Остальные методы без изменений
        public void Open(string fileName) { PictureModel = new FileService().Open(fileName); }
        public void ResetMode()
        {
            var selectedRect = PictureModel.SelectedRectangle;
            if (selectedRect != null)
            {
                selectedRect.Normalize();
                selectedRect.EditMode = EditMode.None;
            }
        }
        public void Save(string fileName) { new FileService().Save(fileName, PictureModel); }

        public void SelectAndSetMoveMode(PointModel loc)
        {
            var selectedRect = PictureModel.Rectangles.LastOrDefault(r => r.IsInside(loc));
            PictureModel.SelectedRectangle = selectedRect;
            if (selectedRect == null) return;
            selectedRect.EditMode = EditMode.Moving;
            selectedRect.Dx = loc.X - selectedRect.Left;
            selectedRect.Dy = loc.Y - selectedRect.Top;
        }

        public void SetFillColor(Color color)
        {
            if (PictureModel.SelectedRectangle != null)
                PictureModel.SelectedRectangle.FillColor = color;
        }

        public void SetResizeMode(EditMode mode) { PictureModel.SelectedRectangle.EditMode = mode; }

        public void UpdateMovingPoint(PointModel loc)
        {
            var selectedRect = PictureModel.SelectedRectangle;
            if (selectedRect == null) return;
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
                case EditMode.Moving:
                    selectedRect.Left = loc.X - selectedRect.Dx;
                    selectedRect.Top = loc.Y - selectedRect.Dy;
                    break;
            }
        }
    }
}