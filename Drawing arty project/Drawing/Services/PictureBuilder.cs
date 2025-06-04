using Drawing.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing.Services
{
    internal class PictureBuilder
    {
        public RectangleModel PictureBounds { get; } = new RectangleModel { X = -5, Width = 20, Y = -1, Height = 15 };

        public void BuildPicture(Painter painter)
        {
            var pen = new Pen(Color.Black, 1.2f);

            var bodyBrush = new SolidBrush(Color.Silver);
            var windowBrush = new SolidBrush(Color.LightSkyBlue);
            var wheelBrush = new SolidBrush(Color.Black);
            var lightBrushFront = new SolidBrush(Color.OrangeRed);
            var lightBrushRear = new SolidBrush(Color.Yellow);
            var detailBrush = new SolidBrush(Color.DimGray);

            // --- Основной корпус ---
            var body = new[]
            {
        new PointModel { X = 1.0, Y = 2.0 },
        new PointModel { X = 2.0, Y = 4.0 },
        new PointModel { X = 4.0, Y = 4.3 },
        new PointModel { X = 9.0, Y = 4.0 },
        new PointModel { X = 10.3, Y = 3.0 },
        new PointModel { X = 11.0, Y = 2.0 },
        new PointModel { X = 10.0, Y = 1.7 },
        new PointModel { X = 8.0, Y = 1.6 },
        new PointModel { X = 4.5, Y = 1.5 },
        new PointModel { X = 3.0, Y = 1.6 },
    };
            painter.DrawPolygon(bodyBrush, pen, body);

            // --- Окна передние ---
            var frontWindow = new[]
            {
        new PointModel { X = 3.2, Y = 3.8 },
        new PointModel { X = 5.5, Y = 3.9 },
        new PointModel { X = 5.0, Y = 2.0 },
        new PointModel { X = 3.8, Y = 2.0 },
    };
            painter.DrawPolygon(windowBrush, pen, frontWindow);

            // --- Окна задние ---
            var rearWindow = new[]
            {
        new PointModel { X = 5.7, Y = 3.9 },
        new PointModel { X = 7.7, Y = 3.7 },
        new PointModel { X = 7.3, Y = 2.0 },
        new PointModel { X = 5.6, Y = 2.0 },
    };
            painter.DrawPolygon(windowBrush, pen, rearWindow);

            //  Колёса 
            painter.DrawEllipse(wheelBrush, pen, new RectangleModel { X = 2.4, Y = 0.6, Width = 1.2, Height = 1.2 });
            painter.DrawEllipse(wheelBrush, pen, new RectangleModel { X = 8.0, Y = 0.6, Width = 1.2, Height = 1.2 });

            //  Фара передняя 
            painter.DrawRectangle(lightBrushFront, pen, new RectangleModel { X = 0.9, Y = 2.4, Width = 0.3, Height = 0.5 });

            //  Фара задняя 
            painter.DrawRectangle(lightBrushRear, pen, new RectangleModel { X = 10.8, Y = 2.4, Width = 0.2, Height = 0.5 });

            //  Дверь и ручка 
            painter.DrawLine(pen, new PointModel { X = 5.6, Y = 3.9 }, new PointModel { X = 5.6, Y = 2.0 });
            painter.DrawRectangle(detailBrush, pen, new RectangleModel { X = 5.3, Y = 2.4, Width = 0.3, Height = 0.1 });

            //  Бампер 
            painter.DrawRectangle(detailBrush, pen, new RectangleModel { X = 10.9, Y = 1.9, Width = 0.3, Height = 0.3 });

            //  Лобовое стекло (передний уклон) 
            painter.DrawLine(pen, new PointModel { X = 3.2, Y = 3.8 }, new PointModel { X = 3.8, Y = 2.0 });

            //  Линия капота 
            painter.DrawLine(pen, new PointModel { X = 3.8, Y = 2.0 }, new PointModel { X = 5.0, Y = 2.0 });

            // Зеркала боковые 
            painter.DrawRectangle(detailBrush, pen, new RectangleModel { X = 3.0, Y = 3.0, Width = 0.2, Height = 0.2 });
            painter.DrawRectangle(detailBrush, pen, new RectangleModel { X = 7.2, Y = 3.0, Width = 0.2, Height = 0.2 });

            pen.Dispose();
            bodyBrush.Dispose();
            windowBrush.Dispose();
            wheelBrush.Dispose();
            lightBrushFront.Dispose();
            lightBrushRear.Dispose();
            detailBrush.Dispose();
        }



    }
}