using System;
using System.Drawing;
using System.Windows.Forms;
using GraphEditor.Business.Models;
using GraphEditor.ViewServices;

namespace GraphEditor
{
    public partial class GraphEditorForm : Form
    {
        private readonly PictureViewService _viewService = new PictureViewService();
        private readonly Painter _painter = new Painter();

        private enum Mode { None, Draw, Move, Resize }
        private Mode _mode = Mode.None;
        private Point _startPoint;
        private Point _shapeOrigin;
        private ShapeModel _draft;

        private ShapeKind _currentKind = ShapeKind.Triangle;
        private bool _drawMode = true;
        private bool _suppressSize;

        public GraphEditorForm()
        {
            InitializeComponent();
            UpdateSizeBoxes();
        }

        private void canvasPanel_Paint(object sender, PaintEventArgs e)
        {
            _painter.Paint(e.Graphics, _viewService.Picture, _viewService.Selected);
        }

        private void canvasPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            _startPoint = e.Location;

            if (_drawMode)
            {
                _mode = Mode.Draw;
                _draft = _viewService.AddShape(_currentKind, e.X, e.Y, 0, 0);
            }
            else
            {
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
                    _mode = Mode.None;
                }
            }

            UpdateSizeBoxes();
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
                    var shape = _viewService.Selected;
                    if (shape != null)
                        _viewService.ResizeSelected(e.X - shape.X, e.Y - shape.Y);
                    break;
            }

            UpdateSizeBoxes();
            canvasPanel.Invalidate();
        }

        private void canvasPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (_mode == Mode.Draw && _draft != null &&
                (_draft.Width < 5 || _draft.Height < 5))
            {
                _viewService.RemoveSelected();
            }

            _mode = Mode.None;
            _draft = null;
            UpdateSizeBoxes();
            canvasPanel.Invalidate();
        }

        private void triangleButton_Click(object sender, EventArgs e)
        {
            _currentKind = ShapeKind.Triangle;
            _drawMode = true;
        }

        private void rectangleButton_Click(object sender, EventArgs e)
        {
            _currentKind = ShapeKind.Rectangle;
            _drawMode = true;
        }

        private void ellipseButton_Click(object sender, EventArgs e)
        {
            _currentKind = ShapeKind.Ellipse;
            _drawMode = true;
        }

        private void diamondButton_Click(object sender, EventArgs e)
        {
            _currentKind = ShapeKind.Diamond;
            _drawMode = true;
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            _drawMode = false;
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            _viewService.NewPicture();
            UpdateSizeBoxes();
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
            UpdateSizeBoxes();
            canvasPanel.Invalidate();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog())
            {
                dlg.Filter = "Рисунок (*.gpic)|*.gpic|Все файлы (*.*)|*.*";
                dlg.DefaultExt = "gpic";
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
                dlg.Filter = "Рисунок (*.gpic)|*.gpic|Все файлы (*.*)|*.*";
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    _viewService.Load(dlg.FileName);
                    UpdateSizeBoxes();
                    canvasPanel.Invalidate();
                }
            }
        }

        private void sizeBox_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressSize) return;
            if (_viewService.Selected == null) return;

            _viewService.ResizeSelected((int)widthBox.Value, (int)heightBox.Value);
            canvasPanel.Invalidate();
        }

        private void UpdateSizeBoxes()
        {
            _suppressSize = true;

            var shape = _viewService.Selected;
            if (shape != null)
            {
                widthBox.Enabled = true;
                heightBox.Enabled = true;
                widthBox.Value = Clamp(shape.Width, (int)widthBox.Minimum, (int)widthBox.Maximum);
                heightBox.Value = Clamp(shape.Height, (int)heightBox.Minimum, (int)heightBox.Maximum);
            }
            else
            {
                widthBox.Enabled = false;
                heightBox.Enabled = false;
            }

            _suppressSize = false;
        }

        private static int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
}
