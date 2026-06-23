using GraphEditor.Business.Models;
using GraphEditor.Business.Models.Xml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GraphEditor.Business.Services
{
    public class PictureService
    {
        public PictureModel PictureModel { get; private set; } = new PictureModel();
        private readonly Stack<PictureModel> UndoStack = new Stack<PictureModel>();
        private readonly Stack<PictureModel> RedoStack = new Stack<PictureModel>();

        public static Color DefaultFillColor { get; set; } = Color.Yellow;
        public static Color DefaultBorderColor { get; set; } = Color.Blue;

        public PictureService()
        {
            PictureModel.Rectangles.Add(new RectangleModel { Left = 150, Top = 50, Width = 200, Height = 100, FillColor = Color.LightSkyBlue });
            var newRectangle = new RectangleModel { Left = 200, Top = 100, Width = 200, Height = 150 };
            PictureModel.Rectangles.Add(newRectangle);
            PictureModel.SelectedRectangle = newRectangle;
            PictureModel.Rectangles.Add(new RectangleModel { Left = 250, Top = 150, Width = 200, Height = 200, FillColor = Color.DarkMagenta });
            SaveState(); 
        }

        private PictureModel ClonePictureModel()
        {
            var clone = new PictureModel
            {
                Rectangles = PictureModel.Rectangles.Select(r => new RectangleModel
                {
                    Left = r.Left,
                    Top = r.Top,
                    Width = r.Width,
                    Height = r.Height,
                    Dx = r.Dx,
                    Dy = r.Dy,
                    EditMode = r.EditMode,
                    FillColor = r.FillColor,
                    BorderColor = r.BorderColor
                }).ToList(),
                SelectedRectangle = null
            };

            if (PictureModel.SelectedRectangle != null)
            {
                var index = PictureModel.Rectangles.IndexOf(PictureModel.SelectedRectangle);
                if (index >= 0)
                {
                    clone.SelectedRectangle = clone.Rectangles[index];
                }
            }

            return clone;
        }

        private void SaveState()
        {
            UndoStack.Push(ClonePictureModel());
            RedoStack.Clear(); 
        }

        public bool CanUndo => UndoStack.Count > 0;
        public bool CanRedo => RedoStack.Count > 0;

        public void Undo()
        {
            if (!CanUndo) return;

            RedoStack.Push(ClonePictureModel());
            PictureModel = UndoStack.Pop();
        }

        public void Redo()
        {
            if (!CanRedo) return;

            UndoStack.Push(ClonePictureModel());
            PictureModel = RedoStack.Pop();
        }

        public void SetMoveMode(PointModel point)
        {
            SaveState();
            var selectedRect = PictureModel.Rectangles.LastOrDefault(r => r.IsInside(point));
            PictureModel.SelectedRectangle = selectedRect;

            if (selectedRect == null)
            {
                return;
            }

            selectedRect.EditMode = EditMode.Moving;
            selectedRect.Dx = point.X - selectedRect.Left;
            selectedRect.Dy = point.Y - selectedRect.Top;
        }

        public void SetResizeMode(EditMode mode)
        {
            SaveState();
            PictureModel.SelectedRectangle.EditMode = mode;
        }

        public void SetCreateMode(PointModel point)
        {
            SaveState();
            var newRectangle = new RectangleModel
            {
                Left = point.X,
                Top = point.Y,
                Width = 0,
                Height = 0,
                FillColor = DefaultFillColor,
                BorderColor = DefaultBorderColor,
            };

            PictureModel.Rectangles.Add(newRectangle);
            PictureModel.SelectedRectangle = newRectangle;
            newRectangle.EditMode = EditMode.Creating;
        }

        public void ResetMode()
        {
            var selectedRect = PictureModel.SelectedRectangle;
            if (selectedRect != null)
            {
                selectedRect.Normalize();
                selectedRect.EditMode = EditMode.None;
            }
        }

        public void UpdateMovingPoint(PointModel loc)
        {
            var selectedRect = PictureModel.SelectedRectangle;
            if (selectedRect == null)
            {
                return;
            }

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
            if (PictureModel.SelectedRectangle == null)
            {
                return;
            }

            SaveState();
            PictureModel.Rectangles.Remove(PictureModel.SelectedRectangle);
            PictureModel.SelectedRectangle = null;
        }

        public void Save(string fileName)
        {
            new FileService().Save(fileName, PictureModel);
        }

        public void Open(string fileName)
        {
            SaveState();
            PictureModel = new FileService().Open(fileName);
        }

        public void CreateNewPicture()
        {
            SaveState();
            PictureModel.Rectangles.Clear();
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
            if (PictureModel.SelectedRectangle == null)
            {
                return;
            }

            SaveState();
            var index = PictureModel.Rectangles.IndexOf(PictureModel.SelectedRectangle);
            if (index < 0 || index == PictureModel.Rectangles.Count - 1)
            {
                return;
            }

            var rect = PictureModel.Rectangles[index];
            PictureModel.Rectangles[index] = PictureModel.Rectangles[index + 1];
            PictureModel.Rectangles[index + 1] = rect;
        }
    }
}