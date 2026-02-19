using System;
using System.Drawing;
using System.Windows.Forms;

namespace AlarmClock.Forms
{
    public partial class MathChallengeForm : Form
    {
        private int _correctAnswer;
        private readonly Random _random = new Random();
        private readonly Timer _timeoutTimer = new Timer();
        private int _timeLeft = 30;
        private bool _challengeCompleted;

        public bool ChallengeCompleted => _challengeCompleted;

        public MathChallengeForm()
        {
            InitializeComponent();
            GenerateNewExample();
            SetupTimeoutTimer();
        }

        private void SetupTimeoutTimer()
        {
            _timeoutTimer.Interval = 1000;
            _timeoutTimer.Tick += TimeoutTimer_Tick;
            _timeoutTimer.Start();
        }

        private void TimeoutTimer_Tick(object sender, EventArgs e)
        {
            _timeLeft--;
            labelTimer.Text = $"Осталось времени: {_timeLeft} сек";
            labelTimer.ForeColor = _timeLeft <= 10 ? Color.Red : Color.Black;

            if (_timeLeft <= 0)
            {
                _timeoutTimer.Stop();
                MessageBox.Show("Время вышло! Попробуйте еще раз.", "Тест не пройден",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                GenerateNewExample();
                _timeLeft = 30;
                _timeoutTimer.Start();
            }
        }

        private void GenerateNewExample()
        {
            int num1 = _random.Next(1, 20);
            int num2 = _random.Next(1, 20);
            int operation = _random.Next(0, 3);

            switch (operation)
            {
                case 0:
                    labelQuestion.Text = $"{num1} + {num2} = ?";
                    _correctAnswer = num1 + num2;
                    break;
                case 1:
                    if (num1 < num2)
                    {
                        (num1, num2) = (num2, num1);
                    }
                    labelQuestion.Text = $"{num1} - {num2} = ?";
                    _correctAnswer = num1 - num2;
                    break;
                case 2:
                    num1 = _random.Next(1, 10);
                    num2 = _random.Next(1, 10);
                    labelQuestion.Text = $"{num1} ? {num2} = ?";
                    _correctAnswer = num1 * num2;
                    break;
            }

            textBoxAnswer.Clear();
            textBoxAnswer.Focus();
        }

        private void TextBoxAnswer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                CheckAnswer();
            }
        }

        private void ButtonSubmit_Click(object sender, EventArgs e)
        {
            CheckAnswer();
        }

        private void CheckAnswer()
        {
            if (string.IsNullOrWhiteSpace(textBoxAnswer.Text))
            {
                MessageBox.Show("Введите ответ!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (int.TryParse(textBoxAnswer.Text, out int userAnswer))
            {
                if (userAnswer == _correctAnswer)
                {
                    _timeoutTimer.Stop();
                    _challengeCompleted = true;
                    MessageBox.Show("Правильно! Будильник отключен.", "Успех!",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Неправильно! Попробуйте еще раз.", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxAnswer.Clear();
                    textBoxAnswer.Focus();
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!_challengeCompleted && e.CloseReason == CloseReason.UserClosing)
            {
                MessageBox.Show("Вы не можете закрыть окно, не решив пример!",
                    "Будильник не отключен", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
            base.OnFormClosing(e);
        }
    }
}