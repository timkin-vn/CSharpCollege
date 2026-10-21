using MemoryGame.Common.BusinessModels;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MemoryGame.Wpf.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        private static readonly Dictionary<string, ImageSource> Images = new Dictionary<string, ImageSource>();

        public int Value { get; set; }

        public CardState State { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public ImageSource Image => LoadImage(ImageName);

        private string ImageName
        {
            get
            {
                switch (State)
                {
                    case CardState.Opened:
                        return $"{Value}.png";
                    case CardState.Matched:
                        return $"{Value}-matched.png";
                    default:
                        return "back.png";
                }
            }
        }

        private static ImageSource LoadImage(string name)
        {
            if (!Images.TryGetValue(name, out var image))
            {
                image = new BitmapImage(new Uri($"pack://application:,,,/Images/{name}", UriKind.Absolute));
                image.Freeze();
                Images[name] = image;
            }

            return image;
        }
    }
}