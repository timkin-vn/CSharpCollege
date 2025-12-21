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
                    return new SolidColorBrush(Color.FromRgb(238, 228, 218)); 
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
                // Green palette
                case 128:
                    return new SolidColorBrush(Color.FromRgb(220, 237, 200)); // light green
                case 256:
                    return new SolidColorBrush(Color.FromRgb(188, 230, 150)); // pale green
                case 512:
                    return new SolidColorBrush(Color.FromRgb(141, 210, 100)); // medium green
                case 1024:
                    return new SolidColorBrush(Color.FromRgb(102, 187, 60)); // strong green
                case 2048:
                    return new SolidColorBrush(Color.FromRgb(67, 160, 71)); // deep green
                default:
                    return new SolidColorBrush(Color.FromRgb(236, 224, 200));
                    
            }
        }
    }
}
