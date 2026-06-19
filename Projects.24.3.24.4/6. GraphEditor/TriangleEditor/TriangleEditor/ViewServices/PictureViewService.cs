using System.Drawing;
using TriangleEditor.Business.Models;
using TriangleEditor.Business.Services;

namespace TriangleEditor.ViewServices
{
    // Прослойка между формой и бизнес-слоём: хранит текущий рисунок и выделение,
    // проксирует операции в PictureService/FileService.
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

        public TriangleModel AddTriangle(int x, int y, int w, int h)
        {
            var triangle = _pictureService.AddTriangle(Picture, x, y, w, h);
            Selected = triangle;
            return triangle;
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
