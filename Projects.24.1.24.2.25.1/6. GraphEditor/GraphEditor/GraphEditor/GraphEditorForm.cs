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
        private bool _isDarkTheme = false;

        public GraphEditorForm()
        {
            InitializeComponent();
        }

        private void GraphEditorForm_Paint(object sender, PaintEventArgs e)
        {
            _viewService.Paint(e.Graphics);
        }

        private void GraphEditorForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            _isMouseDown = true;
            _viewService.MouseDown(e.Location);
            Refresh();
            UpdateViewControls();
        }

        private void GraphEditorForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            _isMouseDown = false;
            _viewService.MouseUp();
            Refresh();
            UpdateViewControls();
        }

        private void GraphEditorForm_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = _viewService.GetCursor(e.Location);
            if (!_isMouseDown) return;
            _viewService.MouseMove(e.Location);
            Refresh();
            UpdateViewControls();
        }

        private void CreateRectangleButton_Click(object sender, EventArgs e)
        {
            _viewService.SelectedShapeType = "Rectangle";
            _viewService.CreateButtonClicked();
            UpdateViewControls();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            _viewService.SelectedShapeType = "Circle";
            _viewService.CreateButtonClicked();
            UpdateViewControls();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            _viewService.SelectedShapeType = "Rhombus";
            _viewService.CreateButtonClicked();
            UpdateViewControls();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            _isDarkTheme = !_isDarkTheme;
            ApplyTheme();
            Refresh();
        }

        private void ApplyTheme()
        {
            if (_isDarkTheme)
            {
                this.BackColor = Color.FromArgb(30, 30, 30);
                this.ForeColor = Color.White;

                foreach (Control control in this.Controls)
                {
                    if (control is ToolStrip toolStrip)
                    {
                        toolStrip.BackColor = Color.FromArgb(45, 45, 48);
                        toolStrip.ForeColor = Color.White;

                        foreach (ToolStripItem item in toolStrip.Items)
                        {
                            item.BackColor = Color.FromArgb(45, 45, 48);
                            item.ForeColor = Color.White;
                        }
                    }
                }
            }
            else
            {
                this.BackColor = SystemColors.Control;
                this.ForeColor = SystemColors.ControlText;

                foreach (Control control in this.Controls)
                {
                    if (control is ToolStrip toolStrip)
                    {
                        toolStrip.BackColor = SystemColors.Control;
                        toolStrip.ForeColor = SystemColors.ControlText;

                        foreach (ToolStripItem item in toolStrip.Items)
                        {
                            item.BackColor = SystemColors.Control;
                            item.ForeColor = SystemColors.ControlText;
                        }
                    }
                }
            }
        }

        private void UpdateViewControls()
        {
            CreateRectangleButton.Checked = _viewService.CreateMode;
            DeleteRectangleButton.Enabled = _viewService.CanDelete;
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
            if (FillColorDialog.ShowDialog() != DialogResult.OK) return;
            _viewService.SetFillColor(FillColorDialog.Color);
            Refresh();
        }

        private void MoveForwardMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.MoveForward();
            Refresh();
        }

        private void FileCreateMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.CreateNewPicture();
            Refresh();
            UpdateViewControls();
        }

        private void FileOpenMenuItem_Click(object sender, EventArgs e)
        {
            if (FileOpenDialog.ShowDialog() != DialogResult.OK) return;
            _viewService.Open(FileOpenDialog.FileName);
            UpdateViewControls();
            Refresh();
        }

        private void FileSaveMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_viewService.FileName)) DoSaveAs();
            else _viewService.Save();
        }

        private void FileSaveAsMenuItem_Click(object sender, EventArgs e)
        {
            DoSaveAs();
        }

        private void FileExportMenuItem_Click(object sender, EventArgs e)
        {
            if (FileExportDialog.ShowDialog() != DialogResult.OK) return;
            _viewService.Export(FileExportDialog.FileName, ClientRectangle, BackColor);
        }

        private void FileExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DoSaveAs()
        {
            if (FileSaveDialog.ShowDialog() != DialogResult.OK) return;
            _viewService.Save(FileSaveDialog.FileName);
            UpdateViewControls();
        }
    }
}