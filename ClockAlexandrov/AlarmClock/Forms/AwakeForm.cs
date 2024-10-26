using AlarmClock.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlarmClock.Forms
{
    public partial class AwakeForm : Form
    {
        private int Music = 0;
        private string NameFile = "";

        private const string ImageFolderName = "Images";

        private List<string> _fileNames = new List<string>();

        private int _imageIndex = 0;

        public AlarmSettings Settings { get; set; }

        public AwakeForm()
        {
            InitializeComponent();
        }

        private void AwakeButton_Click(object sender, EventArgs e)
        {
            Settings.IsAlarmActive = false;
            Settings.Music = 0;
            Close();
        }

        private void AwakeForm_Load(object sender, EventArgs e)
        {
            AwakeMessageLabel.Text = Settings.AlarmMessage;
            InitializeImages();
        }

        private void AwakeTimer_Tick(object sender, EventArgs e)
        {
            _imageIndex++;
            if (_imageIndex >= _fileNames.Count)
            {
                _imageIndex = 0;
            }

            AwakePictureBox.Load(_fileNames[_imageIndex]);
        }

        private void InitializeImages()
        {
            _imageIndex = 0;
            _fileNames.Clear();
            _fileNames.AddRange(Directory.EnumerateFiles(ImageFolderName));
            AwakePictureBox.Load(_fileNames[_imageIndex]);
        }

        private void AwakePictureBox_Click(object sender, EventArgs e)
        {

        }
        private void Button1_Click(object sender, EventArgs e)
        {
            string extension = "";
            if (Music == 0)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    NameFile = openFileDialog1.FileName;
                    extension = Path.GetExtension(NameFile);
                    if (extension != "mp3")
                    {
                        MessageBox.Show("Файл должен иметь расширение mp3");
                        return;
                    }
                    AwakeButton.Text = NameFile.Substring(0, 14) + "...";
                }

            }
            else
            {
                
                AwakeButton.Text = NameFile.Substring(0, 14) + "...";
                Music = 0;
            }
        }
    }
}
