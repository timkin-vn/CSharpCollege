using System;
using System.Drawing;
using System.Windows.Forms;

public class BackgroundManager
{
    private Image backgroundImage;
    private readonly Control canvas;

    public BackgroundManager(Control canvas)
    {
        if (canvas == null)
        {
            throw new ArgumentNullException(nameof(canvas));
        }
        this.canvas = canvas;

    }

    public void LoadBackground()
    {
        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        {
            openFileDialog.Filter = "Изображения|*.png;*.jpg;*.jpeg";
            openFileDialog.Title = "Выберите фоновое изображение";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    backgroundImage = Image.FromFile(openFileDialog.FileName);
                    canvas.Invalidate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки изображения: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

    public void DrawBackground(Graphics g)
    {
        if (backgroundImage != null)
        {
            g.DrawImage(backgroundImage, 0, 0, canvas.Width, canvas.Height);
        }
    }
}
