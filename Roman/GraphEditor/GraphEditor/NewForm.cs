using System;
using System.Drawing;
using System.Windows.Forms;

namespace GraphEditor
{
    public class NewForm : Form
    {
        private Button loadButton;
        private PictureBox pictureBox;

        public NewForm()
        {
            this.Width = 800;
            this.Height = 600;
            this.Text = "окно для срисовки";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            loadButton = new Button();
            loadButton.Text = "Загрузить изображение";
            loadButton.Width = 200;
            loadButton.Height = 50;
            loadButton.Location = new Point(300, 500);  

            loadButton.Click += LoadButton_Click;

            pictureBox = new PictureBox();
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom; 
            pictureBox.Dock = DockStyle.Fill; 


            this.Controls.Add(loadButton);
            this.Controls.Add(pictureBox);
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Изображения JPG|*.jpg;*.jpeg";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {

                        Image image = Image.FromFile(openFileDialog.FileName);

                        
                        pictureBox.Image = image;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка загрузки изображения: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
