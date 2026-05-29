using GraphEditor.ViewServices;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GraphEditor
{
    public partial class GraphEditorForm : Form
    {
        private readonly PictureViewService _pictureViewService = new PictureViewService();

        public GraphEditorForm()
        {
            InitializeComponent();
        }

        private void CreateRectangleButton_Click(object sender, EventArgs e)
        {
            _pictureViewService.CreateButtonClicked();
        }

        private void DeleteRectangleButton_Click(object sender, EventArgs e)
        {
            _pictureViewService.DeleteButtonClicked();
        }

        private void FillColorMenuItem_Click(object sender, EventArgs e)
        {
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
            _pictureViewService.MouseDown(e.Location);
            Invalidate();
        }

        private void GraphEditorForm_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = _pictureViewService.GetCursor(e.Location);
            _pictureViewService.MouseMove(e.Location);
            Invalidate();
        }

        private void GraphEditorForm_MouseUp(object sender, MouseEventArgs e)
        {
            _pictureViewService.MouseUp();
            Invalidate();
        }

        private void FileCreateMenuItem_Click(object sender, EventArgs e)
        {
            _pictureViewService.CreateNewPicture();
            Invalidate();
        }

        private void FileOpenMenuItem_Click(object sender, EventArgs e)
        {
            if (FileOpenDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _pictureViewService.Open(FileOpenDialog.FileName);
                    Invalidate();
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
                FileSaveAsMenuItem_Click(sender, e);
                return;
            }
            _pictureViewService.Save();
        }

        private void FileSaveAsMenuItem_Click(object sender, EventArgs e)
        {
            if (FileSaveDialog.ShowDialog() == DialogResult.OK)
            {
                _pictureViewService.Save(FileSaveDialog.FileName);
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

        private void OpacityMenuItem_Click(object sender, EventArgs e)
        {
            if (_pictureViewService == null || !_pictureViewService.CanDelete)
            {
                MessageBox.Show("Выберите объект для изменения прозрачности.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var inputDialog = new Form())
            {
                inputDialog.Text = "Установка прозрачности";
                inputDialog.Size = new Size(300, 150);
                inputDialog.StartPosition = FormStartPosition.CenterParent;
                inputDialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputDialog.MaximizeBox = false;
                inputDialog.MinimizeBox = false;

                var label = new Label()
                {
                    Text = "Введите значение от 1 до 100:",
                    Location = new Point(10, 20),
                    AutoSize = true
                };

                int currentPercent = (int)(_pictureViewService.GetCurrentOpacity() * 100);

                var textBox = new TextBox()
                {
                    Location = new Point(10, 50),
                    Width = 260,
                    Text = currentPercent.ToString()
                };

                var buttonOk = new Button()
                {
                    Text = "OK",
                    DialogResult = DialogResult.OK,
                    Location = new Point(100, 80)
                };

                var buttonCancel = new Button()
                {
                    Text = "Отмена",
                    DialogResult = DialogResult.Cancel,
                    Location = new Point(180, 80)
                };

                inputDialog.Controls.Add(label);
                inputDialog.Controls.Add(textBox);
                inputDialog.Controls.Add(buttonOk);
                inputDialog.Controls.Add(buttonCancel);
                inputDialog.AcceptButton = buttonOk;
                inputDialog.CancelButton = buttonCancel;

                if (inputDialog.ShowDialog() == DialogResult.OK)
                {
                    if (int.TryParse(textBox.Text, out int opacityPercent))
                    {
                        if (opacityPercent >= 1 && opacityPercent <= 100)
                        {
                            float opacity = opacityPercent / 100.0f;
                            _pictureViewService.SetOpacity(opacity);
                            Invalidate();
                        }
                        else
                        {
                            MessageBox.Show("Значение должно быть от 1 до 100.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введите корректное число.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}