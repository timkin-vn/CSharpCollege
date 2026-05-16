using System;
using System.Windows.Forms;
using Drawing.Models;
using Drawing.Services;

namespace Drawing
{
    public partial class DrawingForm : Form
    {
        private readonly Painter _painter = new Painter();
        private readonly PictureBuilder _pictureBuilder = new PictureBuilder();
        private readonly Scaler _scaler = new Scaler();

        public DrawingForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true; 
            this.Resize += DrawingForm_Resize; 
            this.Paint += DrawingForm_Paint;  
        }

        private void DrawingForm_Paint(object sender, PaintEventArgs e)
        {
            var canvasSize = new SizeModel { Width = ClientSize.Width, Height = ClientSize.Height };
            _pictureBuilder.Build(canvasSize, _scaler);
            _painter.Paint(e.Graphics, _pictureBuilder);
        }

        private void DrawingForm_Resize(object sender, EventArgs e)
        {
            Invalidate(); 
        }
    }
}