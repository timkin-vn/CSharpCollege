using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphEditor.Views
{
    public partial class BorderWidthDialog : Form
    {
        public int BorderWidth { get; set; }
        public BorderWidthDialog(int width)
        {
            BorderWidth = width;
            
            InitializeComponent();
            UpdateControls();
            widthTextBox.Text = BorderWidth.ToString();
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            try
            {
                if(!int.TryParse(widthTextBox.Text, out var result))
                {
                    throw new FormatException("Введен неверный формат");
                }
                widthTextBox.Text = "";
                BorderWidth = result;
                UpdateControls();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateControls()
        {

            widthLabel.Text = $"Толщина: {BorderWidth}";
        }
    }
}
