using System;
using System.Drawing;
using System.Windows.Forms;

namespace GraphEditor
{
    public partial class GradientForm : Form
    {
        public Color Color1 { get; private set; }
        public Color Color2 { get; private set; }

        public GradientForm()
        {
            InitializeComponent();
            Color1 = Color.Yellow;
            Color2 = Color.Red;
            UpdateColorDisplays();
        }

        private void UpdateColorDisplays()
        {
            panelColor1.BackColor = Color1;
            panelColor2.BackColor = Color2;
        }

        private void btnColor1_Click(object sender, EventArgs e)
        {
            using (var colorDialog = new ColorDialog() { Color = Color1 })
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    Color1 = colorDialog.Color;
                    UpdateColorDisplays();
                }
            }
        }

        private void btnColor2_Click(object sender, EventArgs e)
        {
            using (var colorDialog = new ColorDialog() { Color = Color2 })
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    Color2 = colorDialog.Color;
                    UpdateColorDisplays();
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}