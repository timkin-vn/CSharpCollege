using Drawing.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drawing.Services
{
    internal class PictureBuilder
    {
        PointModel startPoint = new PointModel { };
        PointModel endPoint = new PointModel { };
        public RectangleModel SourceBounds { get; } = new RectangleModel
        {
            X = -5,
            Y = -1,
            Width = 19,
            Height = 12,
        };
        public void DrawPicture(Painter painter) 
        {
            DrawBody(painter);
            DrawWheels(painter);
            DrawCaterpillar(painter);
            DrawTanks(painter);
            DrawTower(painter);
        }
        private void DrawWheels(Painter painter) 
        {
            double circle = 2.1;
            double nestedCircle = 0.2;
            double smallerCircle = 0.44;
            var mainPen = new Pen(Color.Black, 3);
            var wheelBrush = new SolidBrush(ColorTranslator.FromHtml("#556B2F"));

            //основные 5 колес
            var rect = new RectangleModel { X = -1, Y = 0, Width = 2, Height = 2 };
            for (var i = 0; i < 5; i++)
            {
                painter.DrawEllipse(wheelBrush, mainPen, rect);
                rect.Y = nestedCircle;
                rect.X += nestedCircle;
                rect.Width = 1.6;
                rect.Height = 1.6;
                mainPen = new Pen(Color.Black, 1);
                painter.DrawEllipse(wheelBrush, mainPen, rect);
                rect.Y += smallerCircle;
                rect.X += smallerCircle;
                rect.Width = 0.7;
                rect.Height = 0.7;
                painter.DrawEllipse(wheelBrush, mainPen, rect);
                rect.Y -= nestedCircle + 0.42;
                rect.X -= nestedCircle + 0.42;
                rect.Width = 2;
                rect.Height = 2;
                if (i < 2)
                {
                    rect.X += circle;
                }
                else
                {
                    rect.X += circle + 0.2;
                }
                mainPen = new Pen(Color.Black, 3);
            }

            //переднее колесо
            rect = new RectangleModel { X = 10, Y = 0.9, Width = 1.5, Height = 1.5 };
            painter.DrawEllipse(wheelBrush, mainPen, rect);
            mainPen = new Pen(Color.Black, 3);

            rect.X = 10.75;
            rect.Y = 1.65;
            double smallCircleRadius = 0.1;
            double bigCircleRadius = rect.Width / 3;
            double angleStep = 2 * Math.PI / 5;
            for (var i = 0; i < 5; i++)
            {
                mainPen = new Pen(Color.Black, 1);
                double angle = i * angleStep;

                double smallCircleX = rect.X + bigCircleRadius * Math.Cos(angle) - smallCircleRadius;
                double smallCircleY = rect.Y + bigCircleRadius * Math.Sin(angle) - smallCircleRadius;

                var smallCircleRect = new RectangleModel
                {
                    X = smallCircleX,
                    Y = smallCircleY,
                    Width = smallCircleRadius * 2,
                    Height = smallCircleRadius * 2
                };
                painter.DrawEllipse(wheelBrush, mainPen, smallCircleRect);
            }
            mainPen = new Pen(Color.Black, 3);
            //заднее колесо
            rect = new RectangleModel { X = -3, Y = 0.6, Width = 1.8, Height = 1.8 };
            painter.DrawEllipse(wheelBrush, mainPen, rect);

            rect.X = -2.1;
            rect.Y = 1.5;
            bigCircleRadius = rect.Width / 3;
            for (var i = 0; i < 5; i++)
            {
                double angle = i * angleStep;

                double smallCircleX = rect.X + bigCircleRadius * Math.Cos(angle) - smallCircleRadius;
                double smallCircleY = rect.Y + bigCircleRadius * Math.Sin(angle) - smallCircleRadius;

                var smallCircleRect = new RectangleModel
                {
                    X = smallCircleX,
                    Y = smallCircleY,
                    Width = smallCircleRadius * 2,
                    Height = smallCircleRadius * 2
                };
                mainPen = new Pen(Color.Black, 1);
                painter.DrawEllipse(wheelBrush, mainPen, smallCircleRect);
            }
        }
        private void DrawCaterpillar(Painter painter) 
        {
            var linePen = new Pen(Color.Black, 3);
            //гусеница
            //верх
            startPoint = new PointModel { X = 0, Y = 2 };
            endPoint = new PointModel { X = 9, Y = 2 };
            painter.DrawLine(linePen, startPoint, endPoint);

            startPoint = new PointModel { X = -2, Y = 2.4 };
            endPoint = new PointModel { X = -0.1, Y = 2 };
            painter.DrawLine(linePen, startPoint, endPoint);

            startPoint = new PointModel { X = 10.5, Y = 2.4 };
            endPoint = new PointModel { X = 9, Y = 2 };
            painter.DrawLine(linePen, startPoint, endPoint);

            //низ
            startPoint = new PointModel { X = 0, Y = 0 };
            endPoint = new PointModel { X = 9, Y = 0 };
            painter.DrawLine(linePen, startPoint, endPoint);

            startPoint = new PointModel { X = -2.5, Y = 0.7 };
            endPoint = new PointModel { X = -0.1, Y = 0 };
            painter.DrawLine(linePen, startPoint, endPoint);

            startPoint = new PointModel { X = 11.1, Y = 1 };
            endPoint = new PointModel { X = 9.1, Y = 0 };
            painter.DrawLine(linePen, startPoint, endPoint);
        }
        private void DrawBody(Painter painter) 
        {
            var mainPen = new Pen(Color.Black, 3);
            var linePen = new Pen(Color.Black, 2);
            //корпус
            var sweeperBrush = new SolidBrush(ColorTranslator.FromHtml("#6B8E23"));
            var sweeperPoints = new[]
            {
                new PointModel { X = -2, Y = 2.89, },
                new PointModel { X = 10, Y = 2.89, },
                new PointModel { X = 8.2, Y = 4.3, },
                new PointModel { X = -0.4, Y = 4.3, },
            };
            painter.DrawPolygon(sweeperBrush, mainPen, sweeperPoints);
            mainPen = new Pen(Color.Black, 0);
            sweeperBrush = new SolidBrush(ColorTranslator.FromHtml("#556B2F"));
            sweeperPoints = new[]
            {
                new PointModel { X = -1.3, Y = 1 },     // Нижний центр
                new PointModel { X = 8.8, Y = 1 },   // Правый нижний угол
                new PointModel { X = 11.9, Y = 2.2 },   // Правый верхний угол
                new PointModel { X = 11.2, Y = 2.8 },     // Верхний центр
                new PointModel { X = -2.93, Y = 2.7 },  // Левый верхний угол
                new PointModel { X = -3.83, Y = 2.21 },  // Левый нижний угол
            };
            //склоны корпуса
            painter.DrawPolygon(sweeperBrush, mainPen, sweeperPoints);
            startPoint = new PointModel { X = 12, Y = 2.2 };
            endPoint = new PointModel { X = 11.4, Y = 2.8 };
            painter.DrawLine(linePen, startPoint, endPoint);

            startPoint = new PointModel { X = 11.9, Y = 2.15 };
            endPoint = new PointModel { X = 11.3, Y = 2.75 };
            painter.DrawLine(linePen, startPoint, endPoint);

            startPoint = new PointModel { X = -3.8, Y = 2.3 };
            endPoint = new PointModel { X = -3, Y = 2.8 };
            painter.DrawLine(linePen, startPoint, endPoint);

            startPoint = new PointModel { X = -3.75, Y = 2.25 };
            endPoint = new PointModel { X = -2.95, Y = 2.75 };
            painter.DrawLine(linePen, startPoint, endPoint);

            startPoint = new PointModel { X = 11.4, Y = 2.8 };
            endPoint = new PointModel { X = -3, Y = 2.8 };
            painter.DrawLine(linePen, startPoint, endPoint);

            startPoint = new PointModel { X = 11.35, Y = 2.72 };
            endPoint = new PointModel { X = -2.95, Y = 2.72 };
            painter.DrawLine(linePen, startPoint, endPoint);

            startPoint = new PointModel { X = -1.2, Y = 1 };
            endPoint = new PointModel { X = 9, Y = 1 };
            painter.DrawLine(linePen, startPoint, endPoint);

            startPoint = new PointModel { X = 12, Y = 2.2 };
            endPoint = new PointModel { X = 9, Y = 1 };
            painter.DrawLine(linePen, startPoint, endPoint);

            startPoint = new PointModel { X = -3.85, Y = 2.26 };
            endPoint = new PointModel { X = -1.2, Y = 1 };
            painter.DrawLine(linePen, startPoint, endPoint);
        }
        private void DrawTanks(Painter painter) 
        {

            var wheelBrush = new SolidBrush(ColorTranslator.FromHtml("#708090"));
            var rect = new RectangleModel { X = -1, Y = 0, Width = 2, Height = 2 };
            var mainPen = new Pen(Color.Black, 3);
            //баки
            rect = new RectangleModel { X = -1.8, Y = 3.7, Width = 1, Height = 1 };
            painter.DrawEllipse(wheelBrush, mainPen, rect);
            mainPen = new Pen(Color.Black, 2);
            rect = new RectangleModel { X = -0.5, Y = 3.85, Width = 2, Height = 0.8 };
            painter.DrawRectangle(wheelBrush, mainPen, rect);
        }
        private void DrawTower(Painter painter) 
        {
            var mainPen = new Pen(Color.Black, 2);
            var rect = new RectangleModel { X = -1, Y = 0, Width = 2, Height = 2 };
            var linePen = new Pen(Color.Black, 2);
            var sweeperBrush = new SolidBrush(ColorTranslator.FromHtml("#006400"));
            var bodyBrush = new SolidBrush(ColorTranslator.FromHtml("#006400"));
            //башня
            startPoint = new PointModel { X = 4, Y = 4.8 };
            endPoint = new PointModel { X = 4, Y = 4.3 };
            painter.DrawLine(linePen, startPoint, endPoint);

            startPoint = new PointModel { X = 8.2, Y = 4.8 };
            endPoint = new PointModel { X = 8.2, Y = 4.3 };
            painter.DrawLine(linePen, startPoint, endPoint);

            var sweeperPoints = new[]
            {
                new PointModel { X = 4, Y = 4.8, },
                new PointModel { X = 4, Y = 4.3, },
                new PointModel { X = 8.2, Y = 4.3, },
                new PointModel { X = 8.2, Y = 4.8, },
            };
            painter.DrawPolygon(sweeperBrush, mainPen, sweeperPoints);

            startPoint = new PointModel { X = 8.2, Y = 4.8 };
            endPoint = new PointModel { X = 2.5, Y = 4.8 };
            painter.DrawLine(linePen, startPoint, endPoint);

            startPoint = new PointModel { X = 8.2, Y = 6.3 };
            endPoint = new PointModel { X = 6.5, Y = 6.5 };
            painter.DrawLine(linePen, startPoint, endPoint);

            startPoint = new PointModel { X = 6.5, Y = 6.5 };
            endPoint = new PointModel { X = 3, Y = 6.5 };
            painter.DrawLine(linePen, startPoint, endPoint);

            rect = new RectangleModel { X = 7.45, Y = 4.8, Width = 1.5, Height = 1.5, };
            painter.DrawPie(bodyBrush, mainPen, rect, 270, 180);

            startPoint = new PointModel { X = 2.5, Y = 4.85 };
            endPoint = new PointModel { X = 3, Y = 6.5 };
            painter.DrawLine(linePen, startPoint, endPoint);

            //свт
            startPoint = new PointModel { X = 8.9, Y = 5.2 };
            endPoint = new PointModel { X = 9.5, Y = 5.2 };
            painter.DrawLine(linePen, startPoint, endPoint);

            startPoint = new PointModel { X = 8.85, Y = 6 };
            endPoint = new PointModel { X = 9.5, Y = 6 };
            painter.DrawLine(linePen, startPoint, endPoint);

            startPoint = new PointModel { X = 9.5, Y = 5.2 };
            endPoint = new PointModel { X = 9.5, Y = 6 };
            painter.DrawLine(linePen, startPoint, endPoint);

            startPoint = new PointModel { X = 9.5, Y = 5.4 };
            endPoint = new PointModel { X = 14, Y = 5.45 };
            painter.DrawLine(linePen, startPoint, endPoint);

            startPoint = new PointModel { X = 9.5, Y = 5.85 };
            endPoint = new PointModel { X = 14, Y = 5.8 };
            painter.DrawLine(linePen, startPoint, endPoint);
            sweeperPoints = new[]
            {
                new PointModel { X = 14, Y = 5.85, },
                new PointModel { X = 14, Y = 5.4, },
                new PointModel { X = 14.4, Y = 5.4, },
                new PointModel { X = 14.4, Y = 5.85, },
            };
            painter.DrawPolygon(sweeperBrush, mainPen, sweeperPoints);

            rect = new RectangleModel { X = 3.2, Y = 6, Width = 1.2, Height = 1 };
            painter.DrawPie(bodyBrush,mainPen,rect, 180, 180);

            linePen = new Pen(Color.Black, 2);
            mainPen = new Pen(Color.Black, 1);
            sweeperPoints = new[]
            {
                new PointModel { X = 4.7, Y = 6.9, },
                new PointModel { X = 4.7, Y = 6.5, },
                new PointModel { X = 6.2, Y = 6.5, },
                new PointModel { X = 6.2, Y = 6.9, },
            };
            painter.DrawPolygon(sweeperBrush, mainPen, sweeperPoints);

            linePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            startPoint = new PointModel { X = 4.7, Y = 6.7 };
            endPoint = new PointModel { X = 6.25, Y = 6.7 };
            painter.DrawLine(linePen, startPoint, endPoint);
        }

    }
}
