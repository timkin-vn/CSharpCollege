using GraphEditor.Business.Services;
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

        // Элементы интерфейса (создаем программно)
        private MenuStrip menuStrip;
        private ToolStrip toolStrip;
        private ToolStripButton btnRect;
        private ToolStripButton btnCircle;
        private ToolStripButton btnTriangle;
        private ToolStripButton btnDelete;
        private ToolStripButton btnColor;
        private ToolStripButton btnBringForward;
        private ToolStripButton btnSendBackward;

        public GraphEditorForm()
        {
            // Базовые настройки формы
            this.Text = "Графический редактор";
            this.Size = new Size(900, 600);
            this.DoubleBuffered = true; // Убирает мерцание при рисовании

            // Инициализация интерфейса кодом
            InitializeCustomUI();
        }

        private void InitializeCustomUI()
        {
            // 1. Создаем верхнее меню (File)
            menuStrip = new MenuStrip();
            var menuFile = new ToolStripMenuItem("Файл");

            var itemNew = new ToolStripMenuItem("Создать", null, (s, e) => { _viewService.CreateNew(); Refresh(); UpdateTitle(); });
            var itemOpen = new ToolStripMenuItem("Открыть...", null, MenuOpen_Click);
            var itemSave = new ToolStripMenuItem("Сохранить", null, MenuSave_Click);
            var itemExport = new ToolStripMenuItem("Экспорт в PNG...", null, MenuExport_Click);
            var itemExit = new ToolStripMenuItem("Выход", null, (s, e) => Close());

            menuFile.DropDownItems.AddRange(new ToolStripItem[] { itemNew, itemOpen, itemSave, new ToolStripSeparator(), itemExport, new ToolStripSeparator(), itemExit });
            menuStrip.Items.Add(menuFile);

            // 2. Создаем панель инструментов (ToolStrip)
            toolStrip = new ToolStrip();

            btnRect = new ToolStripButton("Прямоугольник", null, (s, e) => SetCreationType(PictureService.FigureType.Rectangle, btnRect));
            btnCircle = new ToolStripButton("Круг", null, (s, e) => SetCreationType(PictureService.FigureType.Circle, btnCircle));
            btnTriangle = new ToolStripButton("Треугольник", null, (s, e) => SetCreationType(PictureService.FigureType.Triangle, btnTriangle));

            btnDelete = new ToolStripButton("Удалить", null, (s, e) => { _viewService.DeleteSelected(); Refresh(); });
            btnColor = new ToolStripButton("Цвет...", null, BtnColor_Click);
            btnBringForward = new ToolStripButton("Вперед (Z+)", null, (s, e) => { _viewService.MoveForward(); Refresh(); });
            btnSendBackward = new ToolStripButton("Назад (Z-)", null, (s, e) => { _viewService.MoveBackward(); Refresh(); });

            // Настраиваем стиль кнопок
            foreach (var btn in new[] { btnRect, btnCircle, btnTriangle, btnDelete, btnColor, btnBringForward, btnSendBackward })
            {
                btn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            }

            toolStrip.Items.AddRange(new ToolStripItem[] {
                btnRect, btnCircle, btnTriangle,
                new ToolStripSeparator(),
                btnDelete, btnColor,
                new ToolStripSeparator(),
                btnBringForward, btnSendBackward
            });

            // Добавляем элементы на форму
            this.Controls.Add(toolStrip);
            this.Controls.Add(menuStrip);
            this.MainMenuStrip = menuStrip;

            // Привязка событий мыши к форме
            this.Paint += GraphEditorForm_Paint;
            this.MouseDown += GraphEditorForm_MouseDown;
            this.MouseMove += GraphEditorForm_MouseMove;
            this.MouseUp += GraphEditorForm_MouseUp;

            // Выделяем прямоугольник по умолчанию
            HighlightActiveTool(btnRect);
        }

        private void SetCreationType(PictureService.FigureType type, ToolStripButton activeBtn)
        {
            _viewService.CurrentCreationType = type;
            _viewService.CreateMode = true;
            HighlightActiveTool(activeBtn);
        }

        private void HighlightActiveTool(ToolStripButton activeBtn)
        {
            btnRect.Checked = (activeBtn == btnRect);
            btnCircle.Checked = (activeBtn == btnCircle);
            btnTriangle.Checked = (activeBtn == btnTriangle);
        }

        // --- Обработчики событий мыши и отрисовки ---

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

        private void GraphEditorForm_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = _viewService.GetCursor(e.Location);
            if (!_isMouseDown) return;
            _viewService.MouseMove(e.Location);
            Refresh();
        }

        private void GraphEditorForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            _isMouseDown = false;
            _viewService.MouseUp();

            // Сбрасываем подсветку кнопки создания, так как фигура уже нарисована
            HighlightActiveTool(null);
            Refresh();
        }

        // --- Обработчики меню и кнопок ---

        private void BtnColor_Click(object sender, EventArgs e)
        {
            using (var dlg = new ColorDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _viewService.SetColor(dlg.Color);
                    Refresh();
                }
            }
        }

        private void MenuSave_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog { Filter = "Project files (*.ged)|*.ged" })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _viewService.Save(dlg.FileName);
                    UpdateTitle();
                }
            }
        }

        private void MenuOpen_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog { Filter = "Project files (*.ged)|*.ged" })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _viewService.Open(dlg.FileName);
                    UpdateTitle();
                    Refresh();
                }
            }
        }

        private void MenuExport_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog { Filter = "PNG Image|*.png" })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // Экспортируем всю клиентскую область формы
                    _viewService.ExportPng(dlg.FileName, this.ClientRectangle);
                    MessageBox.Show("Экспорт в PNG успешно выполнен!", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void UpdateTitle()
        {
            this.Text = string.IsNullOrEmpty(_viewService.FileName)
                ? "Графический редактор"
                : $"Графический редактор — {Path.GetFileName(_viewService.FileName)}";
        }
    }
}