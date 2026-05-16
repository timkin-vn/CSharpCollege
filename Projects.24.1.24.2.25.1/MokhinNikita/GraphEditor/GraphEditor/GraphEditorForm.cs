using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GraphEditor.Business.Models;
using GraphEditor.Views;
using GraphEditor.ViewServices;

namespace GraphEditor
{
    public partial class GraphEditorForm : Form
    {
        private PictureViewService _pictureViewService = new PictureViewService();
        private bool _isMouseDown;
        private FigureType[] figureTypes =
        {
            FigureType.Rectangle,
            FigureType.Ellipse,
            FigureType.Pentagon,
            FigureType.Rhombus,
        };
        private FigureType _figureType;
        public GraphEditorForm()
        {
            InitializeComponent();
            FigureTypeComboBox.SelectedIndex = 0;
            FigureTypeComboBox.SelectedIndexChanged += FigureTypeComboBox_SelectedIndexChanged;
            UpdateViewControls();
            _figureType = figureTypes[FigureTypeComboBox.SelectedIndex];
        }

        private void FigureTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _figureType = figureTypes[FigureTypeComboBox.SelectedIndex];
        }

        private void GraphEditorForm_Paint(object sender, PaintEventArgs e)
        {
            _pictureViewService.Paint(e.Graphics);
        }

        private void GraphEditorForm_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button != MouseButtons.Left) return;
            _isMouseDown = false;
            _pictureViewService.MouseUp();
            UpdateViewControls();
            Refresh();
        }

        private void GraphEditorForm_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = _pictureViewService.GetCursor(e.Location);
            if (!_isMouseDown || e.Button != MouseButtons.Left) return;
            _pictureViewService.MouseMove(e.Location);
            Refresh();
        }

        private void GraphEditorForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            _isMouseDown = true;
            _pictureViewService.MouseDown(e.Location, _figureType);
            Refresh();
        }

        private void createRectangleButton_Click(object sender, EventArgs e)
        {
            _pictureViewService.CreateButtonClicked();
            UpdateViewControls();
        }
        private void UpdateViewControls()
        {
            createRectangleButton.Checked = _pictureViewService.CreateMode;
            deleteButton.Enabled = _pictureViewService.CanDelete;
            Text = string.IsNullOrEmpty(_pictureViewService.FileName) ? "Графический редактор" : $"Графический редактор: {_pictureViewService.FileName}";
            FigureTypeComboBox.Enabled = _pictureViewService.CreateMode;
            OpacityTextBox1.Enabled = _pictureViewService.CanDelete;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            _pictureViewService.DeleteButtonClicked();
            Refresh();
            UpdateViewControls();
        }

        private void FillColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillColorDialog.Color = _pictureViewService.GetCurrentFillColor();
            if(FillColorDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            _pictureViewService.SetFillColor(FillColorDialog.Color);
            Refresh();
        }

        private void BorderColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BorderColorDialog.Color = _pictureViewService.GetCurrentBorderColor();
            if(BorderColorDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            _pictureViewService.SetBorderColor(BorderColorDialog.Color);
            Refresh();
        }

        private void MoveForwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _pictureViewService.MoveForward();
            Refresh();
        }

        private void MoveBackwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _pictureViewService.MoveBackward();
            Refresh();
        }

        private void FileCreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _pictureViewService.CreateNewPicture();
            Refresh();
            UpdateViewControls();
        }

        private void FileOpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(FileOpenDialog.ShowDialog() != DialogResult.OK) { return; }
            _pictureViewService.Open(FileOpenDialog.FileName);
            UpdateViewControls();
            Refresh();
        }

        private void FileSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(_pictureViewService.FileName))
            {
                DoSaveAs();
            }
            else
            {
                _pictureViewService.Save();
            }
        }

        private void FileSaveAsКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoSaveAs();
        }

        private void FileExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(FileExportDialog.ShowDialog() != DialogResult.OK) return;
            _pictureViewService.Export(FileExportDialog.FileName, ClientRectangle, BackColor);
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void DoSaveAs()
        {
            if(FileSaveDialog.ShowDialog() != DialogResult.OK) return;

            _pictureViewService.Save(FileSaveDialog.FileName);
            UpdateViewControls();
        }

        private void WeightBorderToolStripButton_Click(object sender, EventArgs e)
        {
            BorderWidthDialog dialog = new BorderWidthDialog(_pictureViewService.GetCurrentBorderWidth());
            if(dialog.ShowDialog() != DialogResult.OK) return;
            _pictureViewService.SetBorderWidth(dialog.BorderWidth);
            Refresh();
        }

        private void MoveToForegroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _pictureViewService.MoveForeground();
            Refresh();
        }

        private void MoveToBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _pictureViewService?.MoveBackground();
            Refresh();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutDialog dialog = new AboutDialog();
            dialog.ShowDialog();
        }

        private void ConfirmOpacityBtn_Click(object sender, EventArgs e)
        {
            if(!byte.TryParse(OpacityTextBox1.Text, out var opacity))
            {
                MessageBox.Show("Введен неверный формат", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _pictureViewService.SetOpacity(opacity);
            Refresh();
        }
    }
}
