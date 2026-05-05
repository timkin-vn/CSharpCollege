using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphEditor.Business.Models;

namespace GraphEditor.Business.Services
{
    public class PictureService
    {
        public PictureModel PictureModel { get; private set; } = new PictureModel();

        public static Color DefaultFillColor { get; set; } = Color.Red;

        public static Color DefaultBorderColor { get; set; } = Color.Yellow;

        public static int DefaultBorderWidth { get; set; } = 3;
        public PictureService()
        {
            PictureModel.Rectangles.Add(new RectangleModel
            {
                Width = 200,
                Height = 200,
                Top = 200,
                Left = 200,
                FillColor = Color.Beige,
                Figure = FigureType.Ellipse,
            });
            var rect = new RectangleModel
            {
                Width = 300,
                Height = 200,
                Top = 100,
                Left = 250,
            };
            PictureModel.Rectangles.Add(rect);
            PictureModel.Selected = rect;
        }

        public void SelectAndSetMoveMode(PointModel loc)
        {
            var selectedRectangle = PictureModel.Rectangles.LastOrDefault(r => r.IsInside(loc));
            PictureModel.Selected = selectedRectangle;
            if(selectedRectangle == null)
            {
                return;
            }
            selectedRectangle.EditMode = EditMode.Moving;
            selectedRectangle.Dx = loc.X - selectedRectangle.Left;
            selectedRectangle.Dy = loc.Y - selectedRectangle.Top;
        }

        public void SetResizeMode(EditMode mode)
        {
            PictureModel.Selected.EditMode = mode;
        }
        public void CreateAndSetCreateMode(PointModel loc, FigureType type)
        {
            var newRectangle = new RectangleModel
            {
                Left = loc.X,
                Top = loc.Y,
                Width = 0,
                Height = 0,
                Figure = type,
            };
            PictureModel.Rectangles.Add(newRectangle);
            PictureModel.Selected = newRectangle;
            newRectangle.EditMode = EditMode.Creating;
        }
        public void ResetMode()
        {
            var selectedRect = PictureModel.Selected;
            if (selectedRect != null)
            {
                selectedRect.Normalize();
                selectedRect.EditMode = EditMode.None;
            }
        }
        public void UpdateMovingPoint(PointModel loc)
        {
            var selected = PictureModel.Selected;
            if (selected == null)
            {
                return;
            }
            switch(selected.EditMode)
            {
                case EditMode.Creating:
                case EditMode.ResizeBR:
                    selected.Width = loc.X - selected.Left;
                    selected.Height = loc.Y - selected.Top;
                    break;
                case EditMode.ResizeR:
                    selected.Width = loc.X - selected.Left;
                    break;
                case EditMode.Moving:
                    selected.Top = loc.Y - selected.Dy;
                    selected.Left  = loc.X - selected.Dx;
                    break;
                case EditMode.ResizeT:
                    selected.Height = selected.Bottom - loc.Y;
                    selected.Top = loc.Y;
                    break;
            }
        }
        public void DeleteRectangle()
        {
            if(PictureModel.Selected == null)
            {
                return;
            }
            PictureModel.Rectangles.Remove(PictureModel.Selected);
            PictureModel.Selected = null;
        }
        public void SetFillColor(Color color)
        {
            if(PictureModel.Selected != null)
            {
                PictureModel.Selected.FillColor = color;
            }
        }
        public void SetBorderColor(Color color)
        {
            if(PictureModel.Selected != null)
            {
                PictureModel.Selected.BorderColor = color;
            }
        }
        public void SetBorderWidth(int width)
        {
            if(PictureModel.Selected != null)
            {
                PictureModel.Selected.BorderWidth = width;
            }
        }
        public void MoveForward()
        {
            if(PictureModel.Selected == null)
            {
                return;
            }

            var selectedIndex = PictureModel.Rectangles.IndexOf(PictureModel.Selected);
            if(selectedIndex < 0 || selectedIndex == PictureModel.Rectangles.Count - 1)
            {
                return;
            }

            var rect = PictureModel.Rectangles[selectedIndex + 1];
            PictureModel.Rectangles[selectedIndex + 1] = PictureModel.Rectangles[selectedIndex];
            PictureModel.Rectangles[selectedIndex] = rect;

        }
        public void MoveBackward()
        {
            if (PictureModel.Selected == null)
            {
                return;
            }
            var selectedIndex = PictureModel.Rectangles.IndexOf(PictureModel.Selected);
            if(selectedIndex <= 0) return;
            var rect = PictureModel.Rectangles[selectedIndex - 1];
            PictureModel.Rectangles[selectedIndex - 1] = PictureModel.Rectangles[selectedIndex];
            PictureModel.Rectangles[selectedIndex]= rect;
        }
        public void MoveForeground()
        {
            if(PictureModel.Selected == null) return;

            var selectedIndex = PictureModel.Rectangles.IndexOf(PictureModel.Selected);
            if(selectedIndex < 0 || selectedIndex == PictureModel.Rectangles.Count - 1) return;
            while(selectedIndex != PictureModel.Rectangles.Count - 1)
            {
                var rect = PictureModel.Rectangles[selectedIndex + 1];
                PictureModel.Rectangles[selectedIndex + 1] = PictureModel.Rectangles[selectedIndex];
                PictureModel.Rectangles[selectedIndex] = rect;
                selectedIndex++;
            }
        }
        public void MoveBackground()
        {
            if (PictureModel.Selected == null) { return; }
            var selectedIndex = PictureModel.Rectangles.IndexOf(PictureModel.Selected);
            if (selectedIndex <= 0) return;
            while(selectedIndex != 0)
            {
                var rect = PictureModel.Rectangles[selectedIndex - 1];
                PictureModel.Rectangles[selectedIndex - 1] = PictureModel.Rectangles[selectedIndex];
                PictureModel.Rectangles[selectedIndex] = rect;
                selectedIndex--;
            }
        }
        public void Save(string filename)
        {
            new FileService().Save(filename, PictureModel);
        }
        public void Open(string filename)
        {
            PictureModel = new FileService().Open(filename);
        }
        public void CreateNewPicture()
        {
            PictureModel.Rectangles.Clear();
            PictureModel.Selected = null;
        }
    }
}
