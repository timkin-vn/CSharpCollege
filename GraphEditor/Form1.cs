using GraphEditor.ViewServices;
using GraphEditor.Export;
using Microsoft.VisualBasic;

namespace GraphEditor;

public partial class GraphEditorForm : Form {
    private readonly PictureViewService _service = new PictureViewService();

    private bool _isMouseDown;

    public GraphEditorForm() {
        InitializeComponent();
    }

    private void GraphEditorForm_Paint(object sender, PaintEventArgs e) {
        _service.Paint(e.Graphics);
    }

    private void GraphEditorForm_MouseDown(object sender, MouseEventArgs e) {
        if (e.Button != MouseButtons.Left) {
            return;
        }

        _isMouseDown = true;
        _service.MouseDown(e.Location);
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
        var model = _service.GetPictureModel();
        JsonExporter.Export(model, dlg.FileName);
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

    private void CreateRectangleButton_Click(object sender, EventArgs e) {
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
        _service.DeleteButtonClick();
        Refresh();
        UpdateVisualState();
    }

    private void FileCreateMenuItem_Click(object sender, EventArgs e) {
        _service.CreateNewPicture();
        Refresh();
        UpdateVisualState();
    }
    
    private void SetTextMenuItem_Click(object sender, EventArgs e) {
        if (_service.PictureModel.SelectedRectangle == null) {
            MessageBox.Show($@"Сначала выберите фигуру.", "info");
            return;
        }

        var input = Microsoft.VisualBasic.Interaction.InputBox(
            "Введите текст для прямоугольника:",
            "Задать текст",
            _service.PictureModel.SelectedRectangle.Text ?? "");

        if (string.IsNullOrWhiteSpace(input)) return;
        _service.SetText(input);
        Refresh();
    }

    private void GroupMenuItem_Click(object sender, EventArgs e) {
        if (_service.PictureModel.SelectedRectangle == null) {
            MessageBox.Show(@"Сначала выберите фигуру.", @"info");
            return;
        }

        _service.GroupSelected("Группа");
        Refresh();
    }

    private void UngroupMenuItem_Click(object sender, EventArgs e) {
        if (_service.PictureModel.SelectedRectangle == null) {
            MessageBox.Show(@"Сначала выберите фигуру.", @"info");
            return;
        }

        var rect = _service.PictureModel.SelectedRectangle;
        var group = _service.PictureModel.Groups.FirstOrDefault(g => g.Contains(rect.Id));
        if (group != null) {
            _service.Ungroup(group.Id);
            Refresh();
        } else {
            MessageBox.Show(@"Эта фигура не входит в группу.", @"info");
        }
    }

    private void FileOpenMenuItem_Click(object sender, EventArgs e) {
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
        if (string.IsNullOrEmpty(_service.FileName)) {
            DoSaveAs();
            return;
        }

        _service.SaveToFile();
        UpdateVisualState();
    }

    private void FileSaveAsMenuItem_Click(object sender, EventArgs e) {
        DoSaveAs();
    }

    private void DoSaveAs() {
        if (FileSaveDialog.ShowDialog() != DialogResult.OK) {
            return;
        }

        _service.SaveToFile(FileSaveDialog.FileName);
        UpdateVisualState();
    }

    private void FileExportMenuItem_Click(object sender, EventArgs e) {
        if (FileExportDialog.ShowDialog() != DialogResult.OK) {
            return;
        }

        _service.Export(FileExportDialog.FileName, ClientRectangle, BackColor);
    }

    private void FileExitMenuItem_Click(object sender, EventArgs e) {
        Close();
    }

    private void FillColorMenuItem_Click(object sender, EventArgs e) {
        FillColorDialog.Color = _service.GetFillColor();
        if (FillColorDialog.ShowDialog() != DialogResult.OK) {
            return;
        }

        _service.SetFillColor(FillColorDialog.Color);
        Refresh();
    }

    private void MoveForwardMenuItem_Click(object sender, EventArgs e) {
        _service.MoveForward();
        Refresh();
    }
}
