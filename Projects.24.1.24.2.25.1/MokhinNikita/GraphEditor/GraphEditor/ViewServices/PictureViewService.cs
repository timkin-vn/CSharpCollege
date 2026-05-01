using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GraphEditor.Business.Models;
using GraphEditor.Business.Services;
using GraphEditor.ViewModels;

namespace GraphEditor.ViewServices
{
    public class PictureViewService
    {
        private readonly PictureService _service = new PictureService();

        private PictureViewModel _pictureViewModel;

        public bool CreateMode {  get; set; }
        public bool CanDelete => !CreateMode && _service.PictureModel.Selected != null;

        public string FileName { get; set; }
        public void CreateButtonClicked()
        {
            CreateMode = !CreateMode;
        }
        public PictureViewService()
        {
            LoadViewModel();
        }
        public void Paint(Graphics g)
        {
            new Painter().Paint(g, _pictureViewModel);
        }
        public void MouseDown(Point loc)
        {
            
            if(CreateMode)
            {
                _service.CreateAndSetCreateMode(ToModel(loc));
                LoadViewModel();
                return;
            }
            if(_pictureViewModel != null)
            {
                var activeMarker = _pictureViewModel.Markers.FirstOrDefault(m => m.IsActive && IsInside(loc, m.Rectangle));
                if(activeMarker != null)
                {
                    _service.SetResizeMode(activeMarker.EditMode);
                    LoadViewModel();
                    return;
                }
            }
            _service.SelectAndSetMoveMode(ToModel(loc));
            LoadViewModel();
        }
        public void MouseMove(Point loc)
        {
            _service.UpdateMovingPoint(ToModel(loc));
            LoadViewModel();
        }
        public void MouseUp()
        {
            _service.ResetMode();
            CreateMode = false;
            LoadViewModel();
        }
        public Color GetCurrentFillColor()
        {
            return _pictureViewModel?.Selected?.FillColor ?? PictureService.DefaultFillColor;
        }
        public Color GetCurrentBorderColor()
        {
            return _pictureViewModel?.Selected?.BorderColor ?? PictureService.DefaultBorderColor;
        }
        public int GetCurrentBorderWidth()
        {
            return _pictureViewModel?.Selected?.BorderWidth ?? PictureService.DefaultBorderWidth;
        }
        private void LoadViewModel()
        {
            var model = _service.PictureModel;
            _pictureViewModel = new PictureViewModel
            {
                Rectangles = (from r in model.Rectangles select ReactangleViewModel.FromBusiness(r)).ToList(),
            };
            if(model.Selected != null)
            {
                var index = model.Rectangles.IndexOf(model.Selected);
                _pictureViewModel.Selected = _pictureViewModel.Rectangles[index];
            }
        }
        public bool IsInside(Point loc, Rectangle r) => loc.X >= r.Left && loc.X <= r.Right && loc.Y >= r.Top && loc.Y <= r.Bottom;
        private PointModel ToModel(Point loc) => new PointModel
        {
            X = loc.X,
            Y = loc.Y,
        };
        public void DeleteButtonClicked()
        {
            _service.DeleteRectangle();
            LoadViewModel();
        }
        public Cursor GetCursor(Point loc)
        {
            if(_pictureViewModel == null)
            {
                return Cursors.Default;
            }
            if(CreateMode)
            {
                return Cursors.Cross;
            }
            var activeMarker = _pictureViewModel.Markers.FirstOrDefault(m => m.IsActive && IsInside(loc, m.Rectangle));
            if(activeMarker != null)
            {
                return activeMarker.Cursor;
            }
            var activeRect = _pictureViewModel.Rectangles.FirstOrDefault(r => IsInside(loc, r.Rectangle));
            if(activeRect != null)
            {
                return Cursors.SizeAll;
            }
            return Cursors.Default;
        }
        public void SetFillColor(Color color)
        {
            if(_pictureViewModel?.Selected == null)
            {
                return;
            }
            _service.SetFillColor(color);
            LoadViewModel();
        }
        public void SetBorderColor(Color color)
        {
            if (_pictureViewModel?.Selected == null) return;
            _service.SetBorderColor(color);
            LoadViewModel();
        }
        public void SetBorderWidth(int width)
        {
            if (_pictureViewModel?.Selected == null) return;
            _service.SetBorderWidth(width);
            LoadViewModel();
        }
        public void MoveForward()
        {
            _service.MoveForward();
            LoadViewModel();
        }
        public void MoveBackward()
        {
            _service.MoveBackward();
            LoadViewModel();
        }
        public void MoveForeground()
        {
            _service.MoveForeground();
            LoadViewModel();
        }
        public void MoveBackground()
        {
            _service.MoveBackground();
            LoadViewModel();
        }
        public void Save(string filename)
        {
            _service.Save(filename);
        }
        public void Open(string filename)
        {
            _service.Open(filename);
            FileName = filename;
            LoadViewModel();
        }
        public void Save()
        {
            Save(FileName);
            LoadViewModel();
        }
        public void CreateNewPicture()
        {
            _service.CreateNewPicture();
        }
        public void Export(string filename, Rectangle size, Color backColor)
        {
            using(var bmp = new Bitmap(size.Width, size.Height))
            {
                using(var g = Graphics.FromImage(bmp))
                {
                    g.Clear(backColor);
                    new Painter().Paint(g, _pictureViewModel, false);
                }
                bmp.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
            }
        }
    }
}
