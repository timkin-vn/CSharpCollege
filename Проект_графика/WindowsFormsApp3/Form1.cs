using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.Paint += new PaintEventHandler(Form1_Paint);

            this.Size = new Size(400, 300);

            this.BackColor = Color.LightBlue;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // сглаживание для линий
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // волны
            using (Pen wavePen = new Pen(Color.DarkBlue, 2))
            {
                for (int i = 0; i < 10; i++)
                {
                    int x = i * 40;
                    g.DrawArc(wavePen, x, 200, 40, 20, 0, 180);
                }
            }

            // Корпус
            Point[] hull = new Point[]
            {
                new Point(100, 180),
                new Point(300, 180),
                new Point(280, 150),
                new Point(120, 150)
            };
            g.FillPolygon(Brushes.Sienna, hull);
            g.DrawPolygon(Pens.Black, hull);

            // Палуба
            g.FillRectangle(Brushes.Peru, 120, 130, 160, 20);
            g.DrawRectangle(Pens.Black, 120, 130, 160, 20);

            // Мачта
            g.FillRectangle(Brushes.Brown, 190, 50, 10, 100);

            // Парус 1
            Point[] sail = new Point[]
            {
                new Point(200, 50),
                new Point(260, 100),
                new Point(200, 100)
            };
            g.FillPolygon(Brushes.White, sail);
            g.DrawPolygon(Pens.Black, sail);

            // парус 2
            Point[] sail2 = new Point[]
            {
                new Point(190, 60),
                new Point(130, 100),
                new Point(190, 100)
            };
            g.FillPolygon(Brushes.White, sail2);
            g.DrawPolygon(Pens.Black, sail2);

            // Окна
            g.FillEllipse(Brushes.Yellow, 150, 155, 15, 15);
            g.DrawEllipse(Pens.Black, 150, 155, 15, 15);
            g.FillEllipse(Brushes.Yellow, 200, 155, 15, 15);
            g.DrawEllipse(Pens.Black, 200, 155, 15, 15);
            g.FillEllipse(Brushes.Yellow, 250, 155, 15, 15);
            g.DrawEllipse(Pens.Black, 250, 155, 15, 15);

            // Флаг
            g.FillRectangle(Brushes.Red, 200, 40, 30, 15);
            g.DrawRectangle(Pens.Black, 200, 40, 30, 15);

            // Солнце
            g.FillEllipse(Brushes.Yellow, 30, 30, 50, 50);
        }
    }
}
