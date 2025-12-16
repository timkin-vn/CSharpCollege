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
        public ShapeType CurrentShapeType { get; set; } = ShapeType.Rectangle;

        public static Color DefaultFillColor = Color.Yellow;
        public static Color DefaultBorderColor = Color.Blue;

        public PictureService()
        {
            // Пример начальных фигур
            PictureModel.Shapes.Add(new ShapeModel
            {
                Left = 100,
                Top = 50,
                Width = 200,
                Height = 150,
                FillColor = Color.LightCyan,
                ShapeType = ShapeType.Rectangle
            });

            var newShape = new ShapeModel
            {
                Left = 200,
                Top = 100,
                Width = 200,
                Height = 150,
                ShapeType = ShapeType.Rectangle
            };
            PictureModel.Shapes.Add(newShape);
            PictureModel.SelectedShape = newShape;

            PictureModel.Shapes.Add(new ShapeModel
            {
                Left = 300,
                Top = 200,
                Width = 200,
                Height = 150,
                FillColor = Color.Pink,
                ShapeType = ShapeType.Rectangle
            });
        }

        public void CreateShape(PointModel loc)
        {
            var newShape = new ShapeModel
            {
                ShapeType = CurrentShapeType,
                Left = loc.X,
                Top = loc.Y,
                Width = 0,
                Height = 0,
                FillColor = DefaultFillColor,
                BorderColor = DefaultBorderColor,
                BorderThickness = 3
            };

            // Для треугольника по умолчанию вершина вверху
            if (CurrentShapeType == ShapeType.Triangle)
            {
                newShape.TrianglePointsUp = true;
            }

            PictureModel.Shapes.Add(newShape);
            PictureModel.SelectedShape = newShape;
            newShape.EditMode = EditMode.Creating;
        }

        public void DeleteShape()
        {
            if (PictureModel.SelectedShape == null)
            {
                return;
            }

            PictureModel.Shapes.Remove(PictureModel.SelectedShape);
            PictureModel.SelectedShape = null;
        }

        public void SetResizeMode(EditMode mode)
        {
            if (PictureModel.SelectedShape != null)
            {
                PictureModel.SelectedShape.EditMode = mode;
            }
        }

        public void SetMoveMode(PointModel loc)
        {
            // Ищем фигуру, содержащую точку (сверху вниз по Z-порядку)
            var selectedShape = PictureModel.Shapes.LastOrDefault(r => r.IsInside(loc));
            PictureModel.SelectedShape = selectedShape;

            if (selectedShape == null)
            {
                return;
            }

            selectedShape.Dx = loc.X - selectedShape.Left;
            selectedShape.Dy = loc.Y - selectedShape.Top;
            selectedShape.EditMode = EditMode.Moving;
        }

        public void ResetMode()
        {
            var selectedShape = PictureModel.SelectedShape;
            if (selectedShape != null)
            {
                selectedShape.Normalize();
                selectedShape.EditMode = EditMode.None;
            }
        }

        public void UpdateMovingPoint(PointModel loc)
        {
            var selectedShape = PictureModel.SelectedShape;
            if (selectedShape == null)
            {
                return;
            }

            switch (selectedShape.EditMode)
            {
                case EditMode.Creating:
                    if (selectedShape.ShapeType == ShapeType.Rectangle ||
                        selectedShape.ShapeType == ShapeType.Circle)
                    {
                        selectedShape.Right = loc.X;
                        selectedShape.Bottom = loc.Y;
                    }
                    else if (selectedShape.ShapeType == ShapeType.Triangle)
                    {
                        selectedShape.Right = loc.X;
                        selectedShape.Bottom = loc.Y;

                        // Определяем направление треугольника по движению мыши
                        // Если рисуем вниз от начальной точки - вершина вверху
                        // Если рисуем вверх от начальной точки - вершина внизу
                        if (loc.Y < selectedShape.Top)
                        {
                            selectedShape.TrianglePointsUp = false; // Вершина внизу
                        }
                        else
                        {
                            selectedShape.TrianglePointsUp = true; // Вершина вверху
                        }
                    }
                    break;

                case EditMode.ResizeBR:
                    selectedShape.Right = loc.X;
                    selectedShape.Bottom = loc.Y;
                    break;

                case EditMode.ResizeR:
                    selectedShape.Right = loc.X;
                    break;

                case EditMode.Moving:
                    selectedShape.Left = loc.X - selectedShape.Dx;
                    selectedShape.Top = loc.Y - selectedShape.Dy;
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
            PictureModel.Shapes.Clear();
            PictureModel.SelectedShape = null;
        }

        public void SetFillColor(Color color)
        {
            if (PictureModel.SelectedShape != null)
            {
                PictureModel.SelectedShape.FillColor = color;
            }
        }

        public void SetBorderColor(Color color)
        {
            if (PictureModel.SelectedShape != null)
            {
                PictureModel.SelectedShape.BorderColor = color;
            }
        }

        public void SetBorderThickness(int thickness)
        {
            if (PictureModel.SelectedShape != null && thickness > 0)
            {
                PictureModel.SelectedShape.BorderThickness = thickness;
            }
        }

        public void MoveForward()
        {
            if (PictureModel.SelectedShape == null)
            {
                return;
            }

            var index = PictureModel.Shapes.IndexOf(PictureModel.SelectedShape);
            if (index < 0 || index == PictureModel.Shapes.Count - 1)
            {
                return;
            }

            var shape = PictureModel.Shapes[index + 1];
            PictureModel.Shapes[index + 1] = PictureModel.Shapes[index];
            PictureModel.Shapes[index] = shape;
        }

        // ДОБАВЬТЕ ЭТИ МЕТОДЫ:
        public Color GetSelectedFillColor()
        {
            return PictureModel.SelectedShape?.FillColor ?? DefaultFillColor;
        }

        public Color GetSelectedBorderColor()
        {
            return PictureModel.SelectedShape?.BorderColor ?? DefaultBorderColor;
        }

        public int GetSelectedBorderThickness()
        {
            return PictureModel.SelectedShape?.BorderThickness ?? 3;
        }
    }
}