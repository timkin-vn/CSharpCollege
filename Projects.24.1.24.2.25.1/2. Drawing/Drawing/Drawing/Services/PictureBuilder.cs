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
           
            Pen pen = new Pen(Color.Black, 2);

            
            RectangleModel body = new RectangleModel { X = 0, Y = 3, Width = 10, Height = 5 };
            painter.DrawRectangle(Brushes.Blue, pen, body);

            
            RectangleModel cabin = new RectangleModel { X = 10, Y = 3, Width = 4, Height = 4 };
            painter.DrawRectangle(Brushes.Red, pen, cabin);

            
            RectangleModel window = new RectangleModel { X = 11, Y = 5, Width = 2, Height = 2 };
            painter.DrawRectangle(Brushes.White, pen, window);

            
            RectangleModel wheel1 = new RectangleModel { X = 1, Y = 1, Width = 2, Height = 2 };
            RectangleModel wheel2 = new RectangleModel { X = 7, Y = 1, Width = 2, Height = 2 };
            RectangleModel wheel3 = new RectangleModel { X = 11, Y = 1, Width = 2, Height = 2 };

            painter.DrawEllipse(Brushes.Black, pen, wheel1);
            painter.DrawEllipse(Brushes.Black, pen, wheel2);
            painter.DrawEllipse(Brushes.Black, pen, wheel3);

            
            RectangleModel light = new RectangleModel { X = 13.5, Y = 3.5, Width = 0.5, Height = 1 };
            painter.DrawEllipse(Brushes.Yellow, pen, light);

            // Важно
            pen.Dispose();
        }
    }
}
