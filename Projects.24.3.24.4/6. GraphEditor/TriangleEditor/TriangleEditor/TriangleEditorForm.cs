using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using TriangleEditor.Business.Models;
using TriangleEditor.ViewServices;

namespace TriangleEditor
{
    public partial class TriangleEditorForm : Form
    {
        private readonly PictureViewService _viewService = new PictureViewService();
        private readonly Painter _painter = new Painter();

        private enum Mode { None, Draw, Move, Resize }
        private Mode _mode = Mode.None;
        private Point _startPoint;
        private Point _shapeOrigin;
        private TriangleModel _draft;

        public TriangleEditorForm()
        {
            InitializeComponent();
        }

        private void canvasPanel_Paint(object sender, PaintEventArgs e)
        {
            _painter.Paint(e.Graphics, _viewService.Picture, _viewService.Selected);
        }

        private void canvasPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            _startPoint = e.Location;
            var hit = _viewService.Pick(e.X, e.Y);

            if (hit != null)
            {
                if ((ModifierKeys & Keys.Control) == Keys.Control)
                {
                    _mode = Mode.Resize;
                }
                else
                {
                    _mode = Mode.Move;
                    _shapeOrigin = new Point(hit.X, hit.Y);
                }
            }
            else
            {
                // Клик по пустому месту — начинаем рисовать новый треугольник.
                _mode = Mode.Draw;
                _draft = _viewService.AddTriangle(e.X, e.Y, 0, 0);
            }

            canvasPanel.Invalidate();
        }

        private void canvasPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mode == Mode.None) return;

            int dx = e.X - _startPoint.X;
            int dy = e.Y - _startPoint.Y;

            switch (_mode)
            {
                case Mode.Draw:
                    if (_draft != null)
                    {
                        _draft.X = Math.Min(_startPoint.X, e.X);
                        _draft.Y = Math.Min(_startPoint.Y, e.Y);
                        _draft.Width = Math.Abs(dx);
                        _draft.Height = Math.Abs(dy);
                    }
                    break;

                case Mode.Move:
                    if (_viewService.Selected != null)
                    {
                        _viewService.Selected.X = _shapeOrigin.X + dx;
                        _viewService.Selected.Y = _shapeOrigin.Y + dy;
                    }
                    break;

                case Mode.Resize:
                    var s = _viewService.Selected;
                    if (s != null)
                        _viewService.ResizeSelected(e.X - s.X, e.Y - s.Y);
                    break;
            }

            canvasPanel.Invalidate();
        }

        private void canvasPanel_MouseUp(object sender, MouseEventArgs e)
        {
            // Слишком маленький "черновик" удаляем — это был случайный клик.
            if (_mode == Mode.Draw && _draft != null &&
                (_draft.Width < 5 || _draft.Height < 5))
            {
                _viewService.RemoveSelected();
            }

            _mode = Mode.None;
            _draft = null;
            canvasPanel.Invalidate();
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            _viewService.NewPicture();
            canvasPanel.Invalidate();
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            if (_viewService.Selected == null) return;

            using (var dlg = new ColorDialog())
            {
                dlg.Color = _viewService.Selected.FillColor;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    _viewService.ColorSelected(dlg.Color);
                    canvasPanel.Invalidate();
                }
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            _viewService.RemoveSelected();
            canvasPanel.Invalidate();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog())
            {
                dlg.Filter = "Рисунок (*.tpic)|*.tpic|Все файлы (*.*)|*.*";
                dlg.DefaultExt = "tpic";
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    _viewService.Save(dlg.FileName);
                }
            }
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Рисунок (*.tpic)|*.tpic|Все файлы (*.*)|*.*";
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    _viewService.Load(dlg.FileName);
                    canvasPanel.Invalidate();
                }
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog())
            {
                dlg.Filter = "PNG-изображение (*.png)|*.png";
                dlg.DefaultExt = "png";
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    using (var bmp = new Bitmap(canvasPanel.Width, canvasPanel.Height))
                    {
                        using (var g = Graphics.FromImage(bmp))
                        {
                            _painter.Paint(g, _viewService.Picture, null);
                        }
                        bmp.Save(dlg.FileName, ImageFormat.Png);
                    }
                }
            }
        }
    }
}
