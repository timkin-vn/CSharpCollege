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
        private readonly PictureViewService _canvasService = new PictureViewService();
        private bool _blockTextEvents = false;

        public GraphEditorForm()
        {
            InitializeComponent();
            UpdateUIState();
        }

        private void GraphEditorForm_Paint(object sender, PaintEventArgs e)
        {
            _canvasService.Paint(e.Graphics);
        }

        private void GraphEditorForm_MouseDown(object sender, MouseEventArgs e)
        {
            _canvasService.MouseDown(e.Location);
            UpdateUIState();
            Invalidate();
        }

        private void GraphEditorForm_MouseMove(object sender, MouseEventArgs e)
        {
            _canvasService.MouseMove(e.Location);
            Cursor = _canvasService.GetCursor(e.Location);
            Invalidate();
        }

        private void GraphEditorForm_MouseUp(object sender, MouseEventArgs e)
        {
            _canvasService.MouseUp();
            UpdateUIState();
            Invalidate();
        }

        private void CreateRectangleButton_Click(object sender, EventArgs e)
        {
            _canvasService.CreateButtonClicked();
            UpdateUIState();
            Invalidate();
        }

        private void DeleteRectangleButton_Click(object sender, EventArgs e)
        {
            _canvasService.DeleteButtonClicked();
            UpdateUIState();
            Invalidate();
        }

        private void FillColorMenuItem_Click(object sender, EventArgs e)
        {
            FillColorDialog.Color = _canvasService.GetCurrentFillColor();
            if (FillColorDialog.ShowDialog() == DialogResult.OK)
            {
                _canvasService.SetFillColor(FillColorDialog.Color);
                Invalidate();
            }
        }

        private void MoveForwardMenuItem_Click(object sender, EventArgs e)
        {
            _canvasService.MoveForward();
            Invalidate();
        }

        // Новые обработчики
        private void OpacityTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_blockTextEvents) return;
            if (int.TryParse(OpacityTextBox.Text, out int opacity))
            {
                _canvasService.SetOpacity(opacity);
                Invalidate();
            }
        }

        private void CornerRadiusTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_blockTextEvents) return;
            if (int.TryParse(CornerRadiusTextBox.Text, out int radius))
            {
                _canvasService.SetCornerRadius(radius);
                Invalidate();
            }
        }

        private void MoveBackwardMenuItem_Click(object sender, EventArgs e)
        {
            _canvasService.MoveBackward();
            Invalidate();
        }

        private void MoveToFrontMenuItem_Click(object sender, EventArgs e)
        {
            _canvasService.MoveToFront();
            Invalidate();
        }

        private void MoveToBackMenuItem_Click(object sender, EventArgs e)
        {
            _canvasService.MoveToBack();
            Invalidate();
        }

        // Обработчики меню Файл
        private void FileCreateMenuItem_Click(object sender, EventArgs e)
        {
            _canvasService.CreateNewPicture();
            UpdateUIState();
            Invalidate();
        }

        private void FileOpenMenuItem_Click(object sender, EventArgs e)
        {
            if (FileOpenDialog.ShowDialog() == DialogResult.OK)
            {
                _canvasService.Open(FileOpenDialog.FileName);
                UpdateUIState();
                Invalidate();
            }
        }

        private void FileSaveMenuItem_Click(object sender, EventArgs e)
        {
            _canvasService.Save();
        }

        private void FileSaveAsMenuItem_Click(object sender, EventArgs e)
        {
            if (FileSaveDialog.ShowDialog() == DialogResult.OK)
                _canvasService.Save(FileSaveDialog.FileName);
        }

        private void FileExportMenuItem_Click(object sender, EventArgs e)
        {
            if (FileExportDialog.ShowDialog() == DialogResult.OK)
                _canvasService.Export(FileExportDialog.FileName, ClientRectangle, BackColor);
        }

        private void FileExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Вспомогательный метод
        private void UpdateUIState()
        {
            DeleteRectangleButton.Enabled = _canvasService.CanDelete;

            _blockTextEvents = true;
            bool hasSelection = _canvasService.CanDelete;
            OpacityTextBox.Enabled = hasSelection && !_canvasService.CreateMode;
            CornerRadiusTextBox.Enabled = hasSelection && !_canvasService.CreateMode;
            BorderOpacityTextBox.Enabled = hasSelection && !_canvasService.CreateMode; // <--

            if (hasSelection)
            {
                OpacityTextBox.Text = _canvasService.SelectedOpacity.ToString();
                CornerRadiusTextBox.Text = _canvasService.SelectedCornerRadius.ToString();
                BorderOpacityTextBox.Text = _canvasService.SelectedBorderOpacity.ToString(); // <--
            }
            else
            {
                OpacityTextBox.Text = "";
                CornerRadiusTextBox.Text = "";
                BorderOpacityTextBox.Text = ""; // <--
            }
            _blockTextEvents = false;
        }

        private void BorderOpacityTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_blockTextEvents) return;
            if (int.TryParse(BorderOpacityTextBox.Text, out int opacity))
            {
                _canvasService.SetBorderOpacity(opacity);
                Invalidate();
            }
        }

    }
}