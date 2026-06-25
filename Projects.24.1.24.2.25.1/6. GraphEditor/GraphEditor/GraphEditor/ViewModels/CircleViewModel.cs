using GraphEditor.Business.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GraphEditor.ViewModels
{
    internal class CircleViewModel
    {
        public int CenterX { get; set; }
        public int CenterY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public EditMode EditMode { get; set; }
        public Color FillColor { get; set; } = Color.Yellow;
        public Color BorderColor { get; set; } = Color.Blue;

        public PointModel GetCenter() => new PointModel { X = CenterX, Y = CenterY };

        public IEnumerable<MarkerViewModel> Markers
        {
            get
            {
                int left = CenterX - Width / 2;
                int top = CenterY - Height / 2;
                int right = CenterX + Width / 2;
                int bottom = CenterY + Height / 2;
                int cx = CenterX, cy = CenterY;
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

        public static CircleViewModel FromBusiness(CircleModel model)
        {
            return new CircleViewModel
            {
                CenterX = model.CenterX,
                CenterY = model.CenterY,
                Width = model.Width,
                Height = model.Height,
                EditMode = model.EditMode,
                FillColor = model.FillColor,
                BorderColor = model.BorderColor,
            };
        }
    }
}