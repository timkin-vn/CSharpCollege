using GraphEditor.ViewServices;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GraphEditor
{
    public partial class GraphEditorForm : Form
    {
        private readonly PictureViewService _pictureViewService = new PictureViewService();

        private bool _isMouseDown;

        public GraphEditorForm()
        {
            InitializeComponent();
            UpdateViewControls();
        }

        private void CreateRectangleButton_Click(object sender, EventArgs e)
        {
            _pictureViewService.CreateButtonClicked();
            UpdateViewControls();
        }

        private void DeleteRectangleButton_Click(object sender, EventArgs e)
        {
            _pictureViewService.DeleteButtonClicked();
            Invalidate();
            UpdateViewControls();
        }

        private void FillColorMenuItem_Click(object sender, EventArgs e)
        {
            FillColorDialog.Color = _pictureViewService.GetCurrentFillColor();
            if (FillColorDialog.ShowDialog() == DialogResult.OK)
            {
                _pictureViewService.SetFillColor(FillColorDialog.Color);
                Invalidate();
            }
        }

        private void GraphEditorForm_Paint(object sender, PaintEventArgs e)
        {
            _pictureViewService.Paint(e.Graphics);
        }

        private void GraphEditorForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            _isMouseDown = true;
            _pictureViewService.MouseDown(e.Location);
            Invalidate();
            UpdateViewControls();
        }

        private void GraphEditorForm_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = _pictureViewService.GetCursor(e.Location);

            if (!_isMouseDown)
            {
                return;
            }

            _pictureViewService.MouseMove(e.Location);
            Invalidate();
        }

        private void GraphEditorForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            _isMouseDown = false;
            _pictureViewService.MouseUp();
            Invalidate();
            UpdateViewControls();
        }

        private void UpdateViewControls()
        {
            CreateRectangleButton.Checked = _pictureViewService.CreateMode;
            DeleteRectangleButton.Enabled = _pictureViewService.CanDelete;
            Text = string.IsNullOrEmpty(_pictureViewService.FileName)
                ? "Графический редактор"
                : $"Графический редактор | {Path.GetFileName(_pictureViewService.FileName)}";
        }

        private void FileCreateMenuItem_Click(object sender, EventArgs e)
        {
            _pictureViewService.CreateNewPicture();
            Invalidate();
            UpdateViewControls();
        }

        private void FileOpenMenuItem_Click(object sender, EventArgs e)
        {
            if (FileOpenDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _pictureViewService.Open(FileOpenDialog.FileName);
                    Invalidate();
                    UpdateViewControls();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FileSaveMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_pictureViewService.FileName))
            {
                DoSaveAs();
                return;
            }

            try
            {
                _pictureViewService.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

            try
            {
                _pictureViewService.Save(FileSaveDialog.FileName);
                UpdateViewControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FileExportMenuItem_Click(object sender, EventArgs e)
        {
            if (FileExportDialog.ShowDialog() == DialogResult.OK)
            {
                _pictureViewService.Export(FileExportDialog.FileName, ClientRectangle, BackColor);
            }
        }

        private void FileExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MoveForwardMenuItem_Click(object sender, EventArgs e)
        {
            _pictureViewService.MoveForward();
            Invalidate();
        }

        private void MoveBackwardMenuItem_Click(object sender, EventArgs e)
        {
            _pictureViewService.MoveBackward();
            Invalidate();
        }

        private void OpacityMenuItem_Click(object sender, EventArgs e)
        {
            if (!_pictureViewService.CanDelete)
            {
                MessageBox.Show("Выберите объект для изменения прозрачности.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var inputDialog = new Form())
            {
                inputDialog.Text = "Установка прозрачности";
                inputDialog.ClientSize = new Size(290, 120);
                inputDialog.StartPosition = FormStartPosition.CenterParent;
                inputDialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputDialog.MaximizeBox = false;
                inputDialog.MinimizeBox = false;

                var label = new Label()
                {
                    Text = "Введите значение от 1 до 100:",
                    Location = new Point(10, 15),
                    AutoSize = true
                };

                int currentPercent = (int)Math.Round(_pictureViewService.GetCurrentOpacity() * 100);

                var textBox = new TextBox()
                {
                    Location = new Point(10, 40),
                    Width = 270,
                    Text = currentPercent.ToString()
                };

                var buttonOk = new Button()
                {
                    Text = "OK",
                    DialogResult = DialogResult.OK,
                    Location = new Point(110, 80)
                };

                var buttonCancel = new Button()
                {
                    Text = "Отмена",
                    DialogResult = DialogResult.Cancel,
                    Location = new Point(195, 80)
                };

                inputDialog.Controls.Add(label);
                inputDialog.Controls.Add(textBox);
                inputDialog.Controls.Add(buttonOk);
                inputDialog.Controls.Add(buttonCancel);
                inputDialog.AcceptButton = buttonOk;
                inputDialog.CancelButton = buttonCancel;

                if (inputDialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                if (!int.TryParse(textBox.Text, out int opacityPercent))
                {
                    MessageBox.Show("Введите корректное число.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (opacityPercent < 1 || opacityPercent > 100)
                {
                    MessageBox.Show("Значение должно быть от 1 до 100.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _pictureViewService.SetOpacity(opacityPercent / 100.0f);
                Invalidate();
            }
        }
    }
}
