using GraphEditor.Business.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GraphEditor.Business.Services
{
    public class PictureService
    {
        public PictureModel PictureModel { get; private set; } = new PictureModel();

        public static Color DefaultFillColor { get; set; } = Color.FromArgb(40, 40, 44);
        public static Color DefaultBorderColor { get; set; } = Color.FromArgb(150, 150, 150);

        public PictureService()
        {
            // Тестовые фигуры
            var rect1 = new RectangleModel { Left = 150, Top = 50, Width = 200, Height = 100, FillColor = Color.FromArgb(60, 60, 70) };
            var rect2 = new RectangleModel { Left = 200, Top = 100, Width = 200, Height = 150 };
            var rect3 = new RectangleModel { Left = 250, Top = 150, Width = 200, Height = 200, FillColor = Color.FromArgb(80, 80, 90) };
            PictureModel.Rectangles.Add(rect1);
            PictureModel.Rectangles.Add(rect2);
            PictureModel.Rectangles.Add(rect3);
            PictureModel.DrawOrder.Add(rect1);
            PictureModel.DrawOrder.Add(rect2);
            PictureModel.DrawOrder.Add(rect3);
            PictureModel.SelectedRectangle = rect2;
        }

        private void AddToDrawOrder(object shape)
        {
            if (!PictureModel.DrawOrder.Contains(shape))
                PictureModel.DrawOrder.Add(shape);
        }

        private void RemoveFromDrawOrder(object shape)
        {
            PictureModel.DrawOrder.Remove(shape);
        }

        // ---- Создание фигур ----
        public void CreateRectangle(PointModel loc)
        {
            var rect = new RectangleModel
            {
                Left = loc.X,
                Top = loc.Y,
                Width = 40,
                Height = 30,
                FillColor = DefaultFillColor,
                BorderColor = DefaultBorderColor,
                EditMode = EditMode.Creating
            };
            PictureModel.Rectangles.Add(rect);
            AddToDrawOrder(rect);
            PictureModel.ClearSelection();
            PictureModel.SelectedRectangle = rect;
        }

        public void CreateTriangle(PointModel loc)
        {
            var tri = new TriangleModel
            {
                X1 = loc.X,
                Y1 = loc.Y,
                X2 = loc.X + 40,
                Y2 = loc.Y,
                X3 = loc.X + 20,
                Y3 = loc.Y + 35,
                FillColor = DefaultFillColor,
                BorderColor = DefaultBorderColor,
                EditMode = EditMode.Creating
            };
            PictureModel.Triangles.Add(tri);
            AddToDrawOrder(tri);
            PictureModel.ClearSelection();
            PictureModel.SelectedTriangle = tri;
        }

        public void CreateCircle(PointModel loc)
        {
            var circ = new CircleModel
            {
                CenterX = loc.X,
                CenterY = loc.Y,
                Width = 40,
                Height = 30,
                FillColor = DefaultFillColor,
                BorderColor = DefaultBorderColor,
                EditMode = EditMode.Creating
            };
            PictureModel.Circles.Add(circ);
            AddToDrawOrder(circ);
            PictureModel.ClearSelection();
            PictureModel.SelectedCircle = circ;
        }

        public void CreateHilbertCurve(PointModel loc)
        {
            var hilb = new HilbertCurveModel
            {
                X = loc.X,
                Y = loc.Y,
                Size = 50,
                Order = 3,
                FillColor = DefaultFillColor,
                BorderColor = DefaultBorderColor,
                EditMode = EditMode.Creating
            };
            PictureModel.HilbertCurves.Add(hilb);
            AddToDrawOrder(hilb);
            PictureModel.ClearSelection();
            PictureModel.SelectedHilbertCurve = hilb;
        }

        // ---- Удаление ----
        public void DeleteSelectedShape()
        {
            if (PictureModel.SelectedRectangle != null)
            {
                RemoveFromDrawOrder(PictureModel.SelectedRectangle);
                PictureModel.Rectangles.Remove(PictureModel.SelectedRectangle);
                PictureModel.SelectedRectangle = null;
            }
            else if (PictureModel.SelectedTriangle != null)
            {
                RemoveFromDrawOrder(PictureModel.SelectedTriangle);
                PictureModel.Triangles.Remove(PictureModel.SelectedTriangle);
                PictureModel.SelectedTriangle = null;
            }
            else if (PictureModel.SelectedCircle != null)
            {
                RemoveFromDrawOrder(PictureModel.SelectedCircle);
                PictureModel.Circles.Remove(PictureModel.SelectedCircle);
                PictureModel.SelectedCircle = null;
            }
            else if (PictureModel.SelectedHilbertCurve != null)
            {
                RemoveFromDrawOrder(PictureModel.SelectedHilbertCurve);
                PictureModel.HilbertCurves.Remove(PictureModel.SelectedHilbertCurve);
                PictureModel.SelectedHilbertCurve = null;
            }
        }

        // ---- Перемещение вперёд (работает через DrawOrder) ----
        public void MoveForward()
        {
            var shape = PictureModel.SelectedShape;
            if (shape == null) return;
            var list = PictureModel.DrawOrder;
            int idx = list.IndexOf(shape);
            if (idx >= 0 && idx < list.Count - 1)
            {
                var temp = list[idx + 1];
                list[idx + 1] = list[idx];
                list[idx] = temp;
            }
        }

        // ---- Открытие/Сохранение ----
        public void Open(string fileName)
        {
            PictureModel = new FileService().Open(fileName);
            // После загрузки нужно синхронизировать DrawOrder с загруженными списками
            PictureModel.DrawOrder.Clear();
            foreach (var r in PictureModel.Rectangles) PictureModel.DrawOrder.Add(r);
            foreach (var t in PictureModel.Triangles) PictureModel.DrawOrder.Add(t);
            foreach (var c in PictureModel.Circles) PictureModel.DrawOrder.Add(c);
            foreach (var h in PictureModel.HilbertCurves) PictureModel.DrawOrder.Add(h);
        }

        public void Save(string fileName) => new FileService().Save(fileName, PictureModel);

        // ---- Сброс режима ----
        public void ResetMode()
        {
            if (PictureModel.SelectedRectangle != null)
            {
                PictureModel.SelectedRectangle.Normalize();
                PictureModel.SelectedRectangle.EditMode = EditMode.None;
            }
            else if (PictureModel.SelectedTriangle != null)
            {
                PictureModel.SelectedTriangle.EditMode = EditMode.None;
            }
            else if (PictureModel.SelectedCircle != null)
            {
                PictureModel.SelectedCircle.EditMode = EditMode.None;
            }
            else if (PictureModel.SelectedHilbertCurve != null)
            {
                PictureModel.SelectedHilbertCurve.EditMode = EditMode.None;
            }
        }

        // ---- Выбор и перемещение ----
        public void SelectAndSetMoveMode(PointModel loc)
        {
            var model = PictureModel;
            // Проверяем в порядке DrawOrder (с конца – последние сверху)
            for (int i = model.DrawOrder.Count - 1; i >= 0; i--)
            {
                var shape = model.DrawOrder[i];
                if (shape is RectangleModel r && r.IsInside(loc))
                {
                    model.ClearSelection();
                    model.SelectedRectangle = r;
                    r.EditMode = EditMode.Moving;
                    r.Dx = loc.X - r.Left;
                    r.Dy = loc.Y - r.Top;
                    return;
                }
                else if (shape is TriangleModel t && t.IsInside(loc))
                {
                    model.ClearSelection();
                    model.SelectedTriangle = t;
                    t.EditMode = EditMode.Moving;
                    var c = t.GetCenter();
                    t.Dx = loc.X - c.X;
                    t.Dy = loc.Y - c.Y;
                    return;
                }
                else if (shape is CircleModel c && c.IsInside(loc))
                {
                    model.ClearSelection();
                    model.SelectedCircle = c;
                    c.EditMode = EditMode.Moving;
                    var center = c.GetCenter();
                    c.Dx = loc.X - center.X;
                    c.Dy = loc.Y - center.Y;
                    return;
                }
                else if (shape is HilbertCurveModel h && h.IsInside(loc))
                {
                    model.ClearSelection();
                    model.SelectedHilbertCurve = h;
                    h.EditMode = EditMode.Moving;
                    var center = h.GetCenter();
                    h.Dx = loc.X - center.X;
                    h.Dy = loc.Y - center.Y;
                    return;
                }
            }
            model.ClearSelection();
        }

        // ---- Цвет ----
        public void SetFillColor(Color color)
        {
            if (PictureModel.SelectedRectangle != null)
                PictureModel.SelectedRectangle.FillColor = color;
            else if (PictureModel.SelectedTriangle != null)
                PictureModel.SelectedTriangle.FillColor = color;
            else if (PictureModel.SelectedCircle != null)
                PictureModel.SelectedCircle.FillColor = color;
            else if (PictureModel.SelectedHilbertCurve != null)
                PictureModel.SelectedHilbertCurve.FillColor = color;
        }

        // ---- Установка режима изменения размера ----
        public void SetResizeMode(EditMode mode)
        {
            if (PictureModel.SelectedRectangle != null)
                PictureModel.SelectedRectangle.EditMode = mode;
            else if (PictureModel.SelectedTriangle != null)
                PictureModel.SelectedTriangle.EditMode = mode;
            else if (PictureModel.SelectedCircle != null)
                PictureModel.SelectedCircle.EditMode = mode;
            else if (PictureModel.SelectedHilbertCurve != null)
                PictureModel.SelectedHilbertCurve.EditMode = mode;
        }

        // ---- Обновление при перемещении мыши ----
        public void UpdateMovingPoint(PointModel loc)
        {
            if (PictureModel.SelectedRectangle != null)
            {
                var r = PictureModel.SelectedRectangle;
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
                    case EditMode.ResizeTL:
                        r.Width = r.Right - loc.X;
                        r.Height = r.Bottom - loc.Y;
                        r.Left = loc.X;
                        r.Top = loc.Y;
                        break;
                    case EditMode.ResizeT:
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
                        r.Height = loc.Y - r.Top;
                        r.Left = loc.X;
                        break;
                    case EditMode.ResizeL:
                        r.Width = r.Right - loc.X;
                        r.Left = loc.X;
                        break;
                    case EditMode.Moving:
                        r.Left = loc.X - r.Dx;
                        r.Top = loc.Y - r.Dy;
                        break;
                }
            }
            else if (PictureModel.SelectedTriangle != null)
            {
                var t = PictureModel.SelectedTriangle;
                if (t.EditMode == EditMode.Moving)
                {
                    var center = t.GetCenter();
                    int dx = loc.X - center.X;
                    int dy = loc.Y - center.Y;
                    t.X1 += dx - t.Dx;
                    t.Y1 += dy - t.Dy;
                    t.X2 += dx - t.Dx;
                    t.Y2 += dy - t.Dy;
                    t.X3 += dx - t.Dx;
                    t.Y3 += dy - t.Dy;
                    t.Dx = dx;
                    t.Dy = dy;
                }
                else if (t.EditMode == EditMode.MoveVertex1) { t.X1 = loc.X; t.Y1 = loc.Y; }
                else if (t.EditMode == EditMode.MoveVertex2) { t.X2 = loc.X; t.Y2 = loc.Y; }
                else if (t.EditMode == EditMode.MoveVertex3) { t.X3 = loc.X; t.Y3 = loc.Y; }
                // Для изменения размера треугольника через bounding box (упрощённо)
                else if (t.EditMode == EditMode.Creating || t.EditMode == EditMode.ResizeBR)
                {
                    // Просто двигаем третью вершину
                    t.X3 = loc.X;
                    t.Y3 = loc.Y;
                }
            }
            else if (PictureModel.SelectedCircle != null)
            {
                var c = PictureModel.SelectedCircle;
                if (c.EditMode == EditMode.Moving)
                {
                    c.CenterX = loc.X - c.Dx;
                    c.CenterY = loc.Y - c.Dy;
                }
                else if (c.EditMode == EditMode.Creating || c.EditMode == EditMode.ResizeBR)
                {
                    int dx = loc.X - c.CenterX;
                    int dy = loc.Y - c.CenterY;
                    // Для эллипса нужно менять Width и Height отдельно? 
                    // Упростим: пока только одинаковое изменение (круг)
                    int newR = (int)Math.Sqrt(dx * dx + dy * dy);
                    c.Width = newR * 2;
                    c.Height = newR * 2;
                }
                else if (c.EditMode == EditMode.ResizeR)
                {
                    c.Width = Math.Abs(loc.X - c.CenterX) * 2;
                }
                else if (c.EditMode == EditMode.ResizeB)
                {
                    c.Height = Math.Abs(loc.Y - c.CenterY) * 2;
                }
            }
            else if (PictureModel.SelectedHilbertCurve != null)
            {
                var h = PictureModel.SelectedHilbertCurve;
                if (h.EditMode == EditMode.Moving)
                {
                    h.X = loc.X - h.Dx;
                    h.Y = loc.Y - h.Dy;
                }
                else if (h.EditMode == EditMode.Creating || h.EditMode == EditMode.ResizeBR)
                {
                    int newSize = Math.Max(10, loc.X - h.X);
                    if (loc.Y - h.Y < newSize) newSize = loc.Y - h.Y;
                    h.Size = newSize;
                }
            }
        }

        public void CreateNewPicture()
        {
            PictureModel.Rectangles.Clear();
            PictureModel.Triangles.Clear();
            PictureModel.Circles.Clear();
            PictureModel.HilbertCurves.Clear();
            PictureModel.DrawOrder.Clear();
            PictureModel.ClearSelection();
        }
    }
}