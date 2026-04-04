using System.Drawing;
using System.Windows.Forms;

namespace AlarmClock.Model
{
    /// <summary>
    /// Таблица цветов для кастомного рендерера меню
    /// </summary>
    internal class ThemeColorTable : ProfessionalColorTable
    {
        private readonly ThemePalette _palette;

        public ThemeColorTable(ThemePalette palette)
        {
            _palette = palette;
        }

        public override Color MenuStripGradientBegin
        {
            get { return _palette.MenuBackColor; }
        }

        public override Color MenuStripGradientEnd
        {
            get { return _palette.MenuBackColor; }
        }

        public override Color MenuItemBorder
        {
            get { return _palette.MenuBackColor; }
        }

        public override Color ImageMarginGradientBegin
        {
            get { return _palette.MenuBackColor; }
        }

        public override Color ImageMarginGradientMiddle
        {
            get { return _palette.MenuBackColor; }
        }

        public override Color ImageMarginGradientEnd
        {
            get { return _palette.MenuBackColor; }
        }

        public override Color MenuItemSelected
        {
            get { return _palette.MenuItemSelectedColor; }
        }

        public override Color MenuItemPressedGradientBegin
        {
            get { return _palette.MenuBackColor; }
        }

        public override Color MenuItemPressedGradientEnd
        {
            get { return _palette.MenuBackColor; }
        }

        public override Color SeparatorDark
        {
            get { return _palette.SeparatorColor; }
        }

        public override Color SeparatorLight
        {
            get { return _palette.SeparatorColor; }
        }
    }
}
