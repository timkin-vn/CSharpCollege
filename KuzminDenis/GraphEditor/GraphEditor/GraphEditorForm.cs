using GraphEditor.ViewServices;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GraphEditor
{
    public partial class GraphEditorForm : Form
    {
        private readonly PictureViewService _viewService = new PictureViewService();
        public bool _isMouseDown;

        public GraphEditorForm()
        {
            InitializeComponent();
            UpdateViewControls();
        }

        private void GraphEditorForm_Paint(object sender, PaintEventArgs e)
        {
            _viewService.Paint(e.Graphics);
        }

        private void GraphEditorForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            _isMouseDown = true;
            _viewService.MouseDown(e.Location);
            Refresh();
        }

        private void GraphEditorForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            _isMouseDown = false;
            _viewService.MouseUp();
            Refresh();
            UpdateViewControls();
        }

        private void GraphEditorForm_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = _viewService.GetCursor(e.Location);
            if (!_isMouseDown) return;

            _viewService.MouseMove(e.Location);
            Refresh();
        }

        private void CreateRectangleButton_Click(object sender, EventArgs e)
        {
            _viewService.CreateButtonClicked();
            UpdateViewControls();
        }

        private void UpdateViewControls()
        {
            CreateRectangleButton.Checked = _viewService.CreateMode;
            DeleteRectangleButton.Enabled = _viewService.CanDelete;

            Text = string.IsNullOrEmpty(_viewService.FileName)
                ? "Графический редактор"
                : $"Графический редактор | {Path.GetFileName(_viewService.FileName)}";
        }

        private void DeleteRectangleButton_Click(object sender, EventArgs e)
        {
            _viewService.DeleteButtonClicked();
            Refresh();
            UpdateViewControls();
        }

        private void FileCreateMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.Create();
            UpdateViewControls();
            Refresh();
        }

        private void FileOpenMenuItem_Click(object sender, EventArgs e)
        {
            if (FileOpenDialog.ShowDialog() != DialogResult.OK) return;

            _viewService.Open(FileOpenDialog.FileName);
            UpdateViewControls();
            Refresh();
        }

        private void FileSaveMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_viewService.FileName))
                DoSaveAs();
            else
                _viewService.Save();
        }

        private void FileSaveAsMenuItem_Click(object sender, EventArgs e)
        {
            DoSaveAs();
        }

        private void DoSaveAs()
        {
            if (FileSaveDialog.ShowDialog() != DialogResult.OK) return;

            _viewService.Save(FileSaveDialog.FileName);
            UpdateViewControls();
        }

        private void FileExportMenuItem_Click(object sender, EventArgs e)
        {
            if (FileExportDialog.ShowDialog() != DialogResult.OK) return;

            _viewService.Export(FileExportDialog.FileName, ClientRectangle, BackColor);
        }

        private void FileExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FillColorMenuItem_Click(object sender, EventArgs e)
        {
            FillColorDialog.Color = _viewService.GetCurrentFillColor();
            if (FillColorDialog.ShowDialog() != DialogResult.OK) return;

            _viewService.SetFillColor(FillColorDialog.Color);
            Refresh();
        }

        private void BorderColorMenuItem_Click(object sender, EventArgs e)
        {
            BorderColorDialog.Color = _viewService.GetCurrentBorderColor();
            if (BorderColorDialog.ShowDialog() != DialogResult.OK) return;

            _viewService.SetBorderColor(BorderColorDialog.Color);
            Refresh();
        }

        private void BorderWidthMenuItem_Click(object sender, EventArgs e)
        {
            using (Form inputForm = new Form())
            {
                inputForm.Text = "Толщина контура";
                inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputForm.StartPosition = FormStartPosition.CenterParent;
                inputForm.ClientSize = new Size(180, 90);
                inputForm.MinimizeBox = false;
                inputForm.MaximizeBox = false;

                Label label = new Label
                {
                    Text = "Толщина:",
                    Location = new Point(10, 15),
                    AutoSize = true
                };

                NumericUpDown numeric = new NumericUpDown
                {
                    Minimum = 1,
                    Maximum = 20,
                    Value = _viewService.GetCurrentBorderWidth(),
                    Location = new Point(80, 12),
                    Width = 80
                };

                Button okButton = new Button
                {
                    Text = "ОК",
                    DialogResult = DialogResult.OK,
                    Location = new Point(50, 50),
                    Width = 80
                };

                inputForm.Controls.Add(label);
                inputForm.Controls.Add(numeric);
                inputForm.Controls.Add(okButton);
                inputForm.AcceptButton = okButton;

                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    _viewService.SetBorderWidth((int)numeric.Value);
                    Refresh();
                }
            }
        }

        private void MoveForwardMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.MoveForward();
            Refresh();
        }

        private void MoveBackwardMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.MoveBackward();
            Refresh();
        }

        private void MoveToFrontMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.MoveToFront();
            Refresh();
        }

        private void MoveToBackMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.MoveToBack();
            Refresh();
        }
    }
}
