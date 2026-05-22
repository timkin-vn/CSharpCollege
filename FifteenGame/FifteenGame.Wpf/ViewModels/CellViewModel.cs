using System.Windows.Media;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        public int Value { get; set; }

        public string Text => Value == 0 ? string.Empty : Value.ToString();

        public int Row { get; set; }

        public int Column { get; set; }

        public Brush TileBrush { get; set; }

        public Brush TextBrush { get; set; }

        public static CellViewModel Create(int row, int column, int value)
        {
            return new CellViewModel
            {
                Row = row,
                Column = column,
                Value = value,
                TileBrush = GetTileBrush(value),
                TextBrush = GetTextBrush(value),
            };
        }

        private static Brush GetTileBrush(int value)
        {
            string color;
            switch (value)
            {
                case 0: color = "#CDC1B4"; break;
                case 2: color = "#EEE4DA"; break;
                case 4: color = "#EDE0C8"; break;
                case 8: color = "#F2B179"; break;
                case 16: color = "#F59563"; break;
                case 32: color = "#F67C5F"; break;
                case 64: color = "#F65E3B"; break;
                case 128: color = "#EDCF72"; break;
                case 256: color = "#EDCC61"; break;
                case 512: color = "#EDC850"; break;
                case 1024: color = "#EDC53F"; break;
                case 2048: color = "#EDC22E"; break;
                default: color = "#3C3A32"; break;
            }

            return (Brush)new BrushConverter().ConvertFromString(color);
        }

        private static Brush GetTextBrush(int value)
        {
            string color = value <= 4 && value != 0 ? "#776E65" : "#F9F6F2";
            return (Brush)new BrushConverter().ConvertFromString(color);
        }
    }
}
