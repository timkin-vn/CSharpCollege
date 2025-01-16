using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RectangleEditor.Models
{
    internal class RectangleModel
    {
        private int _dx;

        private int _dy;

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public RectangleMode Mode { get; private set; }

        public Point Point1
        {
            get => new Point { X = X, Y = Y };
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public Point Point2
        {
            get => new Point { X = X + Width, Y = Y + Height, };
            set
            {
                Width = value.X - X;
                Height = value.Y - Y;
            }
        }

        public Rectangle Rectangle => new Rectangle
        {
            X = Width > 0 ? X : X + Width,
            Y = Height > 0 ? Y : Y + Height,
            Width = Width > 0 ? Width : -Width,
            Height = Height > 0 ? Height : -Height,
        };

        public void SetCreateMode(Point location)
        {
            Point1 = location;
            Point2 = location;
            Mode = RectangleMode.Creating;
        }

        public void SetMoveMode(Point location)
        {
            _dx = location.X - X;
            _dy = location.Y - Y;
            Mode = RectangleMode.Moving;
        }

        public void SetResizeMode(RectangleMode mode)
        {
            Mode = mode;
        }

        public void ResetMode()
        {
            Mode = RectangleMode.None;
        }

        public bool IsInside(Point location) =>
            location.X >= X && location.X <= X + Width &&
            location.Y >= Y && location.Y <= Y + Height;

        public IEnumerable<Marker> Markers => new[]
        {
            new Marker
            {
                Rectangle = new Rectangle(X - 5, Y - 5, 10, 10),
                IsActive = false,
                Mode = RectangleMode.ResizeTL,
                Cursor = Cursors.SizeNWSE,
            },
            new Marker
            {
                Rectangle = new Rectangle(X + Width / 2 - 5, Y - 5, 10, 10),
                IsActive = false,
                Mode = RectangleMode.ResizeT,
                Cursor = Cursors.SizeNS,
            },
            new Marker
            {
                Rectangle = new Rectangle(X + Width - 5, Y - 5, 10, 10),
                IsActive = false,
                Mode = RectangleMode.ResizeTR,
                Cursor = Cursors.SizeNESW,
            },
            new Marker
            {
                Rectangle = new Rectangle(X + Width - 5, Y + Height / 2 - 5, 10, 10),
                IsActive = false,
                Mode = RectangleMode.ResizeR,
                Cursor = Cursors.SizeWE,
            },
            new Marker
            {
                Rectangle = new Rectangle(X + Width - 5, Y + Height - 5, 10, 10),
                IsActive = true,
                Mode = RectangleMode.ResizeBR,
                Cursor = Cursors.SizeNWSE,
            },
            new Marker
            {
                Rectangle = new Rectangle(X + Width / 2 - 5, Y + Height - 5, 10, 10),
                IsActive = false,
                Mode = RectangleMode.ResizeB,
                Cursor = Cursors.SizeNS,
            },
            new Marker
            {
                Rectangle = new Rectangle(X - 5, Y + Height - 5, 10, 10),
                IsActive = false,
                Mode = RectangleMode.ResizeBL,
                Cursor = Cursors.SizeNESW,
            },
            new Marker
            {
                Rectangle = new Rectangle(X - 5, Y + Height / 2 - 5, 10, 10),
                IsActive = false,
                Mode = RectangleMode.ResizeL,
                Cursor = Cursors.SizeWE,
            },
        };

        public void Move(Point location)
        {
            switch (Mode)
            {
                case RectangleMode.Creating:
                case RectangleMode.ResizeBR:
                    Point2 = location;
                    break;
                case RectangleMode.Moving:
                    X = location.X - _dx;
                    Y = location.Y - _dy;
                    break;
            }
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

        public Marker GetMarker(Point location)
        {
            return Markers.FirstOrDefault(m =>
                location.X >= m.Rectangle.Left && location.X <= m.Rectangle.Right &&
                location.Y >= m.Rectangle.Top && location.Y <= m.Rectangle.Bottom);
        }
    }
}
