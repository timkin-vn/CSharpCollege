using Drawing.ViewServices;
using System;
using System.Windows.Forms;

namespace Drawing
{
    public partial class DrawingForm : Form
    {
        private int _pictureNumber = 1;

        public DrawingForm()
        {
            InitializeComponent();
            KeyPreview = true;
            KeyDown += DrawingForm_KeyDown;
            UpdateTitle();
        }

        private void DrawingForm_Paint(object sender, PaintEventArgs e)
        {
            var painter = new Painter();
            painter.Paint(ClientRectangle, e.Graphics, _pictureNumber);
        }

        private void DrawingForm_Resize(object sender, EventArgs e)
        {
            Refresh();
        }

        private void DrawingForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.D1 && e.KeyCode <= Keys.D4)
            {
                _pictureNumber = e.KeyCode - Keys.D0;
                UpdateTitle();
                Refresh();
                return;
            }

            if (e.KeyCode >= Keys.NumPad1 && e.KeyCode <= Keys.NumPad4)
            {
                _pictureNumber = e.KeyCode - Keys.NumPad0;
                UpdateTitle();
                Refresh();
            }
        }

        private void UpdateTitle()
        {
            Text = $"Рисование. Рисунок {_pictureNumber} (клавиши 1-4)";
        }
    }
}
