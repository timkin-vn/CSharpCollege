using GraphEditor.ViewServices;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GraphEditor
{
    public partial class GraphEditorForm : Form
    {
        private readonly PictureViewService _pictureService = new PictureViewService();

        public GraphEditorForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            _pictureService.Paint(e.Graphics);
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            _pictureService.MouseDown(e.Location);
            pictureBox.Invalidate();
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox.Cursor = _pictureService.GetCursor(e.Location);

            if (e.Button == MouseButtons.Left)
            {
                _pictureService.MouseMove(e.Location);
                pictureBox.Invalidate();
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            _pictureService.MouseUp();
            pictureBox.Invalidate();
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            _pictureService.CreateButtonClicked();
            createButton.BackColor = _pictureService.CreateMode ? Color.LightGreen : SystemColors.Control;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            _pictureService.DeleteButtonClicked();
            pictureBox.Invalidate();
        }

        // ДОБАВЛЯЕМ ОБРАБОТЧИК ДЛЯ КНОПКИ СМЕНЫ ЦВЕТА
        private void changeColorButton_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.Color = Color.Yellow;
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    _pictureService.ChangeColor(colorDialog.Color);
                    pictureBox.Invalidate();
                }
            }
        }
    }
}