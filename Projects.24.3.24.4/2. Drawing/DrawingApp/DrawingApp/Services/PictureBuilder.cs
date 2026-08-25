using DrawingApp.Models;
using System.Collections.Generic;
using System.Drawing;

namespace DrawingApp.Services
{
    public class PictureBuilder
    {
        public RectangleModel SourceBounds { get; } = new RectangleModel
        {
            X = 0,
            Y = 600,
            Width = 900,
            Height = 600
        };

        public List<ShapeModel> CreateSceneObjects()
        {
            var shapes = new List<ShapeModel>();

            var outlinePen = new Pen(Color.FromArgb(40, 40, 40), 2);
            var roofPen = new Pen(Color.FromArgb(90, 40, 40), 2);

            // Солнце
            shapes.Add(new ShapeModel
            {
                Type = ShapeType.Ellipse,
                Bounds = new RectangleModel { X = 60, Y = 550, Width = 80, Height = 80 },
                StrokePen = outlinePen,
                FillBrush = new SolidBrush(Color.FromArgb(255, 210, 70))
            });

            // Облака
            shapes.Add(new ShapeModel
            {
                Type = ShapeType.Ellipse,
                Bounds = new RectangleModel { X = 170, Y = 520, Width = 70, Height = 45 },
                StrokePen = outlinePen,
                FillBrush = Brushes.White
            });
            shapes.Add(new ShapeModel
            {
                Type = ShapeType.Ellipse,
                Bounds = new RectangleModel { X = 210, Y = 540, Width = 90, Height = 55 },
                StrokePen = outlinePen,
                FillBrush = Brushes.White
            });

            // Труба
            shapes.Add(new ShapeModel
            {
                Type = ShapeType.Rectangle,
                Bounds = new RectangleModel { X = 469, Y = 475, Width = 35, Height = 65 },
                StrokePen = outlinePen,
                FillBrush = new SolidBrush(Color.FromArgb(160, 110, 90))
            });

            // Стены дома
            shapes.Add(new ShapeModel
            {
                Type = ShapeType.Rectangle,
                Bounds = new RectangleModel { X = 280, Y = 380, Width = 270, Height = 210 },
                StrokePen = outlinePen,
                FillBrush = new SolidBrush(Color.FromArgb(242, 220, 180))
            });

            // Крыша
            shapes.Add(new ShapeModel
            {
                Type = ShapeType.Polygon,
                PolygonPoints = new[]
                {
                    new PointModel(260, 380),
                    new PointModel(415, 490),
                    new PointModel(570, 380)
                },
                StrokePen = roofPen,
                FillBrush = new SolidBrush(Color.FromArgb(170, 70, 60))
            });

            // Дверь
            shapes.Add(new ShapeModel
            {
                Type = ShapeType.Rectangle,
                Bounds = new RectangleModel { X = 385, Y = 285, Width = 60, Height = 115 },
                StrokePen = outlinePen,
                FillBrush = new SolidBrush(Color.FromArgb(120, 75, 40))
            });

            // Левое окно
            shapes.Add(new ShapeModel
            {
                Type = ShapeType.Rectangle,
                Bounds = new RectangleModel { X = 310, Y = 320, Width = 55, Height = 55 },
                StrokePen = outlinePen,
                FillBrush = new SolidBrush(Color.FromArgb(170, 220, 245))
            });

            // Правое окно
            shapes.Add(new ShapeModel
            {
                Type = ShapeType.Rectangle,
                Bounds = new RectangleModel { X = 465, Y = 320, Width = 55, Height = 55 },
                StrokePen = outlinePen,
                FillBrush = new SolidBrush(Color.FromArgb(170, 220, 245))
            });

            return shapes;
        }
    }
}