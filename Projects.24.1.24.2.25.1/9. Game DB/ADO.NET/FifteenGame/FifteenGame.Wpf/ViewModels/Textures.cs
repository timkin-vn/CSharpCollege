using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FifteenGame.Wpf.ViewModels
{
    /// <summary>Текстуры клеток (PNG из папки Assets, те же рисунки, что и в веб-версии).</summary>
    public static class Textures
    {
        private static readonly Dictionary<string, ImageSource> Cache = new Dictionary<string, ImageSource>();

        public static ImageSource Floor => Get("floor");
        public static ImageSource Wall => Get("wall");
        public static ImageSource Player => Get("player");
        public static ImageSource Monster => Get("monster");
        public static ImageSource Key => Get("key");
        public static ImageSource ExitOpen => Get("exit-open");
        public static ImageSource ExitClosed => Get("exit-closed");

        private static ImageSource Get(string name)
        {
            if (!Cache.TryGetValue(name, out var image))
            {
                image = new BitmapImage(new Uri($"pack://application:,,,/Assets/{name}.png", UriKind.Absolute));
                image.Freeze();
                Cache[name] = image;
            }

            return image;
        }
    }
}
