using System.Drawing;

namespace AlarmClock.Model
{
    /// <summary>
    /// Палитра цветов для одной темы оформления
    /// </summary>
    internal class ThemePalette
    {
        public Color FormBackColor { get; set; }
        public Color ForeColor { get; set; }
        public Color ControlBackColor { get; set; }
        public Color ControlForeColor { get; set; }
        public Color ButtonBackColor { get; set; }
        public Color ButtonForeColor { get; set; }
        public Color TextBoxBackColor { get; set; }
        public Color TextBoxForeColor { get; set; }
        public Color CheckBoxBackColor { get; set; }
        public Color CheckBoxForeColor { get; set; }
        public Color PanelBackColor { get; set; }
        public Color GroupBoxBackColor { get; set; }
        public Color MenuBackColor { get; set; }
        public Color MenuForeColor { get; set; }
        public Color MenuItemSelectedColor { get; set; }
        public Color DisplayBackColor { get; set; }       // для Label с часами
        public Color DisplayForeColor { get; set; }
        public Color PictureBoxBackColor { get; set; }    // фон AwakePictureBox
        public Color SeparatorColor { get; set; }         // для ToolStripSeparator

        public static ThemePalette Light = new ThemePalette
        {
            FormBackColor = Color.FromArgb(240, 240, 240),
            ForeColor = Color.Black,
            ControlBackColor = Color.White,
            ControlForeColor = Color.Black,
            ButtonBackColor = Color.FromArgb(225, 225, 225),
            ButtonForeColor = Color.Black,
            TextBoxBackColor = Color.White,
            TextBoxForeColor = Color.Black,
            CheckBoxBackColor = Color.FromArgb(240, 240, 240),
            CheckBoxForeColor = Color.Black,
            PanelBackColor = Color.FromArgb(240, 240, 240),
            GroupBoxBackColor = Color.FromArgb(240, 240, 240),
            MenuBackColor = Color.FromArgb(240, 240, 240),
            MenuForeColor = Color.Black,
            MenuItemSelectedColor = Color.FromArgb(200, 200, 255),
            DisplayBackColor = Color.White,
            DisplayForeColor = Color.FromArgb(0, 128, 0),
            PictureBoxBackColor = Color.WhiteSmoke,
            SeparatorColor = Color.Gray
        };

        public static ThemePalette Dark = new ThemePalette
        {
            FormBackColor = Color.FromArgb(30, 30, 30),
            ForeColor = Color.FromArgb(220, 220, 220),
            ControlBackColor = Color.FromArgb(45, 45, 48),
            ControlForeColor = Color.FromArgb(220, 220, 220),
            ButtonBackColor = Color.FromArgb(60, 60, 65),
            ButtonForeColor = Color.FromArgb(220, 220, 220),
            TextBoxBackColor = Color.FromArgb(45, 45, 48),
            TextBoxForeColor = Color.FromArgb(220, 220, 220),
            CheckBoxBackColor = Color.FromArgb(30, 30, 30),
            CheckBoxForeColor = Color.FromArgb(220, 220, 220),
            PanelBackColor = Color.FromArgb(30, 30, 30),
            GroupBoxBackColor = Color.FromArgb(30, 30, 30),
            MenuBackColor = Color.FromArgb(45, 45, 48),
            MenuForeColor = Color.FromArgb(220, 220, 220),
            MenuItemSelectedColor = Color.FromArgb(70, 70, 100),
            DisplayBackColor = Color.FromArgb(20, 20, 20),
            DisplayForeColor = Color.GreenYellow,
            PictureBoxBackColor = Color.FromArgb(40, 40, 40),
            SeparatorColor = Color.FromArgb(80, 80, 80)
        };
    }
}
