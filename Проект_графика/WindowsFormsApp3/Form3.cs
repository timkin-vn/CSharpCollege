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

namespace WindowsFormsApp3
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

            this.Paint += Form3_Paint;

            this.Size = new Size(600, 500);

            this.BackColor = Color.LightSkyBlue;
        }

        private void Form3_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Трава
            g.FillRectangle(Brushes.Green, 0, 300, 600, 200);

            // Стена
            g.FillRectangle(Brushes.LightYellow, 150, 150, 250, 200);
            g.DrawRectangle(Pens.Brown, 150, 150, 250, 200);

            // Крыша
            Point[] roof = new Point[]
            {
                new Point(120, 150),
                new Point(275, 70),
                new Point(430, 150)
            };
            g.FillPolygon(Brushes.Red, roof);
            g.DrawPolygon(Pens.Brown, roof);

            // Дверь
            g.FillRectangle(Brushes.SaddleBrown, 250, 250, 60, 100);
            g.DrawRectangle(Pens.Black, 250, 250, 60, 100);

            // Ручка
            g.FillEllipse(Brushes.Yellow, 295, 300, 8, 8);

            // Окно 1
            g.FillRectangle(Brushes.LightBlue, 180, 200, 60, 60);
            g.DrawRectangle(Pens.Brown, 180, 200, 60, 60);
            g.DrawLine(Pens.Brown, 210, 200, 210, 260);
            g.DrawLine(Pens.Brown, 180, 230, 240, 230);

            // Окно 2
            g.FillRectangle(Brushes.LightBlue, 300, 200, 60, 60);
            g.DrawRectangle(Pens.Brown, 300, 200, 60, 60);
            g.DrawLine(Pens.Brown, 330, 200, 330, 260);
            g.DrawLine(Pens.Brown, 300, 230, 360, 230);

            // Окно 3
            g.FillEllipse(Brushes.LightBlue, 250, 120, 40, 40);
            g.DrawEllipse(Pens.Brown, 250, 120, 40, 40);
            g.DrawLine(Pens.Brown, 270, 120, 270, 160);
            g.DrawLine(Pens.Brown, 250, 140, 290, 140);

            // Труба
            g.FillRectangle(Brushes.Gray, 350, 70, 40, 60);
            g.DrawRectangle(Pens.Black, 350, 70, 40, 60);

            // Солнце
            g.FillEllipse(Brushes.Yellow, 500, 40, 60, 60);

            // Облака
            g.FillEllipse(Brushes.White, 50, 50, 50, 30);
            g.FillEllipse(Brushes.White, 80, 40, 60, 35);
            g.FillEllipse(Brushes.White, 110, 50, 50, 30);

            g.FillEllipse(Brushes.White, 400, 20, 50, 30);
            g.FillEllipse(Brushes.White, 430, 10, 60, 35);
            g.FillEllipse(Brushes.White, 460, 20, 50, 30);

            // цветок 1
            g.FillEllipse(Brushes.Red, 70, 350, 20, 20);
            g.FillEllipse(Brushes.Red, 85, 340, 20, 20);
            g.FillEllipse(Brushes.Red, 100, 350, 20, 20);
            g.FillEllipse(Brushes.Red, 85, 365, 20, 20);
            g.FillEllipse(Brushes.Yellow, 85, 352, 15, 15);

            // цветок 2
            g.FillEllipse(Brushes.Yellow, 470, 380, 15, 15);
            g.FillEllipse(Brushes.Yellow, 485, 370, 15, 15);
            g.FillEllipse(Brushes.Yellow, 500, 380, 15, 15);
            g.FillEllipse(Brushes.Yellow, 485, 395, 15, 15);
            g.FillEllipse(Brushes.Orange, 485, 382, 10, 10);
        }
    }
}
