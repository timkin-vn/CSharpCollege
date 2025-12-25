using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GraphEditor
{
    public partial class TextureForm : Form
    {
        public string SelectedImagePath { get; private set; } = string.Empty;

        private TextBox textBoxPath;
        private Button btnBrowse;
        private Button btnOk;
        private Button btnCancel;
        private PictureBox pictureBox;

        public TextureForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Выбор картинки";
            this.Size = new Size(450, 350);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            textBoxPath = new TextBox();
            textBoxPath.Location = new Point(10, 10);
            textBoxPath.Size = new Size(300, 20);
            textBoxPath.ReadOnly = true;
            this.Controls.Add(textBoxPath);

            btnBrowse = new Button();
            btnBrowse.Location = new Point(320, 8);
            btnBrowse.Size = new Size(80, 23);
            btnBrowse.Text = "Обзор...";
            btnBrowse.Click += BtnBrowse_Click;
            this.Controls.Add(btnBrowse);

            pictureBox = new PictureBox();
            pictureBox.Location = new Point(10, 40);
            pictureBox.Size = new Size(390, 200);
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            this.Controls.Add(pictureBox);

            btnOk = new Button();
            btnOk.Location = new Point(245, 260);
            btnOk.Size = new Size(75, 23);
            btnOk.Text = "OK";
            btnOk.Click += BtnOk_Click;
            this.Controls.Add(btnOk);

            btnCancel = new Button();
            btnCancel.Location = new Point(325, 260);
            btnCancel.Size = new Size(75, 23);
            btnCancel.Text = "Отмена";
            btnCancel.Click += BtnCancel_Click;
            this.Controls.Add(btnCancel);
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Выберите изображение";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    SelectedImagePath = openFileDialog.FileName;
                    textBoxPath.Text = SelectedImagePath;

                    try
                    {
                        Image previewImage = Image.FromFile(SelectedImagePath);
                        pictureBox.Image = previewImage;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}");
                        pictureBox.Image = null;
                    }
                }
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SelectedImagePath))
            {
                MessageBox.Show("Выберите изображение!");
                return;
            }

            if (!System.IO.File.Exists(SelectedImagePath))
            {
                MessageBox.Show("Выбранный файл не существует!");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}