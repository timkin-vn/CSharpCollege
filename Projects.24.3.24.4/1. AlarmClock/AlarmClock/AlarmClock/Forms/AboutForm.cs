using AlarmClock.Model;
using System;
using System.Windows.Forms;

namespace AlarmClock.Forms
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            var theme = ThemeManager.LoadTheme();
            ThemeManager.ApplyTheme(this, theme);
        }
    }
}
