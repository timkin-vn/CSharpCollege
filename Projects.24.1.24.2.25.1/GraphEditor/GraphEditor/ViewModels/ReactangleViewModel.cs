using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GraphEditor.Business.Models;

namespace GraphEditor.ViewModels
{
    public class ReactangleViewModel
    {
        public int Dx { get; set; }
        public int Dy { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Bottom => Top + Height;
        public int Right => Left + Width;
        public EditMode EditMode { get; set; }
        public int BorderWidth { get; set; } = 3;

        public Color FillColor { get; set; } = Color.Red;

        public Color BorderColor { get; set; } = Color.Yellow;
        public FigureType Figure { get; set; } = FigureType.Rectangle;

        public byte Opacity { get; set; } = 255;
        public static ReactangleViewModel FromBusiness(RectangleModel rect)
        {
            return new ReactangleViewModel
            {
                Dx = rect.Dx,
                Dy = rect.Dy,
                Left = rect.Left,
                Top = rect.Top,
                Width = rect.Width,
                Height = rect.Height,
                EditMode = rect.EditMode,
                FillColor = rect.FillColor,
                BorderColor = rect.BorderColor,
                BorderWidth = rect.BorderWidth,
                Figure = rect.Figure,
                Opacity = rect.Opacity
            };
        }
        public Rectangle Rectangle => new Rectangle
        {
            X = Width < 0 ? Left + Width : Left,
            Y = Height < 0 ? Top + Height : Top,
            Width = Width < 0 ? -Width : Width,
            Height = Height < 0 ? -Height : Height
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
                IsActive = false,
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
                IsActive = false,
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
                IsActive = false,
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
                IsActive = false,
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
                IsActive = false,
                EditMode = EditMode.ResizeL,
                Cursor = Cursors.SizeWE,
            },
        };


    }
}
