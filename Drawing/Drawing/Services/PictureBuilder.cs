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
    
    var penMain = new Pen(Color.Black, 2);
    var penWindow = new Pen(Color.DarkBlue, 1);
    var penDecor = new Pen(Color.DarkGreen, 1);

    
    var houseBrush = Brushes.LightSalmon;
    var roofBrush = Brushes.SlateGray;
    var windowBrush = Brushes.LightCyan;
    var doorBrush = Brushes.Sienna;

    
    painter.DrawRectangle(houseBrush, penMain, new RectangleModel { X = 5, Y = 3, Width = 6, Height = 5 });

    
    painter.DrawRectangle(roofBrush, penMain, new RectangleModel { X = 4.5, Y = 8, Width = 7, Height = 1 });

    
    painter.DrawRectangle(windowBrush, penWindow, new RectangleModel { X = 5.5, Y = 4, Width = 2, Height = 2 });
    painter.DrawRectangle(windowBrush, penWindow, new RectangleModel { X = 8.5, Y = 4, Width = 2, Height = 2 });

    
    painter.DrawRectangle(doorBrush, penMain, new RectangleModel { X = 7, Y = 3, Width = 2, Height = 3 });
    painter.DrawRectangle(windowBrush, null, new RectangleModel { X = 7.5, Y = 4.5, Width = 1, Height = 1 });

    
    painter.DrawLine(penDecor, new PointModel { X = 5, Y = 3 }, new PointModel { X = 5, Y = 8 }); 
    painter.DrawLine(penDecor, new PointModel { X = 11, Y = 3 }, new PointModel { X = 11, Y = 8 }); 

    
    painter.DrawEllipse(Brushes.ForestGreen, null, new RectangleModel { X = 12, Y = 5, Width = 3, Height = 4 });
    painter.DrawRectangle(Brushes.SaddleBrown, null, new RectangleModel { X = 13, Y = 3, Width = 1, Height = 2 });
}
    }
}
