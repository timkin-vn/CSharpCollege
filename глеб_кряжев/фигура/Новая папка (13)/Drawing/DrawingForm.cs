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

            // Цвета
            var olive = new SolidBrush(Color.FromArgb(128, 128, 64));
            var darkGreen = new SolidBrush(Color.FromArgb(0, 100, 0));
            var green = new SolidBrush(Color.FromArgb(107, 142, 35));
            var gray = new SolidBrush(Color.FromArgb(120, 130, 140));
            var lightGray = new SolidBrush(Color.FromArgb(210, 210, 210));
            var black = new SolidBrush(Color.Black);

            Pen outline = new Pen(Color.Black, 2);

            // 1. Гусеницы (нижний многоугольник)
            Point[] track = {
                new Point(60, 340), new Point(720, 340),
                new Point(750, 370), new Point(30, 370)
            };
            g.FillPolygon(black, track);
            g.DrawPolygon(outline, track);

            // 2. Корпус (основной многоугольник)
            Point[] body = {
                new Point(90, 270), new Point(690, 270),
                new Point(750, 340), new Point(30, 340)
            };
            g.FillPolygon(green, body);
            g.DrawPolygon(outline, body);

            // 3. Верхняя накладка корпуса (тёмно-зелёная)
            g.FillRectangle(darkGreen, 200, 250, 400, 30);
            g.DrawRectangle(outline, 200, 250, 400, 30);

            // 4. Башня (многоугольник)
            Point[] turret = {
                new Point(270, 180), new Point(570, 200),
                new Point(540, 250), new Point(300, 250)
            };
            g.FillPolygon(lightGray, turret);
            g.DrawPolygon(outline, turret);

            // 5. Круглая задняя часть башни
            g.FillEllipse(darkGreen, 250, 200, 50, 50);
            g.DrawEllipse(outline, 250, 200, 50, 50);

            // 6. Люк на башне
            g.FillRectangle(green, 400, 170, 60, 20);
            g.DrawRectangle(outline, 400, 170, 60, 20);

            // 7. Малый круг на башне (левый)
            g.FillEllipse(darkGreen, 320, 180, 40, 30);
            g.DrawEllipse(outline, 320, 180, 40, 30);

            // 8. Дуло (ствол)
            g.FillRectangle(lightGray, 570, 215, 160, 15);
            g.DrawRectangle(outline, 570, 215, 160, 15);

            // 9. Конец дула (зелёный)
            g.FillRectangle(green, 730, 218, 20, 9);
            g.DrawRectangle(outline, 730, 218, 20, 9);

            // 10. Круглая задняя часть дула
            g.FillEllipse(darkGreen, 540, 210, 40, 40);
            g.DrawEllipse(outline, 540, 210, 40, 40);

            // 11. Ящик на корпусе
            g.FillRectangle(gray, 120, 250, 60, 30);
            g.DrawRectangle(outline, 120, 250, 60, 30);

            // 12. Фара (малый круг)
            g.FillEllipse(gray, 90, 270, 40, 40);
            g.DrawEllipse(outline, 90, 270, 40, 40);

            // 13. Колёса
            int[] wheelX = { 90, 200, 310, 420, 530, 640 };
            for (int i = 0; i < 6; i++)
            {
                g.FillEllipse(olive, wheelX[i], 320, 80, 80);
                g.DrawEllipse(outline, wheelX[i], 320, 80, 80);
                g.FillEllipse(green, wheelX[i] + 20, 340, 40, 40);
                g.DrawEllipse(outline, wheelX[i] + 20, 340, 40, 40);

                // Болты на крайних колёсах
                if (i == 0 || i == 5)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        double angle = Math.PI / 2 * j;
                        int cx = wheelX[i] + 40 + (int)(25 * Math.Cos(angle));
                        int cy = 360 + (int)(25 * Math.Sin(angle));
                        g.FillEllipse(black, cx, cy, 6, 6);
                    }
                }
            }

            // 14. Нижние линии (имитация тени)
            g.DrawLine(outline, 30, 370, 750, 370);
            g.DrawLine(outline, 30, 370, 90, 340);
            g.DrawLine(outline, 750, 370, 690, 340);

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
            Refresh();
        }
    }
}
