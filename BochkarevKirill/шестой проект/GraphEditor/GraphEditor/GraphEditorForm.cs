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
        private void BackgroundColorMenuItem_Click(object sender, EventArgs e)
        {
            FillColorDialog.Color = BackColor;

            if (FillColorDialog.ShowDialog() != DialogResult.OK)
                return;

            BackColor = FillColorDialog.Color;

            Refresh();
        }

        private void CreateRectangleButton_Click(object sender, EventArgs e)
        {
            _service.CreateButtonClick();
            UpdateVisualState();
        }
        private void CreateRectangleMenuItem_Click(object sender, EventArgs e)
        {
            _service.CreateButtonClick();
            UpdateVisualState();
        }

        private void CreateCircleMenuItem_Click(object sender, EventArgs e)
        {
            _service.CreateCircleButtonClick();
            UpdateVisualState();
        }

        private void UpdateVisualState()
        {
            if (_service.CreateCircleMode)
                CreateRectangleButton.Text = "Создать (Круг)";
            else if (_service.CreateMode)
                CreateRectangleButton.Text = "Создать (Прямоуг.)";
            else
                CreateRectangleButton.Text = "Создать";


            DeleteRectangleButton.Enabled = _service.CanDelete || _service.CanDeleteCircle;
            Text = string.IsNullOrEmpty(_service.FileName) ?
                "Графический редактор" : 
                $"Графический редактор | {Path.GetFileName(_service.FileName)}";
        }

        private void DeleteRectangleButton_Click(object sender, EventArgs e)
        {
            if (_service.CanDeleteCircle)
                _service.DeleteCircleButtonClick();
            else
                _service.DeleteButtonClick();

            Refresh();
            UpdateVisualState();
        }

        private void BorderColorMenuItem_Click(object sender, EventArgs e)
        {
            FillColorDialog.Color = _service.GetBorderColor();
            if (FillColorDialog.ShowDialog() != DialogResult.OK)
                return;

            _service.SetBorderColor(FillColorDialog.Color);
            Refresh();
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
            catch(Exception ex)
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

        private void FillColorMenuItem_Click(object sender, EventArgs e)
        {
            FillColorDialog.Color = _service.GetFillColor();
            if (FillColorDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _service.SetFillColor(FillColorDialog.Color);
            Refresh();
        }

        private void MoveForwardMenuItem_Click(object sender, EventArgs e)
        {
            _service.MoveForward();
            Refresh();
        }
    }
}
