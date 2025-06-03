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
        public RectangleModel PictureBounds { get; } = new RectangleModel { X = -5, Width = 20, Y = 0, Height = 18 };

        public void BuildPicture(Painter painter)
        {
            //пера
            var mainPen = new Pen(Color.Black, 3);
            var thinBluePen = new Pen(Color.FromArgb(0, 150, 200), 1); // RGB-цвет
            var thickSilverPen = new Pen(Color.Silver, 4);

            //кисти
            var bodyBrush = new SolidBrush(Color.FromArgb(80, 80, 80)); // Темно-серый RGB
            var jointBrush = Brushes.Silver;
            var eyeBrush = Brushes.Yellow;
            var detailBrush = new SolidBrush(Color.FromArgb(200, 0, 0)); // Красный RGB

            // ноги (округ)
            var baseRect = new RectangleModel { X = 4, Y = 3, Width = 4, Height = 2 };
            painter.DrawPie(bodyBrush, thickSilverPen, baseRect, 0, 180);

            // 2 ноги прямоуг
            var leftLeg = new RectangleModel { X = 4, Y = 5, Width = 1.5, Height = 3 };
            painter.DrawRectangle(bodyBrush, mainPen, leftLeg);

            var rightLeg = new RectangleModel { X = 6.5, Y = 5, Width = 1.5, Height = 3 };
            painter.DrawRectangle(bodyBrush, mainPen, rightLeg);

            // туловище прямоуг
            var bodyRect = new RectangleModel { X = 3, Y = 8, Width = 6, Height = 4 };
            painter.DrawRectangle(bodyBrush, thickSilverPen, bodyRect);

            // полукруг на груди
            var chestArc = new RectangleModel { X = 3.5, Y = 9, Width = 5, Height = 2 };
            painter.DrawPie(detailBrush, thinBluePen, chestArc, 0, -180);

            // прямоуг шея
            var neckRect = new RectangleModel { X = 5, Y = 12, Width = 2, Height = 1 };
            painter.DrawRectangle(jointBrush, mainPen, neckRect);

            // прямоугольная голова
            var headRect = new RectangleModel { X = 4, Y = 12, Width = 4, Height = 3 };
            painter.DrawRectangle(bodyBrush, thickSilverPen, headRect);

            // 2 элипса глаза
            var leftEye = new RectangleModel { X = 4.5, Y = 12.5, Width = 1, Height = 1 };
            painter.DrawEllipse(eyeBrush, mainPen, leftEye);

            var rightEye = new RectangleModel { X = 6.5, Y = 12.5, Width = 1, Height = 1 };
            painter.DrawEllipse(eyeBrush, mainPen, rightEye);

            // 2 прямоуг руки
            var leftArm = new RectangleModel { X = 1, Y = 8.5, Width = 2, Height = 3 };
            painter.DrawRectangle(bodyBrush, mainPen, leftArm);

            var rightArm = new RectangleModel { X = 9, Y = 8.5, Width = 2, Height = 3 };
            painter.DrawRectangle(bodyBrush, mainPen, rightArm);

            // 2 кисти
            var leftHand = new RectangleModel { X = 0.5, Y = 9, Width = 1, Height = 2 };
            painter.DrawRectangle(jointBrush, mainPen, leftHand);

            var rightHand = new RectangleModel { X = 10.5, Y = 9, Width = 1, Height = 2 };
            painter.DrawRectangle(jointBrush, mainPen, rightHand);

            // антенна
            painter.DrawLine(thinBluePen, new PointModel { X = 6, Y = 15 }, new PointModel { X = 6, Y = 16 });

            // элипс на антенне
            var antennaJoint = new RectangleModel { X = 5.8, Y = 16, Width = 0.4, Height = 0.4 };
            painter.DrawEllipse(detailBrush, null, antennaJoint);
        }
    }
}