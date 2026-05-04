using GraphEditor.Business.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GraphEditor.Business.Services
{
    public enum ShapeType { Rectangle, Ellipse }

    public class PictureService
    {
        public PictureModel PictureModel { get; private set; } = new PictureModel();

        public static Color DefaultFillColor { get; set; } = Color.Yellow;
        public static Color DefaultBorderColor { get; set; } = Color.Blue;

        public ShapeType CurrentShapeType { get; set; } = ShapeType.Rectangle;

        public PictureService()
        {
            
            var rect = new RectangleModel { Left = 150, Top = 50, Width = 200, Height = 100, FillColor = Color.LightSkyBlue };
            var ellipse = new EllipseModel { Left = 250, Top = 150, Width = 200, Height = 200, FillColor = Color.DarkMagenta };

            PictureModel.Shapes.Add(rect);
            PictureModel.Shapes.Add(ellipse);
            PictureModel.SelectedShape = ellipse;
        }

        public void CreateAndSetCreateMode(PointModel loc)
        {
            ShapeModel newShape = CurrentShapeType == ShapeType.Ellipse
    ? (ShapeModel)new EllipseModel()
    : new RectangleModel();

            newShape.Left = loc.X;
            newShape.Top = loc.Y;
            newShape.Width = 0;
            newShape.Height = 0;
            newShape.FillColor = DefaultFillColor;
            newShape.BorderColor = DefaultBorderColor;
            newShape.EditMode = EditMode.Creating;

            PictureModel.Shapes.Add(newShape);
            PictureModel.SelectedShape = newShape;
        }

        public void CreateNewPicture()
        {
            PictureModel.Shapes.Clear();
            PictureModel.SelectedShape = null;
        }

        public void DeleteShape()
        {
            if (PictureModel.SelectedShape == null) return;
            PictureModel.Shapes.Remove(PictureModel.SelectedShape);
            PictureModel.SelectedShape = null;
        }

        public void MoveForward() 
        {
            var selected = PictureModel.SelectedShape;
            if (selected == null) return;

            var index = PictureModel.Shapes.IndexOf(selected);
            if (index < 0 || index == PictureModel.Shapes.Count - 1) return;

            PictureModel.Shapes.RemoveAt(index);
            PictureModel.Shapes.Insert(index + 1, selected);
        }

        public void MoveBackward()
        {
            var selected = PictureModel.SelectedShape;
            if (selected == null) return;

            var index = PictureModel.Shapes.IndexOf(selected);
            if (index <= 0) return;

            PictureModel.Shapes.RemoveAt(index);
            PictureModel.Shapes.Insert(index - 1, selected);
        }

        public void BringToFront()
        {
            var selected = PictureModel.SelectedShape;
            if (selected == null) return;
            PictureModel.Shapes.Remove(selected);
            PictureModel.Shapes.Add(selected);
        }

        public void SendToBack()
        {
            var selected = PictureModel.SelectedShape;
            if (selected == null) return;
            PictureModel.Shapes.Remove(selected);
            PictureModel.Shapes.Insert(0, selected);
        }


        public void Open(string fileName)
        {
            PictureModel = new FileService().Open(fileName);
        }

        public void ResetMode()
        {
            var selected = PictureModel.SelectedShape;
            if (selected != null)
            {
                selected.Normalize();
                selected.EditMode = EditMode.None;
            }
        }

        public void Save(string fileName)
        {
            new FileService().Save(fileName, PictureModel);
        }

        public void SelectAndSetMoveMode(PointModel loc)
        {
            var selected = PictureModel.Shapes.LastOrDefault(r => r.IsInside(loc));
            PictureModel.SelectedShape = selected;

            if (selected == null) return;

            selected.EditMode = EditMode.Moving;
            selected.Dx = loc.X - selected.Left;
            selected.Dy = loc.Y - selected.Top;
        }

        public void SetFillColor(Color color)
        {
            if (PictureModel.SelectedShape != null)
                PictureModel.SelectedShape.FillColor = color;
        }

        public void SetResizeMode(EditMode mode)
        {
            if (PictureModel.SelectedShape != null)
                PictureModel.SelectedShape.EditMode = mode;
        }

        public void UpdateMovingPoint(PointModel loc, bool isShiftPressed = false)
        {
            var selected = PictureModel.SelectedShape;
            if (selected == null) return;

            switch (selected.EditMode)
            {
                case EditMode.Creating:
                case EditMode.ResizeBR:
                    int newWidth = loc.X - selected.Left;
                    int newHeight = loc.Y - selected.Top;

                    if (isShiftPressed)
                    {
                        int size = Math.Max(Math.Abs(newWidth), Math.Abs(newHeight));
                        newWidth = Math.Sign(newWidth) * size;
                        newHeight = Math.Sign(newHeight) * size;
                    }

                    selected.Width = newWidth;
                    selected.Height = newHeight;
                    break;

                case EditMode.ResizeR:
                    selected.Width = loc.X - selected.Left;
                    break;

                case EditMode.Moving:
                    selected.Left = loc.X - selected.Dx;
                    selected.Top = loc.Y - selected.Dy;
                    break;
            }
        }
    }
}
