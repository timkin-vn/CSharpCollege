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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace AlarmClock.Forms
{
    public partial class FocusPeriodsForm : Form
    {
        private byte time;
        private byte totalTimeCount;
        private byte focusTimeCount;
        private byte breakTimeCount;
        private bool isBreakTime = false;
        public FocusPeriodsForm()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            Timer.Enabled = true;
            if (!byte.TryParse(totalTimeTextBox.Text, out var total) 
                | !byte.TryParse(BreakTimeTextBox.Text, out var breakTime))
            {
                MessageBox.Show("Числа дожны быть в пределах от 0 до 255", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            totalTimeCount = total;
            breakTimeCount = breakTime;
            isBreakTime = false;
            time = focusTimeCount;
            DisplayLabel.Text= "Оставшееся рабочее время: " + time.ToString();
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            if (Timer.Enabled)
            {
                Timer.Stop();
                return;
            }
            Timer.Start();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            Timer.Stop();

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (totalTimeCount > 0)
            {
                if (time > 0)
                {

                    if (!isBreakTime)
                    {
                        DisplayLabel.Text = "Оставшееся рабочее время: " + (--time).ToString();
                        totalTimeCount--;
                    }
                    else
                    {
                        DisplayLabel.Text = "Оставшееся время отдыха: " + (--time).ToString();
                    }
                    return;
                }
                if (!isBreakTime)
                {
                    isBreakTime = true;
                    time = breakTimeCount;
                    DisplayLabel.Text = "Оставшееся время отдыха: " + time.ToString();
                }
                else
                {
                    isBreakTime = false;
                    time = Math.Min(totalTimeCount, focusTimeCount);
                    DisplayLabel.Text = "Оставшееся рабочее время: " + time.ToString();
                }
                SystemSounds.Beep.Play();
                return;
            }
            else
            {
                DisplayLabel.Text = "Рабочее время закончилось";
                SystemSounds.Beep.Play();
                Timer.Stop();
            }
        }
    }
}
