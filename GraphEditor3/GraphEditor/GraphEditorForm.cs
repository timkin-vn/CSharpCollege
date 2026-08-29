using GraphEditor.ViewServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            Text = string.IsNullOrEmpty(_service.FileName) ?
                "Графический редактор" :
                $"Графический редактор | {Path.GetFileName(_service.FileName)}";
        }

        private void DeleteRectangleButton_Click(object sender, EventArgs e)
        {
            _service.DeleteButtonClick();
            Refresh();
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
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (BorderColorDialog == null)
            {
                BorderColorDialog = new ColorDialog();
            }

            BorderColorDialog.Color = _service.GetBorderColor();
            if (BorderColorDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _service.SetBorderColor(BorderColorDialog.Color);
            Refresh();
        }

        private void MoveForwardMenuItem_Click(object sender, EventArgs e)
        {
            _service.MoveForward();
            Refresh();
        }
        private void MoveBackwardMenuItem_Click(object sender, EventArgs e)
        {
            _service.MoveBackward();
            Refresh();
        }
        private void MoveToFrontMenuItem_Click(object sender, EventArgs e)
        {
            _service.MoveToFront();
            Refresh();
        }

        private void MoveToBackMenuItem_Click(object sender, EventArgs e)
        {
            _service.MoveToBack();
            Refresh();
        }
        private void gradientMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_service == null)
                {
                    MessageBox.Show("Сервис не инициализирован!", "Ошибка");
                    return;
                }

                using (var gradientForm = new GradientForm())
                {
                    if (gradientForm.ShowDialog() == DialogResult.OK)
                    {
                        _service.SetFillType(GraphEditor.Business.Models.FillType.Gradient);
                        _service.SetGradientColors(gradientForm.Color1, gradientForm.Color2);

                        Refresh();
                        MessageBox.Show("Градиент применен успешно!", "Успех");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

        private void textureMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (var textureForm = new TextureForm())
                {
                    if (textureForm.ShowDialog() == DialogResult.OK)
                    {
                        _service.SetFillType(FillType.Texture);
                        _service.SetTexturePath(textureForm.SelectedImagePath);

                        Refresh();
                        MessageBox.Show("Картинка успешно установлена на фон!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}