using System;
using System.Drawing;
using System.Windows.Forms;

namespace AlarmClockApp
{
    public partial class AlarmPopupForm : Form
    {
        public AlarmPopupForm()
        {
            this.Text = "Будильник сработал!";
            this.TopMost = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(300, 150);

            var label = new Label
            {
                Text = "Время вышло!",
                Font = new Font("Arial", 16),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            var button = new Button
            {
                Text = "ОК",
                Dock = DockStyle.Bottom
            };
            button.Click += (s, e) => this.Close();

            this.Controls.Add(label);
            this.Controls.Add(button);
        }
    }
}
