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

        private bool _isMouseDown;

        public GraphEditorForm()
        {
            InitializeComponent();
        }

        private void GraphEditorForm_Paint(object sender, PaintEventArgs e)
        {
            _viewService.Paint(e.Graphics);
        }

        private void GraphEditorForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            _isMouseDown = true;
            _viewService.MouseDown(e.Location);
            Refresh();
            UpdateViewControls();
        }

        private void GraphEditorForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            _isMouseDown = false;
            _viewService.MouseUp();
            Refresh();
            UpdateViewControls();
        }

        private void GraphEditorForm_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = _viewService.GetCursor(e.Location);

            if (!_isMouseDown)
            {
                return;
            }

            _viewService.MouseMove(e.Location);
            Refresh();
            UpdateViewControls();
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
            Text = string.IsNullOrEmpty(_viewService.FileName) ?
                "Графический редактор" :
                $"Графический редактор | {Path.GetFileName(_viewService.FileName)}";
        }

        private void DeleteRectangleButton_Click(object sender, EventArgs e)
        {
            _viewService.DeleteButtonClicked();
            Refresh();
            UpdateViewControls();
        }

        private void FillColorMenuItem_Click(object sender, EventArgs e)
        {
            
            Color currentFill = _viewService.GetCurrentFillColor();
            FillColorDialog.Color = currentFill;
            FillColorDialog.FullOpen = true;

            if (FillColorDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            Color selectedFillColor = FillColorDialog.Color;

            string fillAlphaInput = ShowInputBox(
                "Укажите прозрачность ЗАЛИВКИ от 0 (прозрачно) до 100 (плотно):",
                "Прозрачность заливки фигуры",
                ((int)(currentFill.A / 2.55)).ToString());

            byte fillAlpha = currentFill.A;
            if (int.TryParse(fillAlphaInput, out int fillPercent))
            {
                fillPercent = Math.Max(0, Math.Min(100, fillPercent));
                fillAlpha = (byte)(fillPercent * 2.55);
            }
            _viewService.SetFillProperties(selectedFillColor, fillAlpha);


            Color currentBorder = _viewService.GetCurrentBorderColor();
            FillColorDialog.Color = currentBorder;

            if (FillColorDialog.ShowDialog() != DialogResult.OK)
            {
                Refresh();
                return;
            }
            Color selectedBorderColor = FillColorDialog.Color;

            string borderAlphaInput = ShowInputBox(
                "Укажите прозрачность ОБВОДКИ от 0 (полностью прозрачно) до 100 (непрозрачно):",
                "Прозрачность границ",
                ((int)(currentBorder.A / 2.55)).ToString());

            byte borderAlpha = currentBorder.A;
            if (int.TryParse(borderAlphaInput, out int borderPercent))
            {
                borderPercent = Math.Max(0, Math.Min(100, borderPercent));
                borderAlpha = (byte)(borderPercent * 2.55);
            }

            float currentThickness = _viewService.GetCurrentBorderThickness();
            string thicknessInput = ShowInputBox(
                "Укажите толщину линии границы (в пикселях от 1 до 20):",
                "Толщина обводки",
                currentThickness.ToString());

            if (!float.TryParse(thicknessInput, out float thickness))
            {
                thickness = currentThickness;
            }
            thickness = Math.Max(1.0f, Math.Min(20.0f, thickness));

            _viewService.SetBorderProperties(selectedBorderColor, borderAlpha, thickness);
            Refresh();
        }

        private void BorderSettingsMenuItem_Click(object sender, EventArgs e)
        {
            Color currentBorder = _viewService.GetCurrentBorderColor();
            float currentThickness = _viewService.GetCurrentBorderThickness();

            FillColorDialog.Color = currentBorder;
            FillColorDialog.FullOpen = true;

            if (FillColorDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string borderAlphaInput = ShowInputBox(
                "Укажите прозрачность ОБВОДКИ от 0 (полностью прозрачно) до 100 (непрозрачно):",
                "Прозрачность границ",
                ((int)(currentBorder.A / 2.55)).ToString());

            byte alpha = currentBorder.A;
            if (int.TryParse(borderAlphaInput, out int borderPercent))
            {
                borderPercent = Math.Max(0, Math.Min(100, borderPercent));
                alpha = (byte)(borderPercent * 2.55);
            }

            string thicknessInput = ShowInputBox(
                "Укажите толщину линии границы (в пикселях от 1 до 20):",
                "Толщина обводки",
                currentThickness.ToString());

            if (!float.TryParse(thicknessInput, out float thickness))
            {
                thickness = currentThickness;
            }
            thickness = Math.Max(1.0f, Math.Min(20.0f, thickness));

            _viewService.SetBorderProperties(FillColorDialog.Color, alpha, thickness);
            Refresh();
        }

        private void MoveForwardMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.MoveForward();
            Refresh();
        }

        private void FileCreateMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.CreateNewPicture();
            Refresh();
            UpdateViewControls();
        }

        private void FileOpenMenuItem_Click(object sender, EventArgs e)
        {
            if (FileOpenDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _viewService.Open(FileOpenDialog.FileName);
            UpdateViewControls();
            Refresh();
        }

        private void FileSaveMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_viewService.FileName))
            {
                DoSaveAs();
            }
            else
            {
                _viewService.Save();
            }
        }

        private void FileSaveAsMenuItem_Click(object sender, EventArgs e)
        {
            DoSaveAs();
        }

        private void FileExportMenuItem_Click(object sender, EventArgs e)
        {
            if (FileExportDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _viewService.Export(FileExportDialog.FileName, ClientRectangle, BackColor);
        }

        private void FileExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DoSaveAs()
        {
            if (FileSaveDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _viewService.Save(FileSaveDialog.FileName);
            UpdateViewControls();
        }

        private void GraphEditorForm_Load(object sender, EventArgs e)
        {

        }

        public static string ShowInputBox(string text, string caption, string defaultValue = "")
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 160,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false
            };
            Label textLabel = new Label() { Left = 20, Top = 15, Width = 350, Text = text };
            TextBox textBox = new TextBox() { Left = 20, Top = 45, Width = 345, Text = defaultValue };
            Button confirmation = new Button() { Text = "OK", Left = 285, Width = 80, Top = 80, DialogResult = DialogResult.OK };

            confirmation.Click += (s, ev) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : defaultValue;
        }
    }
}
