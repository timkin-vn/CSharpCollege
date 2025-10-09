using GraphEditor.Business.Models;

namespace GraphEditor.ViewModels {
    internal class RectangleViewModel {
        private string? Text { get; init; }
        
        private Color TextColor { get; init; } = Color.Black;
        
        private string FontFamily { get; init; } = "Segoe UI";
        
        private float FontSize { get; init; } = 10f;
        
        private TextAlign TextAlign { get; init; } = TextAlign.Center;
        
        private float BorderWidth { get; init; } = 1.5f;
        
        private int Dx { get; init; }

        private int Dy { get; init; }

        private int Left { get; init; }

        private int Top { get; init; }

        private int Width { get; set; }

        private int Height { get; set; }

        public int Bottom {
            get => Top + Height;
            set => Height = value - Top;
        }

        public int Right {
            get => Left + Width;
            set => Width = value - Left;
        }
        
        public RectangleModel ToModel() {
            return new RectangleModel {
                Left = Left,
                Top = Top,
                Width = Width,
                Height = Height,
                FillColor = FillColor,
                BorderColor = BorderColor,
                BorderWidth = BorderWidth,
                Text = Text,
                TextColor = TextColor,
                FontFamily = FontFamily,
                FontSize = FontSize,
                TextAlign = TextAlign
            };
        }
        
        public static RectangleViewModel FromModel(RectangleModel model) {
            return new RectangleViewModel {
                Left = model.Left,
                Top = model.Top,
                Width = model.Width,
                Height = model.Height,
                FillColor = model.FillColor,
                BorderColor = model.BorderColor,
                BorderWidth = model.BorderWidth,
                Text = model.Text,
                TextColor = model.TextColor,
                FontFamily = model.FontFamily,
                FontSize = model.FontSize,
                TextAlign = model.TextAlign
            };
        }

        private EditMode EditMode { get; init; }

        public Color FillColor { get; private init; } = Color.Yellow;

        public Brush? FillBrush { get; private init; }

        private Color BorderColor { get; init; } = Color.Blue;

        public Pen? BorderPen { get; private init; }

        public Rectangle Rectangle => new Rectangle {
            X = Left < Right ? Left : Right,
            Y = Top < Bottom ? Top : Bottom,
            Width = Width > 0 ? Width : -Width,
            Height = Height > 0 ? Height : -Height,
        };

        public IEnumerable<MarkerViewModel> Markers => [
            new MarkerViewModel {
                Rectangle = new Rectangle {
                    X = Left - MarkerViewModel.MarkerHalfSize,
                    Y = Top - MarkerViewModel.MarkerHalfSize,
                    Width = MarkerViewModel.MarkerHalfSize * 2,
                    Height = MarkerViewModel.MarkerHalfSize * 2,
                },
                IsActive = false,
                EditMode = EditMode.ResizeTL,
                Cursor = Cursors.SizeNWSE,
            },
            new MarkerViewModel {
                Rectangle = new Rectangle {
                    X = (Left + Right) / 2 - MarkerViewModel.MarkerHalfSize,
                    Y = Top - MarkerViewModel.MarkerHalfSize,
                    Width = MarkerViewModel.MarkerHalfSize * 2,
                    Height = MarkerViewModel.MarkerHalfSize * 2,
                },
                IsActive = false,
                EditMode = EditMode.ResizeT,
                Cursor = Cursors.SizeNS,
            },
            new MarkerViewModel {
                Rectangle = new Rectangle {
                    X = Right - MarkerViewModel.MarkerHalfSize,
                    Y = Top - MarkerViewModel.MarkerHalfSize,
                    Width = MarkerViewModel.MarkerHalfSize * 2,
                    Height = MarkerViewModel.MarkerHalfSize * 2,
                },
                IsActive = false,
                EditMode = EditMode.ResizeTR,
                Cursor = Cursors.SizeNESW,
            },
            new MarkerViewModel {
                Rectangle = new Rectangle {
                    X = Right - MarkerViewModel.MarkerHalfSize,
                    Y = (Top + Bottom) /2 - MarkerViewModel.MarkerHalfSize,
                    Width = MarkerViewModel.MarkerHalfSize * 2,
                    Height = MarkerViewModel.MarkerHalfSize * 2,
                },
                IsActive = true,
                EditMode = EditMode.ResizeR,
                Cursor = Cursors.SizeWE,
            },
            new MarkerViewModel {
                Rectangle = new Rectangle {
                    X = Right - MarkerViewModel.MarkerHalfSize,
                    Y = Bottom - MarkerViewModel.MarkerHalfSize,
                    Width = MarkerViewModel.MarkerHalfSize * 2,
                    Height = MarkerViewModel.MarkerHalfSize * 2,
                },
                IsActive = true,
                EditMode = EditMode.ResizeBR,
                Cursor = Cursors.SizeNWSE,
            },
            new MarkerViewModel {
                Rectangle = new Rectangle {
                    X = (Left + Right) / 2 - MarkerViewModel.MarkerHalfSize,
                    Y = Bottom - MarkerViewModel.MarkerHalfSize,
                    Width = MarkerViewModel.MarkerHalfSize * 2,
                    Height = MarkerViewModel.MarkerHalfSize * 2,
                },
                IsActive = false,
                EditMode = EditMode.ResizeB,
                Cursor = Cursors.SizeNS,
            },
            new MarkerViewModel {
                Rectangle = new Rectangle {
                    X = Left - MarkerViewModel.MarkerHalfSize,
                    Y = Bottom - MarkerViewModel.MarkerHalfSize,
                    Width = MarkerViewModel.MarkerHalfSize * 2,
                    Height = MarkerViewModel.MarkerHalfSize * 2,
                },
                IsActive = false,
                EditMode = EditMode.ResizeBL,
                Cursor = Cursors.SizeNESW,
            },
            new MarkerViewModel {
                Rectangle = new Rectangle {
                    X = Left - MarkerViewModel.MarkerHalfSize,
                    Y = (Top + Bottom) /2 - MarkerViewModel.MarkerHalfSize,
                    Width = MarkerViewModel.MarkerHalfSize * 2,
                    Height = MarkerViewModel.MarkerHalfSize * 2,
                },
                IsActive = false,
                EditMode = EditMode.ResizeL,
                Cursor = Cursors.SizeWE,
            }
        ];

        public static RectangleViewModel FromBusiness(RectangleModel model) {
            return new RectangleViewModel {
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

        public RectangleModel ToBusiness() {
            return new RectangleModel {
                Dx = Dx,
                Dy = Dy,
                Left = Left,
                Top = Top,
                Width = Width,
                Height = Height,
                EditMode = EditMode,
                FillColor = FillColor,
                BorderColor = BorderColor,
            };
        }
    }
}
