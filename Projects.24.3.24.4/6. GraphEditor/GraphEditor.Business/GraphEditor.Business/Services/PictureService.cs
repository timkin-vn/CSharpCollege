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

        public enum FigureType { Rectangle, Circle, Triangle }

        public void CreateFigureAndSetCreateMode(PointModel loc, FigureType type)
        {
            FigureModel newFigure;
            switch (type)
            {
                case FigureType.Circle: newFigure = new CircleModel(); break;
                case FigureType.Triangle: newFigure = new TriangleModel(); break;
                default: newFigure = new RectangleModel(); break;
            }

            newFigure.Left = loc.X;
            newFigure.Top = loc.Y;
            newFigure.Width = 0;
            newFigure.Height = 0;
            newFigure.FillColor = DefaultFillColor;
            newFigure.BorderColor = DefaultBorderColor;

            PictureModel.Figures.Add(newFigure);
            PictureModel.SelectedFigure = newFigure;
            newFigure.EditMode = EditMode.Creating;
        }

        public void FindAndSetMoveMode(PointModel loc)
        {
            var selected = PictureModel.Figures.LastOrDefault(f => f.IsInside(loc));
            PictureModel.SelectedFigure = selected;

            if (selected == null) return;

            selected.EditMode = EditMode.Moving;
            selected.Dx = loc.X - selected.Left;
            selected.Dy = loc.Y - selected.Top;
        }

        public void SetResizeMode(EditMode mode)
        {
            if (PictureModel.SelectedFigure != null)
            {
                PictureModel.SelectedFigure.EditMode = mode;
            }
        }

        public void ResetMode()
        {
            var sel = PictureModel.SelectedFigure;
            if (sel == null) return;

            sel.Normalize();
            sel.EditMode = EditMode.None;
        }

        public void UpdateMovingPoint(PointModel loc)
        {
            var sel = PictureModel.SelectedFigure;
            if (sel == null) return;

            switch (sel.EditMode)
            {
                case EditMode.Creating:
                case EditMode.ResizeBR:
                    sel.Width = loc.X - sel.Left;
                    sel.Height = loc.Y - sel.Top;
                    break;
                case EditMode.ResizeR:
                    sel.Width = loc.X - sel.Left;
                    break;
                case EditMode.ResizeB:
                    sel.Height = loc.Y - sel.Top;
                    break;
                case EditMode.Moving:
                    sel.Left = loc.X - sel.Dx;
                    sel.Top = loc.Y - sel.Dy;
                    break;
            }
        }

        public void DeleteFigure()
        {
            if (PictureModel.SelectedFigure == null) return;
            PictureModel.Figures.Remove(PictureModel.SelectedFigure);
            PictureModel.SelectedFigure = null;
        }

        public void SetFillColor(Color color)
        {
            if (PictureModel.SelectedFigure != null)
                PictureModel.SelectedFigure.FillColor = color;
        }

        // Изменение наложения: сдвиг вперед (выше по списку отрисовки)
        public void MoveForward()
        {
            var sel = PictureModel.SelectedFigure;
            if (sel == null) return;

            int index = PictureModel.Figures.IndexOf(sel);
            if (index < 0 || index >= PictureModel.Figures.Count - 1) return;

            PictureModel.Figures.RemoveAt(index);
            PictureModel.Figures.Insert(index + 1, sel);
        }

        // Сдвиг назад (ниже по списку)
        public void MoveBackward()
        {
            var sel = PictureModel.SelectedFigure;
            if (sel == null) return;

            int index = PictureModel.Figures.IndexOf(sel);
            if (index <= 0) return;

            PictureModel.Figures.RemoveAt(index);
            PictureModel.Figures.Insert(index - 1, sel);
        }

        public void Save(string fileName) => new FileService().Save(fileName, PictureModel);
        public void Open(string fileName) => PictureModel = new FileService().Open(fileName);

        public void CreateNewPicture()
        {
            PictureModel.Figures.Clear();
            PictureModel.SelectedFigure = null;
        }
    }
}