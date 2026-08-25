using DrawingApp.Models;
using DrawingApp.Services;
using DrawingApp.ViewServices;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DrawingApp
{
    public partial class Form1 : Form
    {
        private readonly Painter _painter = new Painter();
        private readonly PictureBuilder _builder = new PictureBuilder();
        private List<ShapeModel> _shapes;

        private ShapeModel _selectedShape;
        private PointModel _lastMousePosition;
        private bool _isDragging = false;

        public Form1()
        {
            InitializeComponent();

            Text = "Интерактивный домик";
            ClientSize = new Size(900, 600);
            BackColor = Color.White;

            DoubleBuffered = true;
            ResizeRedraw = true;

            _shapes = _builder.CreateSceneObjects();

            MouseDown += Form1_MouseDown;
            MouseMove += Form1_MouseMove;
            MouseUp += Form1_MouseUp;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || _painter.Scaler == null) return;

            PointModel clickPoint = _painter.Scaler.Unscale(e.Location);

            // Ищем объект сверху вниз
            for (int i = _shapes.Count - 1; i >= 0; i--)
            {
                if (_shapes[i].ContainsPoint(clickPoint))
                {
                    _selectedShape = _shapes[i];
                    _lastMousePosition = clickPoint;
                    _isDragging = true;
                    break;
                }
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDragging || _selectedShape == null || _painter.Scaler == null) return;

            PointModel currentMousePosition = _painter.Scaler.Unscale(e.Location);

            double dx = currentMousePosition.X - _lastMousePosition.X;
            double dy = currentMousePosition.Y - _lastMousePosition.Y;

            _selectedShape.Move(dx, dy);

            _lastMousePosition = currentMousePosition;

            Invalidate(); // Запрос перерисовки
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = false;
                _selectedShape = null;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            _painter.Paint(ClientRectangle, e.Graphics, _builder.SourceBounds, _shapes);
        }
    }
}