using GraphEditor.Business.Models;
using System;
using System.Drawing;
using System.Linq;

namespace GraphEditor.Business.Services
{
    public class PictureService
    {
        public PictureModel PictureModel { get; private set; } = new PictureModel();
        public static Color DefaultFillColor { get; set; } = Color.Yellow;
        public static Color DefaultBorderColor { get; set; } = Color.Blue;
        public FigureType CurrentFigureType { get; set; } = FigureType.Rectangle;

        public PictureService()
        {
            PictureModel.Figures.Add(new FigureModel { Left = 150, Top = 50, Width = 200, Height = 100, FillColor = Color.LightSkyBlue, Type = FigureType.Rectangle });
            var newFigure = new FigureModel { Left = 200, Top = 100, Width = 200, Height = 150, Type = FigureType.Ellipse };
            PictureModel.Figures.Add(newFigure);
            PictureModel.SelectedFigure = newFigure;
            PictureModel.Figures.Add(new FigureModel { Left = 250, Top = 150, Width = 200, Height = 200, FillColor = Color.DarkMagenta, Type = FigureType.RoundedRectangle });
        }

        public void SetFigureType(FigureType type) => CurrentFigureType = type;

        public void CreateAndSetCreateMode(PointModel loc)
        {
            var newFigure = new FigureModel
            {
                Left = loc.X,
                Top = loc.Y,
                Width = 0,
                Height = 0,
                FillColor = DefaultFillColor,
                BorderColor = DefaultBorderColor,
                Type = CurrentFigureType,
            };
            PictureModel.Figures.Add(newFigure);
            PictureModel.SelectedFigure = newFigure;
            newFigure.EditMode = EditMode.Creating;
        }

        public void CreateNewPicture()
        {
            PictureModel.Figures.Clear();
            PictureModel.SelectedFigure = null;
        }

        public void DeleteRectangle()
        {
            if (PictureModel.SelectedFigure == null) return;
            PictureModel.Figures.Remove(PictureModel.SelectedFigure);
            PictureModel.SelectedFigure = null;
        }

        public void MoveForward()
        {
            if (PictureModel.SelectedFigure == null) return;
            var selectedIndex = PictureModel.Figures.IndexOf(PictureModel.SelectedFigure);
            if (selectedIndex < 0 || selectedIndex == PictureModel.Figures.Count - 1) return;
            var fig = PictureModel.Figures[selectedIndex + 1];
            PictureModel.Figures[selectedIndex + 1] = PictureModel.Figures[selectedIndex];
            PictureModel.Figures[selectedIndex] = fig;
        }

        public void Open(string fileName) => PictureModel = new FileService().Open(fileName);

        public void ResetMode()
        {
            var selectedFig = PictureModel.SelectedFigure;
            if (selectedFig != null)
            {
                selectedFig.Normalize();
                selectedFig.EditMode = EditMode.None;
            }
        }

        public void Save(string fileName) => new FileService().Save(fileName, PictureModel);

        public void SelectAndSetMoveMode(PointModel loc)
        {
            var selectedFig = PictureModel.Figures.LastOrDefault(r => r.IsInside(loc));
            PictureModel.SelectedFigure = selectedFig;
            if (selectedFig == null) return;
            selectedFig.EditMode = EditMode.Moving;
            selectedFig.Dx = loc.X - selectedFig.Left;
            selectedFig.Dy = loc.Y - selectedFig.Top;
        }

        public void SetFillColor(Color color)
        {
            if (PictureModel.SelectedFigure != null)
                PictureModel.SelectedFigure.FillColor = color;
        }

        public void SetResizeMode(EditMode mode) => PictureModel.SelectedFigure.EditMode = mode;

        public void UpdateMovingPoint(PointModel loc)
        {
            var selectedFig = PictureModel.SelectedFigure;
            if (selectedFig == null) return;

            switch (selectedFig.EditMode)
            {
                case EditMode.Creating:
                case EditMode.ResizeBR:
                    selectedFig.Width = loc.X - selectedFig.Left;
                    selectedFig.Height = loc.Y - selectedFig.Top;
                    break;
                case EditMode.ResizeR:
                    selectedFig.Width = loc.X - selectedFig.Left;
                    break;
                case EditMode.Moving:
                    selectedFig.Left = loc.X - selectedFig.Dx;
                    selectedFig.Top = loc.Y - selectedFig.Dy;
                    break;
            }
        }
    }
}