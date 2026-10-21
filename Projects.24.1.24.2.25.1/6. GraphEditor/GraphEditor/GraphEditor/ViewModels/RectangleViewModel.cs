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

        public FillType FillType { get; set; }

        public Color BorderColor { get; set; } = Color.Blue;

        public bool IsSelected { get; set; }

        public ShapeType ShapeType { get; set; }

        public IEnumerable<MarkerViewModel> ShapeMarkers
        {
            get
            {
                if (ShapeType == ShapeType.Rhombus)
                {
                    return new[]
                    {
                       CreateMarker(Left + Width / 2, Top),
                       CreateMarker(Right, Top + Height / 2),
                       CreateMarker(Left + Width / 2, Bottom),
                       CreateMarker(Left, Top + Height / 2)
                     };
                }

                if (ShapeType == ShapeType.Triangle)
                {
                    return new[]
                    {
                       CreateMarker(Left + Width / 2, Top),
                       CreateMarker(Right, Bottom),
                       CreateMarker(Left, Bottom)
                     };
                }

                return Markers;
            }
        }

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
               IsActive = true,
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
                  IsActive = true,
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
                   IsActive = true,
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
                    IsActive = true,
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
                    IsActive = true,
                    EditMode = EditMode.ResizeL,
                    Cursor = Cursors.SizeWE,
                 },
    };

        private MarkerViewModel CreateMarker(int x, int y)
        {
            return new MarkerViewModel
            {
                Rectangle = new Rectangle
                {
                    X = x - MarkerViewModel.MarkerHalfSize,
                    Y = y - MarkerViewModel.MarkerHalfSize,
                    Width = MarkerViewModel.MarkerHalfSize * 2,
                    Height = MarkerViewModel.MarkerHalfSize * 2,
                },
                IsActive = true,
                EditMode = EditMode.ResizeBR,
                Cursor = Cursors.SizeAll
            };
        }

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
                FillType = model.FillType,
                BorderColor = model.BorderColor,
                ShapeType = model.ShapeType,
                IsSelected = model.IsSelected,
                Dx = model.Dx,
                Dy = model.Dy,
            };
        }
    }
}
