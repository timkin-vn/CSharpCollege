using GraphEditor.ViewServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphEditor
{
    public partial class GraphEditorForm : Form
    {
        private readonly PictureViewService _service = new PictureViewService();

        private bool _isMouseDown;

        public GraphEditorForm()
        {
            InitializeComponent();
        }

        private void GraphEditorForm_Paint(object sender, PaintEventArgs e)
        {
            _service.Paint(e.Graphics);
        }

        private void GraphEditorForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            _isMouseDown = true;
            _service.MouseDown(e.Location);
            Refresh();
            UpdateVisualState();
        }

        private void GraphEditorForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            _isMouseDown = false;
            _service.MouseUp();
            Refresh();
            UpdateVisualState();
        }

        private void GraphEditorForm_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = _service.GetCursor(e.Location);

            if (_isMouseDown)
            {
                _service.MouseMove(e.Location);
                Refresh();
                UpdateVisualState();
            }
        }

        private void CreateRectangleButton_Click(object sender, EventArgs e)
        {
            _service.CreateButtonClick();
            UpdateVisualState();
        }

        private void UpdateVisualState()
        {
            CreateRectangleButton.Checked = _service.CreateMode;
            DeleteRectangleButton.Enabled = _service.CanDelete;
            Text = string.IsNullOrEmpty(_service.FileName) ? "Графический редактор" : $"Графический редактор | {_service.FileName}";
            // update shape button text
            if (ShapeSelectBtn != null)
            {
                ShapeSelectBtn.Text = "Форма: " + (_service.SelectedShape.ToString());
            }
        }

        private void DeleteRectangleButton_Click(object sender, EventArgs e)
        {
            _service.DeleteButtonClick();
            Refresh();
            UpdateVisualState();
        }

        private void ShapeRectangleMenuItem_Click(object sender, EventArgs e)
        {
            _service.SelectedShape = GraphEditor.Business.Models.ShapeType.Rectangle;
            UpdateVisualState();
        }

        private void ShapeCircleMenuItem_Click(object sender, EventArgs e)
        {
            _service.SelectedShape = GraphEditor.Business.Models.ShapeType.Circle;
            UpdateVisualState();
        }

        private void ShapeTriangleMenuItem_Click(object sender, EventArgs e)
        {
            _service.SelectedShape = GraphEditor.Business.Models.ShapeType.Triangle;
            UpdateVisualState();
        }

        private void ShapeHexagonMenuItem_Click(object sender, EventArgs e)
        {
            _service.SelectedShape = GraphEditor.Business.Models.ShapeType.Hexagon;
            UpdateVisualState();
        }

        private void ShapeParallelogramMenuItem_Click(object sender, EventArgs e)
        {
            _service.SelectedShape = GraphEditor.Business.Models.ShapeType.Parallelogram;
            UpdateVisualState();
        }

        private void FileCreateMenuItem_Click(object sender, EventArgs e)
        {
            _service.CreateNewPicture();
            Refresh();
            UpdateVisualState();
        }

        private void FileOpenMenuItem_Click(object sender, EventArgs e)
        {
            if (FileOpenDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                _service.OpenFile(FileOpenDialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Refresh();
            UpdateVisualState();
        }

        private void FileSaveMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_service.FileName))
            {
                DoSaveAs();
                return;
            }

            _service.SaveToFile();
            UpdateVisualState();
        }

        private void FileSaveAsMenuItem_Click(object sender, EventArgs e)
        {
            DoSaveAs();
        }

        private void DoSaveAs()
        {
            if (FileSaveDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _service.SaveToFile(FileSaveDialog.FileName);
            UpdateVisualState();
        }

        private void FileExportMenuItem_Click(object sender, EventArgs e)
        {
            if (FileExportDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            _service.Export(FileExportDialog.FileName, ClientRectangle, BackColor);
        }

        private void FileExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void MoveForwardBtn_Click(object sender, EventArgs e)
        {
            _service.MoveForward();
            Refresh();
        }

        private void MoveBackBtn_Click(object sender, EventArgs e)
        {
            _service.MoveBack();
            Refresh();
        }

        private void BorderColorBtn_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    _service.BorderChangeColor(colorDialog.Color);
                    Refresh();
                }
            }
        }

        private void ChangeRectangleColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    _service.ChangeSelectedRectangleColor(colorDialog.Color);
                    Refresh();
                }
            }
        }
    }
}