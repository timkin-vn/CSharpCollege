using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawingWindows.Models;

namespace DrawingWindows.Services
{
    public class PictureBuilder
    {
        public RectangleModel SourceBounds => new RectangleModel
        {
            X = -5,
            Y = -1,
            Width = 19,
            Height = 12,
        };
        public void DrawPicture(Painter painter)
        {
            var mainPen = new Pen(Color.Black);
            var bodyBrush = new SolidBrush(Color.Red);
            var rect = new RectangleModel
            {
                X = 0,
                Y = 0,
                Width = 8,
                Height = 5,
            };
            painter.DrawRectangle(bodyBrush, mainPen, rect);
            var doorBrush = new SolidBrush(Color.Brown);
            rect = new RectangleModel
            {
                X = 2,
                Y = 0,
                Width = 2,
                Height = 4,
            };
            painter.DrawRectangle(doorBrush, mainPen, rect);

            var windowBrush = new SolidBrush(Color.LightBlue);
            rect = new RectangleModel
            {
                X = 4.5,
                Y = 2,
                Width = 2,
                Height = 2,
            };
            painter.DrawRectangle(windowBrush, mainPen, rect);
            var pipeBrush = new SolidBrush(Color.Gray);
            rect = new RectangleModel
            {
                Height = 4,
                Width = 2,
                X = 5,
                Y = 6
            };
            painter.DrawRectangle(pipeBrush, mainPen, rect);
            var points = new PointModel[]
            {
                new PointModel
                {
                    X = -1,
                    Y = 5,
                },
                new PointModel
                {
                    X = 9,
                    Y = 5,
                },
                new PointModel
                {
                    X = 4,
                    Y = 8,
                },
            };
            var narBrush = new SolidBrush(Color.Violet);
            painter.DrawPolygon(narBrush, mainPen, points);
        }
        
    }
}
