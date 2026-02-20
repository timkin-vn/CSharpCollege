using AlarmClock.Forms;
using AlarmClock.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlarmClock
{
    public partial class AlarmClockForm : Form
    {
        private AlarmState _alarmState = new AlarmState();
        // таймер плодил окна, пришлось добавить этот флаг
        private bool _isQuizOpen = false;
        public AlarmClockForm()
        {
            InitializeComponent();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            DisplayLabel.Text = DateTime.Now.ToLongTimeString();

            if (!_alarmState.IsAlarmActive) return;

            if (_alarmState.IsSoundActive && _alarmState.IsAwakeActivated)
            {
                SystemSounds.Beep.Play();
            }

    
            if (!_alarmState.IsAwakeActivated &&
                DateTime.Now.Hour == _alarmState.AlarmTime.Hour &&
                DateTime.Now.Minute == _alarmState.AlarmTime.Minute &&
                !_isQuizOpen)
            {
                _isQuizOpen = true;

                try
                {
                    // рандомные числа для примера
                    Random rand = new Random();
                    int a = rand.Next(10, 50);
                    int b = rand.Next(10, 50);
                    int correctAnswer = a + b;

                    using (Form quizForm = new Form() { Width = 300, Height = 150, Text = "ПРОСЫПАЙСЯ!", StartPosition = FormStartPosition.CenterScreen, FormBorderStyle = FormBorderStyle.FixedDialog, TopMost = true })
                    {
                        Label textLabel = new Label() { Left = 20, Top = 20, Width = 250, Text = $"Реши пример, чтобы выключить звук: {a} + {b} = ?" };
                        TextBox inputBox = new TextBox() { Left = 20, Top = 50, Width = 100 };
                        Button confirmBtn = new Button() { Text = "ОК", Left = 130, Top = 48, DialogResult = DialogResult.OK };

                        quizForm.Controls.Add(textLabel);
                        quizForm.Controls.Add(inputBox);
                        quizForm.Controls.Add(confirmBtn);
                        quizForm.AcceptButton = confirmBtn;

                        
                        Timer beepTimer = new Timer();
                        beepTimer.Interval = 1000; 
                        beepTimer.Tick += (s, ev) => { SystemSounds.Beep.Play(); };
                        beepTimer.Start();
                        

                        if (quizForm.ShowDialog() == DialogResult.OK)
                        {
                            beepTimer.Stop(); 
                            if (int.TryParse(inputBox.Text, out int userAnswer) && userAnswer == correctAnswer)
                            {
                                _alarmState.IsAwakeActivated = true;
                                var awakeForm = new AwakeForm { AlarmState = _alarmState };
                                awakeForm.FormClosed += AwakeForm_FormClosed;
                                awakeForm.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("Неверно! Слушай писк дальше!");
                            }
                        }
                        else
                        {
                            beepTimer.Stop();
                        }
                    }
                }
                finally
                {
                    _isQuizOpen = false;
                }
            }
        }

        private void AwakeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form)sender).FormClosed -= AwakeForm_FormClosed;
            UpdateControls();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            var form = new AboutForm();
            form.ShowDialog();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            var form = new SettingsForm();
            form.AlarmState = _alarmState;

            if (form.ShowDialog() == DialogResult.OK)
            {
                UpdateControls();
            }
        }

        private void UpdateControls()
        {
            if (_alarmState.IsAlarmActive)
            {
                Text = $"Будильник (ожидает срабатывания в {_alarmState.AlarmTime.ToShortTimeString()})";
            }
            else
            {
                Text = "Будильник";
            }
        }
    }
}
