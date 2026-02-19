using AlarmClock.Models;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AlarmClock.Forms
{
    public partial class AwakeForm : Form
    {
        private const string ImageFolderName = "Images";
        private string[] _imageFileNames;
        private int _imageIndex;

        public AlarmState AlarmState { get; set; }

        public AwakeForm()
        {
            InitializeComponent();
            awakeButton.Visible = false;
        }

        private void AwakeForm_Load(object sender, EventArgs e)
        {
            this.Text = AlarmState?.AlarmMessage ?? "Будильник";
            InitializeImages();

            var infoLabel = new Label
            {
                Text = "Для отключения будильника нужно решить пример!",
                Location = new System.Drawing.Point(150, 320),
                Size = new System.Drawing.Size(300, 20),
                ForeColor = System.Drawing.Color.Red,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold)
            };
            this.Controls.Add(infoLabel);
        }

        private void InitializeImages()
        {
            if (!Directory.Exists(ImageFolderName))
            {
                MessageBox.Show("Папка с изображениями не найдена!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _imageFileNames = Directory.EnumerateFiles(ImageFolderName).ToArray();
            if (_imageFileNames.Length == 0)
            {
                MessageBox.Show("Нет изображений для показа!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _imageIndex = 0;
            awakePictureBox.Load(_imageFileNames[_imageIndex]);
        }

        private void AwakeTimer_Tick(object sender, EventArgs e)
        {
            if (_imageFileNames == null || _imageFileNames.Length == 0)
                return;

            _imageIndex = (_imageIndex + 1) % _imageFileNames.Length;
            awakePictureBox.Load(_imageFileNames[_imageIndex]);
        }

        private void AwakeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK && AlarmState != null)
            {
                AlarmState.IsAlarmActive = false;
                AlarmState.IsAwakeActivated = false;
            }
        }

        private void AwakePictureBox_Click(object sender, EventArgs e)
        {
            ShowMathChallenge();
        }

        private void ShowMathChallenge()
        {
            var mathForm = new MathChallengeForm();
            if (mathForm.ShowDialog() == DialogResult.OK)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}