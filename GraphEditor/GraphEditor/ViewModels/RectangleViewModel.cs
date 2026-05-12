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
        public Color BorderColor { get; set; } = Color.Blue;

        public byte Opacity { get; set; } = 255;
        public float RotationAngle { get; set; } = 0f;
        public bool ShowShadow { get; set; } = false;
        public int ShadowOffsetX { get; set; } = 5;
        public int ShadowOffsetY { get; set; } = 5;
        public Color ShadowColor { get; set; } = Color.Gray;
        public GradientType GradientType { get; set; } = GradientType.None;
        public Color FillColor2 { get; set; } = Color.White;
        public float GradientAngle { get; set; } = 45f;

        public IEnumerable<MarkerViewModel> Markers
        {
            get
            {
                var markers = new[]
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
                            Y = (Top + Bottom) / 2 - MarkerViewModel.MarkerHalfSize,
                            Width = MarkerViewModel.MarkerHalfSize * 2,
                            Height = MarkerViewModel.MarkerHalfSize * 2,
                        },
                        IsActive = true,
                        EditMode = EditMode.ResizeL,
                        Cursor = Cursors.SizeWE,
                    },
                    new MarkerViewModel
                    {
                        Rectangle = new Rectangle
                        {
                            X = (Left + Right) / 2 - MarkerViewModel.MarkerHalfSize,
                            Y = Top - MarkerViewModel.MarkerHalfSize * 4,
                            Width = MarkerViewModel.MarkerHalfSize * 2,
                            Height = MarkerViewModel.MarkerHalfSize * 2,
                        },
                        IsActive = true,
                        EditMode = EditMode.Rotating,
                        Cursor = Cursors.Hand,
                    },
                };
                return markers;
            }
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
                BorderColor = model.BorderColor,
                Dx = model.Dx,
                Dy = model.Dy,
                Opacity = model.Opacity,
                RotationAngle = model.RotationAngle,
                ShowShadow = model.ShowShadow,
                ShadowOffsetX = model.ShadowOffsetX,
                ShadowOffsetY = model.ShadowOffsetY,
                ShadowColor = model.ShadowColor,
                GradientType = model.GradientType,
                FillColor2 = model.FillColor2,
                GradientAngle = model.GradientAngle,
            };
        }
    }
}