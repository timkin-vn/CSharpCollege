using GraphicsApp.Models;
using GraphicsApp.Services;
using System.Drawing;

internal class Painter
{
    public MathRectangle LimitRectangle = new MathRectangle
    {
        Left = -1,
        Right = 1,
        Bottom = -0.1,
        Top = 1.1,
    };

    public void Paint(PaintManager paintManager)
    {
        double width = LimitRectangle.Right - LimitRectangle.Left;
        double height = LimitRectangle.Top - LimitRectangle.Bottom;

        var mainColor = Color.FromArgb(194, 157, 135);
        var mainPen = new Pen(Color.Black, 3);
        var mainBrush = new SolidBrush(mainColor);
        paintManager.DrawRectangle(mainPen, mainBrush,
            new MathRectangle
            {
                Left = -0.3 * width,
                Right = 0.3 * width,
                Bottom = 0,
                Top = 0.5 * height
            });

        var roofColor = Color.FromArgb(139, 69, 19);
        var roofPen = new Pen(Color.Black, 3);
        var roofBrush = new SolidBrush(roofColor);
        paintManager.DrawPolygon(roofPen, roofBrush,
            new[]
            {
                new MathPoint { X = -0.37 * width, Y = 0.5 * height },
                new MathPoint { X = 0, Y = 0.85 * height },
                new MathPoint { X = 0.37 * width, Y = 0.5 * height },
            });

        var windowColor = Color.FromArgb(135, 206, 235);
        var windowPen = new Pen(Color.Black, 3);
        var windowBrush = new SolidBrush(windowColor);

        paintManager.DrawRectangle(windowPen, windowBrush,
            new MathRectangle
            {
                Left = -0.23 * width,
                Right = -0.1 * width,
                Bottom = 0.15 * height,
                Top = 0.35 * height
            });

        paintManager.DrawRectangle(windowPen, windowBrush,
            new MathRectangle
            {
                Left = 0.1 * width,
                Right = 0.23 * width,
                Bottom = 0.15 * height,
                Top = 0.35 * height
            });

        var doorColor = Color.FromArgb(101, 67, 33);
        var doorPen = new Pen(Color.Black, 3);
        var doorBrush = new SolidBrush(doorColor);
        paintManager.DrawRectangle(doorPen, doorBrush,
            new MathRectangle
            {
                Left = -0.07 * width,
                Right = 0.07 * width,
                Bottom = 0,
                Top = 0.3 * height
            });

        var chimneyColor = Color.FromArgb(169, 169, 169);
        var chimneyPen = new Pen(Color.Black, 3);
        var chimneyBrush = new SolidBrush(chimneyColor);
        paintManager.DrawRectangle(chimneyPen, chimneyBrush,
            new MathRectangle
            {
                Left = 0.17 * width,
                Right = 0.23 * width,
                Bottom = 0.6 * height,
                Top = 0.7 * height
            });

        var smokeColor = Color.FromArgb(211, 211, 211);
        var smokePen = new Pen(smokeColor, 3);
        var smokeBrush = new SolidBrush(smokeColor);
        paintManager.DrawEllipse(smokePen, smokeBrush,
            new MathRectangle
            {
                Left = 0.17 * width,
                Right = 0.23 * width,
                Bottom = 0.75 * height,
                Top = 0.85 * height
            });
        paintManager.DrawEllipse(smokePen, smokeBrush,
            new MathRectangle
            {
                Left = 0.2 * width,
                Right = 0.27 * width,
                Bottom = 0.85 * height,
                Top = 0.95 * height
            });

        var groundColor = Color.FromArgb(34, 139, 34);
        var groundPen = new Pen(groundColor, 3);
        var groundBrush = new SolidBrush(groundColor);
        paintManager.DrawRectangle(groundPen, groundBrush,
            new MathRectangle
            {
                Left = -1 * width,
                Right = width,
                Bottom = -0.1 * height,
                Top = 0
            });
    }
}
