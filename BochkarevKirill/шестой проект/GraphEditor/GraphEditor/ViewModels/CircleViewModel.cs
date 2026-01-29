using GraphEditor.Business.Models;
using GraphEditor.Business.Models.Xml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphEditor.ViewModels
{
    internal class CircleViewModel
    {
        public int Dx { get; set; }

        public int Dy { get; set; }

        public int Left { get; set; }

        public int Top { get; set; }

        // Диаметр круга (круг вписан в квадрат)
        public int Diameter { get; set; }

        public int Bottom
        {
            get => Top + Diameter;
            set => Diameter = value - Top;
        }

        public int Right
        {
            get => Left + Diameter;
            set => Diameter = value - Left;
        }

        public EditMode EditMode { get; set; }

        public Color FillColor { get; set; } = Color.Yellow;

        public Brush FillBrush { get; set; }

        public Color BorderColor { get; set; } = Color.Blue;

        public Pen BorderPen { get; set; }

        // Ограничивающий прямоугольник круга
        public Rectangle Rectangle => new Rectangle
        {
            X = Left < Right ? Left : Right,
            Y = Top < Bottom ? Top : Bottom,
            Width = Diameter > 0 ? Diameter : -Diameter,
            Height = Diameter > 0 ? Diameter : -Diameter,
        };

        // Маркеры (как у прямоугольника, но по квадрату круга)
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
                IsActive = false,
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
                IsActive = false,
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
                IsActive = false,
                EditMode = EditMode.ResizeTR,
                Cursor = Cursors.SizeNESW,
            },
            new MarkerViewModel
            {
                Rectangle = new Rectangle
                {
                    X = Right - MarkerViewModel.MarkerHalfSize,
                    Y = (Top + Bottom) / 2 - MarkerViewModel.MarkerHalfSize,
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
                IsActive = false,
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
                IsActive = false,
                EditMode = EditMode.ResizeBL,
                Cursor = Cursors.SizeNESW,
            },
            new MarkerViewModel
            {
                Rectangle = new Rectangle
                {
                    X = Left - MarkerViewModel.MarkerHalfSize,
                    Y = (Top + Bottom) / 2 - MarkerViewModel.MarkerHalfSize,
                    Width = MarkerViewModel.MarkerHalfSize * 2,
                    Height = MarkerViewModel.MarkerHalfSize * 2,
                },
                IsActive = false,
                EditMode = EditMode.ResizeL,
                Cursor = Cursors.SizeWE,
            },
        };

        public static CircleViewModel FromBusiness(CircleModel model)
        {
            return new CircleViewModel
            {
                Dx = model.Dx,
                Dy = model.Dy,
                Left = model.Left,
                Top = model.Top,
                Diameter = model.Diameter,
                EditMode = model.EditMode,
                FillColor = model.FillColor,
                FillBrush = new SolidBrush(model.FillColor),
                BorderColor = model.BorderColor,
                BorderPen = new Pen(model.BorderColor, 3),
            };
        }

        public CircleModel ToBusiness()
        {
            return new CircleModel
            {
                Dx = Dx,
                Dy = Dy,
                Left = Left,
                Top = Top,
                Diameter = Diameter,
                EditMode = EditMode,
                FillColor = FillColor,
                BorderColor = BorderColor,
            };
        }
    }
}
