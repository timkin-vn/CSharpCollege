using System;
using System.Drawing;
using System.Windows.Forms;

namespace Picture
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void DrawCat(Graphics g)
        {
            float baseSize = 500f;

            float scale = Math.Min(
                ClientSize.Width / baseSize,
                ClientSize.Height / baseSize
            );

            // Центрирование
            float offsetX = (ClientSize.Width - baseSize * scale) / 2;
            float offsetY = (ClientSize.Height - baseSize * scale) / 2;

            // Включаем сглаживание
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Применяем масштаб и смещение
            g.TranslateTransform(offsetX, offsetY);
            g.ScaleTransform(scale, scale);

            // ===== Рисуем в "идеальных" координатах (как раньше) =====

            // Голова
            g.FillEllipse(Brushes.Orange, 150, 150, 200, 200);
            g.DrawEllipse(Pens.Black, 150, 150, 200, 200);

            // Уши
            Point[] leftEar = {
            new Point(180,150),
            new Point(220,80),
            new Point(260,150)
    };

            Point[] rightEar = {
            new Point(240,150),
            new Point(280,80),
            new Point(320,150)
    };

            g.FillPolygon(Brushes.Orange, leftEar);
            g.DrawPolygon(Pens.Black, leftEar);

            g.FillPolygon(Brushes.Orange, rightEar);
            g.DrawPolygon(Pens.Black, rightEar);

            // Глаза
            g.FillEllipse(Brushes.White, 190, 200, 40, 40);
            g.FillEllipse(Brushes.White, 270, 200, 40, 40);

            g.FillEllipse(Brushes.Black, 205, 215, 15, 15);
            g.FillEllipse(Brushes.Black, 285, 215, 15, 15);

            // Нос
            Point[] nose = {
                new Point(250,260),
                new Point(240,280),
                new Point(260,280)
    };
            g.FillPolygon(Brushes.Pink, nose);

            // Усы
            g.DrawLine(Pens.Black, 190, 260, 130, 250);
            g.DrawLine(Pens.Black, 190, 280, 130, 280);

            g.DrawLine(Pens.Black, 310, 260, 370, 250);
            g.DrawLine(Pens.Black, 310, 280, 370, 280);

            // Рот
            g.DrawArc(Pens.Black, 235, 270, 30, 20, 0, -180);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var g = CreateGraphics();
            DrawCat(g);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var g = CreateGraphics();
            DrawCat(g);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawCat(e.Graphics);
        }
    }
}