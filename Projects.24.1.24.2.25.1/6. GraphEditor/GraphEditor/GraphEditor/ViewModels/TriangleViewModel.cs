using GraphEditor.Business.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GraphEditor.ViewModels
{
    internal class TriangleViewModel
    {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public int X3 { get; set; }
        public int Y3 { get; set; }
        public EditMode EditMode { get; set; }
        public Color FillColor { get; set; } = Color.Yellow;
        public Color BorderColor { get; set; } = Color.Blue;

        public PointModel GetCenter()
        {
            return new PointModel { X = (X1 + X2 + X3) / 3, Y = (Y1 + Y2 + Y3) / 3 };
        }

        public Rectangle GetRectangle()
        {
            int left = Math.Min(Math.Min(X1, X2), X3);
            int top = Math.Min(Math.Min(Y1, Y2), Y3);
            int right = Math.Max(Math.Max(X1, X2), X3);
            int bottom = Math.Max(Math.Max(Y1, Y2), Y3);
            return new Rectangle(left, top, right - left, bottom - top);
        }

        public IEnumerable<MarkerViewModel> Markers
        {
            get
            {
                int half = MarkerViewModel.MarkerHalfSize;
                return new[]
                {
                    new MarkerViewModel 
                    { 
                        Rectangle = new Rectangle(X1 - half, Y1 - half, half*2, half*2), 
                        IsActive = true, 
                        EditMode = EditMode.MoveVertex1, 
                        Cursor = Cursors.SizeAll 
                    },
                    new MarkerViewModel 
                    { 
                        Rectangle = new Rectangle(X2 - half, Y2 - half, half*2, half*2), 
                        IsActive = true, 
                        EditMode = EditMode.MoveVertex2, 
                        Cursor = Cursors.SizeAll 
                    },
                    new MarkerViewModel 
                    { 
                        Rectangle = new Rectangle(X3 - half, Y3 - half, half*2, half*2), 
                        IsActive = true, 
                        EditMode = EditMode.MoveVertex3, 
                        Cursor = Cursors.SizeAll 
                    },
                };
            }
        }

        public static TriangleViewModel FromBusiness(TriangleModel model)
        {
            return new TriangleViewModel
            {
                X1 = model.X1,
                Y1 = model.Y1,
                X2 = model.X2,
                Y2 = model.Y2,
                X3 = model.X3,
                Y3 = model.Y3,
                EditMode = model.EditMode,
                FillColor = model.FillColor,
                BorderColor = model.BorderColor,
            };
        }
    }
}