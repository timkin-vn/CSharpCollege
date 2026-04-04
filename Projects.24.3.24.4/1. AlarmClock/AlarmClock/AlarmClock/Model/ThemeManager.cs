using AlarmClock.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AlarmClock.Model
{
    /// <summary>
    /// Централизованный менеджер тем: загрузка, сохранение, применение ко всем контролам формы
    /// </summary>
    internal static class ThemeManager
    {
        /// <summary>
        /// Загрузить выбранную тему из настроек пользователя
        /// </summary>
        public static AppTheme LoadTheme()
        {
            try
            {
                return (AppTheme)Enum.Parse(typeof(AppTheme), Settings.Default.SelectedTheme);
            }
            catch
            {
                return AppTheme.Light;
            }
        }

        /// <summary>
        /// Сохранить выбранную тему в настройках пользователя
        /// </summary>
        public static void SaveTheme(AppTheme theme)
        {
            Settings.Default.SelectedTheme = theme.ToString();
            Settings.Default.Save();
        }

        /// <summary>
        /// Получить палитру для заданной темы
        /// </summary>
        public static ThemePalette GetPalette(AppTheme theme)
        {
            switch (theme)
            {
                case AppTheme.Light: return ThemePalette.Light;
                case AppTheme.Dark: return ThemePalette.Dark;
                default: return ThemePalette.Light;
            }
        }

        /// <summary>
        /// Применить тему ко всем контролам на форме (рекурсивно)
        /// </summary>
        public static void ApplyTheme(Form form, AppTheme theme)
        {
            var palette = GetPalette(theme);

            form.BackColor = palette.FormBackColor;
            form.ForeColor = palette.ForeColor;

            ApplyToControls(form.Controls, palette);
        }

        private static void ApplyToControls(Control.ControlCollection controls, ThemePalette palette)
        {
            foreach (Control control in controls)
            {
                ApplyToControl(control, palette);

                // Рекурсивно обрабатываем дочерние контролы
                if (control.Controls.Count > 0)
                {
                    ApplyToControls(control.Controls, palette);
                }

                // MenuStrip: обрабатываем пункты меню
                MenuStrip menuStrip = control as MenuStrip;
                if (menuStrip != null)
                {
                    menuStrip.BackColor = palette.MenuBackColor;
                    menuStrip.ForeColor = palette.MenuForeColor;
                    menuStrip.Renderer = new ToolStripProfessionalRenderer(new ThemeColorTable(palette));
                }
            }
        }

        private static void ApplyToControl(Control control, ThemePalette palette)
        {
            if (control is Button)
            {
                Button btn = (Button)control;
                btn.BackColor = palette.ButtonBackColor;
                btn.ForeColor = palette.ButtonForeColor;
                btn.FlatStyle = FlatStyle.Standard;
                btn.UseVisualStyleBackColor = false;
            }
            else if (control is Label)
            {
                Label lbl = (Label)control;
                // Специальный случай: DisplayLabel (часы) на ClockForm
                if (lbl.Name == "DisplayLabel")
                {
                    lbl.BackColor = palette.DisplayBackColor;
                    lbl.ForeColor = palette.DisplayForeColor;
                }
                else
                {
                    lbl.BackColor = control.Parent != null ? control.Parent.BackColor : palette.FormBackColor;
                    lbl.ForeColor = palette.ForeColor;
                }
            }
            else if (control is TextBox)
            {
                TextBox tb = (TextBox)control;
                tb.BackColor = palette.TextBoxBackColor;
                tb.ForeColor = palette.TextBoxForeColor;
            }
            else if (control is MaskedTextBox)
            {
                MaskedTextBox mtb = (MaskedTextBox)control;
                mtb.BackColor = palette.TextBoxBackColor;
                mtb.ForeColor = palette.TextBoxForeColor;
            }
            else if (control is CheckBox)
            {
                CheckBox cb = (CheckBox)control;
                cb.BackColor = palette.CheckBoxBackColor;
                cb.ForeColor = palette.CheckBoxForeColor;
            }
            else if (control is ComboBox)
            {
                ComboBox cbx = (ComboBox)control;
                cbx.BackColor = palette.TextBoxBackColor;
                cbx.ForeColor = palette.TextBoxForeColor;
            }
            else if (control is Panel)
            {
                Panel panel = (Panel)control;
                panel.BackColor = palette.PanelBackColor;
                panel.ForeColor = palette.ForeColor;
            }
            else if (control is GroupBox)
            {
                GroupBox gb = (GroupBox)control;
                gb.BackColor = palette.GroupBoxBackColor;
                gb.ForeColor = palette.ForeColor;
            }
            else if (control is PictureBox)
            {
                PictureBox pb = (PictureBox)control;
                pb.BackColor = palette.PictureBoxBackColor;
            }
            else if (control is ListBox)
            {
                ListBox lb = (ListBox)control;
                lb.BackColor = palette.TextBoxBackColor;
                lb.ForeColor = palette.TextBoxForeColor;
            }
            else if (control is ListView)
            {
                ListView lv = (ListView)control;
                lv.BackColor = palette.TextBoxBackColor;
                lv.ForeColor = palette.TextBoxForeColor;
            }
            else if (control is DateTimePicker)
            {
                DateTimePicker dtp = (DateTimePicker)control;
                dtp.BackColor = palette.TextBoxBackColor;
                dtp.ForeColor = palette.TextBoxForeColor;
            }
            else if (control is TabControl)
            {
                TabControl tc = (TabControl)control;
                tc.BackColor = palette.PanelBackColor;
                tc.ForeColor = palette.ForeColor;
                foreach (TabPage tp in tc.TabPages)
                {
                    tp.BackColor = palette.PanelBackColor;
                    tp.ForeColor = palette.ForeColor;
                }
            }
            else
            {
                // Для всех остальных контролов — пробуем установить BackColor/ForeColor
                try { control.BackColor = palette.ControlBackColor; } catch { }
                try { control.ForeColor = palette.ControlForeColor; } catch { }
            }
        }
    }
}
