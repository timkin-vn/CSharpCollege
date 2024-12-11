using GraphicsApp.Models;
using GraphicsApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var painter = new BoatPainter();
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
