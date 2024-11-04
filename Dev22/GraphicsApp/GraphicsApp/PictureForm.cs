using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicsApp
{
    public partial class PictureForm : Form
    {

        private List<Line> lines = new List<Line>();
        private Point lpoint;
        private Pen drawingPen;
        private int lineThickness = 2;
        public PictureForm()
        {
            drawingPen = new Pen(Color.Black, lineThickness);

            Panel panel = new Panel {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                IsAccessible = true
                

            };
            ComboBox combo = new ComboBox
            {
                Location = new Point(10, 110),

            };
            GroupBox colorGroup = new GroupBox
            {
                Text = "Выберите цвет",
                Dock = DockStyle.Right,
                Width = 150
            };

            RadioButton blackButton = new RadioButton
            {
                Text = "Черный",
                Checked = true,
                Location = new Point(10, 20)
            };
            blackButton.CheckedChanged += (s, e) => { if (blackButton.Checked) drawingPen.Color = Color.Black; };

            RadioButton redButton = new RadioButton
            {
                Text = "Красный",
                Location = new Point(10, 50)
            };
            redButton.CheckedChanged += (s, e) => { if (redButton.Checked) drawingPen.Color = Color.Red; };

            RadioButton blueButton = new RadioButton
            {
                Text = "Синий",
                Location = new Point(10, 80)
            };
            blueButton.CheckedChanged += (s, e) => { if (blueButton.Checked) drawingPen.Color = Color.Blue; };

            colorGroup.Controls.Add(blackButton);
            colorGroup.Controls.Add(redButton);
            colorGroup.Controls.Add(blueButton);
            this.Controls.Add(colorGroup);

            // Создание TrackBar для изменения толщины линии
            
          
        InitializeComponent();
            this.DoubleBuffered = true;
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left) {
                lpoint = e.Location;
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) // Если левая кнопка мыши нажата
            {
                using (Graphics g = this.CreateGraphics())
                {
                    lines.Add(new Line(lpoint, e.Location, drawingPen.Color, lineThickness));
                    g.DrawLine(drawingPen, lpoint, e.Location); // Рисуем линию
                }
                 lpoint = e.Location; // Обновляем последнюю точку
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            foreach (var line in lines)
            {
                using (Pen pen = new Pen(line.Color, line.Thickness))
                {
                    e.Graphics.DrawLine(pen, line.Start, line.End);
                }
            }
        }
        private void DrawingForm_ResizeEnd(object sender, EventArgs e)
        {
            this.Invalidate(); // Перерисовать форму при изменении размера
        }
        private class Line
        {
            public Point Start { get; }
            public Point End { get; }
            public Color Color { get; }
            public int Thickness { get; }

            public Line(Point start, Point end, Color color, int thickness)
            {
                Start = start;
                End = end;
                Color = color;
                Thickness = thickness;
            }
        }
        private void PictureForm_Paint(object sender, PaintEventArgs e)
        {

          /*  var g = e.Graphics;
            //var brush = new SolidBrush(Color.FromArgb(255, 204, 17));
            var brush = new LinearGradientBrush(
                new Point { X = 200, Y = 50, },
                new Point { X = 400, Y = 200, },
                Color.FromArgb(255, 204, 17),
                Color.FromArgb(0, 68, 153));
            g.FillRectangle(brush, 200, 50, 200, 150);*/

            /*var pen = new Pen(Color.FromArgb(255, 187, 102), 3)
            {
               DashPattern = new[] { 8f, 2f, 2f, 2f, 2f, 2f, },
            };
            g.DrawRectangle(pen, 200, 50, 200, 150);*/

            /*var xMin = 1;
            var yMin = 1;
            var xMax = ClientRectangle.Width - 2;
            var yMax = ClientRectangle.Height - 2;
            var width = xMax - xMin;
            var height = yMax - yMin;
            g.DrawRectangle(Pens.Blue, xMin, yMin, width, height);
            g.DrawLine(Pens.Blue, xMin, yMin, xMax, yMax);
            g.DrawLine(Pens.Blue, xMin, yMax, xMax, yMin);*/
            var dx = ClientRectangle.Width / 6;
            var dy = ClientRectangle.Height / 6;

            /*var pen = new Pen(Color.Blue, 3);
            g.DrawRectangle(pen, dx * 2, dy * 3, dx * 2, dy * 2);
            g.DrawLines(pen, new[]
                {
                    new Point { X = dx * 2, Y = dy * 3 },
                    new Point { X = dx * 3, Y = dy, },
                    new Point { X = dx * 4, Y = dy * 3 }
                });*/
        }
     
        private void PictureForm_Resize(object sender, EventArgs e)
        {
            Refresh();
            this.Invalidate();
        }

 
    }
}
