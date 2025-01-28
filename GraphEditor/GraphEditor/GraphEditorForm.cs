using GraphEditor.PresenterServices;
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
        private VisualService _service = new VisualService();

        private bool _isMousePressed;

        public GraphEditorForm()
        {
            InitializeComponent();
        }

        private void GraphEditorForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isMousePressed = true;
                _service.MouseDown(e.Location);
                Refresh();
            }
        }

        private void GraphEditorForm_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = _service.GetCursor(e.Location);

            if (_isMousePressed)
            {
                _service.MouseMove(e.Location);
                Refresh();
            }
        }

        private void GraphEditorForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isMousePressed)
            {
                _service.MouseUp();
                _isMousePressed = false;
                ResetInterface();
                Refresh();
            }
        }

        private void GraphEditorForm_Paint(object sender, PaintEventArgs e)
        {
            _service.Paint(e.Graphics);
        }

        private void CreateRectangleToolButton_Click(object sender, EventArgs e)
        {
            _service.CreateMode = !_service.CreateMode;
            ResetInterface();
        }

        private void ResetInterface()
        {
            CreateRectangleToolButton.Checked = _service.CreateMode;
            DeleteToolButton.Enabled = _service.DeleteButtonEnabled;
            FillToolMenuItem.Enabled = _service.DeleteButtonEnabled;
            ForwardToolMenuItem.Enabled = _service.DeleteButtonEnabled;
            Text = string.IsNullOrEmpty(_service.FileName) ? "Графический редактор" : $"Графический редактор - {_service.FileName}";
        }

        private void DeleteToolButton_Click(object sender, EventArgs e)
        {
            _service.Delete();
            ResetInterface();
            Refresh();
        }

        private void GraphEditorForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                _service.Delete();
                ResetInterface();
                Refresh();
            }
        }

        private void FillToolMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog.Color = _service.GetSelectedFillColor();
            if (ColorDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _service.SetSelectedFillColor(ColorDialog.Color);
            Refresh();
        }
        private void DrawToolMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog.Color = _service.GetSelectedDrawColor();
            if (ColorDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _service.SetSelectedDrawColor(ColorDialog.Color);
            Refresh();
        }

        private void ForwardToolMenuItem_Click(object sender, EventArgs e)
        {
            _service.MoveForward();
            Refresh();
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FileOpenMenuItem_Click(object sender, EventArgs e)
        {
            OpenDialog.Filter = "Файлы картинок|*.pict";
            if (OpenDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _service.Open(OpenDialog.FileName);
            Refresh();
        }

        private void FileSaveMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_service.FileName))
            {
                SaveAs();
            }

            _service.Save();
        }

        private void FileSaveAsMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void ExportMenuItem_Click(object sender, EventArgs e)
        {
            SaveDialog.Filter = "Файлы png|*.png";
            if (SaveDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _service.Export(SaveDialog.FileName, ClientRectangle, BackColor);
        }

        private void SaveAs()
        {
            SaveDialog.Filter = "Файлы картинок|*.pict";
            if (SaveDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _service.Save(SaveDialog.FileName);
            ResetInterface();
        }

    }
}
