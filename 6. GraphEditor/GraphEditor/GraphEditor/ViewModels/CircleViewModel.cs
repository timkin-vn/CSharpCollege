using GraphEditor.Business.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GraphEditor.ViewModels
{
    internal class CircleViewModel : FigureViewModel
    {
        public override Rectangle Rectangle => new Rectangle(Left, Top, Width, Height);

        public override IEnumerable<MarkerViewModel> Markers => CreateMarkers();

        private IEnumerable<MarkerViewModel> CreateMarkers()
        {
            return new[]
            {
                new MarkerViewModel
                {
                    Rectangle = new Rectangle(Right - 5, Bottom - 5, 10, 10),
                    EditMode = EditMode.ResizeBR,
                    Cursor = Cursors.SizeNWSE,
                }
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

        public static CircleViewModel FromBusiness(CircleModel model)
        {
            return new CircleViewModel
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