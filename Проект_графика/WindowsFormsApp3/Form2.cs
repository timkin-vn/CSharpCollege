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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            this.Paint += Form2_Paint;

            this.Size = new Size(600, 400);

            this.BackColor = Color.LightGreen;
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Дорога
            g.FillRectangle(Brushes.Gray, 0, 250, 600, 100);

            // Разметка на дороге
            using (Pen whitePen = new Pen(Color.White, 3))
            {
                whitePen.DashStyle = DashStyle.Dash;
                g.DrawLine(whitePen, 0, 300, 600, 300);
            }

            // Кузов
            g.FillRectangle(Brushes.Blue, 150, 180, 300, 80);
            g.DrawRectangle(Pens.Black, 150, 180, 300, 80);

            // Крыша
            g.FillRectangle(Brushes.LightBlue, 220, 130, 160, 50);
            g.DrawRectangle(Pens.Black, 220, 130, 160, 50);

            // Окна
            g.FillRectangle(Brushes.LightCyan, 240, 140, 40, 30); 
            g.DrawRectangle(Pens.Black, 240, 140, 40, 30);

            g.FillRectangle(Brushes.LightCyan, 320, 140, 40, 30); 
            g.DrawRectangle(Pens.Black, 320, 140, 40, 30);

            // Фары
            g.FillEllipse(Brushes.Yellow, 430, 200, 25, 25); 
            g.DrawEllipse(Pens.Black, 430, 200, 25, 25);

            g.FillEllipse(Brushes.Orange, 140, 200, 20, 20); 
            g.DrawEllipse(Pens.Black, 140, 200, 20, 20);

            // Колеса
            g.FillEllipse(Brushes.Black, 180, 240, 50, 50);
            g.DrawEllipse(Pens.White, 180, 240, 50, 50);
            g.FillEllipse(Brushes.Gray, 195, 255, 20, 20); 

            g.FillEllipse(Brushes.Black, 370, 240, 50, 50);
            g.DrawEllipse(Pens.White, 370, 240, 50, 50);
            g.FillEllipse(Brushes.Gray, 385, 255, 20, 20); 

            // Ручки
            g.FillRectangle(Brushes.Black, 280, 200, 20, 5);
            g.FillRectangle(Brushes.Black, 330, 200, 20, 5);
        }
    }
}
