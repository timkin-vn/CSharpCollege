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
using GraphEditor.Business.Models;

namespace GraphEditor
{
    public partial class GraphEditorForm : Form
    {
        private readonly PictureViewService _viewService = new PictureViewService();

        private bool _isMouseDown;

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
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            _isMouseDown = true;
            _viewService.MouseDown(e.Location, ModifierKeys.HasFlag(Keys.Control));
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
        }

        private void MoveBackwardMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.MoveBackward();
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

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {

        }

        private void цветКонтураToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillColorDialog.Color = _viewService.GetCurrentBorderColor();

            if (FillColorDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _viewService.SetBorderColor(FillColorDialog.Color);
            Refresh();
        }

        private void сплошнаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.SetFillType(FillType.Solid);
            Refresh();
        }

        private void безЗаливкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.SetFillType(FillType.None);
            Refresh();
        }

        private void горизонтальнаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.SetFillType(FillType.Horizontal);
            Refresh();
        }

        private void вертикальнаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.SetFillType(FillType.Vertical);
            Refresh();
        }

        private void диагональнаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.SetFillType(FillType.Diagonal);
            Refresh();
        }

        private void прямоугольникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.SetShapeType(ShapeType.Rectangle);
        }

        private void ромбToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.SetShapeType(ShapeType.Rhombus);
        }

        private void треугольникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.SetShapeType(ShapeType.Triangle);
        }
    }
}
