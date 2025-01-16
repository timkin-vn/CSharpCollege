using GraphEditor.Business.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphEditor.Business.Services
{
    public class GraphEditorService
    {
        private PictureModel _picture = new PictureModel();

        public void SetCreateMode(Point location)
        {
            _picture.EditMode = EditMode.Creating;
            var newRectangle = new RectangleModel
            {
                Left = location.X,
                Top = location.Y,
                Width = 0,
                Height = 0,
            };

            _picture.AddNewRectangle(newRectangle);
        }

        public void SetNewLocation(Point location)
        {
            var rect = _picture.SelectedRectangle;
            if (rect == null)
            {
                return;
            }

            switch (_picture.EditMode)
            {
                case EditMode.Moving:
                    rect.Left = location.X - _picture.Diff.X;
                    rect.Top = location.Y - _picture.Diff.Y;
                    break;

                case EditMode.Creating:
                case EditMode.ResizeBR:
                    rect.Right = location.X;
                    rect.Bottom = location.Y;
                    break;
            }
        }

        public void ResetMode()
        {
            _picture.SelectedRectangle?.Normalize();
            _picture.EditMode = EditMode.None;
        }

        public IEnumerable<Rectangle> GetRectangles()
        {
            return _picture.Rectangles.Select(r => r.GetRectangle()).ToList();
        }

        public IEnumerable<Marker> GetMarkers()
        {
            var rect = _picture.SelectedRectangle;
            if (rect == null)
            {
                return Enumerable.Empty<Marker>();
            }

            return new[]
            {
                new Marker
                {
                    Rectangle = new Rectangle{ X = rect.Left - 5, Y = rect.Top - 5, Width = 10, Height = 10, },
                    IsActive = false,
                    EditMode = EditMode.ResizeTL,
                    Cursor = Cursors.SizeNWSE,
                },
                new Marker
                {
                    Rectangle = new Rectangle{ X = (rect.Left + rect.Right) / 2 - 5, Y = rect.Top - 5, Width = 10, Height = 10, },
                    IsActive = false,
                    EditMode = EditMode.ResizeT,
                    Cursor = Cursors.SizeNS,
                },
                new Marker
                {
                    Rectangle = new Rectangle{ X = rect.Right - 5, Y = rect.Top - 5, Width = 10, Height = 10, },
                    IsActive = false,
                    EditMode = EditMode.ResizeTR,
                    Cursor = Cursors.SizeNESW,
                },
                new Marker
                {
                    Rectangle = new Rectangle{ X = rect.Right - 5, Y = (rect.Top + rect.Bottom) / 2 - 5, Width = 10, Height = 10, },
                    IsActive = false,
                    EditMode = EditMode.ResizeR,
                    Cursor = Cursors.SizeWE,
                },
                new Marker
                {
                    Rectangle = new Rectangle{ X = rect.Right - 5, Y = rect.Bottom - 5, Width = 10, Height = 10, },
                    IsActive = true,
                    EditMode = EditMode.ResizeBR,
                    Cursor = Cursors.SizeNWSE,
                },
                new Marker
                {
                    Rectangle = new Rectangle{ X = (rect.Left + rect.Right) / 2 - 5, Y = rect.Bottom - 5, Width = 10, Height = 10, },
                    IsActive = false,
                    EditMode = EditMode.ResizeB,
                    Cursor = Cursors.SizeNS,
                },
                new Marker
                {
                    Rectangle = new Rectangle{ X = rect.Left - 5, Y = rect.Bottom - 5, Width = 10, Height = 10, },
                    IsActive = false,
                    EditMode = EditMode.ResizeBL,
                    Cursor = Cursors.SizeNESW,
                },
                new Marker
                {
                    Rectangle = new Rectangle{ X = rect.Left - 5, Y = (rect.Top + rect.Bottom) / 2 - 5, Width = 10, Height = 10, },
                    IsActive = false,
                    EditMode = EditMode.ResizeL,
                    Cursor = Cursors.SizeWE,
                },
            };
        }
    }
}
