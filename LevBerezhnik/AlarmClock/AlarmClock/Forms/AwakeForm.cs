using AlarmClock.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace AlarmClock.Forms
{
    public partial class AwakeForm : Form
    {
        public AlarmSettings Settings { get; set; }

        private int _imageIndex = 0;

        private Random _random = new Random();

        private Bitmap[] _images = new[]
        {
            Properties.Resources.Image1,
            Properties.Resources.Image3,
            Properties.Resources.Image4,
            Properties.Resources.Image5,
            Properties.Resources.Image7,
        };

        public AwakeForm()
        {
            InitializeComponent();
        }

        private void AwakeButton_Click(object sender, EventArgs e)
        {
            int number = _random.Next(100000, 999999);

            Form inputForm = new Form()
            {
                Width = 300,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Postpone Challenge",
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label label = new Label()
            {
                Text = "Чтобы выключить будильник, введите: " + number.ToString(),
                Left = 20,
                Top = 20,
                Width = 260,
                AutoSize = true
            };

            TextBox textBox = new TextBox()
            {
                Left = 20,
                Top = 50,
                Width = 260
            };

            Button confirmButton = new Button()
            {
                Text = "OK",
                Left = 20,
                Width = 260,
                Top = 80,
                DialogResult = DialogResult.OK
            };

            inputForm.Controls.Add(label);
            inputForm.Controls.Add(textBox);
            inputForm.Controls.Add(confirmButton);
            inputForm.AcceptButton = confirmButton;

            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                if (textBox.Text == number.ToString())
                {
                    Settings.IsAlarmActive = false;
                    Hide();
                }
                else
                {
                    MessageBox.Show("Попробуйте еще раз.",
                                  "Ошибка",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Warning);
                }
            }
        }

        private void AwakeTimer_Tick(object sender, EventArgs e)
        {
            _imageIndex++;
            if (_imageIndex >= _images.Length)
            {
                _imageIndex = 0;
            }

            AwakePictureBox.Image = _images[_imageIndex];
        }

        private void AwakeForm_Load(object sender, EventArgs e)
        {
            AwakeTimer.Enabled = true;
            AwakePictureBox.Image = Properties.Resources.Image1;
        }
    }
}
