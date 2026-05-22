using GraphEditor.Business.Models;
using GraphEditor.ViewServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphEditor
{
    public partial class GraphEditorForm : Form
    {
        private readonly PictureViewService _viewService = new PictureViewService();

        private bool _isMouseDown;

        public GraphEditorForm()
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.KeyDown += GraphEditorForm_KeyDown;

            this.MouseWheel += GraphEditorForm_MouseWheel;

            GridToggleButton.Checked = _viewService.ShowGrid;
            UpdateViewControls();
        }

        private void GraphEditorForm_Paint(object sender, PaintEventArgs e)
        {
            _viewService.Paint(e.Graphics);
        }

        private void GraphEditorForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            _isMouseDown = true;
            _viewService.MouseDown(e.Location);
            Refresh();
            UpdateViewControls();
        }

        private void GraphEditorForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            _isMouseDown = false;
            _viewService.MouseUp();
            Refresh();
            UpdateViewControls();
        }

        private void GraphEditorForm_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = _viewService.GetCursor(e.Location);

            if (!_isMouseDown)
            {
                return;
            }

            _viewService.MouseMove(e.Location);
            Refresh();
            UpdateViewControls();
        }

        private void GraphEditorForm_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                if (e.Delta > 0)
                    _viewService.ZoomIn();
                else
                    _viewService.ZoomOut();
                Refresh();
            }
        }

        private void GraphEditorForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                FileSaveMenuItem_Click(this, EventArgs.Empty);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.O)
            {
                FileOpenMenuItem_Click(this, EventArgs.Empty);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                FileCreateMenuItem_Click(this, EventArgs.Empty);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                FileExportMenuItem_Click(this, EventArgs.Empty);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (_viewService.CanDelete)
                {
                    DeleteRectangleButton_Click(this, EventArgs.Empty);
                    e.Handled = true;
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (_viewService.IsCreating)
                {
                    _viewService.CancelCreating();
                    UpdateViewControls();
                    Refresh();
                    e.Handled = true;
                }
                else if (_viewService.IsShapeTool)
                {
                    SetDrawingTool(DrawingTool.Select);
                    e.Handled = true;
                }
            }
            else if (e.Control && e.KeyCode == Keys.G)
            {
                _viewService.ShowGrid = !_viewService.ShowGrid;
                GridToggleButton.Checked = _viewService.ShowGrid;
                Refresh();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.B)
            {
                _viewService.SnapToGridEnabled = !_viewService.SnapToGridEnabled;
                SnapToggleButton.Checked = _viewService.SnapToGridEnabled;
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Up)
            {
                _viewService.MoveForward();
                Refresh();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.Down)
            {
                _viewService.MoveBackward();
                Refresh();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.D)
            {
                _viewService.DuplicateSelected();
                Refresh();
                UpdateViewControls();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void SelectToolButton_Click(object sender, EventArgs e)
        {
            SetDrawingTool(DrawingTool.Select);
        }

        private void RectangleToolButton_Click(object sender, EventArgs e)
        {
            SetDrawingTool(DrawingTool.Rectangle);
        }

        private void EllipseToolButton_Click(object sender, EventArgs e)
        {
            SetDrawingTool(DrawingTool.Ellipse);
        }

        private void LineToolButton_Click(object sender, EventArgs e)
        {
            SetDrawingTool(DrawingTool.Line);
        }

        private void SetDrawingTool(DrawingTool tool)
        {
            _viewService.SetActiveTool(tool);
            UpdateViewControls();
            Refresh();
        }

        private void DeleteRectangleButton_Click(object sender, EventArgs e)
        {
            _viewService.DeleteButtonClicked();
            Refresh();
            UpdateViewControls();
        }

        private void FillColorMenuItem_Click(object sender, EventArgs e)
        {
            FillColorDialog.Color = _viewService.GetCurrentFillColor();
            if (FillColorDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _viewService.SetFillColor(FillColorDialog.Color);
            Refresh();
        }

        private void BorderColorMenuItem_Click(object sender, EventArgs e)
        {
            using (var colorDialog = new ColorDialog())
            {
                colorDialog.Color = _viewService.GetCurrentBorderColor();
                if (colorDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                _viewService.SetBorderColor(colorDialog.Color);
                Refresh();
            }
        }

        private void MoveForwardMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.MoveForward();
            Refresh();
        }

        private void MoveBackwardMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.MoveBackward();
            Refresh();
        }

        private void MoveToFrontMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.MoveToFront();
            Refresh();
        }

        private void MoveToBackMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.MoveToBack();
            Refresh();
        }

        private void GridToggleButton_Click(object sender, EventArgs e)
        {
            _viewService.ShowGrid = !_viewService.ShowGrid;
            GridToggleButton.Checked = _viewService.ShowGrid;
            Refresh();
        }

        private void SnapToggleButton_Click(object sender, EventArgs e)
        {
            _viewService.SnapToGridEnabled = !_viewService.SnapToGridEnabled;
            SnapToggleButton.Checked = _viewService.SnapToGridEnabled;
        }

        private void OpacityTrackBar_Scroll(object sender, EventArgs e)
        {
            if (_viewService.GetSelectedRectangle() == null)
                return;

            byte opacity = (byte)OpacityTrackBar.Value;
            _viewService.SetOpacity(opacity);
            OpacityLabel.Text = $"Прозрачность: {opacity}";
            Refresh();
        }

        private void ShadowToggleButton_Click(object sender, EventArgs e)
        {
            if (_viewService.GetSelectedRectangle() == null)
                return;

            _viewService.ToggleShadow();
            ShadowToggleButton.Checked = _viewService.GetSelectedRectangle()?.ShowShadow ?? false;
            Refresh();
        }

        private void FileCreateMenuItem_Click(object sender, EventArgs e)
        {

            if (_viewService.HasRectangles())
            {
                var result = MessageBox.Show(
                    "Сохранить текущий рисунок перед созданием нового?",
                    "Создать новый документ",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    FileSaveMenuItem_Click(sender, e);
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }

            _viewService.CreateNewPicture();
            Refresh();
            UpdateViewControls();
        }

        private void FileOpenMenuItem_Click(object sender, EventArgs e)
        {

            if (_viewService.HasRectangles())
            {
                var result = MessageBox.Show(
                    "Сохранить текущий рисунок перед открытием другого?",
                    "Открыть файл",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    FileSaveMenuItem_Click(sender, e);
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }

            if (FileOpenDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _viewService.Open(FileOpenDialog.FileName);
            UpdateViewControls();
            Refresh();
        }

        private void FileSaveMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_viewService.FileName))
            {
                DoSaveAs();
            }
            else
            {
                _viewService.Save();
            }
        }

        private void FileSaveAsMenuItem_Click(object sender, EventArgs e)
        {
            DoSaveAs();
        }

        private void FileExportMenuItem_Click(object sender, EventArgs e)
        {
            if (FileExportDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _viewService.Export(FileExportDialog.FileName, ClientRectangle, BackColor);

            MessageBox.Show(
                $"Изображение успешно экспортировано в:\n{FileExportDialog.FileName}",
                "Экспорт завершён",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void FileExitMenuItem_Click(object sender, EventArgs e)
        {

            if (_viewService.HasRectangles())
            {
                var result = MessageBox.Show(
                    "Сохранить изменения перед выходом?",
                    "Выход",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    FileSaveMenuItem_Click(sender, e);
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }

            Close();
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Графический редактор v1.2\n\n" +
                "Инструменты (как в Paint):\n" +
                "• Выделение — перемещение и изменение фигур\n" +
                "• Прямоугольник, овал, линия\n\n" +
                "Дополнительно:\n" +
                "• Поворот, прозрачность, тень\n" +
                "• Сетка с привязкой, Z-порядок\n" +
                "• Сохранение и экспорт в PNG\n\n" +
                "Горячие клавиши:\n" +
                "Ctrl+S/O/N/E — файл\n" +
                "Ctrl+D — дублировать\n" +
                "Delete — удалить\n" +
                "Escape — отмена / выбор",
                "О программе",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void DoSaveAs()
        {
            if (FileSaveDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _viewService.Save(FileSaveDialog.FileName);
            UpdateViewControls();
        }

        private void UpdateViewControls()
        {
            SelectToolButton.Checked = _viewService.ActiveTool == DrawingTool.Select;
            RectangleToolButton.Checked = _viewService.ActiveTool == DrawingTool.Rectangle;
            EllipseToolButton.Checked = _viewService.ActiveTool == DrawingTool.Ellipse;
            LineToolButton.Checked = _viewService.ActiveTool == DrawingTool.Line;

            DeleteRectangleButton.Enabled = _viewService.CanDelete;
            GridToggleButton.Checked = _viewService.ShowGrid;
            SnapToggleButton.Checked = _viewService.SnapToGridEnabled;

            MoveForwardMenuItem.Enabled = _viewService.CanDelete;

            var selectedRect = _viewService.GetSelectedRectangle();
            if (selectedRect != null)
            {
                OpacityTrackBar.Value = selectedRect.Opacity;
                OpacityLabel.Text = $"Прозрачность: {selectedRect.Opacity}";
                OpacityTrackBar.Enabled = true;
                ShadowToggleButton.Checked = selectedRect.ShowShadow;
                ShadowToggleButton.Enabled = true;
            }
            else
            {
                OpacityTrackBar.Enabled = false;
                ShadowToggleButton.Enabled = false;
            }

            Text = string.IsNullOrEmpty(_viewService.FileName) ?
                "Графический редактор" :
                $"Графический редактор | {Path.GetFileName(_viewService.FileName)}";
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (_viewService.HasRectangles() && e.CloseReason == CloseReason.UserClosing)
            {
                var result = MessageBox.Show(
                    "Сохранить изменения перед выходом?",
                    "Подтверждение выхода",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    FileSaveMenuItem_Click(this, EventArgs.Empty);
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}