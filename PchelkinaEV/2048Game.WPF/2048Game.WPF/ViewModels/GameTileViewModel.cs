using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace _2048Game.WPF.ViewModels
{
    public class GameTileViewModel
    {
        public int Value { get; set; }
        public Brush Background => GetTileColor();

        private Brush GetTileColor()
        {
            switch(Value)
            {
                case 2:
                    return new SolidColorBrush(Color.FromRgb(236, 224, 200));
                case 4:
                    return new SolidColorBrush(Color.FromRgb(238, 228, 218));
                case 8:
                    return new SolidColorBrush(Color.FromRgb(242, 177, 174));
                case 16:
                    return new SolidColorBrush(Color.FromRgb(245, 149, 99));
                case 32:
                    return new SolidColorBrush(Color.FromRgb(246, 124, 95));
                case 64:
                    return new SolidColorBrush(Color.FromRgb(246, 94, 59));
                case 128:
                    return new SolidColorBrush(Color.FromRgb(237, 207, 114));
                case 256:
                    return new SolidColorBrush(Color.FromRgb(237, 204, 97));
                case 512:
                    return new SolidColorBrush(Color.FromRgb(237, 200, 80));
                case 1024:
                    return new SolidColorBrush(Color.FromRgb(237, 197, 63));
                case 2048:
                    return new SolidColorBrush(Color.FromRgb(237, 194, 46));
                default:
                    return new SolidColorBrush(Color.FromRgb(205, 193, 180));
            }
        }
    }
}
