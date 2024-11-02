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
        private Random random = new Random();
        private float scaleFactorX = 1.0f;
        private float scaleFactorY = 1.0f;

        public PictureForm()
        {
            InitializeComponent();
        }

        
        private void UpdateScaleFactors()
        {
            scaleFactorX = (float)this.ClientSize.Width / 800f;
            scaleFactorY = (float)this.ClientSize.Height / 600f;
        }

        private void PictureForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;


            UpdateScaleFactors();


            g.ScaleTransform(scaleFactorX, scaleFactorY);

            for (int i = 0; i < 10; i++)
            {
                int shapeType = random.Next(3);
                int x = random.Next(0, this.ClientSize.Width - 100);
                int y = random.Next(0, this.ClientSize.Height - 100);
                int size = 70;

                switch (shapeType)
                {
                    case 0: // Круг
                        DrawFilledCircle(g, x, y, size);
                        break;
                    case 1: // Прямоугольник
                        DrawFilledRectangle(g, x, y, size, size);
                        break;
                    case 2: // Треугольник
                        DrawFilledTriangle(g, x, y, size);
                        break;
                }
            }


            g.DrawLine(Pens.Black, random.Next(150), random.Next(30), 200, random.Next(30));
            g.DrawLine(Pens.Red, random.Next(150), random.Next(30), 200, random.Next(30));


            using (Brush gradientBrush = new LinearGradientBrush(new Rectangle(50, 50, 200, 50), Color.Blue, Color.Green, 45F))
            {
                g.FillRectangle(gradientBrush, 50, 50, 200, 50);
            }
        }

        private void DrawFilledCircle(Graphics g, int x, int y, int size)
        {
            Color fillColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            using (Brush brush = new SolidBrush(fillColor))
            {
                g.FillEllipse(brush, x, y, size, size);
            }
        }

        private void DrawFilledRectangle(Graphics g, int x, int y, int width, int height)
        {
            Color fillColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            using (Brush brush = new SolidBrush(fillColor))
            {
                g.FillRectangle(brush, x, y, width, height);
            }
        }

        private void DrawFilledTriangle(Graphics g, int x, int y, int size)
        {
            Point[] points = {
                new Point(x, y + size),
                new Point(x + size / 2, y),
                new Point(x + size, y + size)
            };
            Color fillColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            using (Brush brush = new SolidBrush(fillColor))
            {
                g.FillPolygon(brush, points);
            }
        }

        private void PictureForm_Resize(object sender, EventArgs e)
        {

            UpdateScaleFactors();
            Refresh();
        }
    }
}
