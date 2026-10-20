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
        private bool _isMouseDown;

        private TextBox _txtLeft, _txtTop, _txtWidth, _txtHeight;
        private CheckBox _chkSnap;

        public GraphEditorForm()
        {
            InitializeComponent();

            KeyPreview = true;
            KeyDown += GraphEditorForm_KeyDown;

            CreatePropertiesPanel();
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
            UpdateViewControls();
        }

        private void GraphEditorForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            _isMouseDown = false;
            _viewService.MouseUp();
            UpdateViewControls();
            Refresh();
        }

        private void GraphEditorForm_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = _viewService.GetCursor(e.Location);
            if (!_isMouseDown) return;
            _viewService.MouseMove(e.Location);
            Refresh();
        }

        private void GraphEditorForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.C:
                        _viewService.Copy();
                        break;
                    case Keys.V:
                        _viewService.Paste();
                        Refresh();
                        UpdateViewControls();
                        break;
                    case Keys.Z:
                        _viewService.Undo();
                        Refresh();
                        UpdateViewControls();
                        break;
                    case Keys.Y:
                        _viewService.Redo();
                        Refresh();
                        UpdateViewControls();
                        break;
                }
            }
        }

        private void CreatePropertiesPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Right,
                Width = 200,
                BackColor = Color.WhiteSmoke,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10)
            };

            int y = 20;

            _chkSnap = new CheckBox
            {
                Text = "Привязать к сетке",
                Location = new Point(10, y),
                Checked = _viewService.SnapToGrid,
                AutoSize = true
            };
            _chkSnap.CheckedChanged += (s, e) => { _viewService.SnapToGrid = _chkSnap.Checked; Refresh(); };
            panel.Controls.Add(_chkSnap);
            y += 30;

            y = AddPropertyField(panel, "X:", y, out _txtLeft);
            y = AddPropertyField(panel, "Y:", y, out _txtTop);
            y = AddPropertyField(panel, "Ширина:", y, out _txtWidth);
            y = AddPropertyField(panel, "Высота:", y, out _txtHeight);

            var btnApply = new Button
            {
                Text = "Применить",
                Location = new Point(10, y),
                Width = 160,
                Height = 30
            };
            btnApply.Click += BtnApply_Click;
            panel.Controls.Add(btnApply);

            Controls.Add(panel);
        }

        private int AddPropertyField(Panel panel, string label, int yPos, out TextBox textBox)
        {
            var lbl = new Label { Text = label, Location = new Point(10, yPos + 3), AutoSize = true };
            textBox = new TextBox { Location = new Point(70, yPos), Width = 100 };
            panel.Controls.Add(lbl);
            panel.Controls.Add(textBox);
            return yPos + 30;
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            if (int.TryParse(_txtLeft.Text, out int l) &&
                int.TryParse(_txtTop.Text, out int t) &&
                int.TryParse(_txtWidth.Text, out int w) &&
                int.TryParse(_txtHeight.Text, out int h))
            {
                _viewService.UpdateSelectedProperties(l, t, w, h);
                Refresh();
            }
            else
            {
                MessageBox.Show("Введите корректные числовые значения!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateViewControls()
        {
            CreateRectangleToolStripButton.Checked = _viewService.CreateMode;
            DeleteRectangleToolStripButton.Enabled = _viewService.CanDelete;
            Text = string.IsNullOrEmpty(_viewService.FileName)
                ? "Графический редактор"
                : $"Графический редактор | {Path.GetFileName(_viewService.FileName)}";

            var sel = _viewService.GetSelectedRectangle();
            if (sel != null)
            {
                _txtLeft.Text = sel.Left.ToString();
                _txtTop.Text = sel.Top.ToString();
                _txtWidth.Text = sel.Width.ToString();
                _txtHeight.Text = sel.Height.ToString();
            }
            else
            {
                _txtLeft.Text = _txtTop.Text = _txtWidth.Text = _txtHeight.Text = "";
            }
        }

        private void CreateRectangleToolStripButton_Click(object sender, EventArgs e) { _viewService.CreateButtonClicked(); UpdateViewControls(); }
        private void DeleteRectangleToolStripButton_Click(object sender, EventArgs e) { _viewService.DeleteButtonClicked(); Refresh(); UpdateViewControls(); }
        private void FillColorMenuItem_Click(object sender, EventArgs e) { FillColorDialog.Color = _viewService.GetCurrentFillColor(); if (FillColorDialog.ShowDialog() == DialogResult.OK) { _viewService.SetFillColor(FillColorDialog.Color); Refresh(); } }
        private void MoveForwardMenuItem_Click(object sender, EventArgs e) { _viewService.MoveForward(); Refresh(); }
        private void FileCreateMenuItem_Click(object sender, EventArgs e) { _viewService.CreateNewPicture(); Refresh(); UpdateViewControls(); }
        private void FileOpenMenuItem_Click(object sender, EventArgs e) { if (FileOpenDialog.ShowDialog() == DialogResult.OK) { _viewService.Open(FileOpenDialog.FileName); UpdateViewControls(); Refresh(); } }
        private void FileSaveMenuItem_Click(object sender, EventArgs e) { if (string.IsNullOrEmpty(_viewService.FileName)) DoSaveAs(); else _viewService.Save(); }
        private void FileSaveAsMenuItem_Click(object sender, EventArgs e) { DoSaveAs(); }
        private void DoSaveAs() { if (FileSaveDialog.ShowDialog() == DialogResult.OK) { _viewService.Save(FileSaveDialog.FileName); UpdateViewControls(); } }
        private void FileExportMenuItem_Click(object sender, EventArgs e) { if (FileExportDialog.ShowDialog() == DialogResult.OK) { _viewService.Export(FileExportDialog.FileName, ClientRectangle, BackColor); } }
        private void FileExitMenuItem_Click(object sender, EventArgs e) { Close(); }
    }
}