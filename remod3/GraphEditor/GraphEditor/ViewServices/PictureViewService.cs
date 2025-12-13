using System.Drawing.Imaging;
using GraphEditor.Business.Models;
using GraphEditor.Business.Services;
using GraphEditor.ViewModels;

namespace GraphEditor.ViewServices;

internal class PictureViewService {
    private readonly PictureService _businessService = new();

    private PictureViewModel _viewModel;

    public PictureViewService() {
        _viewModel = FromBusiness(_businessService.PictureModel);
        FileName = string.Empty;
    }
    
    public PictureModel PictureModel => _businessService.PictureModel;

    public bool CreateMode { get; private set; }

    public bool CanDelete => !CreateMode && PictureModel.SelectedRectangleIds.Any();

    public string FileName { get; private set; }

    public PictureModel GetPictureModel() => _businessService.GetPictureModel();
    
    public void Paint(Graphics g) => Painter.Paint(g, _viewModel, true);

    public Cursor? GetCursor(Point loc) {
        var activeMarker = _viewModel.Markers.FirstOrDefault(m => m.IsActive && IsInside(loc, m.Rectangle));
        if (activeMarker != null) {
            return activeMarker.Cursor;
        }

        var activeRect = _viewModel.Rectangles.LastOrDefault(r => IsInside(loc, r.Rectangle));
        return activeRect != null ? Cursors.SizeAll : Cursors.Default;
    }

    public void MouseDown(Point loc, bool additiveSelection) {
        if (CreateMode) {
            _businessService.CreateRectangle(ToModel(loc));
            RefreshViewModel();
            return;
        }

        var activeMarker = _viewModel.Markers.FirstOrDefault(m => m.IsActive && IsInside(loc, m.Rectangle));
        if (activeMarker != null) {
            _businessService.SetResizeMode(activeMarker.EditMode);
            RefreshViewModel();
            return;
        }

        _businessService.SetMoveMode(ToModel(loc), additiveSelection);
        RefreshViewModel();
    }

    public void MouseMove(Point loc) {
        _businessService.UpdateMovingPoint(ToModel(loc));
        RefreshViewModel();
    }

    public void MouseUp() {
        _businessService.ResetMode();
        CreateMode = false;
        RefreshViewModel();
    }

    public void CreateButtonClick() {
        CreateMode = !CreateMode;
    }

    public void DeleteButtonClick() {
        _businessService.DeleteRectangle();
        RefreshViewModel();
    }

    public void OpenFile(string fileName) {
        _businessService.OpenFile(fileName);
        RefreshViewModel();
        FileName = fileName;
    }

    public void SaveToFile(string fileName) {
        _businessService.SaveToFile(fileName);
        FileName = fileName;
    }

    public void SaveToFile() {
        if (!string.IsNullOrEmpty(FileName)) {
            _businessService.SaveToFile(FileName);
        }
    }

    public void CreateNewPicture() {
        _businessService.CreateNewPicture();
        RefreshViewModel();
        FileName = string.Empty;
    }

    public void Export(string fileName, Rectangle size, Color backColor) {
        using var bmp = new Bitmap(size.Width, size.Height);
        using (var g = Graphics.FromImage(bmp)) {
            g.Clear(backColor);
            Painter.Paint(g, _viewModel, false);
        }

        bmp.Save(fileName, ImageFormat.Png);
    }

    public Color GetFillColor() => _viewModel.SelectedRectangle?.FillColor ?? PictureService.DefaultFillColor;

    public void SetFillColor(Color color) {
        _businessService.SetFillColor(color);
        RefreshViewModel();
    }

    public void MoveForward() {
        _businessService.MoveForward();
        RefreshViewModel();
    }
        
    public void SetText(string text) {
        _businessService.SetText(text);
        RefreshViewModel();
    }

    public bool GroupSelected(string? name = null) {
        var result = _businessService.GroupSelected(name);
        RefreshViewModel();
        return result;
    }

    public void Ungroup(Guid groupId) {
        _businessService.Ungroup(groupId);
        RefreshViewModel();
    }

    public bool TryBeginTextEdit(Point location, out Rectangle bounds, out string text, out string fontFamily, out float fontSize, out Color textColor, out TextAlign textAlign, out Color fillColor) {
        var rect = _businessService.SelectAt(ToModel(location), additiveSelection: false, includeGroup: false);
        RefreshViewModel();

        if (rect == null) {
            bounds = default;
            text = string.Empty;
            fontFamily = string.Empty;
            fontSize = 0;
            textColor = Color.Black;
            textAlign = TextAlign.Left;
            fillColor = Color.White;
            return false;
        }

        var viewRect = _viewModel.Rectangles.FirstOrDefault(r => r.Id == rect.Id);
        if (viewRect == null) {
            bounds = default;
            text = string.Empty;
            fontFamily = string.Empty;
            fontSize = 0;
            textColor = Color.Black;
            textAlign = TextAlign.Left;
            fillColor = Color.White;
            return false;
        }

        bounds = viewRect.Rectangle;
        text = rect.Text ?? string.Empty;
        fontFamily = viewRect.FontFamily;
        fontSize = viewRect.FontSize;
        textColor = viewRect.TextColor;
        textAlign = viewRect.TextAlign;
        fillColor = viewRect.FillColor;
        return true;
    }

    private static bool IsInside(Point p, Rectangle rect) =>
        p.X >= rect.Left && p.X <= rect.Right &&
        p.Y >= rect.Top && p.Y <= rect.Bottom;

    private static PointModel ToModel(Point loc) => new() { X = loc.X, Y = loc.Y };

    private static PictureViewModel FromBusiness(PictureModel model) => PictureViewModel.FromModel(model);

    private void RefreshViewModel() {
        _viewModel = FromBusiness(_businessService.PictureModel);
    }
}