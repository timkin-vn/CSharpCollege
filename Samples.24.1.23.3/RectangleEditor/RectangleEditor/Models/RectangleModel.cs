using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RectangleEditor.Models
{
    internal class RectangleModel
    {
        private int _dx;

        private int _dy;

        public int Left { get; set; }

        public int Top { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int Right => Left + Width;

        public int Bottom => Top + Height;

        public EditMode EditMode { get; set; }

        public IEnumerable<Marker> Markers => new[]
        {
            new Marker
            {
                Rectangle = new Rectangle
                {
                    X = Left - Marker.MarkerHalfSize,
                    Y = Top - Marker.MarkerHalfSize,
                    Width = Marker.MarkerHalfSize * 2,
                    Height = Marker.MarkerHalfSize * 2,
                },
                IsActive = false,
                EditMode = EditMode.ResizeTL,
                Cursor = Cursors.SizeNWSE,
            },
            new Marker
            {
                Rectangle = new Rectangle
                {
                    X = (Left + Right) / 2 - Marker.MarkerHalfSize,
                    Y = Top - Marker.MarkerHalfSize,
                    Width = Marker.MarkerHalfSize * 2,
                    Height = Marker.MarkerHalfSize * 2,
                },
                IsActive = false,
                EditMode = EditMode.ResizeT,
                Cursor = Cursors.SizeNS,
            },
                new Marker
            {
                Rectangle = new Rectangle
                {
                    X = Right - Marker.MarkerHalfSize,
                    Y = Top - Marker.MarkerHalfSize,
                    Width = Marker.MarkerHalfSize * 2,
                    Height = Marker.MarkerHalfSize * 2,
                },
                IsActive = false,
                EditMode = EditMode.ResizeTR,
                Cursor = Cursors.SizeNESW,
            },
            new Marker
            {
                Rectangle = new Rectangle
                {
                    X = Right - Marker.MarkerHalfSize,
                    Y = (Top + Bottom) /2 - Marker.MarkerHalfSize,
                    Width = Marker.MarkerHalfSize * 2,
                    Height = Marker.MarkerHalfSize * 2,
                },
                IsActive = true,
                EditMode = EditMode.ResizeR,
                Cursor = Cursors.SizeWE,
            },
            new Marker
            {
                Rectangle = new Rectangle
                {
                    X = Right - Marker.MarkerHalfSize,
                    Y = Bottom - Marker.MarkerHalfSize,
                    Width = Marker.MarkerHalfSize * 2,
                    Height = Marker.MarkerHalfSize * 2,
                },
                IsActive = true,
                EditMode = EditMode.ResizeBR,
                Cursor = Cursors.SizeNWSE,
            },
            new Marker
            {
                Rectangle = new Rectangle
                {
                    X = (Left + Right) / 2 - Marker.MarkerHalfSize,
                    Y = Bottom - Marker.MarkerHalfSize,
                    Width = Marker.MarkerHalfSize * 2,
                    Height = Marker.MarkerHalfSize * 2,
                },
                IsActive = false,
                EditMode = EditMode.ResizeB,
                Cursor = Cursors.SizeNS,
            },
            new Marker
            {
                Rectangle = new Rectangle
                {
                    X = Left - Marker.MarkerHalfSize,
                    Y = Bottom - Marker.MarkerHalfSize,
                    Width = Marker.MarkerHalfSize * 2,
                    Height = Marker.MarkerHalfSize * 2,
                },
                IsActive = false,
                EditMode = EditMode.ResizeBL,
                Cursor = Cursors.SizeNESW,
            },
            new Marker
            {
                Rectangle = new Rectangle
                {
                    X = Left - Marker.MarkerHalfSize,
                    Y = (Top + Bottom) /2 - Marker.MarkerHalfSize,
                    Width = Marker.MarkerHalfSize * 2,
                    Height = Marker.MarkerHalfSize * 2,
                },
                IsActive = false,
                EditMode = EditMode.ResizeL,
                Cursor = Cursors.SizeWE,
            },
        };

        public Rectangle GetRectangle()
        {
            return new Rectangle
            {
                X = Width < 0 ? Left + Width : Left,
                Y = Height < 0 ? Top + Height : Top,
                Width = Width < 0 ? -Width : Width,
                Height = Height < 0 ? -Height : Height,
            };
        }

        public void SetCreatingMode(Point loc)
        {
            EditMode = EditMode.Creating;

            Left = loc.X;
            Top = loc.Y;
            Width = 0;
            Height = 0;
        }

        public void SetMovingMode(Point loc)
        {
            EditMode = EditMode.Moving;

            _dx = loc.X - Left;
            _dy = loc.Y - Top;
        }

        public void SetResizeMode(EditMode mode)
        {
            EditMode = mode;
        }

        public void ResetMode()
        {
            EditMode = EditMode.None;
        }

        public void UpdateMovingPoint(Point loc)
        {
            switch (EditMode)
            {
                case EditMode.Creating:
                case EditMode.ResizeBR:
                    Width = loc.X - Left;
                    Height = loc.Y - Top;
                    break;

                case EditMode.ResizeR:
                    Width = loc.X - Left;
                    break;

                case EditMode.Moving:
                    Left = loc.X - _dx;
                    Top = loc.Y - _dy;
                    break;
            }
        }

        public bool IsInside(Point loc)
        {
            return loc.X >= Left && loc.X <= Right &&
                loc.Y >= Top && loc.Y <= Bottom;
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

        public Marker GetMarkerUnderCursor(Point loc)
        {
            foreach (var marker in Markers)
            {
                var rect = marker.Rectangle;
                if (loc.X >= rect.Left && loc.Y<= rect.Right &&
                    loc.Y >= rect.Top && loc.Y <= rect.Bottom)
                {
                    return marker;
                }
            }

            return null;
        }
    }
}
