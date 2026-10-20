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

        private StatusStrip _statusStrip;
        private ToolStripStatusLabel _selectionLabel;
        private ToolStripStatusLabel _countLabel;
        private ToolStripButton _duplicateButton;
        private ToolStripButton _gridButton;
        private ToolStripSplitButton _borderColorButton;
        private ColorDialog _borderColorDialog = new ColorDialog();

        private sealed class DarkColorTable : ProfessionalColorTable
        {
            public override Color MenuItemSelected => Color.FromArgb(70, 70, 78);
            public override Color MenuItemSelectedGradientBegin => Color.FromArgb(70, 70, 78);
            public override Color MenuItemSelectedGradientEnd => Color.FromArgb(70, 70, 78);
            public override Color MenuItemBorder => Color.FromArgb(0, 150, 255);
            public override Color MenuItemPressedGradientBegin => Color.FromArgb(55, 55, 62);
            public override Color MenuItemPressedGradientEnd => Color.FromArgb(55, 55, 62);
            public override Color ToolStripDropDownBackground => Color.FromArgb(40, 40, 44);
            public override Color ImageMarginGradientBegin => Color.FromArgb(40, 40, 44);
            public override Color ImageMarginGradientMiddle => Color.FromArgb(40, 40, 44);
            public override Color ImageMarginGradientEnd => Color.FromArgb(40, 40, 44);
            public override Color ButtonSelectedHighlight => Color.FromArgb(70, 70, 78);
            public override Color ButtonSelectedGradientBegin => Color.FromArgb(70, 70, 78);
            public override Color ButtonSelectedGradientEnd => Color.FromArgb(70, 70, 78);
            public override Color ButtonCheckedGradientBegin => Color.FromArgb(0, 100, 180);
            public override Color ButtonCheckedGradientMiddle => Color.FromArgb(0, 100, 180);
            public override Color ButtonCheckedGradientEnd => Color.FromArgb(0, 100, 180);
            public override Color ButtonPressedGradientBegin => Color.FromArgb(0, 120, 210);
            public override Color ButtonPressedGradientMiddle => Color.FromArgb(0, 120, 210);
            public override Color ButtonPressedGradientEnd => Color.FromArgb(0, 120, 210);
            public override Color ButtonSelectedBorder => Color.FromArgb(0, 150, 255);
            public override Color ButtonCheckedHighlightBorder => Color.FromArgb(0, 150, 255);
            public override Color ToolStripGradientBegin => Color.FromArgb(40, 40, 44);
            public override Color ToolStripGradientMiddle => Color.FromArgb(40, 40, 44);
            public override Color ToolStripGradientEnd => Color.FromArgb(40, 40, 44);
            public override Color MenuStripGradientBegin => Color.FromArgb(40, 40, 44);
            public override Color MenuStripGradientEnd => Color.FromArgb(40, 40, 44);
            public override Color SeparatorDark => Color.FromArgb(80, 80, 88);
            public override Color SeparatorLight => Color.FromArgb(80, 80, 88);
            public override Color MenuBorder => Color.FromArgb(80, 80, 88);
            public override Color ToolStripBorder => Color.FromArgb(40, 40, 44);
        }

        public GraphEditorForm()
        {
            InitializeComponent();
            ApplyDarkTheme();
            ExtendUi();
            KeyPreview = true;
            ClientSize = new Size(1000, 650);
            StartPosition = FormStartPosition.CenterScreen;
            UpdateViewControls();
        }

        private void ExtendUi()
        {
            // Подсказки и горячие клавиши для существующих пунктов
            CreateRectangleButton.ToolTipText = "Нарисовать прямоугольник (R)";
            CreateTriangleButton.ToolTipText = "Нарисовать треугольник (T)";
            CreateCircleButton.ToolTipText = "Нарисовать эллипс (E)";
            CreateHilbertCurveButton.ToolTipText = "Нарисовать кривую Гильберта (H)";
            DeleteRectangleButton.ToolTipText = "Удалить выбранную фигуру (Delete)";
            FileCreateMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            FileOpenMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            FileSaveMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            FileSaveAsMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            FileExportMenuItem.ShortcutKeys = Keys.Control | Keys.E;
            FillColorMenuItem.Text = "Цвет заливки...";

            // Новые кнопки на панели
            _duplicateButton = new ToolStripButton("Дублировать") { DisplayStyle = ToolStripItemDisplayStyle.Text, ToolTipText = "Дублировать фигуру (Ctrl+D)" };
            _duplicateButton.Click += (s, e) => { _viewService.Duplicate(); Invalidate(); UpdateViewControls(); };

            _gridButton = new ToolStripButton("Сетка") { DisplayStyle = ToolStripItemDisplayStyle.Text, CheckOnClick = true, Checked = _viewService.ShowGrid, ToolTipText = "Показать/скрыть сетку (G)" };
            _gridButton.Click += (s, e) => { _viewService.ShowGrid = _gridButton.Checked; Invalidate(); };

            var borderItem = new ToolStripMenuItem("Цвет границы...");
            borderItem.Click += BorderColorMenuItem_Click;
            _borderColorButton = new ToolStripSplitButton("Граница") { DisplayStyle = ToolStripItemDisplayStyle.Text };
            _borderColorButton.ButtonClick += BorderColorMenuItem_Click;
            _borderColorButton.DropDownItems.Add(borderItem);

            toolStrip1.Items.Add(_borderColorButton);
            toolStrip1.Items.Add(_duplicateButton);
            toolStrip1.Items.Add(new ToolStripSeparator());
            toolStrip1.Items.Add(_gridButton);

            // Порядок
            MoveForwardMenuItem.Text = "Вперёд на один слой";
            var back = new ToolStripMenuItem("Назад на один слой");
            back.Click += (s, e) => { _viewService.MoveBackward(); Invalidate(); };
            var front = new ToolStripMenuItem("На передний план") { ShortcutKeys = Keys.Control | Keys.Shift | Keys.F };
            front.Click += (s, e) => { _viewService.BringToFront(); Invalidate(); };
            var toBack = new ToolStripMenuItem("На задний план") { ShortcutKeys = Keys.Control | Keys.Shift | Keys.B };
            toBack.Click += (s, e) => { _viewService.SendToBack(); Invalidate(); };
            toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { back, front, toBack });

            // Меню «Правка»
            var edit = new ToolStripMenuItem("Правка");
            var dup = new ToolStripMenuItem("Дублировать") { ShortcutKeys = Keys.Control | Keys.D };
            dup.Click += (s, e) => { _viewService.Duplicate(); Invalidate(); UpdateViewControls(); };
            var del = new ToolStripMenuItem("Удалить") { ShortcutKeyDisplayString = "Del" };
            del.Click += DeleteRectangleButton_Click;
            var orderUp = new ToolStripMenuItem("Порядок кривой Гильберта +") { ShortcutKeyDisplayString = "+" };
            orderUp.Click += (s, e) => { _viewService.ChangeHilbertOrder(1); Invalidate(); UpdateViewControls(); };
            var orderDown = new ToolStripMenuItem("Порядок кривой Гильберта −") { ShortcutKeyDisplayString = "−" };
            orderDown.Click += (s, e) => { _viewService.ChangeHilbertOrder(-1); Invalidate(); UpdateViewControls(); };
            edit.DropDownItems.AddRange(new ToolStripItem[] { dup, del, new ToolStripSeparator(), orderUp, orderDown });
            menuStrip1.Items.Insert(1, edit);

            // Статус-бар
            _selectionLabel = new ToolStripStatusLabel { Spring = true, TextAlign = ContentAlignment.MiddleLeft };
            _countLabel = new ToolStripStatusLabel();
            _statusStrip = new StatusStrip { SizingGrip = false };
            _statusStrip.Items.AddRange(new ToolStripItem[] { _selectionLabel, _countLabel });
            Controls.Add(_statusStrip);

            // Тема
            var renderer = new ToolStripProfessionalRenderer(new DarkColorTable());
            ToolStripManager.Renderer = renderer;
            _statusStrip.BackColor = Color.FromArgb(0, 100, 180);
            _statusStrip.ForeColor = Color.White;
            foreach (var item in menuStrip1.Items.OfType<ToolStripMenuItem>())
            {
                item.ForeColor = Color.White;
                item.DropDown.BackColor = Color.FromArgb(40, 40, 44);
                foreach (ToolStripItem sub in item.DropDownItems) sub.ForeColor = Color.White;
            }
            foreach (ToolStripItem item in toolStrip1.Items) item.ForeColor = Color.White;
            foreach (var dd in toolStrip1.Items.OfType<ToolStripDropDownItem>())
                foreach (ToolStripItem sub in dd.DropDownItems) sub.ForeColor = Color.White;
            toolStrip1.Padding = new Padding(6, 3, 6, 3);
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
        }

        private void BorderColorMenuItem_Click(object sender, EventArgs e)
        {
            if (!_viewService.HasSelection) return;
            _borderColorDialog.Color = _viewService.GetCurrentBorderColor();
            if (_borderColorDialog.ShowDialog() != DialogResult.OK) return;
            _viewService.SetBorderColor(_borderColorDialog.Color);
            Invalidate();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int step = 1;
            Keys key = keyData;
            if ((keyData & Keys.Shift) == Keys.Shift) { step = 10; key = keyData & ~Keys.Shift; }

            if (key == Keys.Left || key == Keys.Right || key == Keys.Up || key == Keys.Down)
            {
                if (_viewService.HasSelection)
                {
                    int dx = key == Keys.Left ? -step : key == Keys.Right ? step : 0;
                    int dy = key == Keys.Up ? -step : key == Keys.Down ? step : 0;
                    _viewService.Nudge(dx, dy);
                    Invalidate();
                    UpdateViewControls();
                    return true;
                }
            }

            switch (keyData)
            {
                case Keys.Delete:
                    if (_viewService.CanDelete) DeleteRectangleButton_Click(this, EventArgs.Empty);
                    return true;
                case Keys.Escape:
                    _viewService.CancelCreateMode();
                    Invalidate();
                    UpdateViewControls();
                    return true;
                case Keys.R: CreateRectangleButton_Click(this, EventArgs.Empty); return true;
                case Keys.T: CreateTriangleButton_Click(this, EventArgs.Empty); return true;
                case Keys.E: CreateCircleButton_Click(this, EventArgs.Empty); return true;
                case Keys.H: CreateHilbertCurveButton_Click(this, EventArgs.Empty); return true;
                case Keys.G:
                    _gridButton.Checked = !_gridButton.Checked;
                    _viewService.ShowGrid = _gridButton.Checked;
                    Invalidate();
                    return true;
                case Keys.Oemplus:
                case Keys.Add:
                    _viewService.ChangeHilbertOrder(1); Invalidate(); UpdateViewControls(); return true;
                case Keys.OemMinus:
                case Keys.Subtract:
                    _viewService.ChangeHilbertOrder(-1); Invalidate(); UpdateViewControls(); return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
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
            _viewService.Paint(e.Graphics, ClientRectangle);
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
            if (_duplicateButton != null)
            {
                _duplicateButton.Enabled = _viewService.HasSelection;
                _borderColorButton.Enabled = _viewService.HasSelection;
                _selectionLabel.Text = _viewService.CreateMode
                    ? "Режим создания: кликните и тяните мышью (Esc — отмена)"
                    : _viewService.SelectionDescription;
                _countLabel.Text = $"Фигур: {_viewService.ShapeCount}";
            }
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
