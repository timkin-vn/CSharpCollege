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
            Height = 10,
        };

        public void DrawPicture(Painter painter)
        {
            var BeigePen = new Pen(Color.Beige, 2);
            var CherryPen = new Pen(Color.Crimson, 2);
            var GreenPen = new Pen(Color.Green, 3);
            var PinkPen = new Pen(Color.LightPink, 2);
            var SpoonPen = new Pen(Color.Gray, 6);
            var LavenderBrush = Brushes.Lavender;
            var SandBrownBrush = Brushes.SandyBrown;
            var LemonBrush = Brushes.LemonChiffon;
            var ChocoBrush = Brushes.Brown;
            var CherryBrush = Brushes.Crimson;
            var SpoonBrush = Brushes.Gray;
            var WhiteBrush = Brushes.White;
            var PinkBrush = Brushes.LightPink;
            var TeaBrush = Brushes.SaddleBrown;
            

            var Rect = new RectangleModel { X = -5, Y = 0, Width = 8, Height = 2 };
            painter.DrawEllipse(LavenderBrush, BeigePen, Rect);
            
            var layer1points = new[]
            {
                new PointModel {X = -4, Y = 0.5, },
                new PointModel {X = -4, Y = 2.5, },
                new PointModel {X = 0, Y = 4, },
                new PointModel {X = 2, Y = 3, },
                new PointModel {X = 2, Y = 1, },
                
            };
            painter.DrawPolygon(SandBrownBrush, BeigePen, layer1points);

            var layer2points = new[]
            {
                new PointModel {X = -4, Y = 1.5, },
                new PointModel {X = -4, Y = 3.5, },
                new PointModel {X = 0, Y = 5, },
                new PointModel {X = 2, Y = 4, },
                new PointModel {X = 2, Y = 2, },
            };

            painter.DrawPolygon(LemonBrush, BeigePen, layer2points);

            var layer3points = new[]
            {
                new PointModel {X = -4, Y = 2.5, },
                new PointModel {X = -4, Y = 3.5, },
                new PointModel {X = 0, Y = 5, },
                new PointModel {X = 2, Y = 4, },
                new PointModel {X = 2, Y = 3, },

            };

            painter.DrawPolygon(ChocoBrush, BeigePen, layer3points);
            
            Rect = new RectangleModel { X = -1, Y = 4, Width = 1.5, Height = 1.25 };
            painter.DrawEllipse(CherryBrush, CherryPen, Rect);
            Rect = new RectangleModel { X = -1, Y = 4.35, Width = 1, Height = 1 };
            painter.DrawEllipse(CherryBrush, CherryPen, Rect);
            Rect = new RectangleModel { X = -0.5, Y = 4.35, Width = 1, Height = 1 };
            painter.DrawEllipse(CherryBrush, CherryPen, Rect);

            var branch1 = new PointModel { X = -0.25, Y = 5.30 };
            var branch2 = new PointModel { X = -0.15, Y = 6.50 };
            var branch3 = new PointModel { X = 0, Y = 7 };

            painter.DrawLine(GreenPen, branch1, branch2);
            painter.DrawLine(GreenPen, branch2, branch3);

            Rect = new RectangleModel { X = 6, Y = -0.5, Width = 6, Height = 3 };
            painter.DrawEllipse(PinkBrush, PinkPen, Rect);
            Rect = new RectangleModel { X = 10, Y = 1, Width = 4, Height = 3 };
            painter.DrawEllipse(PinkBrush, PinkPen, Rect);
            Rect = new RectangleModel { X = 11, Y = 1.5, Width = 2.6, Height = 2 };
            painter.DrawEllipse(WhiteBrush, PinkPen, Rect);

            var cupPoints = new[]
            {
                new PointModel { X = 6, Y = 1 },
                new PointModel { X = 6, Y = 4 },
                new PointModel { X = 12, Y = 4 },
                new PointModel { X = 12, Y = 1 },
            };

            painter.DrawPolygon(PinkBrush, PinkPen, cupPoints);
            Rect = new RectangleModel { X = 6, Y = 2.5, Width = 6, Height = 3 };
            painter.DrawEllipse(PinkBrush, PinkPen, Rect);
            Rect = new RectangleModel { X = 6.5, Y = 3, Width = 5, Height = 2 };
            painter.DrawEllipse(TeaBrush, PinkPen, Rect);

            var petal1 = new[]
            {
                new PointModel {X = 8.5, Y = 1 },
                new PointModel {X = 8, Y = 1.25},
                new PointModel {X = 8.5, Y = 1.5},
                new PointModel {X = 9, Y = 1.25},
            };
            painter.DrawPolygon(CherryBrush, PinkPen, petal1);
            var petal2 = new[]
            {
                new PointModel {X = 9, Y=1.25},
                new PointModel {X = 9.5, Y=1},
                new PointModel {X = 10, Y = 1.25 },
                new PointModel {X = 9.5, Y=1.5 }   
            };
            painter.DrawPolygon(CherryBrush, PinkPen, petal2 );
            var petal3 = new[]
            {
                new PointModel {X = 8.5, Y = 2},
                new PointModel {X = 8.5, Y = 1.5},
                new PointModel {X = 9, Y = 1.25},
                new PointModel {X = 9, Y = 1.75}
            };
            painter.DrawPolygon(CherryBrush, PinkPen, petal3 );

            var petal4 = new[]
            {
                new PointModel {X = 9.5, Y = 2},
                new PointModel {X = 9.5, Y = 1.5},
                new PointModel {X = 9, Y = 1.25},
                new PointModel {X = 9, Y = 1.75}
            };
            painter.DrawPolygon(CherryBrush, PinkPen, petal4);
            var petal5 = new[]
            {
                new PointModel {X = 8.5, Y = 0.5},
                new PointModel {X = 8.5, Y = 1},
                new PointModel {X = 9, Y = 1.25},
                new PointModel {X = 9, Y = 0.75}
            };
            painter.DrawPolygon(CherryBrush, PinkPen, petal5);

            var petal6 = new[]
            {
                new PointModel {X = 9.5, Y = 0.5},
                new PointModel {X = 9.5, Y = 1},
                new PointModel {X = 9, Y = 1.25},
                new PointModel {X = 9, Y = 0.75}
            };
            painter.DrawPolygon(CherryBrush, PinkPen, petal6 );
            Rect = new RectangleModel { X = 8.75, Y = 1, Width = 0.5, Height = 0.5 };

            painter.DrawEllipse(LemonBrush, PinkPen, Rect);


            Rect = new RectangleModel { X = 1, Y = 0, Width = 2, Height = 1.25 };
            painter.DrawEllipse(SpoonBrush, SpoonPen, Rect);
            var spoon1 = new PointModel { X = 3, Y = 0.5 };
            var spoon2 = new PointModel { X = 7, Y = -0.5 };
            painter.DrawLine(SpoonPen, spoon1, spoon2);
            Rect = new RectangleModel { X = 7, Y = -0.9, Width = 0.75, Height = 0.75 };
            painter.DrawEllipse(SpoonBrush, SpoonPen, Rect);
        }
    }
}
