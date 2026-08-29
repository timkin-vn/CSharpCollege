using GraphEditor.Business.Models;
using System.Drawing;
using System.Linq;

namespace GraphEditor.Business.Services
{
    public class PictureService
    {
        public PictureModel PictureModel { get; private set; } = new PictureModel();

        public static Color DefaultFillColor { get; set; } = Color.Yellow;

        public static Color DefaultBorderColor { get; set; } = Color.Blue;

        public PictureService()
        {
            PictureModel.Rectangles.Add(new RectangleModel { Left = 150, Top = 50, Width = 200, Height = 100, FillColor = Color.LightSkyBlue });

            var newRectangle = new RectangleModel { Left = 200, Top = 100, Width = 200, Height = 150 };
            PictureModel.Rectangles.Add(newRectangle);
            PictureModel.SelectedRectangle = newRectangle;
            PictureModel.SelectedRectangles.Add(newRectangle);

            PictureModel.Rectangles.Add(new RectangleModel { Left = 250, Top = 150, Width = 200, Height = 200, FillColor = Color.DarkMagenta });
        }

        public void CreateRectangleAndSetCreateMode(PointModel loc)
        {
            var newRectangle = new RectangleModel
            {
                Left = loc.X,
                Top = loc.Y,
                Width = 0,
                Height = 0,
                FillColor = DefaultFillColor,
                BorderColor = DefaultBorderColor,
            };

            PictureModel.Rectangles.Add(newRectangle);

            PictureModel.SelectedRectangles.Clear();
            PictureModel.SelectedRectangles.Add(newRectangle);
            PictureModel.SelectedRectangle = newRectangle;

            newRectangle.EditMode = EditMode.Creating;
        }

        public void FindAndSetMoveMode(PointModel loc, bool ctrlPressed)
        {
            var selectedRect = PictureModel.Rectangles.LastOrDefault(r => r.IsInside(loc));

            if (selectedRect == null)
            {
                if (!ctrlPressed)
                {
                    PictureModel.SelectedRectangle = null;
                    PictureModel.SelectedRectangles.Clear();
                }

                return;
            }

            if (ctrlPressed)
            {
                if (PictureModel.SelectedRectangles.Contains(selectedRect))
                {
                    PictureModel.SelectedRectangles.Remove(selectedRect);
                    PictureModel.SelectedRectangle = PictureModel.SelectedRectangles.LastOrDefault();
                }
                else
                {
                    PictureModel.SelectedRectangles.Add(selectedRect);
                    PictureModel.SelectedRectangle = selectedRect;
                }
            }
            else
            {
                PictureModel.SelectedRectangles.Clear();
                PictureModel.SelectedRectangles.Add(selectedRect);
                PictureModel.SelectedRectangle = selectedRect;
            }

            foreach (var rect in PictureModel.SelectedRectangles)
            {
                rect.EditMode = EditMode.Moving;
                rect.Dx = loc.X - rect.Left;
                rect.Dy = loc.Y - rect.Top;
            }
        }

        public void SetResizeMode(EditMode mode)
        {
            if (PictureModel.SelectedRectangle != null)
            {
                PictureModel.SelectedRectangle.EditMode = mode;
            }
        }

        public void ResetMode()
        {
            foreach (var rect in PictureModel.SelectedRectangles)
            {
                rect.Normalize();
                rect.EditMode = EditMode.None;
            }

            if (PictureModel.SelectedRectangle != null)
            {
                PictureModel.SelectedRectangle.Normalize();
                PictureModel.SelectedRectangle.EditMode = EditMode.None;
            }
        }

        public void UpdateMovingPoint(PointModel loc)
        {
            if (PictureModel.SelectedRectangle == null)
            {
                return;
            }

            if (PictureModel.SelectedRectangles.Count > 1)
            {
                foreach (var rect in PictureModel.SelectedRectangles)
                {
                    if (rect.EditMode == EditMode.Moving)
                    {
                        rect.Left = loc.X - rect.Dx;
                        rect.Top = loc.Y - rect.Dy;
                    }
                }

                return;
            }

            var selectedRect = PictureModel.SelectedRectangle;

            switch (selectedRect.EditMode)
            {
                case EditMode.Creating:
                case EditMode.ResizeBR:
                    selectedRect.Width = loc.X - selectedRect.Left;
                    selectedRect.Height = loc.Y - selectedRect.Top;
                    break;

                case EditMode.ResizeR:
                    selectedRect.Width = loc.X - selectedRect.Left;
                    break;

                case EditMode.Moving:
                    selectedRect.Left = loc.X - selectedRect.Dx;
                    selectedRect.Top = loc.Y - selectedRect.Dy;
                    break;
            }
        }

        public void DeleteRectangle()
        {
            if (PictureModel.SelectedRectangles.Count > 0)
            {
                foreach (var rect in PictureModel.SelectedRectangles.ToList())
                {
                    PictureModel.Rectangles.Remove(rect);
                }

                PictureModel.SelectedRectangles.Clear();
                PictureModel.SelectedRectangle = null;
                return;
            }

            if (PictureModel.SelectedRectangle == null)
            {
                return;
            }

            PictureModel.Rectangles.Remove(PictureModel.SelectedRectangle);
            PictureModel.SelectedRectangle = null;
        }

        public void SetFillColor(Color color)
        {
            if (PictureModel.SelectedRectangles.Count > 0)
            {
                foreach (var rect in PictureModel.SelectedRectangles)
                {
                    rect.FillColor = color;
                }

                return;
            }

            if (PictureModel.SelectedRectangle != null)
            {
                PictureModel.SelectedRectangle.FillColor = color;
            }
        }

        public void MoveForward()
        {
            if (PictureModel.SelectedRectangle == null)
            {
                return;
            }

            var index = PictureModel.Rectangles.IndexOf(PictureModel.SelectedRectangle);
            if (index < 0 || index == PictureModel.Rectangles.Count - 1)
            {
                return;
            }

            var rect = PictureModel.Rectangles[index];
            PictureModel.Rectangles[index] = PictureModel.Rectangles[index + 1];
            PictureModel.Rectangles[index + 1] = rect;
        }

        public void Save(string fileName)
        {
            new FileService().Save(fileName, PictureModel);
        }

        public void Open(string fileName)
        {
            PictureModel = new FileService().Open(fileName);

            if (PictureModel.SelectedRectangles == null)
            {
                PictureModel.SelectedRectangles = new System.Collections.Generic.List<RectangleModel>();
            }
        }

        public void CreateNewPicture()
        {
            PictureModel.Rectangles.Clear();
            PictureModel.SelectedRectangle = null;
            PictureModel.SelectedRectangles.Clear();
        }
    }
}