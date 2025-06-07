using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp15
{
    public partial class Form1 : Form
    {
        private List<Shape> owlShapes = new List<Shape>();
        private float scale = 1.0f;
        private PointF panOffset = new PointF(0, 0);
        private Size originalClientSize = new Size(800, 800);

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public Form1()
        {
            // Настройка формы
            this.Text = "Рисование совы из фигур";
            this.ClientSize = originalClientSize;
            this.BackColor = Color.White;
            this.DoubleBuffered = true;

            // Заранее создаем фигуры для совы
            CreateOwlShapes();

            // Обработчик изменения размера окна
            this.Resize += Form1_Resize;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // Вычисляем новый масштаб на основе размера окна
            float scaleX = (float)this.ClientSize.Width / originalClientSize.Width;
            float scaleY = (float)this.ClientSize.Height / originalClientSize.Height;
            scale = Math.Min(scaleX, scaleY); // Сохраняем пропорции

            this.Invalidate();
        }

        private void CreateOwlShapes()
        {
            // Очищаем предыдущие фигуры
            owlShapes.Clear();

            // Основные цвета
            var bodyColor = Brushes.DarkGray;
            var eyeColor = Brushes.White;
            var pupilColor = Brushes.Black;
            var beakColor = Brushes.Orange;
            var decorationColor = Brushes.LightGray;

            // Тело совы (большой овал из шестиугольников)
            for (int i = 0; i < 12; i++)
            {
                double angle = 2 * Math.PI * i / 12;
                int x = (int)(300 + 120 * Math.Cos(angle));
                int y = (int)(300 + 180 * Math.Sin(angle));
                owlShapes.Add(new Hexagon(x, y, 40, bodyColor, new Font("Arial", 8)));
            }

            // Глаза (2 больших круга)
            owlShapes.Add(new Circle(250, 180, 60, eyeColor, new Font("Arial", 8)));
            owlShapes.Add(new Circle(325, 180, 60, eyeColor, new Font("Arial", 8)));

            // Зрачки (2 маленьких круга)
            owlShapes.Add(new Circle(260, 193, 25, pupilColor, new Font("Arial", 8)));
            owlShapes.Add(new Circle(330, 193, 25, pupilColor, new Font("Arial", 8)));

            // Клюв (треугольник)
            owlShapes.Add(new Triangle(300, 250, 40, beakColor, new Font("Arial", 8)));

            // Ушки (2 треугольника)
            owlShapes.Add(new Triangle(250, 90, 40, bodyColor, new Font("Arial", 8)));
            owlShapes.Add(new Triangle(350, 90, 40, bodyColor, new Font("Arial", 8)));

            // Украшения на теле (квадраты)
            owlShapes.Add(new Square(280, 300, 30, decorationColor, new Font("Arial", 8)));
            owlShapes.Add(new Square(320, 350, 30, decorationColor, new Font("Arial", 8)));
            owlShapes.Add(new Square(250, 400, 30, decorationColor, new Font("Arial", 8)));
            owlShapes.Add(new Square(350, 380, 30, decorationColor, new Font("Arial", 8)));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Центрируем изображение
            float offsetX = (this.ClientSize.Width - originalClientSize.Width * scale) / 2;
            float offsetY = (this.ClientSize.Height - originalClientSize.Height * scale) / 2;

            e.Graphics.TranslateTransform(offsetX, offsetY);
            e.Graphics.ScaleTransform(scale, scale);

            // Рисуем все фигуры совы
            foreach (var shape in owlShapes)
            {
                shape.Draw(e.Graphics);
            }

            // Рисуем информацию о масштабе
            string scaleText = $"Масштаб: {scale * 100:0}%";
            e.Graphics.ResetTransform();
            e.Graphics.DrawString(scaleText, new Font("Arial", 12), Brushes.Black, 10, 10);
        }
    }

    abstract class Shape
    {
        public int X { get; }
        public int Y { get; }
        public int Size { get; }
        public Brush Brush { get; }
        public Font Font { get; }

        public Shape(int x, int y, int size, Brush brush, Font font)
        {
            X = x;
            Y = y;
            Size = size;
            Brush = brush;
            Font = font;
        }

        public abstract void Draw(Graphics g);
    }

    class Square : Shape
    {
        public Square(int x, int y, int size, Brush brush, Font font) : base(x, y, size, brush, font) { }

        public override void Draw(Graphics g)
        {
            g.FillRectangle(Brush, X, Y, Size, Size);
            g.DrawRectangle(Pens.Black, X, Y, Size, Size);
        }
    }

    class Circle : Shape
    {
        public Circle(int x, int y, int size, Brush brush, Font font) : base(x, y, size, brush, font) { }

        public override void Draw(Graphics g)
        {
            g.FillEllipse(Brush, X, Y, Size, Size);
            g.DrawEllipse(Pens.Black, X, Y, Size, Size);
        }
    }

    class Triangle : Shape
    {
        public Triangle(int x, int y, int size, Brush brush, Font font) : base(x, y, size, brush, font) { }

        public override void Draw(Graphics g)
        {
            Point[] points = {
                new Point(X + Size/2, Y),
                new Point(X, Y + Size),
                new Point(X + Size, Y + Size)
            };
            g.FillPolygon(Brush, points);
            g.DrawPolygon(Pens.Black, points);
        }
    }

    class Hexagon : Shape
    {
        public Hexagon(int x, int y, int size, Brush brush, Font font) : base(x, y, size, brush, font) { }

        public override void Draw(Graphics g)
        {
            Point[] points = new Point[6];
            for (int i = 0; i < 6; i++)
            {
                double angle = 2 * Math.PI * i / 6;
                points[i] = new Point(
                    (int)(X + Size / 2 + Size / 2 * Math.Cos(angle)),
                    (int)(Y + Size / 2 + Size / 2 * Math.Sin(angle))
                );
            }
            g.FillPolygon(Brush, points);
            g.DrawPolygon(Pens.Black, points);
        }
    }
}