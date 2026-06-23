using System.Drawing;
using GraphEditor.Business.Models;
using GraphEditor.Business.Services;

namespace GraphEditor.ViewServices
{
    public class PictureViewService
    {
        private readonly PictureService _pictureService = new PictureService();
        private readonly FileService _fileService = new FileService();

        public PictureModel Picture { get; private set; }
        public ShapeModel Selected { get; set; }

        public PictureViewService()
        {
            Picture = new PictureModel();
        }

        public void NewPicture()
        {
            Picture = new PictureModel();
            Selected = null;
        }

        public ShapeModel AddShape(ShapeKind kind, int x, int y, int w, int h)
        {
            var shape = _pictureService.AddShape(Picture, kind, x, y, w, h);
            Selected = shape;
            return shape;
        }

        public ShapeModel Pick(int x, int y)
        {
            Selected = _pictureService.HitTest(Picture, x, y);
            return Selected;
        }

        public void ResizeSelected(int w, int h)
        {
            _pictureService.Resize(Selected, w, h);
        }

        public void ColorSelected(Color color)
        {
            _pictureService.SetColor(Selected, color);
        }

        public void RemoveSelected()
        {
            _pictureService.Remove(Picture, Selected);
            Selected = null;
        }

        public void Save(string path)
        {
            _fileService.Save(Picture, path);
        }

        public void Load(string path)
        {
            Picture = _fileService.Load(path);
            _pictureService.EnsureNextId(Picture);
            Selected = null;
        }
    }
}
