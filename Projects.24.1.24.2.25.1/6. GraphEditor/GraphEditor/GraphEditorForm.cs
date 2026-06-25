using GraphEditor.Business.Models;
using GraphEditor.ViewServices;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GraphEditor
{
    public partial class GraphEditorForm : Form
    {
        private readonly PictureViewService _viewService = new PictureViewService();

        private bool _isMouseDown;
        private bool _isDarkTheme;

        public GraphEditorForm()
        {
            InitializeComponent();
            ApplyTheme();
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

        private void CreateRectangleButton_Click(object sender, EventArgs e)
        {
            _viewService.CreateButtonClicked();
            UpdateViewControls();
        }

        private void ShapeRectangleMenuItem_Click(object sender, EventArgs e)
        {
            SelectShape(ShapeType.Rectangle);
        }

        private void ShapeSquareMenuItem_Click(object sender, EventArgs e)
        {
            SelectShape(ShapeType.Square);
        }

        private void ShapeCircleMenuItem_Click(object sender, EventArgs e)
        {
            SelectShape(ShapeType.Circle);
        }

        private void ShapeHexagonMenuItem_Click(object sender, EventArgs e)
        {
            SelectShape(ShapeType.Hexagon);
        }

        private void SelectShape(ShapeType shapeType)
        {
            _viewService.SetCurrentShapeType(shapeType);
            Refresh();
            UpdateViewControls();
        }

        private void UpdateViewControls()
        {
            CreateRectangleButton.Checked = _viewService.CreateMode;
            CreateRectangleButton.Text = "Создать: " + _viewService.CurrentShapeName;
            ShapeRectangleMenuItem.Checked = _viewService.CurrentShapeType == ShapeType.Rectangle;
            ShapeSquareMenuItem.Checked = _viewService.CurrentShapeType == ShapeType.Square;
            ShapeCircleMenuItem.Checked = _viewService.CurrentShapeType == ShapeType.Circle;
            ShapeHexagonMenuItem.Checked = _viewService.CurrentShapeType == ShapeType.Hexagon;
            DeleteRectangleButton.Enabled = _viewService.CanDelete;
            MoveForwardMenuItem.Enabled = _viewService.CanMoveForward;
            MoveBackwardMenuItem.Enabled = _viewService.CanMoveBackward;
            MoveToFrontMenuItem.Enabled = _viewService.CanMoveForward;
            MoveToBackMenuItem.Enabled = _viewService.CanMoveBackward;
            DarkThemeButton.Checked = _isDarkTheme;

            Text = string.IsNullOrEmpty(_viewService.FileName) ?
                "Графический редактор" :
                $"Графический редактор | {Path.GetFileName(_viewService.FileName)}";
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

        private void MoveForwardMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.MoveForward();
            Refresh();
            UpdateViewControls();
        }

        private void MoveBackwardMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.MoveBackward();
            Refresh();
            UpdateViewControls();
        }

        private void MoveToFrontMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.MoveToFront();
            Refresh();
            UpdateViewControls();
        }

        private void MoveToBackMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.MoveToBack();
            Refresh();
            UpdateViewControls();
        }

        private void DarkThemeButton_Click(object sender, EventArgs e)
        {
            _isDarkTheme = !_isDarkTheme;
            ApplyTheme();
            Refresh();
            UpdateViewControls();
        }

        private void FileCreateMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.CreateNewPicture();
            Refresh();
            UpdateViewControls();
        }

        private void FileOpenMenuItem_Click(object sender, EventArgs e)
        {
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
        }

        private void FileExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
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

        private void ApplyTheme()
        {
            var formBackColor = _isDarkTheme ? Color.FromArgb(45, 45, 48) : SystemColors.Control;
            var panelBackColor = _isDarkTheme ? Color.FromArgb(30, 30, 30) : SystemColors.Control;
            var foreColor = _isDarkTheme ? Color.Gainsboro : SystemColors.ControlText;

            BackColor = formBackColor;
            ForeColor = foreColor;
            ApplyToolStripTheme(menuStrip1, panelBackColor, foreColor);
            ApplyToolStripTheme(toolStrip1, panelBackColor, foreColor);
        }

        private void ApplyToolStripTheme(ToolStrip toolStrip, Color backColor, Color foreColor)
        {
            toolStrip.BackColor = backColor;
            toolStrip.ForeColor = foreColor;
            toolStrip.RenderMode = ToolStripRenderMode.System;

            foreach (ToolStripItem item in toolStrip.Items)
            {
                ApplyToolStripItemTheme(item, backColor, foreColor);
            }
        }

        private void ApplyToolStripItemTheme(ToolStripItem item, Color backColor, Color foreColor)
        {
            item.BackColor = backColor;
            item.ForeColor = foreColor;

            if (item is ToolStripDropDownItem dropDownItem)
            {
                foreach (ToolStripItem child in dropDownItem.DropDownItems)
                {
                    ApplyToolStripItemTheme(child, backColor, foreColor);
                }
            }
        }
    }
}
