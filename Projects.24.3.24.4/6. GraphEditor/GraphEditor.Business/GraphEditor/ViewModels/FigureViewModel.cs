using GraphEditor.Business.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GraphEditor.ViewModels
{
    internal class FigureViewModel
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Right => Left + Width;
        public int Bottom => Top + Height;

        public Rectangle Bounds => new Rectangle(Left, Top, Width, Height);
        public Color FillColor { get; set; }
        public Color BorderColor { get; set; }
        public EditMode EditMode { get; set; }
        public string FigureType { get; set; } // "Rectangle", "Circle", "Triangle"

        public IEnumerable<MarkerViewModel> Markers => new[]
        {
            new MarkerViewModel { Rectangle = new Rectangle(Left - 4, Top - 4, 8, 8), IsActive = true, EditMode = EditMode.ResizeTL, Cursor = Cursors.SizeNWSE },
            new MarkerViewModel { Rectangle = new Rectangle(Right - 4, Bottom - 4, 8, 8), IsActive = true, EditMode = EditMode.ResizeBR, Cursor = Cursors.SizeNWSE },
            new MarkerViewModel { Rectangle = new Rectangle(Right - 4, Top - 4, 8, 8), IsActive = true, EditMode = EditMode.ResizeTR, Cursor = Cursors.SizeNESW },
            new MarkerViewModel { Rectangle = new Rectangle(Left - 4, Bottom - 4, 8, 8), IsActive = true, EditMode = EditMode.ResizeBL, Cursor = Cursors.SizeNESW },
        };

        public static FigureViewModel FromBusiness(FigureModel model)
        {
            string type = "Rectangle";
            if (model is CircleModel) type = "Circle";
            else if (model is TriangleModel) type = "Triangle";

            return new FigureViewModel
            {
                Left = model.Left,
                Top = model.Top,
                Width = model.Width,
                Height = model.Height,
                FillColor = model.FillColor,
                BorderColor = model.BorderColor,
                EditMode = model.EditMode,
                FigureType = type
            };
        }
    }

    internal class PictureViewModel
    {
        public IList<FigureViewModel> Figures { get; set; } = new List<FigureViewModel>();
        public FigureViewModel SelectedFigure { get; set; }
        public IEnumerable<MarkerViewModel> Markers => SelectedFigure?.Markers ?? new List<MarkerViewModel>();
    }
}