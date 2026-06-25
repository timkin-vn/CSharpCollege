using GraphEditor.Business.Models;
using System.Drawing;
using System.Linq;

namespace GraphEditor.Business.Services
{
    public class PictureService
    {
        private readonly FileService _fileService = new FileService();

        public static readonly Color DefaultFillColor = PictureServiceDefaultColors.DefaultFillColor;

        public static readonly Color DefaultBorderColor = PictureServiceDefaultColors.DefaultBorderColor;

        public PictureModel PictureModel { get; private set; }

        public PictureService()
        {
            CreateNewPicture();
        }

        public void CreateNewPicture()
        {
            PictureModel = new PictureModel();
        }

        public void CreateAndSetCreateMode(PointModel loc)
        {
            CreateAndSetCreateMode(loc, ShapeType.Rectangle);
        }

        public void CreateAndSetCreateMode(PointModel loc, ShapeType shapeType)
        {
            ResetMode();

            var rectangle = new RectangleModel
            {
                Left = loc.X,
                Top = loc.Y,
                Width = 0,
                Height = 0,
                Dx = loc.X,
                Dy = loc.Y,
                EditMode = EditMode.Creating,
                ShapeType = shapeType,
                FillColor = DefaultFillColor,
                BorderColor = DefaultBorderColor,
            };

            PictureModel.Rectangles.Add(rectangle);
            PictureModel.SelectedRectangle = rectangle;
        }

        public void DeleteRectangle()
        {
            if (PictureModel.SelectedRectangle == null)
            {
                return;
            }

            PictureModel.Rectangles.Remove(PictureModel.SelectedRectangle);
            PictureModel.SelectedRectangle = null;
        }

        public void MoveForward()
        {
            var index = GetSelectedIndex();
            if (index < 0 || index >= PictureModel.Rectangles.Count - 1)
            {
                return;
            }

            Swap(index, index + 1);
        }

        public void MoveBackward()
        {
            var index = GetSelectedIndex();
            if (index <= 0)
            {
                return;
            }

            Swap(index, index - 1);
        }

        public void MoveToFront()
        {
            var rectangle = PictureModel.SelectedRectangle;
            if (rectangle == null)
            {
                return;
            }

            PictureModel.Rectangles.Remove(rectangle);
            PictureModel.Rectangles.Add(rectangle);
            PictureModel.SelectedRectangle = rectangle;
        }

        public void MoveToBack()
        {
            var rectangle = PictureModel.SelectedRectangle;
            if (rectangle == null)
            {
                return;
            }

            PictureModel.Rectangles.Remove(rectangle);
            PictureModel.Rectangles.Insert(0, rectangle);
            PictureModel.SelectedRectangle = rectangle;
        }

        public void Open(string fileName)
        {
            PictureModel = _fileService.Open(fileName);
            ResetMode();
        }

        public void Save(string fileName)
        {
            ResetMode();
            _fileService.Save(PictureModel, fileName);
        }

        public void SelectAndSetMoveMode(PointModel loc)
        {
            ResetMode();

            var rectangle = PictureModel.Rectangles
                .Reverse()
                .FirstOrDefault(r => IsInside(loc, r));

            PictureModel.SelectedRectangle = rectangle;
            if (rectangle == null)
            {
                return;
            }

            rectangle.Dx = loc.X - rectangle.Left;
            rectangle.Dy = loc.Y - rectangle.Top;
            rectangle.EditMode = EditMode.Moving;
        }

        public void SetFillColor(Color color)
        {
            if (PictureModel.SelectedRectangle == null)
            {
                return;
            }

            PictureModel.SelectedRectangle.FillColor = color;
        }

        public void SetResizeMode(EditMode mode)
        {
            if (PictureModel.SelectedRectangle == null)
            {
                return;
            }

            PictureModel.SelectedRectangle.EditMode = mode;
        }

        public void ResetMode()
        {
            if (PictureModel?.SelectedRectangle == null)
            {
                return;
            }

            Normalize(PictureModel.SelectedRectangle);
            PictureModel.SelectedRectangle.EditMode = EditMode.None;
        }

        public void UpdateMovingPoint(PointModel loc)
        {
            var rectangle = PictureModel.SelectedRectangle;
            if (rectangle == null)
            {
                return;
            }

            var right = rectangle.Right;
            var bottom = rectangle.Bottom;

            switch (rectangle.EditMode)
            {
                case EditMode.Creating:
                    rectangle.Width = loc.X - rectangle.Left;
                    rectangle.Height = loc.Y - rectangle.Top;
                    MakeRegularIfNeeded(rectangle);
                    break;

                case EditMode.Moving:
                    rectangle.Left = loc.X - rectangle.Dx;
                    rectangle.Top = loc.Y - rectangle.Dy;
                    break;

                case EditMode.ResizeTL:
                    rectangle.Left = loc.X;
                    rectangle.Top = loc.Y;
                    rectangle.Width = right - loc.X;
                    rectangle.Height = bottom - loc.Y;
                    MakeRegularIfNeeded(rectangle);
                    break;

                case EditMode.ResizeT:
                    rectangle.Top = loc.Y;
                    rectangle.Height = bottom - loc.Y;
                    MakeRegularIfNeeded(rectangle);
                    break;

                case EditMode.ResizeTR:
                    rectangle.Top = loc.Y;
                    rectangle.Width = loc.X - rectangle.Left;
                    rectangle.Height = bottom - loc.Y;
                    MakeRegularIfNeeded(rectangle);
                    break;

                case EditMode.ResizeR:
                    rectangle.Width = loc.X - rectangle.Left;
                    MakeRegularIfNeeded(rectangle);
                    break;

                case EditMode.ResizeBR:
                    rectangle.Width = loc.X - rectangle.Left;
                    rectangle.Height = loc.Y - rectangle.Top;
                    MakeRegularIfNeeded(rectangle);
                    break;

                case EditMode.ResizeB:
                    rectangle.Height = loc.Y - rectangle.Top;
                    MakeRegularIfNeeded(rectangle);
                    break;

                case EditMode.ResizeBL:
                    rectangle.Left = loc.X;
                    rectangle.Width = right - loc.X;
                    rectangle.Height = loc.Y - rectangle.Top;
                    MakeRegularIfNeeded(rectangle);
                    break;

                case EditMode.ResizeL:
                    rectangle.Left = loc.X;
                    rectangle.Width = right - loc.X;
                    MakeRegularIfNeeded(rectangle);
                    break;
            }
        }

        private int GetSelectedIndex()
        {
            if (PictureModel?.SelectedRectangle == null)
            {
                return -1;
            }

            return PictureModel.Rectangles.IndexOf(PictureModel.SelectedRectangle);
        }

        private bool IsInside(PointModel loc, RectangleModel rectangle)
        {
            var left = rectangle.Width < 0 ? rectangle.Left + rectangle.Width : rectangle.Left;
            var top = rectangle.Height < 0 ? rectangle.Top + rectangle.Height : rectangle.Top;
            var right = left + (rectangle.Width < 0 ? -rectangle.Width : rectangle.Width);
            var bottom = top + (rectangle.Height < 0 ? -rectangle.Height : rectangle.Height);

            return loc.X >= left && loc.X <= right && loc.Y >= top && loc.Y <= bottom;
        }

        private bool IsRegularShape(RectangleModel rectangle)
        {
            return rectangle.ShapeType == ShapeType.Square
                || rectangle.ShapeType == ShapeType.Circle
                || rectangle.ShapeType == ShapeType.Hexagon;
        }

        private void MakeRegularIfNeeded(RectangleModel rectangle)
        {
            if (!IsRegularShape(rectangle))
            {
                return;
            }

            var widthSign = rectangle.Width < 0 ? -1 : 1;
            var heightSign = rectangle.Height < 0 ? -1 : 1;
            var size = System.Math.Max(System.Math.Abs(rectangle.Width), System.Math.Abs(rectangle.Height));

            rectangle.Width = size * widthSign;
            rectangle.Height = size * heightSign;
        }

        private void Normalize(RectangleModel rectangle)
        {
            if (rectangle.Width < 0)
            {
                rectangle.Left += rectangle.Width;
                rectangle.Width = -rectangle.Width;
            }

            if (rectangle.Height < 0)
            {
                rectangle.Top += rectangle.Height;
                rectangle.Height = -rectangle.Height;
            }

            if (IsRegularShape(rectangle))
            {
                var size = System.Math.Max(rectangle.Width, rectangle.Height);
                rectangle.Width = size;
                rectangle.Height = size;
            }
        }

        private void Swap(int firstIndex, int secondIndex)
        {
            var selected = PictureModel.SelectedRectangle;
            var temp = PictureModel.Rectangles[firstIndex];
            PictureModel.Rectangles[firstIndex] = PictureModel.Rectangles[secondIndex];
            PictureModel.Rectangles[secondIndex] = temp;
            PictureModel.SelectedRectangle = selected;
        }
    }
}
