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
            // Стартовые фигуры для теста
            var rect1 = new RectangleModel { Left = 150, Top = 50, Width = 200, Height = 100, FillColor = Color.LightSkyBlue };
            var rect2 = new RectangleModel { Left = 200, Top = 100, Width = 200, Height = 150 };
            var rect3 = new RectangleModel { Left = 250, Top = 150, Width = 200, Height = 200, FillColor = Color.DarkMagenta };

            PictureModel.Rectangles.Add(rect1);
            PictureModel.Rectangles.Add(rect2);
            PictureModel.Rectangles.Add(rect3);

            PictureModel.SelectedRectangle = rect2;
            PictureModel.SelectedRectangles.Add(rect2);
        }

        public void CreateRectangleAndSetCreateMode(PointModel loc)
        {
            var newRectangle = new RectangleModel
            {
                Left = loc.X,
                Top = loc.Y,
                Width = 0,
                Height = 0,
                FillColor = DefaultFillColor,
                BorderColor = DefaultBorderColor,
                EditMode = EditMode.Creating
            };

            PictureModel.Rectangles.Add(newRectangle);

            PictureModel.SelectedRectangles.Clear();
            PictureModel.SelectedRectangles.Add(newRectangle);
            PictureModel.SelectedRectangle = newRectangle;
        }

        // Обновленный метод с поддержкой Ctrl
        public void FindAndSetMoveMode(PointModel loc, bool ctrlPressed)
        {
            var selectedRect = PictureModel.Rectangles.LastOrDefault(r => r.IsInside(loc));

            if (selectedRect == null)
            {
                if (!ctrlPressed)
                {
                    PictureModel.SelectedRectangle = null;
                    PictureModel.SelectedRectangles.Clear();
                }
                return;
            }

            if (ctrlPressed)
            {
                if (PictureModel.SelectedRectangles.Contains(selectedRect))
                {
                    PictureModel.SelectedRectangles.Remove(selectedRect);
                    PictureModel.SelectedRectangle = PictureModel.SelectedRectangles.LastOrDefault();
                }
                else
                {
                    PictureModel.SelectedRectangles.Add(selectedRect);
                    PictureModel.SelectedRectangle = selectedRect;
                }
            }
            else
            {
                // Если кликнули по уже выделенной в группе фигуре без Ctrl, не сбрасываем выделение сразу, чтобы можно было тащить группу
                if (!PictureModel.SelectedRectangles.Contains(selectedRect))
                {
                    PictureModel.SelectedRectangles.Clear();
                    PictureModel.SelectedRectangles.Add(selectedRect);
                    PictureModel.SelectedRectangle = selectedRect;
                }
            }

            // Задаем режим движения для всех выбранных фигур
            foreach (var rect in PictureModel.SelectedRectangles)
            {
                rect.EditMode = EditMode.Moving;
                rect.Dx = loc.X - rect.Left;
                rect.Dy = loc.Y - rect.Top;
            }
        }

        public void SetResizeMode(EditMode mode)
        {
            if (PictureModel.SelectedRectangle != null)
            {
                PictureModel.SelectedRectangle.EditMode = mode;
            }
        }

        public void ResetMode()
        {
            foreach (var rect in PictureModel.SelectedRectangles)
            {
                rect.Normalize();
                rect.EditMode = EditMode.None;
            }

            if (PictureModel.SelectedRectangle != null)
            {
                PictureModel.SelectedRectangle.Normalize();
                PictureModel.SelectedRectangle.EditMode = EditMode.None;
            }
        }

        public void UpdateMovingPoint(PointModel loc)
        {
            if (PictureModel.SelectedRectangle == null)
            {
                return;
            }

            // Если двигаем группу фигур
            if (PictureModel.SelectedRectangles.Count > 1)
            {
                foreach (var rect in PictureModel.SelectedRectangles)
                {
                    if (rect.EditMode == EditMode.Moving)
                    {
                        rect.Left = loc.X - rect.Dx;
                        rect.Top = loc.Y - rect.Dy;
                    }
                }
                return;
            }

            // Если изменяем размер или двигаем одну фигуру
            var selectedRect = PictureModel.SelectedRectangle;
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

        public void DeleteRectangle()
        {
            if (PictureModel.SelectedRectangles.Count > 0)
            {
                foreach (var rect in PictureModel.SelectedRectangles.ToList())
                {
                    PictureModel.Rectangles.Remove(rect);
                }
                PictureModel.SelectedRectangles.Clear();
                PictureModel.SelectedRectangle = null;
                return;
            }

            if (PictureModel.SelectedRectangle != null)
            {
                PictureModel.Rectangles.Remove(PictureModel.SelectedRectangle);
                PictureModel.SelectedRectangle = null;
            }
        }

        public void SetFillColor(Color color)
        {
            if (PictureModel.SelectedRectangles.Count > 0)
            {
                foreach (var rect in PictureModel.SelectedRectangles)
                {
                    rect.FillColor = color;
                }
                return;
            }

            if (PictureModel.SelectedRectangle != null)
            {
                PictureModel.SelectedRectangle.FillColor = color;
            }
        }

        public void MoveForward()
        {
            if (PictureModel.SelectedRectangle == null) return;

            var index = PictureModel.Rectangles.IndexOf(PictureModel.SelectedRectangle);
            if (index < 0 || index == PictureModel.Rectangles.Count - 1) return;

            var rect = PictureModel.Rectangles[index];
            PictureModel.Rectangles[index] = PictureModel.Rectangles[index + 1];
            PictureModel.Rectangles[index + 1] = rect;
        }

        public void Save(string fileName) => new FileService().Save(fileName, PictureModel);

        public void Open(string fileName)
        {
            PictureModel = new FileService().Open(fileName);
            if (PictureModel.SelectedRectangles == null)
            {
                PictureModel.SelectedRectangles = new List<RectangleModel>();
            }
        }

        public void CreateNewPicture()
        {
            PictureModel.Rectangles.Clear();
            PictureModel.SelectedRectangle = null;
            PictureModel.SelectedRectangles.Clear();
        }
    }
}