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

            // Включаем обработку клавиш на уровне формы
            this.KeyPreview = true;
            this.KeyDown += GraphEditorForm_KeyDown;

            // Подключаем обработчик колеса мыши
            this.MouseWheel += GraphEditorForm_MouseWheel;

            // Начальное состояние кнопки сетки
            GridToggleButton.Checked = _viewService.ShowGrid;
        }

        // ==================== ОТРИСОВКА ====================

        private void GraphEditorForm_Paint(object sender, PaintEventArgs e)
        {
            _viewService.Paint(e.Graphics);
        }

        // ==================== МЫШЬ ====================

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

        // НОВОЕ: Обработчик колеса мыши для масштабирования
        private void GraphEditorForm_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                // Ctrl + колесо = масштабирование
                if (e.Delta > 0)
                    _viewService.ZoomIn();
                else
                    _viewService.ZoomOut();
                Refresh();
            }
        }

        // ==================== ГОРЯЧИЕ КЛАВИШИ ====================

        private void GraphEditorForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                // Ctrl+S - Сохранить
                FileSaveMenuItem_Click(this, EventArgs.Empty);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.O)
            {
                // Ctrl+O - Открыть
                FileOpenMenuItem_Click(this, EventArgs.Empty);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                // Ctrl+N - Новый
                FileCreateMenuItem_Click(this, EventArgs.Empty);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                // Ctrl+E - Экспорт
                FileExportMenuItem_Click(this, EventArgs.Empty);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                // Delete - Удалить выбранный прямоугольник
                if (_viewService.CanDelete)
                {
                    DeleteRectangleButton_Click(this, EventArgs.Empty);
                    e.Handled = true;
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                // Escape - Отменить создание
                if (_viewService.CreateMode)
                {
                    _viewService.CreateButtonClicked();
                    UpdateViewControls();
                    Refresh();
                    e.Handled = true;
                }
            }
            else if (e.Control && e.KeyCode == Keys.G)
            {
                // Ctrl+G - Включить/выключить сетку
                _viewService.ShowGrid = !_viewService.ShowGrid;
                GridToggleButton.Checked = _viewService.ShowGrid;
                Refresh();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.B)
            {
                // Ctrl+B - Включить/выключить привязку к сетке
                _viewService.SnapToGridEnabled = !_viewService.SnapToGridEnabled;
                SnapToggleButton.Checked = _viewService.SnapToGridEnabled;
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Up)
            {
                // Ctrl+Up - Переместить вверх по Z-порядку
                _viewService.MoveForward();
                Refresh();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.Down)
            {
                // Ctrl+Down - Переместить вниз по Z-порядку
                _viewService.MoveBackward();
                Refresh();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.D)
            {
                // Ctrl+D - Дублировать выделенную фигуру
                _viewService.DuplicateSelected();
                Refresh();
                UpdateViewControls();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        // ==================== ИНСТРУМЕНТЫ ====================

        private void CreateRectangleButton_Click(object sender, EventArgs e)
        {
            _viewService.CreateButtonClicked();
            UpdateViewControls();
        }

        private void DeleteRectangleButton_Click(object sender, EventArgs e)
        {
            _viewService.DeleteButtonClicked();
            Refresh();
            UpdateViewControls();
        }

        // ==================== ЦВЕТА ====================

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

        // НОВОЕ: Цвет границы
        private void BorderColorMenuItem_Click(object sender, EventArgs e)
        {
            using (var colorDialog = new ColorDialog())
            {
                colorDialog.Color = _viewService.GetCurrentBorderColor();
                if (colorDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                _viewService.SetBorderColor(colorDialog.Color);
                Refresh();
            }
        }

        // ==================== Z-ПОРЯДОК ====================

        private void MoveForwardMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.MoveForward();
            Refresh();
        }

        // НОВОЕ: Переместить назад
        private void MoveBackwardMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.MoveBackward();
            Refresh();
        }

        // НОВОЕ: На самый верх
        private void MoveToFrontMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.MoveToFront();
            Refresh();
        }

        // НОВОЕ: В самый низ
        private void MoveToBackMenuItem_Click(object sender, EventArgs e)
        {
            _viewService.MoveToBack();
            Refresh();
        }

        // ==================== СЕТКА ====================

        // НОВОЕ: Переключение сетки
        private void GridToggleButton_Click(object sender, EventArgs e)
        {
            _viewService.ShowGrid = !_viewService.ShowGrid;
            GridToggleButton.Checked = _viewService.ShowGrid;
            Refresh();
        }

        // НОВОЕ: Переключение привязки к сетке
        private void SnapToggleButton_Click(object sender, EventArgs e)
        {
            _viewService.SnapToGridEnabled = !_viewService.SnapToGridEnabled;
            SnapToggleButton.Checked = _viewService.SnapToGridEnabled;
        }

        // ==================== ПРОЗРАЧНОСТЬ ====================

        // НОВОЕ: Изменение прозрачности
        private void OpacityTrackBar_Scroll(object sender, EventArgs e)
        {
            if (_viewService.GetSelectedRectangle() == null)
                return;

            byte opacity = (byte)OpacityTrackBar.Value;
            _viewService.SetOpacity(opacity);
            OpacityLabel.Text = $"Прозрачность: {opacity}";
            Refresh();
        }

        // ==================== ТЕНЬ ====================

        // НОВОЕ: Переключение тени
        private void ShadowToggleButton_Click(object sender, EventArgs e)
        {
            if (_viewService.GetSelectedRectangle() == null)
                return;

            _viewService.ToggleShadow();
            ShadowToggleButton.Checked = _viewService.GetSelectedRectangle()?.ShowShadow ?? false;
            Refresh();
        }

        // ==================== ФАЙЛОВЫЕ ОПЕРАЦИИ ====================

        private void FileCreateMenuItem_Click(object sender, EventArgs e)
        {
            // Проверяем, есть ли несохранённые изменения
            if (_viewService.HasRectangles())
            {
                var result = MessageBox.Show(
                    "Сохранить текущий рисунок перед созданием нового?",
                    "Создать новый документ",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    FileSaveMenuItem_Click(sender, e);
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }

            _viewService.CreateNewPicture();
            Refresh();
            UpdateViewControls();
        }

        private void FileOpenMenuItem_Click(object sender, EventArgs e)
        {
            // Проверяем несохранённые изменения
            if (_viewService.HasRectangles())
            {
                var result = MessageBox.Show(
                    "Сохранить текущий рисунок перед открытием другого?",
                    "Открыть файл",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    FileSaveMenuItem_Click(sender, e);
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }

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

            MessageBox.Show(
                $"Изображение успешно экспортировано в:\n{FileExportDialog.FileName}",
                "Экспорт завершён",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void FileExitMenuItem_Click(object sender, EventArgs e)
        {
            // Проверяем несохранённые изменения перед выходом
            if (_viewService.HasRectangles())
            {
                var result = MessageBox.Show(
                    "Сохранить изменения перед выходом?",
                    "Выход",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    FileSaveMenuItem_Click(sender, e);
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }

            Close();
        }

        // ==================== О ПРОГРАММЕ ====================

        // НОВОЕ: Диалог "О программе"
        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Графический редактор v1.1\n\n" +
                "Возможности:\n" +
                "• Создание и редактирование прямоугольников\n" +
                "• Масштабирование по 8 маркерам\n" +
                "• Поворот фигур\n" +
                "• Настройка прозрачности\n" +
                "• Тени и градиенты\n" +
                "• Сетка с привязкой\n" +
                "• Z-порядок (вперёд/назад)\n" +
                "• Дублирование фигур\n\n" +
                "Горячие клавиши:\n" +
                "Ctrl+S - Сохранить\n" +
                "Ctrl+O - Открыть\n" +
                "Ctrl+N - Новый документ\n" +
                "Ctrl+E - Экспорт в PNG\n" +
                "Ctrl+D - Дублировать\n" +
                "Ctrl+G - Сетка\n" +
                "Ctrl+B - Привязка к сетке\n" +
                "Ctrl+Up/Down - Z-порядок\n" +
                "Delete - Удалить фигуру\n" +
                "Escape - Отмена создания",
                "О программе",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // ==================== ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ ====================

        private void DoSaveAs()
        {
            if (FileSaveDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _viewService.Save(FileSaveDialog.FileName);
            UpdateViewControls();
        }

        private void UpdateViewControls()
        {
            CreateRectangleButton.Checked = _viewService.CreateMode;
            DeleteRectangleButton.Enabled = _viewService.CanDelete;
            GridToggleButton.Checked = _viewService.ShowGrid;
            SnapToggleButton.Checked = _viewService.SnapToGridEnabled;

            // Обновляем состояние кнопок Z-порядка
            MoveForwardMenuItem.Enabled = _viewService.CanDelete;

            // Обновляем слайдер прозрачности
            var selectedRect = _viewService.GetSelectedRectangle();
            if (selectedRect != null)
            {
                OpacityTrackBar.Value = selectedRect.Opacity;
                OpacityLabel.Text = $"Прозрачность: {selectedRect.Opacity}";
                OpacityTrackBar.Enabled = true;
                ShadowToggleButton.Checked = selectedRect.ShowShadow;
                ShadowToggleButton.Enabled = true;
            }
            else
            {
                OpacityTrackBar.Enabled = false;
                ShadowToggleButton.Enabled = false;
            }

            // Обновляем заголовок окна
            Text = string.IsNullOrEmpty(_viewService.FileName) ?
                "Графический редактор" :
                $"Графический редактор | {Path.GetFileName(_viewService.FileName)}";
        }

        // Обработчик закрытия формы (крестик)
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (_viewService.HasRectangles() && e.CloseReason == CloseReason.UserClosing)
            {
                var result = MessageBox.Show(
                    "Сохранить изменения перед выходом?",
                    "Подтверждение выхода",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    FileSaveMenuItem_Click(this, EventArgs.Empty);
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}