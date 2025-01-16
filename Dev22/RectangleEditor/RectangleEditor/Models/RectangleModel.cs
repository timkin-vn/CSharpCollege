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
        private Point _diff;

        public int Left { get; set; }

        public int Top { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int Bottom
        {
            get => Top + Height;
            set => Height = value - Top;
        }

        public int Right
        {
            get => Left + Width;
            set => Width = value - Left;
        }

        public EditMode EditMode { get; set; }

        public IEnumerable<Marker> Markers => new[]
        {
            new Marker
            {
                Rectangle = new Rectangle{ X = Left - 5, Y = Top - 5, Width = 10, Height = 10, },
                IsActive = false,
                EditMode = EditMode.ResizeTL,
                Cursor = Cursors.SizeNWSE,
            },
            new Marker
            {
                Rectangle = new Rectangle{ X = (Left + Right) / 2 - 5, Y = Top - 5, Width = 10, Height = 10, },
                IsActive = false,
                EditMode = EditMode.ResizeT,
                Cursor = Cursors.SizeNS,
            },
            new Marker
            {
                Rectangle = new Rectangle{ X = Right - 5, Y = Top - 5, Width = 10, Height = 10, },
                IsActive = false,
                EditMode = EditMode.ResizeTR,
                Cursor = Cursors.SizeNESW,
            },
            new Marker
            {
                Rectangle = new Rectangle{ X = Right - 5, Y = (Top + Bottom) / 2 - 5, Width = 10, Height = 10, },
                IsActive = false,
                EditMode = EditMode.ResizeR,
                Cursor = Cursors.SizeWE,
            },
            new Marker
            {
                Rectangle = new Rectangle{ X = Right - 5, Y = Bottom - 5, Width = 10, Height = 10, },
                IsActive = true,
                EditMode = EditMode.ResizeBR,
                Cursor = Cursors.SizeNWSE,
            },
            new Marker
            {
                Rectangle = new Rectangle{ X = (Left + Right) / 2 - 5, Y = Bottom - 5, Width = 10, Height = 10, },
                IsActive = false,
                EditMode = EditMode.ResizeB,
                Cursor = Cursors.SizeNS,
            },
            new Marker
            {
                Rectangle = new Rectangle{ X = Left - 5, Y = Bottom - 5, Width = 10, Height = 10, },
                IsActive = false,
                EditMode = EditMode.ResizeBL,
                Cursor = Cursors.SizeNESW,
            },
            new Marker
            {
                Rectangle = new Rectangle{ X = Left - 5, Y = (Top + Bottom) / 2 - 5, Width = 10, Height = 10, },
                IsActive = false,
                EditMode = EditMode.ResizeL,
                Cursor = Cursors.SizeWE,
            },
        };

        public Rectangle GetRectangle()
        {
            return new Rectangle
            {
                X = Left < Right ? Left : Right,
                Y = Top < Bottom ? Top : Bottom,
                Width = Width > 0 ? Width : -Width,
                Height = Height > 0 ? Height : -Height,
            };
        }

        public void Normalize()
        {
            if (Width < 0)
            {
                Left += Width;
                Width = -Width;
            }

            if (Height < 0)
            {
                Top += Height;
                Height = -Height;
            }
        }

        public bool IsInside(Point location)
        {
            return location.X >= Left && location.X <= Right &&
                location.Y >= Top && location.Y <= Bottom;
        }

        public void SetMovingMode(Point location)
        {
            EditMode = EditMode.Moving;
            _diff.X = location.X - Left;
            _diff.Y = location.Y - Top;
        }

        public void SetResizeMode(EditMode mode)
        {
            EditMode = mode;
        }

        public void SetCreateMode(Point location)
        {
            EditMode = EditMode.Creating;
            Left = location.X;
            Top = location.Y;
            Width = 0;
            Height = 0;
        }

        public void SetNewLocation(Point location)
        {
            switch (EditMode)
            {
                case EditMode.Moving:
                    Left = location.X - _diff.X;
                    Top = location.Y - _diff.Y;
                    break;

                case EditMode.Creating:
                case EditMode.ResizeBR:
                    Right = location.X;
                    Bottom = location.Y;
                    break;
            }
        }

        public void ResetMode()
        {
            EditMode = EditMode.None;
        }

        public Marker GetActiveMarker(Point location)
        {
            foreach (var marker in Markers)
            {
                var rect = marker.Rectangle;
                if (location.X >= rect.Left && location.X <= rect.Right &&
                    location.Y >= rect.Top && location.Y <= rect.Bottom)
                {
                    return marker;
                }
            }

            return null;
        }
    }
}
