using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp8
{
    public partial class MainForm : Form
    {
        private const int OriginalWidth = 300;
        private const int OriginalHeight = 300;


        public MainForm()
        {
            InitializeComponent();
            this.Width = OriginalWidth;
            this.Height = OriginalHeight;
            this.ResizeRedraw = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var graphics = e.Graphics;

            //работа масштабирования -
            float scaleX = (float)Width / OriginalWidth;
            float scaleY = (float)Height / OriginalHeight;

            
            using (var pen = new Pen(Color.Black, 3 / Math.Max(scaleX, scaleY)))
            using (var brush = new SolidBrush(Color.Green))
            {
               
                graphics.FillRectangle(brush, new Rectangle(
                    (int)(50 * scaleX),
                    (int)(100 * scaleY),
                    (int)(100 * scaleX),
                    (int)(100 * scaleY)
                ));

                //крыша
                graphics.DrawLine(pen, 50 * scaleX, 100 * scaleY, 100 * scaleX, 50 * scaleY);
                graphics.DrawLine(pen, 100 * scaleX, 50 * scaleY, 150 * scaleX, 100 * scaleY);
                graphics.DrawLine(pen, 50 * scaleX, 100 * scaleY, 150 * scaleX, 100 * scaleY);

                //окно
                using (var windowBrush = new SolidBrush(Color.LightBlue))
                {
                    graphics.FillRectangle(windowBrush, new Rectangle(
                        (int)(70 * scaleX),
                        (int)(130 * scaleY),
                        (int)(40 * scaleX),
                        (int)(40 * scaleY)
                    ));
                }

                using (var windowPen = new Pen(Color.Black, 2 / Math.Max(scaleX, scaleY)))
                {
                    graphics.DrawRectangle(windowPen, new Rectangle(
                        (int)(70 * scaleX),
                        (int)(130 * scaleY),
                        (int)(40 * scaleX),
                        (int)(40 * scaleY)
                    ));
                    graphics.DrawLine(windowPen, 70 * scaleX, 150 * scaleY, 110 * scaleX, 150 * scaleY);
                    graphics.DrawLine(windowPen, 90 * scaleX, 130 * scaleY, 90 * scaleX, 170 * scaleY);
                }

            }

            olimpickiecolca(e);
            son(e);
            ptichi(e);



        }

        int diameter = 40;

        public void olimpickiecolca(PaintEventArgs e)
        {
            base.OnPaint(e);
            var graphics = e.Graphics;

            
            float scaleX = (float)Width / OriginalWidth;
            float scaleY = (float)Height / OriginalHeight;



            using (var penBlue = new Pen(Color.Blue, 3 / Math.Max(scaleX, scaleY)))
            {
                graphics.DrawEllipse(penBlue, 243 * scaleX, 200 * scaleY, diameter * scaleX, diameter * scaleY);
            }

            using (var penYellow = new Pen(Color.Yellow, 3 / Math.Max(scaleX, scaleY)))
            {
                graphics.DrawEllipse(penYellow, 210 * scaleX, 200 * scaleY, diameter * scaleX, diameter * scaleY); 
            }

            using (var penBlack = new Pen(Color.Black, 3 / Math.Max(scaleX, scaleY)))
            {
                graphics.DrawEllipse(penBlack, 176 * scaleX, 200 * scaleY, diameter * scaleX, diameter * scaleY); 
            }

            using (var penGreen = new Pen(Color.Green, 3 / Math.Max(scaleX, scaleY)))
            {
                graphics.DrawEllipse(penGreen, 193 * scaleX, 225 * scaleY, diameter * scaleX, diameter * scaleY); 
            }

            using (var penRed = new Pen(Color.Red, 3 / Math.Max(scaleX, scaleY)))
            {
                graphics.DrawEllipse(penRed, 225 * scaleX, 225 * scaleY, diameter * scaleX, diameter * scaleY); 
            }
        }

        private void son(PaintEventArgs e)
        {
            base.OnPaint(e);
            var graphics = e.Graphics;

            
            float scaleX = (float)Width / OriginalWidth;
            float scaleY = (float)Height / OriginalHeight;

            //заливка
            using (var brushYellow = new SolidBrush(Color.Yellow))
            {
                
                graphics.FillEllipse(brushYellow, 240 * scaleX, -10 * scaleY, 100 * scaleX, 60 * scaleY);
            }
        }

        private void ptichi(PaintEventArgs e)
        {
            base.OnPaint(e);
            var graphics = e.Graphics;

            
            float scaleX = (float)Width / OriginalWidth;
            float scaleY = (float)Height / OriginalHeight;

            var pen = new Pen(Color.Black, 2 / Math.Max(scaleX, scaleY));
            var font = new Font("Arial", 20);
            var brush = new SolidBrush(Color.Black);
            {
                // это птицы 
                graphics.DrawString("✔", font, brush, 50 * scaleX, 20 * scaleY);
                graphics.DrawString("✔", font, brush, 100 * scaleX, 20 * scaleY);
                graphics.DrawString("✔", font, brush, 150 * scaleX, 20 * scaleY);

                
              
            }
        }


        [STAThread]
            public static void Main()
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }
    }


