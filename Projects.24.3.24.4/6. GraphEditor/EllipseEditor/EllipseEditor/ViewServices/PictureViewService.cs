using System.Drawing;
using EllipseEditor.Business.Models;
using EllipseEditor.Business.Services;

namespace EllipseEditor.ViewServices
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

        public EllipseModel AddEllipse(int x, int y, int w, int h)
        {
            var ellipse = _pictureService.AddEllipse(Picture, x, y, w, h);
            Selected = ellipse;
            return ellipse;
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
