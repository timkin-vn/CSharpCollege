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
            var mainPen = new Pen(Color.Black, 3);

            var wheelBrush = Brushes.Black;

            var rect = new RectangleModel { X = 0, Y = 1, Width = 2, Height = 2, };
            painter.DrawEllipse(wheelBrush, mainPen, rect);

            rect.X = 2.5;
            painter.DrawEllipse(wheelBrush, mainPen, rect);

            rect.X = 8;
            painter.DrawEllipse(wheelBrush, mainPen, rect);

            var diskBrush = Brushes.Silver;

            rect = new RectangleModel { X = 0.3, Y = 1.3, Width = 1.4, Height = 1.4, };
            painter.DrawEllipse(diskBrush, mainPen, rect);

            rect.X = 2.8;
            painter.DrawEllipse(diskBrush, mainPen, rect);

            rect.X = 8.3;
            painter.DrawEllipse(diskBrush, mainPen, rect);

            var darkBeamBrush = Brushes.DarkGray;
            rect = new RectangleModel { X = 0.5, Y = 3.25, Width = 3, Height = 0.5, };
            painter.DrawRectangle(darkBeamBrush, mainPen, rect);

            var tankBrush = Brushes.DarkGray;
            rect = new RectangleModel { X = 5, Y = 1.8, Width = 2.5, Height = 1.5, };
            painter.DrawRectangle(tankBrush, mainPen, rect);

            var capBrush = Brushes.DarkGray;
            rect = new RectangleModel { X = 6.8, Y = 2, Width = 0.4, Height = 0.4, };
            painter.DrawEllipse(capBrush, mainPen, rect);

            var beamBrush = Brushes.LightGray;
            rect = new RectangleModel { X = -0.5, Y = 2.8, Width = 11, Height = 0.5, };
            painter.DrawRectangle(beamBrush, mainPen, rect);

            var redBrush = Brushes.Red;
            rect = new RectangleModel { X = 5.5, Y = 3.3, Width = 5, Height = 6, };
            painter.DrawRectangle(redBrush, mainPen, rect);

            var windowBrush = Brushes.LightSkyBlue;
            rect = new RectangleModel { X = 7.5, Y = 5, Width = 3, Height = 4, };
            painter.DrawRectangle(windowBrush, mainPen, rect);

            var pipeTopBrush = Brushes.Black;
            rect = new RectangleModel { X = 5.1, Y = 9.8, Width = .3, Height = 0.5, };
            painter.DrawRectangle(pipeTopBrush, mainPen, rect);

            var pipeBrush = Brushes.DarkGray;
            rect = new RectangleModel { X = 5, Y = 3.3, Width = .5, Height = 6.5, };
            painter.DrawRectangle(pipeBrush, mainPen, rect);


        }
    }
}