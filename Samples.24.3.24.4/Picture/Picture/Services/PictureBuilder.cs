using Picture.Models;
using Picture.ViewServices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Picture.Services
{
    internal class PictureBuilder
    {
        private Pen CreatePen(Color color, float width = 2, DashStyle dashStyle = DashStyle.Solid)
        {
            return new Pen(color, width) { DashStyle = dashStyle };
        }

        private Brush CreateSolidBrush(Color color)
        {
            return new SolidBrush(color);
        }

        private Brush CreateHatchBrush(HatchStyle style, Color foreColor, Color backColor)
        {
            return new HatchBrush(style, foreColor, backColor);
        }

        private Brush CreateGradientBrush(RectangleModel rect, Color color1, Color color2, float angle)
        {
            var scaledRect = new RectangleModel(rect.X, rect.Y, rect.Width, rect.Height);
            var rectForGradient = new Rectangle(
                (int)scaledRect.X,
                (int)scaledRect.Y,
                (int)scaledRect.Width,
                (int)scaledRect.Height
            );
            return new LinearGradientBrush(rectForGradient, color1, color2, angle);
        }
        public RectangleModel SourceBounds { get; } = new RectangleModel(0, 0, 560, 500);

        public void DrawPicture(Painter painter)
        {
            // 1. Фон (прямоугольник с градиентом) - тип: Rectangle
            var skyRect = new RectangleModel(0, 0, SourceBounds.Width, SourceBounds.Height);
            var skyBrush = new LinearGradientBrush(
                new Rectangle(0, 0, (int)SourceBounds.Width, (int)SourceBounds.Height),
                Color.LightBlue,
                Color.White,
                90f);
            painter.DrawRectangle(null, skyBrush, skyRect);

            // 2. Трава (прямоугольник с штриховкой) - тип: Rectangle
            var grassRect = new RectangleModel(0, SourceBounds.Height * 0.6, SourceBounds.Width, SourceBounds.Height * 0.4);
            var grassBrush = new HatchBrush(HatchStyle.Wave, Color.Green, Color.LightGreen);
            painter.DrawRectangle(null, grassBrush, grassRect);

            // 3. Солнце (закрашенный эллипс) - тип: Ellipse
            var sunRect = new RectangleModel(SourceBounds.Width * 0.8, SourceBounds.Height * 0.1, 80, 80);
            var sunPen = CreatePen(Color.Orange, 3);
            var sunBrush = CreateSolidBrush(Color.Yellow);
            painter.DrawEllipse(sunPen, sunBrush, sunRect);

            // 4. Лучи солнца (линии с пунктиром) - тип: Line
            var sunPenDashed = CreatePen(Color.Orange, 2, DashStyle.Dash);
            for (int i = 0; i < 8; i++)
            {
                double angle = i * 45 * Math.PI / 180;
                double StartX = sunRect.X + sunRect.Width / 2 + Math.Cos(angle) * 45;
                double startY = sunRect.Y + sunRect.Height / 2 + Math.Sin(angle) * 45;
                double endX = sunRect.X + sunRect.Width / 2 + Math.Cos(angle) * 70;
                double endY = sunRect.Y + sunRect.Height / 2 + Math.Sin(angle) * 70;

                painter.DrawLine(sunPenDashed, new PointModel(StartX, startY), new PointModel(endX, endY));
            }

            // 5. Облако (три эллипса с разной заливкой) - тип: Ellipse
            var cloudBrush = new SolidBrush(Color.FromArgb(200, Color.White)); // полупрозрачный
            var cloudPen = new Pen(Color.LightGray, 1);

            painter.DrawEllipse(cloudPen, cloudBrush, new RectangleModel(100, 50, 60, 40));
            painter.DrawEllipse(cloudPen, cloudBrush, new RectangleModel(140, 40, 70, 50));
            painter.DrawEllipse(cloudPen, cloudBrush, new RectangleModel(190, 50, 60, 40));

            // 6. Дом (основная часть) - тип: Rectangle
            var houseRect = new RectangleModel(200, 200, 190, 150);
            var houseBrush = new HatchBrush(HatchStyle.Cross, Color.SaddleBrown, Color.Peru);
            var housePen = CreatePen(Color.Black, 2);
            painter.DrawRectangle(housePen, houseBrush, houseRect);

            // 7. Крыша (многоугольник) - тип: Polygon
            var roofPoints = new[]
            {
                new PointModel(180, 210),
                new PointModel(300, 120),
                new PointModel(410, 210)
            };
            var roofBrush = new SolidBrush(Color.Red);
            var roofPen = CreatePen(Color.DarkRed, 3);
            painter.DrawPolygon(roofPen, roofBrush, roofPoints);

            // 8.Труба на крыше(многоугольник со скошенным низом) -тип: Polygon
            PointModel[] chimneyPoints = new[]
            {
                new PointModel(330, 100),                    // левый верх
                new PointModel(355, 100),                    // правый верх
                new PointModel(355, 165),                    // правый низ (скошен)
                new PointModel(330, 145)                     // левый низ (скошен)
            };
            var chimneyBrush = new HatchBrush(HatchStyle.LightVertical, Color.Gray, Color.DarkGray);
            var chimneyPen = CreatePen(Color.Black, 1);
            painter.DrawPolygon(chimneyPen, chimneyBrush, chimneyPoints);

            // 9. Дверь (прямоугольник) - тип: Rectangle
            var doorRect = new RectangleModel(310, 270, 60, 80);
            var doorBrush = new SolidBrush(Color.Brown);
            var doorPen = CreatePen(Color.Black, 2);
            painter.DrawRectangle(doorPen, doorBrush, doorRect);

            // 10. Окно из левого и правого полуэллипсов (исправлено)
            double windowX = 220;
            double windowY = 230;
            double windowWidth = 70;  // ширина всего окна
            double windowHeight = 70; // высота всего окна

            var framePen = CreatePen(Color.DarkBlue, 2);
            var windowBrush = new SolidBrush(Color.FromArgb(180, Color.LightBlue));

            // Единый внешний прямоугольник для всего окна
            var fullWindowRect = new RectangleModel(windowX, windowY, windowWidth, windowHeight);

            // Левый полуэллипс: используем полный прямоугольник, но рисуем только левую половину (от 90° до 270°)
            painter.DrawPie(framePen, windowBrush, fullWindowRect, 90, 180);

            // Правый полуэллипс: используем тот же прямоугольник, но рисуем правую половину (от -90° до 90°)
            painter.DrawPie(framePen, windowBrush, fullWindowRect, -90, 180);

            // Вертикальная линия-перегородка (по центру)
            var dividerPen = CreatePen(Color.DarkBlue, 2);
            painter.DrawLine(dividerPen,
                new PointModel(windowX + windowWidth / 2, windowY),
                new PointModel(windowX + windowWidth / 2, windowY + windowHeight));

            // Горизонтальная линия-перегородка (опционально)
            painter.DrawLine(dividerPen,
                new PointModel(windowX, windowY + windowHeight / 2),
                new PointModel(windowX + windowWidth, windowY + windowHeight / 2));

            // 11. Ручка на двери (маленький эллипс) - тип: Ellipse
            var handleRect = new RectangleModel(315, 310, 9, 9);
            var handleBrush = new SolidBrush(Color.Gold);
            painter.DrawEllipse(null, handleBrush, handleRect);

            // 12. Дерево (ствол - прямоугольник, крона - 2 эллипса) - тип: Rectangle + Ellipse
            var trunkRect = new RectangleModel(70, 250, 40, 100);
            var trunkBrush = new HatchBrush(HatchStyle.LightVertical, Color.SaddleBrown, Color.Peru);
            painter.DrawRectangle(null, trunkBrush, trunkRect);

            var crownRect = new RectangleModel(40, 200, 100, 65);
            var crownBrush = new HatchBrush(HatchStyle.Wave,Color.DarkGreen, Color.Green);
            painter.DrawEllipse(null, crownBrush, crownRect);

            var crown2Rect = new RectangleModel(60, 180, 60, 60);
            var crown2Brush = new HatchBrush(HatchStyle.Wave, Color.DarkGreen, Color.Green);
            painter.DrawEllipse(null, crown2Brush, crown2Rect);

            // 13. Забор с треугольными верхушками и перекладиной
            var fencePostBrush = new HatchBrush(HatchStyle.WideDownwardDiagonal, Color.SaddleBrown, Color.Peru);
            var fencePostPen = CreatePen(Color.Black, 1);
            var fenceTopBrush = new SolidBrush(Color.Brown);
            var fenceTopPen = CreatePen(Color.DarkRed, 1);
            var fenceLinePen = CreatePen(Color.SaddleBrown, 4);

            double startX = 50;
            double postWidth = 20;
            double postHeight = 50;
            double topHeight = 20;
            double spacing = 25;
            int postCount = 5;

            // Перекладины (горизонтальные линии через все столбы)
            double lineY = 320 + postHeight - 15; // чуть выше низа столбов
            painter.DrawLine(fenceLinePen, new PointModel(startX - 5, lineY), new PointModel(startX + postCount * (postWidth + spacing) - spacing + 5, lineY));

            double line1Y = 320 + postHeight - 35; // чуть ниже верха столбов
            painter.DrawLine(fenceLinePen, new PointModel(startX - 5, line1Y), new PointModel(startX + postCount * (postWidth + spacing) - spacing + 5, line1Y));

            for (int i = 0; i < postCount; i++)
            {
                double x = startX + i * (postWidth + spacing);

                // 1. Основа столба (прямоугольник)
                var postRect = new RectangleModel(x, 320, postWidth, postHeight);
                painter.DrawRectangle(fencePostPen, fencePostBrush, postRect);

                // 2. Треугольная верхушка
                var topPoints = new[]
                {
                    new PointModel(x - 3, 320),                // левый низ
                    new PointModel(x + postWidth / 2, 320 - topHeight), // вершина
                    new PointModel(x + postWidth + 3, 320)     // правый низ
                };
                painter.DrawPolygon(fenceTopPen, fenceTopBrush, topPoints);
            }

            // 14. Человечек справа от домика
            double manX = 450; // правее домика (дом заканчивается ~420)
            double manY = 220;

            // Голова (эллипс с градиентом)
            var headRect = new RectangleModel(manX, manY, 30, 30);
            var headBrush = new LinearGradientBrush(
                new Rectangle((int)headRect.X, (int)headRect.Y, (int)headRect.Width, (int)headRect.Height),
                Color.LightPink,
                Color.LightCoral,
                45f);
            var headPen = CreatePen(Color.Black, 1);
            painter.DrawEllipse(headPen, headBrush, headRect);

            // Глаза (два маленьких эллипса)
            var leftEyeRect = new RectangleModel(manX + 7, manY + 8, 4, 6);
            var rightEyeRect = new RectangleModel(manX + 19, manY + 8, 4, 6);
            var eyeBrush = new SolidBrush(Color.Black);
            painter.DrawEllipse(null, eyeBrush, leftEyeRect);
            painter.DrawEllipse(null, eyeBrush, rightEyeRect);

            // Рот (дуга)
            var mouthRect = new RectangleModel(manX + 7, manY + 15, 16, 8);
            var mouthPen = CreatePen(Color.DarkRed, 1);
            painter.DrawArc(mouthPen, mouthRect, 0, 180); // улыбка

            // тело (трапеция в полоску)
            PointModel[] bodyPoints = new[]
            {
                new PointModel(manX + 10, manY + 30),  // левое плечо (верх-лево)
                new PointModel(manX + 20, manY + 30),  // правое плечо (верх-право)
                new PointModel(manX + 28, manY + 70),  // правый низ (шире)
                new PointModel(manX + 2, manY + 70)    // левый низ (шире)
            };
            var bodyBrush = new HatchBrush(HatchStyle.LightHorizontal, Color.Blue, Color.LightBlue);
            var bodyPen = CreatePen(Color.Black, 1);
            painter.DrawPolygon(bodyPen, bodyBrush, bodyPoints);


            // Левая рука (линия)
            painter.DrawLine(CreatePen(Color.Black, 1),
                new PointModel(manX + 7, manY + 40),
                new PointModel(manX - 7, manY + 50));

            // Правая рука (линия)
            painter.DrawLine(CreatePen(Color.Black, 1),
                new PointModel(manX + 25, manY + 40),
                new PointModel(manX + 40, manY + 45));

            // Левая нога (линия)
            painter.DrawLine(CreatePen(Color.Black, 1),
                new PointModel(manX + 10, manY + 70),
                new PointModel(manX + 5, manY + 90));

            // Правая нога (линия)
            painter.DrawLine(CreatePen(Color.Black, 1),
                new PointModel(manX + 23, manY + 70),
                new PointModel(manX + 30, manY + 90));

            // Шляпа (прямоугольники)
            var hatTopRect = new RectangleModel(manX + 2, manY - 7, 26, 8);
            var hatTopBrush = new SolidBrush(Color.DarkRed);
            painter.DrawRectangle(null, hatTopBrush, hatTopRect);

            var hatBrimRect = new RectangleModel(manX - 3, manY - 1, 36, 5);
            var hatBrimBrush = new SolidBrush(Color.Red);
            painter.DrawRectangle(null, hatBrimBrush, hatBrimRect);

            //var hatPomponRect = new RectangleModel(manX + 13, manY - 20, 4, 4);
            //var hatPomponBrush = new SolidBrush(Color.White);
            //painter.DrawEllipse(null, hatPomponBrush, hatPomponRect);
        }
    }
}