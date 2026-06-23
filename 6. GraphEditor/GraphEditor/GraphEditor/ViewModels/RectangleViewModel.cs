using GraphEditor.Business.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GraphEditor.ViewModels
{
    internal class RectangleViewModel : FigureViewModel
    {
        public EditMode EditMode { get; set; }

        public override Rectangle Rectangle => new Rectangle
        {
            X = Left,
            Y = Top,
            Width = Width,
            Height = Height
        };

        public override IEnumerable<MarkerViewModel> Markers
        {
            get
            {
                var markers = new List<MarkerViewModel>();

                markers.Add(new MarkerViewModel
                {
                    Rectangle = new Rectangle(Left - 5, Top - 5, 10, 10),
                    IsActive = true,
                    EditMode = EditMode.ResizeTL,
                    Cursor = Cursors.SizeNWSE,
                });

                markers.Add(new MarkerViewModel
                {
                    Rectangle = new Rectangle(Left + Width / 2 - 5, Top - 5, 10, 10),
                    IsActive = true,
                    EditMode = EditMode.ResizeT,
                    Cursor = Cursors.SizeNS,
                });

                markers.Add(new MarkerViewModel
                {
                    Rectangle = new Rectangle(Left + Width - 5, Top - 5, 10, 10),
                    IsActive = true,
                    EditMode = EditMode.ResizeTR,
                    Cursor = Cursors.SizeNESW,
                });

                markers.Add(new MarkerViewModel
                {
                    Rectangle = new Rectangle(Left + Width - 5, Top + Height / 2 - 5, 10, 10),
                    IsActive = true,
                    EditMode = EditMode.ResizeR,
                    Cursor = Cursors.SizeWE,
                });

                markers.Add(new MarkerViewModel
                {
                    Rectangle = new Rectangle(Left + Width - 5, Top + Height - 5, 10, 10),
                    IsActive = true,
                    EditMode = EditMode.ResizeBR,
                    Cursor = Cursors.SizeNWSE,
                });

                markers.Add(new MarkerViewModel
                {
                    Rectangle = new Rectangle(Left + Width / 2 - 5, Top + Height - 5, 10, 10),
                    IsActive = true,
                    EditMode = EditMode.ResizeB,
                    Cursor = Cursors.SizeNS,
                });

                markers.Add(new MarkerViewModel
                {
                    Rectangle = new Rectangle(Left - 5, Top + Height - 5, 10, 10),
                    IsActive = true,
                    EditMode = EditMode.ResizeBL,
                    Cursor = Cursors.SizeNESW,
                });

                markers.Add(new MarkerViewModel
                {
                    Rectangle = new Rectangle(Left - 5, Top + Height / 2 - 5, 10, 10),
                    IsActive = true,
                    EditMode = EditMode.ResizeL,
                    Cursor = Cursors.SizeWE,
                });

                return markers;
            }
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
            };
        }
    }
}