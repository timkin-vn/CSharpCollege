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
        public int Right => Left + Width;
        public int Bottom => Top + Height;
        public EditMode EditMode { get; set; }
        public Color FillColor { get; set; } = Color.Yellow;
        public Color BorderColor { get; set; } = Color.Blue;
        public int Opacity { get; set; } = 255;
        public int CornerRadius { get; set; } = 0;
        public int BorderOpacity { get; set; } = 255;

        public IEnumerable<MarkerViewModel> Markers => new[]
        {
            new MarkerViewModel
            {
                Rectangle = new Rectangle(Left - MarkerViewModel.MarkerHalfSize, Top - MarkerViewModel.MarkerHalfSize,
                                          MarkerViewModel.MarkerHalfSize * 2, MarkerViewModel.MarkerHalfSize * 2),
                IsActive = false, EditMode = EditMode.ResizeTL, Cursor = Cursors.SizeNWSE,
            },
            new MarkerViewModel
            {
                Rectangle = new Rectangle((Left + Right) / 2 - MarkerViewModel.MarkerHalfSize, Top - MarkerViewModel.MarkerHalfSize,
                                          MarkerViewModel.MarkerHalfSize * 2, MarkerViewModel.MarkerHalfSize * 2),
                IsActive = false, EditMode = EditMode.ResizeT, Cursor = Cursors.SizeNS,
            },
            new MarkerViewModel
            {
                Rectangle = new Rectangle(Right - MarkerViewModel.MarkerHalfSize, Top - MarkerViewModel.MarkerHalfSize,
                                          MarkerViewModel.MarkerHalfSize * 2, MarkerViewModel.MarkerHalfSize * 2),
                IsActive = false, EditMode = EditMode.ResizeTR, Cursor = Cursors.SizeNESW,
            },
            new MarkerViewModel
            {
                Rectangle = new Rectangle(Right - MarkerViewModel.MarkerHalfSize, (Top + Bottom) /2 - MarkerViewModel.MarkerHalfSize,
                                          MarkerViewModel.MarkerHalfSize * 2, MarkerViewModel.MarkerHalfSize * 2),
                IsActive = true, EditMode = EditMode.ResizeR, Cursor = Cursors.SizeWE,
            },
            new MarkerViewModel
            {
                Rectangle = new Rectangle(Right - MarkerViewModel.MarkerHalfSize, Bottom - MarkerViewModel.MarkerHalfSize,
                                          MarkerViewModel.MarkerHalfSize * 2, MarkerViewModel.MarkerHalfSize * 2),
                IsActive = true, EditMode = EditMode.ResizeBR, Cursor = Cursors.SizeNWSE,
            },
            new MarkerViewModel
            {
                Rectangle = new Rectangle((Left + Right) / 2 - MarkerViewModel.MarkerHalfSize, Bottom - MarkerViewModel.MarkerHalfSize,
                                          MarkerViewModel.MarkerHalfSize * 2, MarkerViewModel.MarkerHalfSize * 2),
                IsActive = false, EditMode = EditMode.ResizeB, Cursor = Cursors.SizeNS,
            },
            new MarkerViewModel
            {
                Rectangle = new Rectangle(Left - MarkerViewModel.MarkerHalfSize, Bottom - MarkerViewModel.MarkerHalfSize,
                                          MarkerViewModel.MarkerHalfSize * 2, MarkerViewModel.MarkerHalfSize * 2),
                IsActive = false, EditMode = EditMode.ResizeBL, Cursor = Cursors.SizeNESW,
            },
            new MarkerViewModel
            {
                Rectangle = new Rectangle(Left - MarkerViewModel.MarkerHalfSize, (Top + Bottom) /2 - MarkerViewModel.MarkerHalfSize,
                                          MarkerViewModel.MarkerHalfSize * 2, MarkerViewModel.MarkerHalfSize * 2),
                IsActive = false, EditMode = EditMode.ResizeL, Cursor = Cursors.SizeWE,
            },
        };

        public Rectangle Rectangle => new Rectangle
        {
            X = Width < 0 ? Left + Width : Left,
            Y = Height < 0 ? Top + Height : Top,
            Width = Width < 0 ? -Width : Width,
            Height = Height < 0 ? -Height : Height,
        };

        public static RectangleViewModel FromBusiness(RectangleModel model)
        {
            return new RectangleViewModel
            {
                Left = model.Left,
                Top = model.Top,
                Width = model.Width,
                Height = model.Height,
                EditMode = model.EditMode,
                FillColor = model.FillColor,
                BorderColor = model.BorderColor,
                Dx = model.Dx,
                Dy = model.Dy,
                Opacity = model.Opacity,
                CornerRadius = model.CornerRadius,
                BorderOpacity = model.BorderOpacity,
            };
        }
    }

}