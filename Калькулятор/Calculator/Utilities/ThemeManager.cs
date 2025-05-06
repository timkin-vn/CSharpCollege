using System.Drawing;
using System.Windows.Forms;

namespace Calculator.Utilities
{
    public static class ThemeManager
    {
        public static void ApplyDarkTheme(Form form)
        {
            form.BackColor = Color.FromArgb(32, 32, 32);
            form.ForeColor = Color.White;

            foreach (Control control in form.Controls)
            {
                if (control is Button button)
                {
                    button.BackColor = Color.FromArgb(64, 64, 64);
                    button.ForeColor = Color.White;
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderColor = Color.Gray;
                }
                else if (control is Label label)
                {
                    label.BackColor = Color.Black;
                    label.ForeColor = Color.LightGoldenrodYellow;
                }
            }
        }

        public static void ApplyLightTheme(Form form)
        {
            form.BackColor = SystemColors.Control;
            form.ForeColor = SystemColors.ControlText;

            foreach (Control control in form.Controls)
            {
                if (control is Button button)
                {
                    button.BackColor = SystemColors.Control;
                    button.ForeColor = SystemColors.ControlText;
                    button.FlatStyle = FlatStyle.Standard;
                }
                else if (control is Label label)
                {
                    label.BackColor = SystemColors.ControlLight;
                    label.ForeColor = SystemColors.ControlText;
                }
            }
        }

        public static bool IsSystemThemeDark()
        {
            return Microsoft.Win32.Registry.GetValue(
                @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize",
                "AppsUseLightTheme", 1) is int themeValue && themeValue == 0;
        }
    }
}