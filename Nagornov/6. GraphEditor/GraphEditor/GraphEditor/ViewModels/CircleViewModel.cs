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
    internal class CircleViewModel
    {
        public int Dx { get; set; }
        public int Dy { get; set; }
        public int CenterX { get; set; }
        public int CenterY { get; set; }
        public int Radius { get; set; }
        public EditMode EditMode { get; set; }
        public Color FillColor { get; set; }
        public Color BorderColor { get; set; }

        // Анимационные свойства
        public bool IsAnimating { get; set; }
        public double AnimationPhase { get; set; }

        public int Left => CenterX - Radius;
        public int Top => CenterY - Radius;
        public int Width => Radius * 2;
        public int Height => Radius * 2;

        public Rectangle BoundingRectangle => new Rectangle(Left, Top, Width, Height);

        // Анимированный цвет для выделения
        public Color AnimatedBorderColor
        {
            get
            {
                if (!IsAnimating) return BorderColor;

                var phase = (Math.Sin(AnimationPhase * 8) + 1) / 2; // 0 to 1
                return InterpolateColor(BorderColor, Color.Gold, phase);
            }
        }

        public Color AnimatedFillColor
        {
            get
            {
                if (!IsAnimating) return FillColor;

                var phase = (Math.Cos(AnimationPhase * 6) + 1) / 2; // 0 to 1
                return InterpolateColor(FillColor, LightenColor(FillColor, 0.2f), phase);
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
                    X = CenterX - MarkerViewModel.MarkerHalfSize,
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
                    X = Left + Width - MarkerViewModel.MarkerHalfSize,
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
                    X = Left + Width - MarkerViewModel.MarkerHalfSize,
                    Y = CenterY - MarkerViewModel.MarkerHalfSize,
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
                    X = Left + Width - MarkerViewModel.MarkerHalfSize,
                    Y = Top + Height - MarkerViewModel.MarkerHalfSize,
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
                    X = CenterX - MarkerViewModel.MarkerHalfSize,
                    Y = Top + Height - MarkerViewModel.MarkerHalfSize,
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
                    Y = Top + Height - MarkerViewModel.MarkerHalfSize,
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
                    Y = CenterY - MarkerViewModel.MarkerHalfSize,
                    Width = MarkerViewModel.MarkerHalfSize * 2,
                    Height = MarkerViewModel.MarkerHalfSize * 2,
                },
                IsActive = true,
                EditMode = EditMode.ResizeL,
                Cursor = Cursors.SizeWE,
            },
        };

        private Color InterpolateColor(Color color1, Color color2, double ratio)
        {
            ratio = Math.Max(0, Math.Min(1, ratio));
            return Color.FromArgb(
                (int)(color1.A + (color2.A - color1.A) * ratio),
                (int)(color1.R + (color2.R - color1.R) * ratio),
                (int)(color1.G + (color2.G - color1.G) * ratio),
                (int)(color1.B + (color2.B - color1.B) * ratio)
            );
        }

        private Color LightenColor(Color color, float factor)
        {
            factor = Math.Max(0, Math.Min(1, factor));
            return Color.FromArgb(
                color.A,
                Math.Min(255, (int)(color.R + (255 - color.R) * factor)),
                Math.Min(255, (int)(color.G + (255 - color.G) * factor)),
                Math.Min(255, (int)(color.B + (255 - color.B) * factor))
            );
        }

        public static CircleViewModel FromBusiness(CircleModel model)
        {
            return new CircleViewModel
            {
                CenterX = model.CenterX,
                CenterY = model.CenterY,
                Radius = model.Radius,
                FillColor = model.FillColor,
                BorderColor = model.BorderColor,
                EditMode = model.EditMode,
                Dx = model.Dx,
                Dy = model.Dy,
                IsAnimating = model.EditMode != EditMode.None
            };
        }
    }
}