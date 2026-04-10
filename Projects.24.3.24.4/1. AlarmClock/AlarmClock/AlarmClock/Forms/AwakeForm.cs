using AlarmClock.Model;
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
        private const string ImageFolderName = "Images";

        private string[] _imageFileNames;

        private int _imageIndex;

        internal AlarmClockState ClockState { get; set; }

        public AwakeForm()
        {
            InitializeComponent();
            GenerateMathProblem();
        }

        private void AwakeForm_Load(object sender, EventArgs e)
        {
            AlarmMessageLabel.Text = ClockState.AlarmMessage;
            InitializeImages();
        }

        private void InitializeImages()
        {
            _imageFileNames = Directory.EnumerateFiles(ImageFolderName).ToArray();
            _imageIndex = 0;
            AwakePictureBox.Load(_imageFileNames[_imageIndex]);
        }

        private int correctAnswer;
        
        private void AwakeTimer_Tick(object sender, EventArgs e)
        {
            if (int.TryParse(InputProblem.Text, out int userAnswer))
            {
                if (userAnswer == correctAnswer)
                {
                    InputProblem.Text = "Верно!";
                    ActiveForm.Close();
                }
                else
                {
                    ProblemText.BackColor = Color.White;
                }
            }
            
            _imageIndex++;
            if (_imageIndex >= _imageFileNames.Length)
            {
                _imageIndex = 0;
            }

            AwakePictureBox.Load(_imageFileNames[_imageIndex]);
        }

        private void GenerateMathProblem()
        {
            Random rnd = new Random();
            int a = rnd.Next(5, 20);
            int b = rnd.Next(5, 20);
            correctAnswer = a + b;
            

            ProblemText.Text = $"{a} + {b} = ?";
            InputProblem.Focus();
        }

        private void AlarmMessageLabel_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
