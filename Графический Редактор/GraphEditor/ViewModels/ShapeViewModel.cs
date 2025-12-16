using GraphEditor.Business.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphEditor.ViewModels
{
    internal class ShapeViewModel
    {
        public ShapeType ShapeType { get; set; }
        public bool TrianglePointsUp { get; set; } = true;
        public int Dx { get; set; }
        public int Dy { get; set; }
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
        public Color FillColor { get; set; } = Color.Yellow;
        public Brush FillBrush { get; set; }
        public Color BorderColor { get; set; } = Color.Blue;
        public Pen BorderPen { get; set; }
        public int BorderThickness { get; set; } = 3;

        public Rectangle Rectangle => new Rectangle
        {
            X = Math.Min(Left, Right),
            Y = Math.Min(Top, Bottom),
            Width = Math.Abs(Width),
            Height = Math.Abs(Height),
        };

        public IEnumerable<MarkerViewModel> Markers
        {
            get
            {
                if (ShapeType != ShapeType.Rectangle)
                    return Enumerable.Empty<MarkerViewModel>();

                return CreateRectangleMarkers();
            }
        }

        private IEnumerable<MarkerViewModel> CreateRectangleMarkers()
        {
            var markers = new List<MarkerViewModel>();

            markers.Add(new MarkerViewModel
            {
                Rectangle = new Rectangle(
                    Left - MarkerViewModel.MarkerHalfSize,
                    Top - MarkerViewModel.MarkerHalfSize,
                    MarkerViewModel.MarkerHalfSize * 2,
                    MarkerViewModel.MarkerHalfSize * 2),
                IsActive = true,
                EditMode = EditMode.ResizeTL,
                Cursor = Cursors.SizeNWSE,
            });

            markers.Add(new MarkerViewModel
            {
                Rectangle = new Rectangle(
                    (Left + Right) / 2 - MarkerViewModel.MarkerHalfSize,
                    Top - MarkerViewModel.MarkerHalfSize,
                    MarkerViewModel.MarkerHalfSize * 2,
                    MarkerViewModel.MarkerHalfSize * 2),
                IsActive = true,
                EditMode = EditMode.ResizeT,
                Cursor = Cursors.SizeNS,
            });

            markers.Add(new MarkerViewModel
            {
                Rectangle = new Rectangle(
                    Right - MarkerViewModel.MarkerHalfSize,
                    Top - MarkerViewModel.MarkerHalfSize,
                    MarkerViewModel.MarkerHalfSize * 2,
                    MarkerViewModel.MarkerHalfSize * 2),
                IsActive = true,
                EditMode = EditMode.ResizeTR,
                Cursor = Cursors.SizeNESW,
            });

            markers.Add(new MarkerViewModel
            {
                Rectangle = new Rectangle(
                    Right - MarkerViewModel.MarkerHalfSize,
                    (Top + Bottom) / 2 - MarkerViewModel.MarkerHalfSize,
                    MarkerViewModel.MarkerHalfSize * 2,
                    MarkerViewModel.MarkerHalfSize * 2),
                IsActive = true,
                EditMode = EditMode.ResizeR,
                Cursor = Cursors.SizeWE,
            });

            markers.Add(new MarkerViewModel
            {
                Rectangle = new Rectangle(
                    Right - MarkerViewModel.MarkerHalfSize,
                    Bottom - MarkerViewModel.MarkerHalfSize,
                    MarkerViewModel.MarkerHalfSize * 2,
                    MarkerViewModel.MarkerHalfSize * 2),
                IsActive = true,
                EditMode = EditMode.ResizeBR,
                Cursor = Cursors.SizeNWSE,
            });

            markers.Add(new MarkerViewModel
            {
                Rectangle = new Rectangle(
                    (Left + Right) / 2 - MarkerViewModel.MarkerHalfSize,
                    Bottom - MarkerViewModel.MarkerHalfSize,
                    MarkerViewModel.MarkerHalfSize * 2,
                    MarkerViewModel.MarkerHalfSize * 2),
                IsActive = true,
                EditMode = EditMode.ResizeB,
                Cursor = Cursors.SizeNS,
            });

            markers.Add(new MarkerViewModel
            {
                Rectangle = new Rectangle(
                    Left - MarkerViewModel.MarkerHalfSize,
                    Bottom - MarkerViewModel.MarkerHalfSize,
                    MarkerViewModel.MarkerHalfSize * 2,
                    MarkerViewModel.MarkerHalfSize * 2),
                IsActive = true,
                EditMode = EditMode.ResizeBL,
                Cursor = Cursors.SizeNESW,
            });

            markers.Add(new MarkerViewModel
            {
                Rectangle = new Rectangle(
                    Left - MarkerViewModel.MarkerHalfSize,
                    (Top + Bottom) / 2 - MarkerViewModel.MarkerHalfSize,
                    MarkerViewModel.MarkerHalfSize * 2,
                    MarkerViewModel.MarkerHalfSize * 2),
                IsActive = true,
                EditMode = EditMode.ResizeL,
                Cursor = Cursors.SizeWE,
            });

            return markers;
        }

        public static ShapeViewModel FromBusiness(ShapeModel model)
        {
            return new ShapeViewModel
            {
                ShapeType = model.ShapeType,
                TrianglePointsUp = model.TrianglePointsUp,
                Dx = model.Dx,
                Dy = model.Dy,
                Left = model.Left,
                Top = model.Top,
                Width = model.Width,
                Height = model.Height,
                EditMode = model.EditMode,
                FillColor = model.FillColor,
                FillBrush = new SolidBrush(model.FillColor),
                BorderColor = model.BorderColor,
                BorderThickness = model.BorderThickness,
                BorderPen = new Pen(model.BorderColor, model.BorderThickness),
            };
        }

        public ShapeModel ToBusiness()
        {
            return new ShapeModel
            {
                ShapeType = ShapeType,
                TrianglePointsUp = TrianglePointsUp,
                Dx = Dx,
                Dy = Dy,
                Left = Left,
                Top = Top,
                Width = Width,
                Height = Height,
                EditMode = EditMode,
                FillColor = FillColor,
                BorderColor = BorderColor,
                BorderThickness = BorderThickness,
            };
        }
    }
}