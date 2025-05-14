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
            var mainPen = new Pen(Color.Black, 2);
            var RoseLeaf = Brushes.LightPink;
            var DarkRoseLeaf = Brushes.DarkRed;
            var ShadowRoseLeaf = Brushes.Black;
            var GreenRoseLeaf = Brushes.DarkGreen;
            var SkyBrush = Brushes.Blue;

            var Sky = new[]
            {
                new PointModel { X = -100, Y = 100, },
                new PointModel { X = -100, Y = -100, },
                new PointModel { X = 100, Y = -100, },
                new PointModel { X = 100, Y = 100, },
            };
            painter.DrawPolygon(SkyBrush, mainPen, Sky);

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

            var GLeafPoints1 = new[]
            {
                new PointModel { X = 4.5, Y = 7, },
                new PointModel { X = 4.3, Y = 1, },
                new PointModel { X = 4.6, Y = 0, },
                new PointModel { X = 4.8, Y = 7, },
            };
            painter.DrawPolygon(GreenRoseLeaf, mainPen, GLeafPoints1);

            var Spike1 = new[]
            {
                new PointModel { X = 4.45, Y = 4, },
                new PointModel { X = 4.25, Y = 4.4, },
            };
            painter.DrawPolygon(GreenRoseLeaf, mainPen, Spike1);

            var Spike2 = new[]
            {
                new PointModel { X = 4.45, Y = 3, },
                new PointModel { X = 4.25, Y = 3.4, },
            };
            painter.DrawPolygon(GreenRoseLeaf, mainPen, Spike2);

            var Spike3 = new[]
            {
                new PointModel { X = 4.45, Y = 2, },
                new PointModel { X = 4.25, Y = 2.4, },
            };
            painter.DrawPolygon(GreenRoseLeaf, mainPen, Spike3);

            var Spike4 = new[]
            {
                new PointModel { X = 4.65, Y = 3.5, },
                new PointModel { X = 4.85, Y = 3.9, },
            };
            painter.DrawPolygon(GreenRoseLeaf, mainPen, Spike4);

            var Spike5 = new[]
            {
                new PointModel { X = 4.65, Y = 2.5, },
                new PointModel { X = 4.85, Y = 2.9, },
            };
            painter.DrawPolygon(GreenRoseLeaf, mainPen, Spike5);

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
                new PointModel { X = 2.45, Y = 8.8, },
                new PointModel { X = 3.1, Y = 9.8, },
                new PointModel { X = 4, Y = 9.6, },
                new PointModel { X = 2.8, Y = 8.7, },

            };
            painter.DrawPolygon(RoseLeaf, mainPen, LeafPoints7);

            var LeafPoints8 = new[]
            {
                new PointModel { X = 2.4, Y = 7.05, },
                new PointModel { X = 2, Y = 6.8, },
                new PointModel { X = 1.7, Y = 6.3, },
                new PointModel { X = 3.4, Y = 6.2, },

            };
            painter.DrawPolygon(RoseLeaf, mainPen, LeafPoints8);

            var LeafPoints9 = new[]
            {
                new PointModel { X = 1.7, Y = 6.3, },
                new PointModel { X = 3, Y = 5, },
                new PointModel { X = 3.45, Y = 5.3, },
                new PointModel { X = 3.4, Y = 6.2, },

            };
            painter.DrawPolygon(RoseLeaf, mainPen, LeafPoints9);

            var LeafPoints10 = new[]
            {
                new PointModel { X = 3.4, Y = 6.2, },
                new PointModel { X = 3.5, Y = 5, },
                new PointModel { X = 5.2, Y = 5.2, },
                new PointModel { X = 4.2, Y = 5.65, },
            };
            painter.DrawPolygon(RoseLeaf, mainPen, LeafPoints10);

            var LeafPoints11 = new[]
            {
                new PointModel { X = 6.1, Y = 8.95, },
                new PointModel { X = 6.6, Y = 9.1, },
                new PointModel { X = 7, Y = 9, },
                new PointModel { X = 8, Y = 7.7, },
                new PointModel { X = 7.3, Y = 6.8, },
            };
            painter.DrawPolygon(RoseLeaf, mainPen, LeafPoints11);

            var LeafPoints20 = new[]
            {
                new PointModel { X = 4.5, Y = 8, },
                new PointModel { X = 4.4, Y = 7.4, },
                new PointModel { X = 3.2, Y = 7.75, },
                new PointModel { X = 4.2, Y = 8.75, },

            };
            painter.DrawPolygon(RoseLeaf, mainPen, LeafPoints20);

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

            var LeafPoints12 = new[]
            {
                new PointModel { X = 7, Y = 8.5, },
                new PointModel { X = 5.6, Y = 7.5, },
                new PointModel { X = 6.1, Y = 5.5, },
                new PointModel { X = 6.7, Y = 6, },
                new PointModel { X = 6.9, Y = 6.6, },
            };
            painter.DrawPolygon(RoseLeaf, mainPen, LeafPoints12);


            var LeafPoints13 = new[]
            {
                new PointModel { X = 3.5, Y = 5, },
                new PointModel { X = 5.2, Y = 5.2, },
                new PointModel { X = 5.8, Y = 4.1, },
            };
            painter.DrawPolygon(RoseLeaf, mainPen, LeafPoints13);

            var LeafPoints14 = new[]
            {
                new PointModel { X = 5.2, Y = 5.2, },
                new PointModel { X = 7, Y = 7.6, },
                new PointModel { X = 7.3, Y = 6, },
            };
            painter.DrawPolygon(RoseLeaf, mainPen, LeafPoints14);

            var LeafPoints15 = new[]
            {
                new PointModel { X = 5.2, Y = 5.2, },
                new PointModel { X = 7.3, Y = 6, },
                new PointModel { X = 5.8, Y = 4.1, },
            };
            painter.DrawPolygon(RoseLeaf, mainPen, LeafPoints15);

            var LeafPoints16 = new[]
            {
                new PointModel { X = 5.2, Y = 8.6, },
                new PointModel { X = 5.2, Y = 8.8, },
                new PointModel { X = 5.1, Y = 9, },
                new PointModel { X = 4.7, Y = 9, },
                new PointModel { X = 4.2, Y = 8.75, },
                new PointModel { X = 4.1, Y = 8.85, },
                new PointModel { X = 4, Y = 8.75, },
                new PointModel { X = 4, Y = 8.45, },
                new PointModel { X = 4.5, Y = 8, },
                new PointModel { X = 4.6, Y = 8.3, },
            };
            painter.DrawPolygon(RoseLeaf, mainPen, LeafPoints16);

            var DarkLeafPoints1 = new[]
            {
                new PointModel { X = 4.5, Y = 8, },
                new PointModel { X = 4.6, Y = 8.3, },
                new PointModel { X = 5.2, Y = 8.6, },
                new PointModel { X = 5.4, Y = 8.4, },
                new PointModel { X = 5.5, Y = 8, },
                new PointModel { X = 5.2, Y = 8.3, },

            };
            painter.DrawPolygon(DarkRoseLeaf, mainPen, DarkLeafPoints1);

            var Shadows1 = new[]
            {
                new PointModel { X = 4.5, Y = 8, },
                new PointModel { X = 4.9, Y = 7.9, },
                new PointModel { X = 5.5, Y = 8, },
                new PointModel { X = 5.2, Y = 8.3, },
            };
            painter.DrawPolygon(ShadowRoseLeaf, mainPen, Shadows1);

            var LeafPoints19 = new[]
            {
                new PointModel { X = 4.9, Y = 7.9, },
                new PointModel { X = 4.9, Y = 7.5, },
                new PointModel { X = 5.6, Y = 7.5, },
                new PointModel { X = 5.5, Y = 8, },
            };
            painter.DrawPolygon(RoseLeaf, mainPen, LeafPoints19);

            var LeafPoints17 = new[]
            {
                new PointModel { X = 4.5, Y = 8, },
                new PointModel { X = 4.9, Y = 7.9, },
                new PointModel { X = 4.9, Y = 7.5, },
                new PointModel { X = 4.4, Y = 7.4, },
            };
            painter.DrawPolygon(RoseLeaf, mainPen, LeafPoints17);

            var LeafPoints18 = new[]
            {
                new PointModel { X = 5.5, Y = 8, },
                new PointModel { X = 5.25, Y = 7.2, },
                new PointModel { X = 4.8, Y = 6.9, },
                new PointModel { X = 5.25, Y = 6.8, },
                new PointModel { X = 5.55, Y = 7.5, },
            };
            painter.DrawPolygon(DarkRoseLeaf, mainPen, LeafPoints18);

            var Shadows2 = new[]
            {
                new PointModel { X = 3.2, Y = 7.75, },
                new PointModel { X = 3.8, Y = 8.35, },
                new PointModel { X = 3.8, Y = 7.6, },
            };
            painter.DrawPolygon(ShadowRoseLeaf, mainPen, Shadows2);

            var LeafPoints21 = new[]
            {
                new PointModel { X = 3.2, Y = 7.75, },
                new PointModel { X = 3.2, Y = 7.25, },
                new PointModel { X = 4.8, Y = 6.9, },
                new PointModel { X = 5.25, Y = 7.2, },
                new PointModel { X = 5.35, Y = 7.55, },
                new PointModel { X = 4.4, Y = 7.4, },
            };
            painter.DrawPolygon(RoseLeaf, mainPen, LeafPoints21);

            var LeafPoints22 = new[]
            {
                new PointModel { X = 3.2, Y = 7.25, },
                new PointModel { X = 3.9, Y = 5.9, },
                new PointModel { X = 5.2, Y = 5.2, },
                new PointModel { X = 5, Y = 6.4, },
                new PointModel { X = 5.25, Y = 6.8, },
            };
            painter.DrawPolygon(RoseLeaf, mainPen, LeafPoints22);

            var LeafPoints23 = new[]
            {
                new PointModel { X = 5, Y = 6.4, },
                new PointModel { X = 5.2, Y = 5.2, },
                new PointModel { X = 6, Y = 6.2, },
                new PointModel { X = 5.6, Y = 7.6, },
            };
            painter.DrawPolygon(RoseLeaf, mainPen, LeafPoints23);
        }
    }
}
