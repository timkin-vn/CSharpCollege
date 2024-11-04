using GraphicsApp.Models;
using GraphicsApp.Services;
using System;
using System.Windows.Forms;

namespace GraphicsApp
{
    public partial class PictureForm : Form
    {
        public PictureForm()
        {
            InitializeComponent();
        }

        private void PictureForm_Paint(object sender, PaintEventArgs e)
        {
            var painter = new Painter();
            var limitRectangle = ClientRectangle;
            var scaler = new Scaler
            {
                MathRectangle = painter.LimitRectangle,
                ScreenRectangle = limitRectangle,
            };

            var paintManager = new PaintManager
            {
                Graphics = e.Graphics,
                Scaler = scaler,
            };

            painter.Paint(paintManager);
        }

        private void PictureForm_Resize(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
