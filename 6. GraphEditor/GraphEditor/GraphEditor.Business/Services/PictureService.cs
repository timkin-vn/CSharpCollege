using GraphEditor.Business.Models;
using System.Drawing;
using System.Linq;

namespace GraphEditor.Business.Services
{
    public class PictureService
    {
        public PictureModel PictureModel { get; private set; } = new PictureModel();

        public PictureService()
        {
            PictureModel.Figures.Add(new RectangleModel
            {
                Left = 100,
                Top = 50,
                Width = 200,
                Height = 150,
                FillColor = Color.LightCyan
            });

            var circle = new CircleModel
            {
                Left = 200,
                Top = 100,
                Width = 200,
                Height = 200,
                FillColor = Color.LightGreen
            };
            PictureModel.Figures.Add(circle);

            PictureModel.Figures.Add(new TriangleModel
            {
                Left = 300,
                Top = 200,
                Width = 200,
                Height = 150,
                FillColor = Color.Pink
            });

            PictureModel.SelectedFigure = circle;
            PictureModel.CurrentFigureType = FigureType.Rectangle;
        }

        public void CreateFigure(PointModel loc)
        {
            FigureModel newFigure;

            if (PictureModel.CurrentFigureType == FigureType.Rectangle)
            {
                newFigure = new RectangleModel { Left = loc.X, Top = loc.Y, Width = 0, Height = 0 };
            }
            else if (PictureModel.CurrentFigureType == FigureType.Circle)
            {
                newFigure = new CircleModel { Left = loc.X, Top = loc.Y, Width = 0, Height = 0 };
            }
            else if (PictureModel.CurrentFigureType == FigureType.Triangle)
            {
                newFigure = new TriangleModel { Left = loc.X, Top = loc.Y, Width = 0, Height = 0 };
            }
            else
            {
                newFigure = new RectangleModel { Left = loc.X, Top = loc.Y, Width = 0, Height = 0 };
            }

            PictureModel.Figures.Add(newFigure);
            PictureModel.SelectedFigure = newFigure;
            newFigure.EditMode = EditMode.Creating;
        }

        public void SetFigureType(FigureType type)
        {
            PictureModel.CurrentFigureType = type;
        }

        public void DeleteFigure()
        {
            if (PictureModel.SelectedFigure == null) return;
            PictureModel.Figures.Remove(PictureModel.SelectedFigure);
            PictureModel.SelectedFigure = null;
        }

        public void SetResizeMode(EditMode mode)
        {
            if (PictureModel.SelectedFigure != null)
                PictureModel.SelectedFigure.EditMode = mode;
        }

        public void SetMoveMode(PointModel loc)
        {
            FigureModel selectedFig = null;
            for (int i = PictureModel.Figures.Count - 1; i >= 0; i--)
            {
                if (PictureModel.Figures[i].IsInside(loc))
                {
                    selectedFig = PictureModel.Figures[i];
                    break;
                }
            }

            PictureModel.SelectedFigure = selectedFig;

            if (selectedFig == null) return;

            selectedFig.Dx = loc.X - selectedFig.Left;
            selectedFig.Dy = loc.Y - selectedFig.Top;
            selectedFig.EditMode = EditMode.Moving;
        }

        public void ResetMode()
        {
            var selectedFig = PictureModel.SelectedFigure;
            if (selectedFig != null)
            {
                selectedFig.Normalize();
                selectedFig.EditMode = EditMode.None;
            }
        }

        public void UpdateMovingPoint(PointModel loc)
        {
            var selectedFig = PictureModel.SelectedFigure;
            if (selectedFig == null) return;

            switch (selectedFig.EditMode)
            {
                case EditMode.Creating:
                case EditMode.ResizeBR:
                    selectedFig.Right = loc.X;
                    selectedFig.Bottom = loc.Y;
                    break;

                case EditMode.ResizeR:
                    selectedFig.Right = loc.X;
                    break;

                case EditMode.ResizeB:
                    selectedFig.Bottom = loc.Y;
                    break;

                case EditMode.ResizeL:
                    selectedFig.Left = loc.X;
                    break;

                case EditMode.ResizeT:
                    selectedFig.Top = loc.Y;
                    break;

                case EditMode.ResizeTR:
                    selectedFig.Right = loc.X;
                    selectedFig.Top = loc.Y;
                    break;

                case EditMode.ResizeTL:
                    selectedFig.Left = loc.X;
                    selectedFig.Top = loc.Y;
                    break;

                case EditMode.ResizeBL:
                    selectedFig.Left = loc.X;
                    selectedFig.Bottom = loc.Y;
                    break;

                case EditMode.Moving:
                    selectedFig.Left = loc.X - selectedFig.Dx;
                    selectedFig.Top = loc.Y - selectedFig.Dy;
                    break;
            }
        }

        public void OpenFile(string fileName)
        {
            var fileService = new FileService();
            PictureModel = fileService.OpenFile(fileName);
        }

        public void SaveToFile(string fileName)
        {
            var fileService = new FileService();
            fileService.SaveToFile(fileName, PictureModel);
        }

        public void CreateNewPicture()
        {
            PictureModel.Figures.Clear();
            PictureModel.SelectedFigure = null;
        }

        public void ChangeSelectedFillColor(Color newColor)
        {
            if (PictureModel.SelectedFigure != null)
            {
                PictureModel.SelectedFigure.ChangeFillColor(newColor);
            }
        }

        public void ChangeSelectedBorderColor(Color newColor)
        {
            if (PictureModel.SelectedFigure != null)
            {
                PictureModel.SelectedFigure.ChangeBorderColor(newColor);
            }
        }
    }
}