using GraphEditor.Business.Models;
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
        private ColorDialog _colorDialog;

        public GraphEditorForm()
        {
            InitializeComponent();
            _colorDialog = new ColorDialog();
            _colorDialog.AnyColor = true;
            _colorDialog.FullOpen = true;
            InitializeFigureTypeComboBox();
            UpdateColorButtons();
        }

        private void InitializeFigureTypeComboBox()
        {
            FigureTypeComboBox.Items.Add("Прямоугольник");
            FigureTypeComboBox.Items.Add("Круг");
            FigureTypeComboBox.Items.Add("Треугольник");
            FigureTypeComboBox.SelectedIndex = 0;

            FigureTypeComboBox.SelectedIndexChanged += FigureTypeComboBox_SelectedIndexChanged;
        }

        private void UpdateColorButtons()
        {
            UpdateButtonColor(FillColorButton, Color.Yellow);
            UpdateButtonColor(BorderColorButton, Color.Blue);
        }

        private void UpdateButtonColor(ToolStripButton button, Color color)
        {
            button.BackColor = color;
            button.ForeColor = GetContrastColor(color);
            button.Text = "";
            button.ToolTipText = $"Цвет: {color.Name}";
        }

        private Color GetContrastColor(Color color)
        {
            double luminance = (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;
            return luminance > 0.5 ? Color.Black : Color.White;
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

            FillColorButton.Enabled = _service.CanDelete;
            BorderColorButton.Enabled = _service.CanDelete;
        }

        private void DeleteRectangleButton_Click(object sender, EventArgs e)
        {
            _service.DeleteButtonClick();
            Refresh();
            UpdateVisualState();
        }

        private void FigureTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FigureType figureType = FigureType.Rectangle;

            if (FigureTypeComboBox.SelectedIndex == 1)
                figureType = FigureType.Circle;
            else if (FigureTypeComboBox.SelectedIndex == 2)
                figureType = FigureType.Triangle;

            _service.SetFigureType(figureType);
        }

        private void FillColorButton_Click(object sender, EventArgs e)
        {
            if (!FillColorButton.Enabled) return;

            _colorDialog.Color = FillColorButton.BackColor;
            if (_colorDialog.ShowDialog() == DialogResult.OK)
            {
                FillColorButton.BackColor = _colorDialog.Color;
                FillColorButton.ForeColor = GetContrastColor(_colorDialog.Color);
                _service.ChangeSelectedFillColor(_colorDialog.Color);
                Refresh();
            }
        }

        private void BorderColorButton_Click(object sender, EventArgs e)
        {
            if (!BorderColorButton.Enabled) return;

            _colorDialog.Color = BorderColorButton.BackColor;
            if (_colorDialog.ShowDialog() == DialogResult.OK)
            {
                BorderColorButton.BackColor = _colorDialog.Color;
                BorderColorButton.ForeColor = GetContrastColor(_colorDialog.Color);
                _service.ChangeSelectedBorderColor(_colorDialog.Color);
                Refresh();
            }
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
        }

        private void FileExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}