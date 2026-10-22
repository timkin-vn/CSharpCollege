using Drawing.Services;
using System;
using System.Windows.Forms;

namespace Drawing
{
    public partial class DrawingForm : Form
    {
        private Painter _painter = new Painter();
        private PictureBuilder _builder = new PictureBuilder();

        public DrawingForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            this.MouseClick += DrawingForm_MouseClick;
            this.Resize += DrawingForm_Resize;
           
        }

        private void DrawingForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (_builder.CurrentPicture == PictureType.Car)
                _builder.CurrentPicture = PictureType.House;
            else if (_builder.CurrentPicture == PictureType.House)
                _builder.CurrentPicture = PictureType.Ship;
            else
                _builder.CurrentPicture = PictureType.Car;

            this.Invalidate();
            this.Update();
        }

        private void DrawingForm_Resize(object sender, EventArgs e)
        {
          
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            _painter.Initialize(this.ClientRectangle, _builder);
            _painter.Paint(e.Graphics);
        }
    }
}