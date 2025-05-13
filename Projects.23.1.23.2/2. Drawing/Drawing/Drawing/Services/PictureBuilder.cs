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
        public RectangleModel SourceBounds { get; } = new RectangleModel
        {
            X = -5,
            Y = -1,
            Width = 19,
            Height = 12,
        };

        public void DrawPicture(Painter painter)
        {
            /*var mainPen = new Pen(Color.Black, 3);
            var wheelBrush = Brushes.LightGray;

            var rect = new RectangleModel { X = -1, Y = 0, Width = 2, Height = 2 };
            painter.DrawEllipse(wheelBrush, mainPen, rect);

            rect.X = 2;
            painter.DrawEllipse(wheelBrush, mainPen, rect);

            rect.X = 5;
            painter.DrawEllipse(wheelBrush, mainPen, rect);

            rect.X = 8;
            painter.DrawEllipse(wheelBrush, mainPen, rect);

            var bodyBrush = Brushes.DarkGray;

            rect = new RectangleModel { X = 0, Y = 2, Width = 12, Height = 4, };
            painter.DrawRectangle(bodyBrush, mainPen, rect);

            rect = new RectangleModel { X = -4, Y = 2, Width = 4, Height = 8, };
            painter.DrawRectangle(bodyBrush, mainPen, rect);

            var windowBrush = Brushes.LightSkyBlue;

            rect = new RectangleModel { X = -2, Y = 6, Width = 1.5, Height = 3, };
            painter.DrawRectangle(windowBrush, mainPen, rect);

            rect = new RectangleModel { X = 11, Y = 2, Width = 2, Height = 4, };
            painter.DrawPie(bodyBrush, mainPen, rect, 270, 180);

            var sweeperBrush = Brushes.Red;
            var sweeperPoints = new[]
            {
                new PointModel { X = 11, Y = 2, },
                new PointModel { X = 12, Y = 2, },
                new PointModel { X = 13, Y = 0.5, },
                new PointModel { X = 11, Y = 0.5, },
            };

            painter.DrawPolygon(sweeperBrush, mainPen, sweeperPoints);

            var pipePoints = new[]
            {
                new PointModel { X = 8.5, Y = 9, },
                new PointModel { X = 10.5, Y = 9, },
                new PointModel { X = 10, Y = 6, },
                new PointModel { X = 9, Y = 6, },
            };

            painter.DrawPolygon(bodyBrush, mainPen, pipePoints);*/

            var mainPen = new Pen(Color.Black, 2);
            var RoseLeaf = Brushes.LightPink;

            var LeafPoints1 = new[]
            {
                new PointModel { X = 4.5, Y = 10, },
                new PointModel { X = 5.3, Y = 9.3, },
                new PointModel { X = 7, Y = 8.5, },
                new PointModel { X = 5.6, Y = 7.5, },
                new PointModel { X = 5.4, Y = 8.4, },
                new PointModel { X = 5.2, Y = 8.6, },
                new PointModel { X = 5.2, Y = 8.8, },
                new PointModel { X = 5.1, Y = 9, },
                new PointModel { X = 4.7, Y = 9, },
                new PointModel { X = 4.2, Y = 8.75, },
            };
            painter.DrawPolygon(RoseLeaf, mainPen, LeafPoints1);

            var LeafPoints2 = new[]
            {
                new PointModel { X = 4.45, Y = 10, },
                new PointModel { X = 4.2, Y = 8.75, },
                new PointModel { X = 3.2, Y = 7.75, },
                new PointModel { X = 3.2, Y = 7.25, },
                new PointModel { X = 2.7, Y = 7.75, },
                new PointModel { X = 2.67, Y = 8.5, },
                
            };
            painter.DrawPolygon(RoseLeaf, mainPen, LeafPoints2);

            var LeafPoints3 = new[]
            {
                new PointModel { X = 2.67, Y = 8.5, },
                new PointModel { X = 2.7, Y = 7.75, },
                new PointModel { X = 2.2, Y = 7.25, },

            };
            painter.DrawPolygon(RoseLeaf, mainPen, LeafPoints3);

            var LeafPoints4 = new[]
            {
                new PointModel { X = 2.7, Y = 7.75, },
                new PointModel { X = 3.4, Y = 6.2, },
                new PointModel { X = 3.9, Y = 5.9, },
                new PointModel { X = 3.2, Y = 7.25, },
            };
            painter.DrawPolygon(RoseLeaf, mainPen, LeafPoints4);

            var LeafPoints5 = new[]
            {
                new PointModel { X = 2.7, Y = 7.75, },
                new PointModel { X = 2.2, Y = 7.2, },
                new PointModel { X = 3.4, Y = 6.2, },

            };
            painter.DrawPolygon(RoseLeaf, mainPen, LeafPoints5);

            var LeafPoints6 = new[]
            {
                new PointModel { X = 2.2, Y = 7.25, },
                new PointModel { X = 1.45, Y = 7.75, },
                new PointModel { X = 2.2, Y = 8.9, },
                new PointModel { X = 2.8, Y = 8.7, },

            };
            painter.DrawPolygon(RoseLeaf, mainPen, LeafPoints6);

            var LeafPoints7 = new[]
            {
                new PointModel { X = 3.9, Y = 5.9, },
                new PointModel { X = 5.6, Y = 7.5, },
                new PointModel { X = 5.2, Y = 8.6, },
                new PointModel { X = 5.2, Y = 8.8, },
                new PointModel { X = 5.1, Y = 9, },
                new PointModel { X = 4.7, Y = 9, },
                new PointModel { X = 4.2, Y = 8.75, },
                new PointModel { X = 3.2, Y = 7.75, },
                new PointModel { X = 3.2, Y = 7.25, },
            };
            painter.DrawPolygon(RoseLeaf, mainPen, LeafPoints7);

            var LeafPoints8 = new[]
            {
                new PointModel { X = 3.9, Y = 5.9, },
                new PointModel { X = 6, Y = 5.9, },
                new PointModel { X = 6, Y = 8.3, },
                
            };
            painter.DrawPolygon(RoseLeaf, mainPen, LeafPoints8);

            var RoseStem = Brushes.DarkGreen;
            var stem = new RectangleModel { X = 3.9, Y = 2, Width = 0.75, Height = 3.9, };
            painter.DrawRectangle(RoseStem, mainPen, stem);


        }
    }
}
