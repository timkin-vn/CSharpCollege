using GraphEditor.Business.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GraphEditor.ViewModels
{
    internal class HilbertCurveViewModel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }
        public int Order { get; set; }
        public EditMode EditMode { get; set; }
        public Color FillColor { get; set; } = Color.Yellow;
        public Color BorderColor { get; set; } = Color.Blue;

        public PointModel GetCenter() => new PointModel { X = X + Size / 2, Y = Y + Size / 2 };

        public IEnumerable<MarkerViewModel> Markers
        {
            get
            {
                int left = X, top = Y, right = X + Size, bottom = Y + Size;
                int cx = (left + right) / 2, cy = (top + bottom) / 2;
                int half = MarkerViewModel.MarkerHalfSize;

                return new[]
                {
                    new MarkerViewModel { Rectangle = new Rectangle(left - half, top - half, half*2, half*2), IsActive = false, EditMode = EditMode.ResizeTL, Cursor = Cursors.SizeNWSE },
                    new MarkerViewModel { Rectangle = new Rectangle(cx - half, top - half, half*2, half*2), IsActive = false, EditMode = EditMode.ResizeT,  Cursor = Cursors.SizeNS },
                    new MarkerViewModel { Rectangle = new Rectangle(right - half, top - half, half*2, half*2), IsActive = false, EditMode = EditMode.ResizeTR, Cursor = Cursors.SizeNESW },
                    new MarkerViewModel { Rectangle = new Rectangle(right - half, cy - half, half*2, half*2), IsActive = true,  EditMode = EditMode.ResizeR,  Cursor = Cursors.SizeWE },
                    new MarkerViewModel { Rectangle = new Rectangle(right - half, bottom - half, half*2, half*2), IsActive = true,  EditMode = EditMode.ResizeBR, Cursor = Cursors.SizeNWSE },
                    new MarkerViewModel { Rectangle = new Rectangle(cx - half, bottom - half, half*2, half*2), IsActive = false, EditMode = EditMode.ResizeB,  Cursor = Cursors.SizeNS },
                    new MarkerViewModel { Rectangle = new Rectangle(left - half, bottom - half, half*2, half*2), IsActive = false, EditMode = EditMode.ResizeBL, Cursor = Cursors.SizeNESW },
                    new MarkerViewModel { Rectangle = new Rectangle(left - half, cy - half, half*2, half*2), IsActive = false, EditMode = EditMode.ResizeL,  Cursor = Cursors.SizeWE },
                };
            }
        }

        public static HilbertCurveViewModel FromBusiness(HilbertCurveModel model)
        {
            return new HilbertCurveViewModel
            {
                X = model.X,
                Y = model.Y,
                Size = model.Size,
                Order = model.Order,
                EditMode = model.EditMode,
                FillColor = model.FillColor,
                BorderColor = model.BorderColor,
            };
        }
    }
}