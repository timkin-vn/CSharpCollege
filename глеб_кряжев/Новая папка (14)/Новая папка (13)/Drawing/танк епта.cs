using Drawing.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drawing
{
    public partial class DrawingForm : Form
    {
        public DrawingForm()
        {
            InitializeComponent();
        }

        private void DrawingForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Базовые размеры (как на оригинальном рисунке)
            float baseWidth = 800f;
            float baseHeight = 450f;

            // Текущие размеры окна
            float scaleX = this.ClientSize.Width / baseWidth;
            float scaleY = this.ClientSize.Height / baseHeight;

            // Вспомогательная функция для масштабирования точек
            PointF Scale(float x, float y) => new PointF(x * scaleX, y * scaleY);

            // Цвета
            var olive = new SolidBrush(Color.FromArgb(128, 128, 64));
            var darkGreen = new SolidBrush(Color.FromArgb(0, 100, 0));
            var green = new SolidBrush(Color.FromArgb(107, 142, 35));
            var gray = new SolidBrush(Color.FromArgb(120, 130, 140));
            var lightGray = new SolidBrush(Color.FromArgb(210, 210, 210));
            var black = new SolidBrush(Color.Black);
            var yellow = new SolidBrush(Color.Yellow);
            Pen outline = new Pen(Color.Black, 2 * scaleX);

            //Гусеницы
            PointF[] track = {
                Scale(60, 340), Scale(720, 340),
                Scale(750, 370), Scale(30, 370)
            };
            g.FillPolygon(black, track);
            g.DrawPolygon(outline, track);

            //Корпус
            PointF[] body = {
                Scale(90, 270), Scale(690, 270),
                Scale(750, 340), Scale(30, 340)
            };
            g.FillPolygon(green, body);
            g.DrawPolygon(outline, body);

            //Верхняя накладка корпуса
            g.FillRectangle(darkGreen, 200 * scaleX, 250 * scaleY, 400 * scaleX, 30 * scaleY);
            g.DrawRectangle(outline, 200 * scaleX, 250 * scaleY, 400 * scaleX, 30 * scaleY);

            //Башня
            PointF[] turret = {
                Scale(270, 180), Scale(570, 200),
                Scale(540, 250), Scale(300, 250)
            };
            g.FillPolygon(lightGray, turret);
            g.DrawPolygon(outline, turret);

            //Круглая задняя часть башни
            g.FillEllipse(darkGreen, 250 * scaleX, 200 * scaleY, 50 * scaleX, 50 * scaleY);
            g.DrawEllipse(outline, 250 * scaleX, 200 * scaleY, 50 * scaleX, 50 * scaleY);

            //Люк на башне
            g.FillRectangle(green, 400 * scaleX, 170 * scaleY, 60 * scaleX, 20 * scaleY);
            g.DrawRectangle(outline, 400 * scaleX, 170 * scaleY, 60 * scaleX, 20 * scaleY);

            //Малый круг на башне
            g.FillEllipse(darkGreen, 320 * scaleX, 180 * scaleY, 40 * scaleX, 30 * scaleY);
            g.DrawEllipse(outline, 320 * scaleX, 180 * scaleY, 40 * scaleX, 30 * scaleY);

            //Дуло
            g.FillRectangle(lightGray, 570 * scaleX, 215 * scaleY, 160 * scaleX, 15 * scaleY);
            g.DrawRectangle(outline, 570 * scaleX, 215 * scaleY, 160 * scaleX, 15 * scaleY);

            //Конец дула
            g.FillRectangle(green, 730 * scaleX, 218 * scaleY, 20 * scaleX, 9 * scaleY);
            g.DrawRectangle(outline, 730 * scaleX, 218 * scaleY, 20 * scaleX, 9 * scaleY);

            //задняя часть дула
            g.FillEllipse(darkGreen, 540 * scaleX, 210 * scaleY, 40 * scaleX, 40 * scaleY);
            g.DrawEllipse(outline, 540 * scaleX, 210 * scaleY, 40 * scaleX, 40 * scaleY);

            //Ящик на корпусе
            g.FillRectangle(gray, 120 * scaleX, 250 * scaleY, 60 * scaleX, 30 * scaleY);
            g.DrawRectangle(outline, 120 * scaleX, 250 * scaleY, 60 * scaleX, 30 * scaleY);

            //Фара
            g.FillEllipse(yellow, 680 * scaleX, 270 * scaleY, 40 * scaleX, 40 * scaleY);
            g.DrawEllipse(outline, 680 * scaleX, 270 * scaleY, 40 * scaleX, 40 * scaleY);

            // 13. Колёса
            float[] wheelX = { 90, 200, 310, 420, 530, 640 };
            for (int i = 0; i < 6; i++)
            {
                g.FillEllipse(olive, wheelX[i] * scaleX, 320 * scaleY, 80 * scaleX, 80 * scaleY);
                g.DrawEllipse(outline, wheelX[i] * scaleX, 320 * scaleY, 80 * scaleX, 80 * scaleY);
                g.FillEllipse(green, (wheelX[i] + 20) * scaleX, 340 * scaleY, 40 * scaleX, 40 * scaleY);
                g.DrawEllipse(outline, (wheelX[i] + 20) * scaleX, 340 * scaleY, 40 * scaleX, 40 * scaleY);

                // Болты на крайних колёсах
                if (i == 0 || i == 5)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        double angle = Math.PI / 2 * j;
                        float cx = (float)(wheelX[i] + 37 + 25 * Math.Cos(angle)) * scaleX;
                        float cy = (float)(357 + 25 * Math.Sin(angle)) * scaleY;
                        g.FillEllipse(black, cx, cy, 6 * scaleX, 6 * scaleY);
                    }
                }
            }

            // Освобождение ресурсов
            olive.Dispose();
            darkGreen.Dispose();
            green.Dispose();
            gray.Dispose();
            lightGray.Dispose();
            black.Dispose();
            outline.Dispose();
        }

        private void DrawingForm_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
