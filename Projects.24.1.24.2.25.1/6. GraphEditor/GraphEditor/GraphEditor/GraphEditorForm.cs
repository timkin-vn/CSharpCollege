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
using GraphEditor.Business.Enums;

namespace GraphEditor
{
    public partial class GraphEditorForm : Form
    {
        private readonly PictureViewService _viewService = new PictureViewService();

        private bool _isMouseDown;

        public GraphEditorForm()
        {
            InitializeComponent();
            ApplyDarkTheme();
        }

        private void ApplyDarkTheme()
        {
            // Установка тёмной темы для формы
            this.BackColor = Color.FromArgb(30, 30, 34);
            this.ForeColor = Color.White;
            
            // Настройка элементов управления
            CreateRectangleButton.BackColor = Color.FromArgb(50, 50, 54);
            CreateRectangleButton.ForeColor = Color.White;
            
            DeleteRectangleButton.BackColor = Color.FromArgb(50, 50, 54);
            DeleteRectangleButton.ForeColor = Color.White;
            
            // Настройка меню
            menuStrip1.BackColor = Color.FromArgb(40, 40, 44);
            menuStrip1.ForeColor = Color.White;
            
            foreach (var item in menuStrip1.Items)
            {
                if (item is ToolStripMenuItem menuItem)
                {
                    menuItem.BackColor = Color.FromArgb(40, 40, 44);
                    menuItem.ForeColor = Color.White;
                    menuItem.DropDown.BackColor = Color.FromArgb(40, 40, 44);
                    menuItem.DropDown.ForeColor = Color.White;
                }
            }
            
            // Настройка панели инструментов
            toolStrip1.BackColor = Color.FromArgb(40, 40, 44);
            toolStrip1.ForeColor = Color.White;
            
            // Настройка диалоговых окон
            FillColorDialog.Color = Color.FromArgb(50, 50, 54);
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
            _viewService.CreateMode = true;
            _viewService.CurrentShapeType = ShapeType.Rectangle;
            UpdateViewControls();
        }

        private void CreateTriangleButton_Click(object sender, EventArgs e)
        {
            _viewService.CreateMode = true;
            _viewService.CurrentShapeType = ShapeType.Triangle;
            UpdateViewControls();
        }

        private void CreateCircleButton_Click(object sender, EventArgs e)
        {
            _viewService.CreateMode = true;
            _viewService.CurrentShapeType = ShapeType.Circle;
            UpdateViewControls();
        }

        private void CreateHilbertCurveButton_Click(object sender, EventArgs e)
        {
            _viewService.CreateMode = true;
            _viewService.CurrentShapeType = ShapeType.HilbertCurve;
            UpdateViewControls();
        }

        private void UpdateViewControls()
        {
            CreateRectangleButton.Checked = _viewService.CreateMode && _viewService.CurrentShapeType == ShapeType.Rectangle;
            CreateTriangleButton.Checked = _viewService.CreateMode && _viewService.CurrentShapeType == ShapeType.Triangle;
            CreateCircleButton.Checked = _viewService.CreateMode && _viewService.CurrentShapeType == ShapeType.Circle;
            CreateHilbertCurveButton.Checked = _viewService.CreateMode && _viewService.CurrentShapeType == ShapeType.HilbertCurve;
            
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
            FillColorDialog.Color = _viewService.GetCurrentFillColor();
            if (FillColorDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _viewService.SetFillColor(FillColorDialog.Color);
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
    }
}
