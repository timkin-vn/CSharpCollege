using GraphEditor.Business.Models;
using GraphEditor.Export;
using GraphEditor.ViewServices;

namespace GraphEditor;

public sealed partial class GraphEditorForm : Form {
    private readonly PictureViewService _service = new();

    private bool _isMouseDown;
    
    private readonly TextBox _textEditor;

    private bool _isHandlingTextEditor;

    public GraphEditorForm() {
        InitializeComponent();
        
        _textEditor = new TextBox {
            Visible = false,
            Multiline = true,
            BorderStyle = BorderStyle.FixedSingle,
            Font = Font,
        };
        _textEditor.Leave += TextEditorOnLeave;
        _textEditor.KeyDown += TextEditorOnKeyDown;
        Controls.Add(_textEditor);
    }

    private void GraphEditorForm_Paint(object sender, PaintEventArgs e) {
        _service.Paint(e.Graphics);
    }

    private void GraphEditorForm_MouseDown(object sender, MouseEventArgs e) {
        if (e.Button != MouseButtons.Left) {
            return;
        }
        
        HideTextEditor(true);

        _isMouseDown = true;
        var additive = (ModifierKeys & Keys.Control) != Keys.None;
        _service.MouseDown(e.Location, additive);
        Refresh();
        UpdateVisualState();
    }
        
    private void FileExportSvgMenuItem_Click(object sender, EventArgs e) {
        using var dlg = new SaveFileDialog();
        dlg.Filter = @"SVG|*.svg";
        if (dlg.ShowDialog() != DialogResult.OK) return;
        var model = _service.GetPictureModel();
        var size = ClientRectangle;
        SvgExporter.Export(model, dlg.FileName, size.Width, size.Height, BackColor);
    }

    private void FileExportJsonMenuItem_Click(object sender, EventArgs e) {
        using var dlg = new SaveFileDialog();
        dlg.Filter = @"JSON|*.json";
        if (dlg.ShowDialog() != DialogResult.OK) return;
        _service.GetPictureModel();
        JsonExporter.Export(dlg.FileName);
    }

    private void GraphEditorForm_MouseUp(object sender, MouseEventArgs e) {
        if (e.Button != MouseButtons.Left) {
            return;
        }

        _isMouseDown = false;
        _service.MouseUp();
        Refresh();
        UpdateVisualState();
    }

    private void GraphEditorForm_MouseMove(object sender, MouseEventArgs e) {
        Cursor = _service.GetCursor(e.Location);

        if (!_isMouseDown) return;
        _service.MouseMove(e.Location);
        Refresh();
        UpdateVisualState();
    }
    
    private void GraphEditorForm_MouseDoubleClick(object sender, MouseEventArgs e) {
        if (e.Button != MouseButtons.Left) {
            return;
        }

        if (!_service.TryBeginTextEdit(e.Location, out var bounds, out var text, out var fontFamily, out var fontSize,
                out var textColor, out var align, out var fillColor)) return;
        ShowTextEditor(bounds, text, fontFamily, fontSize, textColor, align, fillColor);
        Refresh();
        UpdateVisualState();
    }

    private void CreateRectangleButton_Click(object sender, EventArgs e) {
        HideTextEditor(true);
        _service.CreateButtonClick();
        UpdateVisualState();
    }

    private void UpdateVisualState() {
        CreateRectangleButton.Checked = _service.CreateMode;
        DeleteRectangleButton.Enabled = _service.CanDelete;
        Text = string.IsNullOrEmpty(_service.FileName) ?
            "Графический редактор" : 
            $"Графический редактор | {Path.GetFileName(_service.FileName)}";
    }

    private void DeleteRectangleButton_Click(object sender, EventArgs e) {
        HideTextEditor(true);
        _service.DeleteButtonClick();
        Refresh();
        UpdateVisualState();
    }

    private void FileCreateMenuItem_Click(object sender, EventArgs e) {
        HideTextEditor(true);
        _service.CreateNewPicture();
        Refresh();
        UpdateVisualState();
    }
    
    private void SetTextMenuItem_Click(object sender, EventArgs e) {
        if (_service.PictureModel.SelectedRectangle == null) {
            MessageBox.Show($@"Сначала выберите фигуру.", @"info");
            return;
        }
        
        HideTextEditor(true);

        var input = Microsoft.VisualBasic.Interaction.InputBox(
            "Введите текст для прямоугольника:",
            "Задать текст",
            _service.PictureModel.SelectedRectangle.Text ?? "");

        if (string.IsNullOrWhiteSpace(input)) return;
        _service.SetText(input);
        Refresh();
    }

    private void GroupMenuItem_Click(object sender, EventArgs e) {
        HideTextEditor(true);
        if (_service.PictureModel.SelectedRectangleIds.Count < 2) {
            MessageBox.Show(@"Выберите как минимум две фигуры (используйте Ctrl+клик).", @"info");
            return;
        }

        if (!_service.GroupSelected("Группа")) return;
        Refresh();
        UpdateVisualState();
    }

    private void UngroupMenuItem_Click(object sender, EventArgs e) {
        HideTextEditor(true);
        var selectedIds = _service.PictureModel.SelectedRectangleIds;
        if (!selectedIds.Any()) {
            MessageBox.Show(@"Сначала выберите фигуру.", @"info");
            return;
        }

        var group = _service.PictureModel.Groups.FirstOrDefault(g => g.RectangleIds.Any(selectedIds.Contains));
        if (group != null) {
            _service.Ungroup(group.Id);
            Refresh();
            UpdateVisualState();
        } else {
            MessageBox.Show(@"Выбранные фигуры не входят в группу.", @"info");
        }
    }

    private void FileOpenMenuItem_Click(object sender, EventArgs e) {
        HideTextEditor(true);
        if (FileOpenDialog.ShowDialog() != DialogResult.OK) {
            return;
        }

        try {
            _service.OpenFile(FileOpenDialog.FileName);
        } catch(Exception ex) {
            MessageBox.Show(ex.Message, @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        Refresh();
        UpdateVisualState();
    }

    private void FileSaveMenuItem_Click(object sender, EventArgs e) {
        HideTextEditor(true);
        if (string.IsNullOrEmpty(_service.FileName)) {
            DoSaveAs();
            return;
        }

        _service.SaveToFile();
        UpdateVisualState();
    }

    private void FileSaveAsMenuItem_Click(object sender, EventArgs e) {
        HideTextEditor(true);
        DoSaveAs();
    }

    private void DoSaveAs() {
        HideTextEditor(true);
        if (FileSaveDialog.ShowDialog() != DialogResult.OK) {
            return;
        }

        _service.SaveToFile(FileSaveDialog.FileName);
        UpdateVisualState();
    }

    private void FileExportMenuItem_Click(object sender, EventArgs e) {
        HideTextEditor(true);
        if (FileExportDialog.ShowDialog() != DialogResult.OK) {
            return;
        }

        _service.Export(FileExportDialog.FileName, ClientRectangle, BackColor);
    }

    private void FileExitMenuItem_Click(object sender, EventArgs e) {
        HideTextEditor(true);
        Close();
    }

    private void FillColorMenuItem_Click(object sender, EventArgs e) {
        HideTextEditor(true);
        FillColorDialog.Color = _service.GetFillColor();
        if (FillColorDialog.ShowDialog() != DialogResult.OK) {
            return;
        }

        _service.SetFillColor(FillColorDialog.Color);
        Refresh();
    }

    private void MoveForwardMenuItem_Click(object sender, EventArgs e) {
        HideTextEditor(true);
        _service.MoveForward();
        Refresh();
    }
    
    private void ShowTextEditor(Rectangle bounds, string text, string fontFamily, float fontSize, Color textColor, TextAlign align, Color fillColor) {
        const int padding = 4;
        var editorBounds = Rectangle.Inflate(bounds, -padding, -padding);
        if (editorBounds.Width < 20) editorBounds.Width = 20;
        if (editorBounds.Height < 20) editorBounds.Height = 20;

        _isHandlingTextEditor = true;
        try {
            _textEditor.Bounds = editorBounds;
            if (!string.IsNullOrWhiteSpace(fontFamily) && fontSize > 0) {
                _textEditor.Font = new Font(fontFamily, fontSize);
            }
            _textEditor.ForeColor = textColor;
            _textEditor.BackColor = fillColor;
            _textEditor.TextAlign = ToHorizontalAlignment(align);
            _textEditor.Text = text;
            _textEditor.Visible = true;
            _textEditor.Focus();
            _textEditor.SelectAll();
        } finally {
            _isHandlingTextEditor = false;
        }
    }

    private void HideTextEditor(bool commit) {
        if (!_textEditor.Visible || _isHandlingTextEditor) {
            return;
        }

        _isHandlingTextEditor = true;
        try {
            var newText = _textEditor.Text;
            _textEditor.Visible = false;

            if (commit) {
                _service.SetText(newText);
            }
            Refresh();
            UpdateVisualState();
        } finally {
            _isHandlingTextEditor = false;
        }
    }

    private void TextEditorOnLeave(object? sender, EventArgs e) {
        HideTextEditor(true);
    }

    private void TextEditorOnKeyDown(object? sender, KeyEventArgs e) {
        switch (e.KeyCode) {
            case Keys.Enter when e.Control:
                HideTextEditor(true);
                break;
            case Keys.Escape:
                HideTextEditor(false);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        e.SuppressKeyPress = true;
    }

    private static HorizontalAlignment ToHorizontalAlignment(TextAlign align) => align switch {
        TextAlign.Left => HorizontalAlignment.Left,
        TextAlign.Right => HorizontalAlignment.Right,
        _ => HorizontalAlignment.Center
    };
}
