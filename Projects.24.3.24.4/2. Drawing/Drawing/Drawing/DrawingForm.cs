using Drawing.ViewServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Drawing.Services;

namespace Drawing
{
    public partial class DrawingForm : Form
    {
        private Painter _painter = new Painter();
        private PictureBuilder _pictureBuilder;
        public DrawingForm()
        {
            InitializeComponent();
            this.Text = "Рисунок: Train";
            this.KeyDown += DrawingForm_KeyDown;
            _pictureBuilder = _painter.Builder;
        }

        private void DrawingForm_Paint(object sender, PaintEventArgs e)
        {
            _painter.Paint(ClientRectangle, e.Graphics);
        }

        private void DrawingForm_Resize(object sender, EventArgs e)
        {
            Refresh();
        }
        
        private void DrawingForm_KeyDown(object sender, KeyEventArgs e)
        {
            bool pictureChanged = false;

            // checking keycode
            switch (e.KeyCode)
            {
                case Keys.D1:
                    _pictureBuilder.CurrentPicture = PictureType.Train;
                    pictureChanged = true;
                    break;
                case Keys.D2:
                    _pictureBuilder.CurrentPicture = PictureType.HouseScene;
                    pictureChanged = true;
                    break;
                case Keys.D3:
                    _pictureBuilder.CurrentPicture = PictureType.SunAndClouds;
                    pictureChanged = true;
                    break;
                case Keys.D4:
                    _pictureBuilder.CurrentPicture = PictureType.Abstract;
                    pictureChanged = true;
                    break;
            }

            // if picture is changed
            if (pictureChanged)
            {
                this.Text = $"Рисунок: {_pictureBuilder.CurrentPicture}"; // updating title
                Refresh();
            }
        }
    }
}
