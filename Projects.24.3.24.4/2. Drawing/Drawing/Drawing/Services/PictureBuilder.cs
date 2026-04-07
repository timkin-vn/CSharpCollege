using Drawing.Models;
using Drawing.ViewServices;
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
        public int CurrentPicture { get; set; } = 1;

        public RectangleModel SourceBounds { get; } = new RectangleModel
        {
            X = -5,
            Y = -1,
            Width = 19,
            Height = 12,
        };

        public void DrawPicture(Painter painter)
        {
            if (CurrentPicture == 1) DrawCar(painter);
            else if (CurrentPicture == 2) DrawHouse(painter);
            else if (CurrentPicture == 3) DrawTree(painter);
            else DrawCar(painter);
        }

        private void DrawCar(Painter painter)
        {
            var mainPen = new Pen(Color.Black, 3);
            var wheelBrush = Brushes.LightGray;

            var rect = new RectangleModel { X = -1, Y = 0, Width = 2, Height = 2, };
            painter.DrawEllipse(mainPen, wheelBrush, rect);

            rect.X = 2;
            painter.DrawEllipse(mainPen, wheelBrush, rect);

            rect.X = 5;
            painter.DrawEllipse(mainPen, wheelBrush, rect);

            rect.X = 8;
            painter.DrawEllipse(mainPen, wheelBrush, rect);

            var bodyBrush = Brushes.DarkGray;

            rect = new RectangleModel { X = 0, Y = 2, Width = 12, Height = 4, };
            painter.DrawRectangle(mainPen, bodyBrush, rect);

            rect = new RectangleModel { X = -4, Y = 2, Width = 4, Height = 8, };
            painter.DrawRectangle(mainPen, bodyBrush, rect);

            var windowBrush = Brushes.LightSkyBlue;

            rect = new RectangleModel { X = -2, Y = 6, Width = 1.5, Height = 3, };
            painter.DrawRectangle(mainPen, windowBrush, rect);

            rect = new RectangleModel { X = 11, Y = 2, Width = 2, Height = 4, };
            painter.DrawPie(mainPen, bodyBrush, rect, 270, 180);

            var sweeperBrush = Brushes.Red;
            var sweeperPoints = new[]
            {
                new PointModel { X = 11, Y = 2, },
                new PointModel { X = 12, Y = 2, },
                new PointModel { X = 13, Y = 0.5, },
                new PointModel { X = 11, Y = 0.5, },
            };

            painter.DrawPolygon(mainPen, sweeperBrush, sweeperPoints);

            var pipePoints = new[]
            {
                new PointModel { X = 8.5, Y = 9, },
                new PointModel { X = 10.5, Y = 9, },
                new PointModel { X = 10, Y = 6, },
                new PointModel { X = 9, Y = 6, },
            };

            painter.DrawPolygon(mainPen, bodyBrush, pipePoints);
        }

        private void DrawHouse(Painter painter)
        {
            var mainPen = new Pen(Color.Black, 3);
            
            var wallBrush = Brushes.BurlyWood;
            painter.DrawRectangle(mainPen, wallBrush, new RectangleModel { X = 2, Y = 0, Width = 6, Height = 5 });
            
            var roofBrush = Brushes.Brown;
            var roofPoints = new[]
            {
                new PointModel { X = 1, Y = 5 },
                new PointModel { X = 5, Y = 9 },
                new PointModel { X = 9, Y = 5 }
            };
            painter.DrawPolygon(mainPen, roofBrush, roofPoints);

            var windowBrush = Brushes.LightSkyBlue;
            painter.DrawRectangle(mainPen, windowBrush, new RectangleModel { X = 3.5, Y = 1.5, Width = 3, Height = 2 });
        }

        private void DrawTree(Painter painter)
        {
            var mainPen = new Pen(Color.Black, 3);

            var trunkBrush = Brushes.SaddleBrown;
            painter.DrawRectangle(mainPen, trunkBrush, new RectangleModel { X = 4, Y = 0, Width = 2, Height = 5 });

            var leavesBrush = Brushes.ForestGreen;
            painter.DrawEllipse(mainPen, leavesBrush, new RectangleModel { X = 1, Y = 4, Width = 8, Height = 6 });
        }
    }
}