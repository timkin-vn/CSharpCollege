using GraphEditor.Business.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GraphEditor.ViewModels
{
    internal class FigureViewModel
    {
        public int Dx { get; set; }
        public int Dy { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Right => Left + Width;
        public int Bottom => Top + Height;
        public EditMode EditMode { get; set; }
        public Color FillColor { get; set; } = Color.Yellow;
        public Color BorderColor { get; set; } = Color.Blue;
        public FigureType Type { get; set; }
        public int CornerRadius { get; set; } = 20;

        public Rectangle Rectangle => new Rectangle
        {
            X = Width < 0 ? Left + Width : Left,
            Y = Height < 0 ? Top + Height : Top,
            Width = Width < 0 ? -Width : Width,
            Height = Height < 0 ? -Height : Height,
        };

        public IEnumerable<MarkerViewModel> Markers => new[]
        {
            new MarkerViewModel { Rectangle = new Rectangle { X = Left - MarkerViewModel.MarkerHalfSize, Y = Top - MarkerViewModel.MarkerHalfSize, Width = MarkerViewModel.MarkerHalfSize * 2, Height = MarkerViewModel.MarkerHalfSize * 2 }, IsActive = false, EditMode = EditMode.ResizeTL, Cursor = Cursors.SizeNWSE },
            new MarkerViewModel { Rectangle = new Rectangle { X = (Left + Right) / 2 - MarkerViewModel.MarkerHalfSize, Y = Top - MarkerViewModel.MarkerHalfSize, Width = MarkerViewModel.MarkerHalfSize * 2, Height = MarkerViewModel.MarkerHalfSize * 2 }, IsActive = false, EditMode = EditMode.ResizeT, Cursor = Cursors.SizeNS },
            new MarkerViewModel { Rectangle = new Rectangle { X = Right - MarkerViewModel.MarkerHalfSize, Y = Top - MarkerViewModel.MarkerHalfSize, Width = MarkerViewModel.MarkerHalfSize * 2, Height = MarkerViewModel.MarkerHalfSize * 2 }, IsActive = false, EditMode = EditMode.ResizeTR, Cursor = Cursors.SizeNESW },
            new MarkerViewModel { Rectangle = new Rectangle { X = Right - MarkerViewModel.MarkerHalfSize, Y = (Top + Bottom) / 2 - MarkerViewModel.MarkerHalfSize, Width = MarkerViewModel.MarkerHalfSize * 2, Height = MarkerViewModel.MarkerHalfSize * 2 }, IsActive = true, EditMode = EditMode.ResizeR, Cursor = Cursors.SizeWE },
            new MarkerViewModel { Rectangle = new Rectangle { X = Right - MarkerViewModel.MarkerHalfSize, Y = Bottom - MarkerViewModel.MarkerHalfSize, Width = MarkerViewModel.MarkerHalfSize * 2, Height = MarkerViewModel.MarkerHalfSize * 2 }, IsActive = true, EditMode = EditMode.ResizeBR, Cursor = Cursors.SizeNWSE },
            new MarkerViewModel { Rectangle = new Rectangle { X = (Left + Right) / 2 - MarkerViewModel.MarkerHalfSize, Y = Bottom - MarkerViewModel.MarkerHalfSize, Width = MarkerViewModel.MarkerHalfSize * 2, Height = MarkerViewModel.MarkerHalfSize * 2 }, IsActive = false, EditMode = EditMode.ResizeB, Cursor = Cursors.SizeNS },
            new MarkerViewModel { Rectangle = new Rectangle { X = Left - MarkerViewModel.MarkerHalfSize, Y = Bottom - MarkerViewModel.MarkerHalfSize, Width = MarkerViewModel.MarkerHalfSize * 2, Height = MarkerViewModel.MarkerHalfSize * 2 }, IsActive = false, EditMode = EditMode.ResizeBL, Cursor = Cursors.SizeNESW },
            new MarkerViewModel { Rectangle = new Rectangle { X = Left - MarkerViewModel.MarkerHalfSize, Y = (Top + Bottom) / 2 - MarkerViewModel.MarkerHalfSize, Width = MarkerViewModel.MarkerHalfSize * 2, Height = MarkerViewModel.MarkerHalfSize * 2 }, IsActive = false, EditMode = EditMode.ResizeL, Cursor = Cursors.SizeWE },
        };

        public static FigureViewModel FromBusiness(FigureModel model)
        {
            return new FigureViewModel
            {
                Left = model.Left,
                Top = model.Top,
                Width = model.Width,
                Height = model.Height,
                EditMode = model.EditMode,
                FillColor = model.FillColor,
                BorderColor = model.BorderColor,
                Type = model.Type,
                CornerRadius = model.CornerRadius,
                Dx = model.Dx,
                Dy = model.Dy,
            };
        }
    }
}