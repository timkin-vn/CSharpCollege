
using Drawing.Services;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Drawing
{
    public partial class DrawingForm : Form
    {
        private Button SaveButton;
        private Button LoadButton;

        private Image loadedImage;

        public DrawingForm()
        {
            InitializeComponent();

            SaveButton = new Button
            {
                Name = "SaveButton",
                Text = "Сохранить PNG",
                Width = 120,
                Height = 32,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            LoadButton = new Button
            {
                Name = "LoadButton",
                Text = "Загрузить рисунок",
                Width = 140,
                Height = 32,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            SaveButton.Click += SaveButton_Click;
            LoadButton.Click += LoadButton_Click;

            Controls.Add(SaveButton);
            Controls.Add(LoadButton);

            Load += DrawingForm_Load;
            FormClosed += DrawingForm_FormClosed;
        }

        private void DrawingForm_Load(object sender, EventArgs e)
        {
            Text = "Drawing";
            MinimumSize = new Size(400, 300);

            UpdateButtonsPosition();
            Refresh();
        }

        private void DrawingForm_Paint(object sender, PaintEventArgs e)
        {
            if (loadedImage != null)
            {
                e.Graphics.DrawImage(
                    loadedImage,
                    ClientRectangle
                );

                return;
            }

            var painter = new Painter();
            painter.Initialize(ClientRectangle);
            painter.Paint(e.Graphics);
        }

        private void DrawingForm_Resize(object sender, EventArgs e)
        {
            UpdateButtonsPosition();
            Refresh();
        }

        private void UpdateButtonsPosition()
        {
            if (SaveButton == null || LoadButton == null)
            {
                return;
            }

            int rightMargin = 10;
            int topMargin = 10;
            int spacing = 8;

            SaveButton.Location = new Point(
                Math.Max(
                    10,
                    ClientSize.Width - SaveButton.Width - rightMargin
                ),
                topMargin
            );

            LoadButton.Location = new Point(
                Math.Max(
                    10,
                    SaveButton.Left - LoadButton.Width - spacing
                ),
                topMargin
            );
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Загрузить рисунок";
                dialog.Filter =
                    "Файлы изображений|*.png;*.jpg;*.jpeg;*.bmp;*.gif;*.tif;*.tiff|" +
                    "Все файлы (*.*)|*.*";

                dialog.Multiselect = false;
                dialog.CheckFileExists = true;

                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                try
                {

                    Image newImage;

                    using (Image source = Image.FromFile(dialog.FileName))
                    {
                        newImage = new Bitmap(source);
                    }

                    Image oldImage = loadedImage;
                    loadedImage = newImage;

                    if (oldImage != null)
                    {
                        oldImage.Dispose();
                    }

                    Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Не удалось загрузить изображение.\n\n" + ex.Message,
                        "Ошибка загрузки",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "Сохранить рисунок";
                dialog.Filter = "PNG изображение (*.png)|*.png";
                dialog.DefaultExt = "png";
                dialog.AddExtension = true;
                dialog.FileName = "Drawing.png";
                dialog.OverwritePrompt = true;

                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    SavePictureToPng(dialog.FileName);

                    MessageBox.Show(
                        "Рисунок успешно сохранён.",
                        "Сохранение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Не удалось сохранить рисунок.\n\n" + ex.Message,
                        "Ошибка сохранения",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        private void SavePictureToPng(string fileName)
        {
            if (ClientSize.Width <= 0 || ClientSize.Height <= 0)
            {
                throw new InvalidOperationException(
                    "Размер области рисования должен быть больше нуля."
                );
            }

            using (Bitmap bitmap = new Bitmap(
                ClientSize.Width,
                ClientSize.Height))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(BackColor);

                    if (loadedImage != null)
                    {
                        graphics.DrawImage(
                            loadedImage,
                            new Rectangle(
                                0,
                                0,
                                ClientSize.Width,
                                ClientSize.Height
                            )
                        );
                    }
                    else
                    {
                        var painter = new Painter();
                        painter.Initialize(ClientRectangle);
                        painter.Paint(graphics);
                    }
                }

                bitmap.Save(fileName, ImageFormat.Png);
            }
        }

        private void DrawingForm_FormClosed(
            object sender,
            FormClosedEventArgs e)
        {
            if (loadedImage != null)
            {
                loadedImage.Dispose();
                loadedImage = null;
            }
        }
    }
}
