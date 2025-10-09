using GraphEditor.Business.Models;

namespace GraphEditor.Business.Services {
    public class PictureService {
        public PictureModel PictureModel { get; private set; } = new PictureModel();

        public static Color DefaultFillColor = Color.Yellow;

        public static Color DefaultBorderColor = Color.Blue;

        public PictureService() {
            PictureModel.Rectangles.Add(new RectangleModel { Left = 100, Top = 50, Width = 200, Height = 150, FillColor = Color.LightCyan, });
            var newRectangle = new RectangleModel { Left = 200, Top = 100, Width = 200, Height = 150, };
            PictureModel.Rectangles.Add(newRectangle);
            PictureModel.SelectedRectangle = newRectangle;
            PictureModel.Rectangles.Add(new RectangleModel { Left = 300, Top = 200, Width = 200, Height = 150, FillColor = Color.Pink, });
        }

        public void CreateRectangle(PointModel loc) {
            var newRectangle = new RectangleModel { 
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
        
        public void SetText(string text) {
            if (PictureModel.SelectedRectangle != null) {
                PictureModel.SelectedRectangle.Text = text;
            }
        }
        
        public PictureModel GetPictureModel() {
            return PictureModel;
        }

        public void SetTextColor(Color color) {
            if (PictureModel.SelectedRectangle != null) {
                PictureModel.SelectedRectangle.TextColor = color;
            }
        }

        public void SetBorderWidth(float width) {
            if (PictureModel.SelectedRectangle != null) {
                PictureModel.SelectedRectangle.BorderWidth = width;
            }
        }

        public void GroupSelected(string? name = null) {
            if (PictureModel.SelectedRectangle == null) return;
            var g = new GroupModel { Name = name };
            g.Add(PictureModel.SelectedRectangle.Id);
            PictureModel.Groups.Add(g);
        }

        public void Ungroup(Guid groupId) {
            var group = PictureModel.Groups.FirstOrDefault(g => g.Id == groupId);
            if (group != null) PictureModel.Groups.Remove(group);
        }

        public void DeleteRectangle() {
            if (PictureModel.SelectedRectangle == null) {
                return;
            }

            PictureModel.Rectangles.Remove(PictureModel.SelectedRectangle);
            PictureModel.SelectedRectangle = null;
        }

        public void SetResizeMode(EditMode mode) {
            if (PictureModel.SelectedRectangle != null) PictureModel.SelectedRectangle.EditMode = mode;
        }

        public void SetMoveMode(PointModel loc) {
            var selectedRect = PictureModel.Rectangles.LastOrDefault(r => r.IsInside(loc));
            PictureModel.SelectedRectangle = selectedRect;

            if (selectedRect == null) {
                return;
            }

            selectedRect.Dx = loc.X - selectedRect.Left;
            selectedRect.Dy = loc.Y - selectedRect.Top;
            selectedRect.EditMode = EditMode.Moving;
        }

        public void ResetMode() {
            var selectedRect = PictureModel.SelectedRectangle;
            if (selectedRect != null) {
                selectedRect.Normalize();
                selectedRect.EditMode = EditMode.None;
            }
        }

        public void UpdateMovingPoint(PointModel loc) {
            var selectedRect = PictureModel.SelectedRectangle;
            if (selectedRect == null) {
                return;
            }

            switch (selectedRect.EditMode) {
                case EditMode.Creating:
                case EditMode.ResizeBR:
                    selectedRect.Right = loc.X;
                    selectedRect.Bottom = loc.Y;
                    break;

                case EditMode.ResizeR:
                    selectedRect.Right = loc.X;
                    break;

                case EditMode.Moving:
                    selectedRect.Left = loc.X - selectedRect.Dx;
                    selectedRect.Top = loc.Y - selectedRect.Dy;
                    break;
            }
        }

        public void OpenFile(string fileName) {
            var fileService = new FileService();
            PictureModel = fileService.OpenFile(fileName);
        }

        public void SaveToFile(string fileName) {
            var fileService = new FileService();
            fileService.SaveToFile(fileName, PictureModel);
        }

        public void CreateNewPicture() {
            PictureModel.Rectangles.Clear();
            PictureModel.SelectedRectangle = null;
        }

        public void SetFillColor(Color color) {
            if (PictureModel.SelectedRectangle != null) {
                PictureModel.SelectedRectangle.FillColor = color;
            }
        }

        public void MoveForward() {
            if (PictureModel.SelectedRectangle == null) {
                return;
            }

            var index = PictureModel.Rectangles.IndexOf(PictureModel.SelectedRectangle);
            if (index < 0 || index == PictureModel.Rectangles.Count - 1) {
                return;
            }

            (PictureModel.Rectangles[index + 1], PictureModel.Rectangles[index]) = (PictureModel.Rectangles[index], PictureModel.Rectangles[index + 1]);
        }
    }
}
