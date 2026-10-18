
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
        private readonly PictureViewService _viewService =
            new PictureViewService();

        private readonly ToolStripButton _duplicateRectangleButton;

        private bool _isMouseDown;

        public GraphEditorForm()
        {
            InitializeComponent();

            var shapeMenu = new ToolStripDropDownButton("Фигура");

            shapeMenu.DropDownItems.Add("Прямоугольник", null, (s, e) =>
            {
                _viewService.SelectedShapeType = ShapeType.Rectangle;
                shapeMenu.Text = "Фигура: прямоугольник";
            });

            shapeMenu.DropDownItems.Add("Эллипс / круг", null, (s, e) =>
            {
                _viewService.SelectedShapeType = ShapeType.Ellipse;
                shapeMenu.Text = "Фигура: эллипс";
            });

            shapeMenu.DropDownItems.Add("Треугольник", null, (s, e) =>
            {
                _viewService.SelectedShapeType = ShapeType.Triangle;
                shapeMenu.Text = "Фигура: треугольник";
            });

            toolStrip1.Items.Insert(0, shapeMenu);

            _duplicateRectangleButton =
                new ToolStripButton("Дублировать");

            _duplicateRectangleButton.DisplayStyle =
                ToolStripItemDisplayStyle.Text;

            _duplicateRectangleButton.Click +=
                DuplicateRectangleButton_Click;

            toolStrip1.Items.Insert(2, _duplicateRectangleButton);

            UpdateViewControls();
        }

        private void GraphEditorForm_Paint(
            object sender,
            PaintEventArgs e)
        {
            _viewService.Paint(e.Graphics);
        }

        private void GraphEditorForm_MouseDown(
            object sender,
            MouseEventArgs e)
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

        private void GraphEditorForm_MouseUp(
            object sender,
            MouseEventArgs e)
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

        private void GraphEditorForm_MouseMove(
            object sender,
            MouseEventArgs e)
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

        private void CreateRectangleButton_Click(
            object sender,
            EventArgs e)
        {
            _viewService.CreateButtonClicked();
            UpdateViewControls();
        }

        private void UpdateViewControls()
        {
            CreateRectangleButton.Checked = _viewService.CreateMode;

            DeleteRectangleButton.Enabled = _viewService.CanDelete;

            _duplicateRectangleButton.Enabled =
                _viewService.CanDuplicate;

            Text = string.IsNullOrEmpty(_viewService.FileName)
                ? "Графический редактор"
                : $"Графический редактор | " +
                  $"{Path.GetFileName(_viewService.FileName)}";
        }

        private void DeleteRectangleButton_Click(
            object sender,
            EventArgs e)
        {
            _viewService.DeleteButtonClicked();
            Refresh();
            UpdateViewControls();
        }

        private void DuplicateRectangleButton_Click(
            object sender,
            EventArgs e)
        {
            _viewService.DuplicateSelectedRectangle();
            Refresh();
            UpdateViewControls();
        }

        private void FillColorMenuItem_Click(
            object sender,
            EventArgs e)
        {
            FillColorDialog.Color = _viewService.GetCurrentFillColor();

            if (FillColorDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _viewService.SetFillColor(FillColorDialog.Color);
            Refresh();
        }

        private void MoveForwardMenuItem_Click(
            object sender,
            EventArgs e)
        {
            _viewService.MoveForward();
            Refresh();
        }

        private void FileCreateMenuItem_Click(
            object sender,
            EventArgs e)
        {
            _viewService.CreateNewPicture();
            Refresh();
            UpdateViewControls();
        }

        private void FileOpenMenuItem_Click(
            object sender,
            EventArgs e)
        {
            if (FileOpenDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _viewService.Open(FileOpenDialog.FileName);
            UpdateViewControls();
            Refresh();
        }

        private void FileSaveMenuItem_Click(
            object sender,
            EventArgs e)
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

        private void FileSaveAsMenuItem_Click(
            object sender,
            EventArgs e)
        {
            DoSaveAs();
        }

        private void FileExportMenuItem_Click(
            object sender,
            EventArgs e)
        {
            if (FileExportDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _viewService.Export(
                FileExportDialog.FileName,
                ClientRectangle,
                BackColor);
        }

        private void FileExitMenuItem_Click(
            object sender,
            EventArgs e)
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
    }
}
