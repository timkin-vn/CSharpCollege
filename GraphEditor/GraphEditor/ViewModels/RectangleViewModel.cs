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
    internal class RectangleViewModel
    {
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
        public Color GradientColor { get; set; } = Color.White;
        public bool UseGradient { get; set; } = false;
        public Rectangle Rectangle => new Rectangle
        {
            X = Left < Right ? Left : Right,
            Y = Top < Bottom ? Top : Bottom,
            Width = Width > 0 ? Width : -Width,
            Height = Height > 0 ? Height : -Height,
        };

        public IEnumerable<MarkerViewModel> Markers => new[]
        {
            new MarkerViewModel
            {
                Rectangle = new Rectangle
                {
                    X = Left - MarkerViewModel.MarkerHalfSize,
                    Y = Top - MarkerViewModel.MarkerHalfSize,
                    Width = MarkerViewModel.MarkerHalfSize * 2,
                    Height = MarkerViewModel.MarkerHalfSize * 2,
                },
                IsActive = true,
                EditMode = EditMode.ResizeTL,
                Cursor = Cursors.SizeNWSE,
            },
            new MarkerViewModel
            {
                Rectangle = new Rectangle
                {
                    X = (Left + Right) / 2 - MarkerViewModel.MarkerHalfSize,
                    Y = Top - MarkerViewModel.MarkerHalfSize,
                    Width = MarkerViewModel.MarkerHalfSize * 2,
                    Height = MarkerViewModel.MarkerHalfSize * 2,
                },
                IsActive = true,
                EditMode = EditMode.ResizeT,
                Cursor = Cursors.SizeNS,
            },
            new MarkerViewModel
            {
                Rectangle = new Rectangle
                {
                    X = Right - MarkerViewModel.MarkerHalfSize,
                    Y = Top - MarkerViewModel.MarkerHalfSize,
                    Width = MarkerViewModel.MarkerHalfSize * 2,
                    Height = MarkerViewModel.MarkerHalfSize * 2,
                },
                IsActive = true,
                EditMode = EditMode.ResizeTR,
                Cursor = Cursors.SizeNESW,
            },
            new MarkerViewModel
            {
                Rectangle = new Rectangle
                {
                    X = Right - MarkerViewModel.MarkerHalfSize,
                    Y = (Top + Bottom) /2 - MarkerViewModel.MarkerHalfSize,
                    Width = MarkerViewModel.MarkerHalfSize * 2,
                    Height = MarkerViewModel.MarkerHalfSize * 2,
                },
                IsActive = true,
                EditMode = EditMode.ResizeR,
                Cursor = Cursors.SizeWE,
            },
            new MarkerViewModel
            {
                Rectangle = new Rectangle
                {
                    X = Right - MarkerViewModel.MarkerHalfSize,
                    Y = Bottom - MarkerViewModel.MarkerHalfSize,
                    Width = MarkerViewModel.MarkerHalfSize * 2,
                    Height = MarkerViewModel.MarkerHalfSize * 2,
                },
                IsActive = true,
                EditMode = EditMode.ResizeBR,
                Cursor = Cursors.SizeNWSE,
            },
            new MarkerViewModel
            {
                Rectangle = new Rectangle
                {
                    X = (Left + Right) / 2 - MarkerViewModel.MarkerHalfSize,
                    Y = Bottom - MarkerViewModel.MarkerHalfSize,
                    Width = MarkerViewModel.MarkerHalfSize * 2,
                    Height = MarkerViewModel.MarkerHalfSize * 2,
                },
                IsActive = true,
                EditMode = EditMode.ResizeB,
                Cursor = Cursors.SizeNS,
            },
            new MarkerViewModel
            {
                Rectangle = new Rectangle
                {
                    X = Left - MarkerViewModel.MarkerHalfSize,
                    Y = Bottom - MarkerViewModel.MarkerHalfSize,
                    Width = MarkerViewModel.MarkerHalfSize * 2,
                    Height = MarkerViewModel.MarkerHalfSize * 2,
                },
                IsActive = true,
                EditMode = EditMode.ResizeBL,
                Cursor = Cursors.SizeNESW,
            },
            new MarkerViewModel
            {
                Rectangle = new Rectangle
                {
                    X = Left - MarkerViewModel.MarkerHalfSize,
                    Y = (Top + Bottom) /2 - MarkerViewModel.MarkerHalfSize,
                    Width = MarkerViewModel.MarkerHalfSize * 2,
                    Height = MarkerViewModel.MarkerHalfSize * 2,
                },
                IsActive = true,
                EditMode = EditMode.ResizeL,
                Cursor = Cursors.SizeWE,
            },
        };

        public static RectangleViewModel FromBusiness(RectangleModel model)
        {
            return new RectangleViewModel
            {
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
                BorderPen = new Pen(model.BorderColor, 3),
                GradientColor = model.GradientColor,
                UseGradient = model.UseGradient
            };
        }

        public RectangleModel ToBusiness()
        {
            return new RectangleModel
            {
                Dx = Dx,
                Dy = Dy,
                Left = Left,
                Top = Top,
                Width = Width,
                Height = Height,
                EditMode = EditMode,
                FillColor = FillColor,
                BorderColor = BorderColor,
            };
        }
    }
}
