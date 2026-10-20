using GraphEditor.Business.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GraphEditor.Business.Services
{
    public class PictureService
    {
        public PictureModel PictureModel { get; private set; } = new PictureModel();

        private Stack<PictureModel> _undoStack = new Stack<PictureModel>();
        private Stack<PictureModel> _redoStack = new Stack<PictureModel>();
        private RectangleModel _clipboardRectangle = null;

        public static Color DefaultFillColor { get; set; } = Color.Yellow;
        public static Color DefaultBorderColor { get; set; } = Color.Blue;

        public PictureService()
        {
            //Начальные фигуры
            PictureModel.Rectangles.Add(new RectangleModel { Left = 150, Top = 50, Width = 200, Height = 100, FillColor = Color.LightSkyBlue, });
            var newRectangle = new RectangleModel { Left = 200, Top = 100, Width = 200, Height = 150, };
            PictureModel.Rectangles.Add(newRectangle);
            PictureModel.SelectedRectangle = newRectangle;
            PictureModel.Rectangles.Add(new RectangleModel { Left = 250, Top = 150, Width = 200, Height = 200, FillColor = Color.DarkMagenta, });

            SaveState();
        }

        public void SaveState()
        {
            _undoStack.Push(ClonePicture(PictureModel));
            _redoStack.Clear();
        }

        public void Undo()
        {
            if (_undoStack.Count > 1)
            {
                _redoStack.Push(ClonePicture(PictureModel));
                _undoStack.Pop();
                PictureModel = ClonePicture(_undoStack.Peek());
            }
        }

        public void Redo()
        {
            if (_redoStack.Count > 0)
            {
                var state = _redoStack.Pop();
                _undoStack.Push(ClonePicture(state));
                PictureModel = ClonePicture(state);
            }
        }

        public bool CanUndo => _undoStack.Count > 1;
        public bool CanRedo => _redoStack.Count > 0;

        private PictureModel ClonePicture(PictureModel source)
        {
            var clonedRectangles = source.Rectangles.Select(r => new RectangleModel
            {
                Left = r.Left,
                Top = r.Top,
                Width = r.Width,
                Height = r.Height,
                FillColor = r.FillColor,
                BorderColor = r.BorderColor,
                Dx = r.Dx,
                Dy = r.Dy,
                EditMode = r.EditMode
            }).ToList();

            RectangleModel clonedSelected = null;
            if (source.SelectedRectangle != null)
            { 
                int selectedIndex = source.Rectangles.IndexOf(source.SelectedRectangle);
                if (selectedIndex >= 0 && selectedIndex < clonedRectangles.Count)
                {
                    clonedSelected = clonedRectangles[selectedIndex];
                }
            }

            return new PictureModel
            {
                Rectangles = clonedRectangles,
                SelectedRectangle = clonedSelected
            };
        }

        public void Copy()
        {
            if (PictureModel.SelectedRectangle != null)
            {
                _clipboardRectangle = new RectangleModel
                {
                    Left = PictureModel.SelectedRectangle.Left,
                    Top = PictureModel.SelectedRectangle.Top,
                    Width = PictureModel.SelectedRectangle.Width,
                    Height = PictureModel.SelectedRectangle.Height,
                    FillColor = PictureModel.SelectedRectangle.FillColor,
                    BorderColor = PictureModel.SelectedRectangle.BorderColor
                };
            }
        }

        public void Paste()
        {
            if (_clipboardRectangle != null)
            {
                SaveState();
                var newRect = new RectangleModel
                {
                    Left = _clipboardRectangle.Left + 20,
                    Top = _clipboardRectangle.Top + 20,
                    Width = _clipboardRectangle.Width,
                    Height = _clipboardRectangle.Height,
                    FillColor = _clipboardRectangle.FillColor,
                    BorderColor = _clipboardRectangle.BorderColor
                };
                PictureModel.Rectangles.Add(newRect);
                PictureModel.SelectedRectangle = newRect;
            }
        }

        public void CreateRectangleAndSetCreateMode(PointModel loc)
        {
            SaveState();
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
            PictureModel.SelectedRectangle = newRectangle;
            newRectangle.EditMode = EditMode.Creating;
        }

        public void FindAndSetMoveMode(PointModel loc)
        {
            var selectedRect = PictureModel.Rectangles.LastOrDefault(r => r.IsInside(loc));
            PictureModel.SelectedRectangle = selectedRect;

            if (selectedRect == null) return;

            selectedRect.EditMode = EditMode.Moving;
            selectedRect.Dx = loc.X - selectedRect.Left;
            selectedRect.Dy = loc.Y - selectedRect.Top;
        }

        public void SetResizeMode(EditMode mode)
        {
            if (PictureModel.SelectedRectangle != null)
                PictureModel.SelectedRectangle.EditMode = mode;
        }

        public void ResetMode()
        {
            var selectedRect = PictureModel.SelectedRectangle;
            if (selectedRect == null) return;

            selectedRect.Normalize();
            selectedRect.EditMode = EditMode.None;

            SaveState();
        }

        public void UpdateMovingPoint(PointModel loc)
        {
            var selectedRect = PictureModel.SelectedRectangle;
            if (selectedRect == null) return;

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
            if (PictureModel.SelectedRectangle == null) return;
            SaveState();
            PictureModel.Rectangles.Remove(PictureModel.SelectedRectangle);
            PictureModel.SelectedRectangle = null;
        }

        public void SetFillColor(Color color)
        {
            if (PictureModel.SelectedRectangle != null)
            {
                SaveState();
                PictureModel.SelectedRectangle.FillColor = color;
            }
        }

        public void MoveForward()
        {
            if (PictureModel.SelectedRectangle == null) return;
            var index = PictureModel.Rectangles.IndexOf(PictureModel.SelectedRectangle);
            if (index < 0 || index == PictureModel.Rectangles.Count - 1) return;

            SaveState();
            var rect = PictureModel.Rectangles[index];
            PictureModel.Rectangles[index] = PictureModel.Rectangles[index + 1];
            PictureModel.Rectangles[index + 1] = rect;
        }

        public void Save(string fileName) => new FileService().Save(fileName, PictureModel);

        public void Open(string fileName)
        {
            PictureModel = new FileService().Open(fileName);
            SaveState();
        }

        public void CreateNewPicture()
        {
            SaveState();
            PictureModel.Rectangles.Clear();
            PictureModel.SelectedRectangle = null;
        }

        public void UpdateSelectedRectangleProperties(int left, int top, int width, int height)
        {
            if (PictureModel.SelectedRectangle != null)
            {
                SaveState();
                PictureModel.SelectedRectangle.Left = left;
                PictureModel.SelectedRectangle.Top = top;
                PictureModel.SelectedRectangle.Width = width;
                PictureModel.SelectedRectangle.Height = height;
            }
        }
    }
}