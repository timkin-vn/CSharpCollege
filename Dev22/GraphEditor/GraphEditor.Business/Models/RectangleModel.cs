using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.Business.Models
{
    public class RectangleModel
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public PointModel Point1
        {
            get => new PointModel { X = X, Y = Y };
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public PointModel Point2
        {
            get => new PointModel { X = X + Width, Y = Y + Height, };
            set
            {
                Width = value.X - X;
                Height = value.Y - Y;
            }
        }

        public IEnumerable<Marker> Markers => new[]
        {
            new Marker
            {
                Rectangle = new RectangleModel(X - 5, Y - 5, 10, 10),
                IsActive = false,
                Mode = PictureMode.ResizeTL,
                Cursor = PictureCursor.SizeNWSE,
            },
            new Marker
            {
                Rectangle = new RectangleModel(X + Width / 2 - 5, Y - 5, 10, 10),
                IsActive = false,
                Mode = PictureMode.ResizeT,
                Cursor = PictureCursor.SizeNS,
            },
            new Marker
            {
                Rectangle = new RectangleModel(X + Width - 5, Y - 5, 10, 10),
                IsActive = false,
                Mode = PictureMode.ResizeTR,
                Cursor = PictureCursor.SizeNESW,
            },
            new Marker
            {
                Rectangle = new RectangleModel(X + Width - 5, Y + Height / 2 - 5, 10, 10),
                IsActive = false,
                Mode = PictureMode.ResizeR,
                Cursor = PictureCursor.SizeWE,
            },
            new Marker
            {
                Rectangle = new RectangleModel(X + Width - 5, Y + Height - 5, 10, 10),
                IsActive = true,
                Mode = PictureMode.ResizeBR,
                Cursor = PictureCursor.SizeNWSE,
            },
            new Marker
            {
                Rectangle = new RectangleModel(X + Width / 2 - 5, Y + Height - 5, 10, 10),
                IsActive = false,
                Mode = PictureMode.ResizeB,
                Cursor = PictureCursor.SizeNS,
            },
            new Marker
            {
                Rectangle = new RectangleModel(X - 5, Y + Height - 5, 10, 10),
                IsActive = false,
                Mode = PictureMode.ResizeBL,
                Cursor = PictureCursor.SizeNESW,
            },
            new Marker
            {
                Rectangle = new RectangleModel(X - 5, Y + Height / 2 - 5, 10, 10),
                IsActive = false,
                Mode = PictureMode.ResizeL,
                Cursor = PictureCursor.SizeWE,
            },
        };

        public Color FillColor { get; set; } = Color.Yellow;

        public Color DrawColor { get; set; } = Color.Blue;

        public RectangleModel() { }

        public RectangleModel(int left, int top, int width, int height)
        {
            X = left;
            Y = top;
            Width = width;
            Height = height;
        }

        public void Normalize()
        {
            if (Width < 0)
            {
                X += Width;
                Width = -Width;
            }

            if (Height < 0)
            {
                Y += Height;
                Height = -Height;
            }
        }

        public bool IsInside(PointModel location) =>
            location.X >= X && location.X <= X + Width &&
            location.Y >= Y && location.Y <= Y + Height;
    }
}
