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
        private bool _suppressTextBoxEvents = false;

        public GraphEditorForm()
        {
            InitializeComponent();
            UpdateUIState();
        }

        private void GraphEditorForm_Paint(object sender, PaintEventArgs e)
        {
            _viewService.Paint(e.Graphics);
        }

        private void GraphEditorForm_MouseDown(object sender, MouseEventArgs e)
        {
            _viewService.MouseDown(e.Location);
            UpdateUIState();
            Invalidate();
        }

        private void GraphEditorForm_MouseMove(object sender, MouseEventArgs e)
        {
            _viewService.MouseMove(e.Location);
            Cursor = _viewService.GetCursor(e.Location);
            Invalidate();
        }

        private void GraphEditorForm_MouseUp(object sender, MouseEventArgs e)
        {
            _viewService.MouseUp();
            UpdateUIState();
            Invalidate();
        }

        private void CreateRectangleButton_Click(object sender, EventArgs e)
        {
            _viewService.CreateButtonClicked();
            UpdateUIState();
            Invalidate();
        }

        private void DeleteRectangleButton_Click(object sender, EventArgs e)
        {
            _viewService.DeleteButtonClicked();
            UpdateUIState();
            Invalidate();
        }

        private void FillColorMenuItem_Click(object sender, EventArgs e)
        {
            FillColorDialog.Color = _viewService.GetCurrentFillColor();
            if (FillColorDialog.ShowDialog() == DialogResult.OK)
            {
                _viewService.SetFillColor(FillColorDialog.Color);
                Invalidate();
            }
        }

        private void MoveForwardMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.MoveForward();
            Invalidate();
        }

        // Новые обработчики
        private void OpacityTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_suppressTextBoxEvents) return;
            if (int.TryParse(OpacityTextBox.Text, out int opacity))
            {
                _viewService.SetOpacity(opacity);
                Invalidate();
            }
        }

        private void CornerRadiusTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_suppressTextBoxEvents) return;
            if (int.TryParse(CornerRadiusTextBox.Text, out int radius))
            {
                _viewService.SetCornerRadius(radius);
                Invalidate();
            }
        }

        private void MoveBackwardMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.MoveBackward();
            Invalidate();
        }

        private void MoveToFrontMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.MoveToFront();
            Invalidate();
        }

        private void MoveToBackMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.MoveToBack();
            Invalidate();
        }

        // Обработчики меню Файл
        private void FileCreateMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.CreateNewPicture();
            UpdateUIState();
            Invalidate();
        }

        private void FileOpenMenuItem_Click(object sender, EventArgs e)
        {
            if (FileOpenDialog.ShowDialog() == DialogResult.OK)
            {
                _viewService.Open(FileOpenDialog.FileName);
                UpdateUIState();
                Invalidate();
            }
        }

        private void FileSaveMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.Save();
        }

        private void FileSaveAsMenuItem_Click(object sender, EventArgs e)
        {
            if (FileSaveDialog.ShowDialog() == DialogResult.OK)
                _viewService.Save(FileSaveDialog.FileName);
        }

        private void FileExportMenuItem_Click(object sender, EventArgs e)
        {
            if (FileExportDialog.ShowDialog() == DialogResult.OK)
                _viewService.Export(FileExportDialog.FileName, ClientRectangle, BackColor);
        }

        private void FileExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Вспомогательный метод
        private void UpdateUIState()
        {
            DeleteRectangleButton.Enabled = _viewService.CanDelete;

            _suppressTextBoxEvents = true;
            bool hasSelection = _viewService.CanDelete;
            OpacityTextBox.Enabled = hasSelection && !_viewService.CreateMode;
            CornerRadiusTextBox.Enabled = hasSelection && !_viewService.CreateMode;
            BorderOpacityTextBox.Enabled = hasSelection && !_viewService.CreateMode; // <--

            if (hasSelection)
            {
                OpacityTextBox.Text = _viewService.SelectedOpacity.ToString();
                CornerRadiusTextBox.Text = _viewService.SelectedCornerRadius.ToString();
                BorderOpacityTextBox.Text = _viewService.SelectedBorderOpacity.ToString(); // <--
            }
            else
            {
                OpacityTextBox.Text = "";
                CornerRadiusTextBox.Text = "";
                BorderOpacityTextBox.Text = ""; // <--
            }
            _suppressTextBoxEvents = false;
        }

        private void BorderOpacityTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_suppressTextBoxEvents) return;
            if (int.TryParse(BorderOpacityTextBox.Text, out int opacity))
            {
                _viewService.SetBorderOpacity(opacity);
                Invalidate();
            }
        }

    }
}