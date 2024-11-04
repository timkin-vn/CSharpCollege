using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp8
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();//такого размера будет окно при открытии 
            this.Width = 300;  
            this.Height = 300; 
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);//не обязательно но желательно 
            var graphics = e.Graphics;

            // ручки для рисования 
            var pen = new Pen(Color.Black, 3);
            var brush = new SolidBrush(Color.Green);

            // Рисование квадрата дома
            graphics.FillRectangle(brush, new Rectangle(50, 100, 100, 100));

            // для крыши 
            graphics.DrawLine(pen, 50, 100, 100, 50);
            graphics.DrawLine(pen, 100, 50, 150, 100);
            graphics.DrawLine(pen, 50, 100, 150, 100);

            // для окна
            graphics.FillRectangle(new SolidBrush(Color.LightBlue), new Rectangle(70, 130, 40, 40)); 
            graphics.DrawRectangle(new Pen(Color.Black, 2), new Rectangle(70, 130, 40, 40)); 

            graphics.DrawLine(new Pen(Color.Black, 2), 70, 150, 110, 150); 
            graphics.DrawLine(new Pen(Color.Black, 2), 90, 130, 90, 170); 
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
