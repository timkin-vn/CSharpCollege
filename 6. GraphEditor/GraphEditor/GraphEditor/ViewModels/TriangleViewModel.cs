using GraphEditor.Business.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GraphEditor.ViewModels
{
    internal class TriangleViewModel : FigureViewModel
    {
        public override Rectangle Rectangle => new Rectangle(Left, Top, Width, Height);

        public override IEnumerable<MarkerViewModel> Markers => CreateMarkers();

        private IEnumerable<MarkerViewModel> CreateMarkers()
        {
            return new[]
            {
                new MarkerViewModel
                {
                    Rectangle = new Rectangle(Left + Width - 5, Top + Height - 5, 10, 10),
                    EditMode = EditMode.ResizeBR,
                    Cursor = Cursors.SizeNWSE,
                }
            };
        }

        public Point[] GetPoints()
        {
            return new Point[]
            {
                new Point(Left + Width / 2, Top),              
                new Point(Left, Top + Height),                      
                new Point(Left + Width, Top + Height)               
            };
        }

        public override void UpdateFillColor(Color newColor)
        {
            FillColor = newColor;
            FillBrush?.Dispose();
            FillBrush = new SolidBrush(newColor);
        }

        public override void UpdateBorderColor(Color newColor)
        {
            BorderColor = newColor;
            BorderPen?.Dispose();
            BorderPen = new Pen(newColor, 3);
        }

        public static TriangleViewModel FromBusiness(TriangleModel model)
        {
            return new TriangleViewModel
            {
                Dx = model.Dx,
                Dy = model.Dy,
                Left = model.Left,
                Top = model.Top,
                Width = model.Width,
                Height = model.Height,
                FillColor = model.FillColor,
                FillBrush = new SolidBrush(model.FillColor),
                BorderColor = model.BorderColor,
                BorderPen = new Pen(model.BorderColor, 3)
            };
        }
    }
}