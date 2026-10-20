using System.Windows.Media;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        private ImageSource _image;

        public ImageSource Image
        {
            get => _image;
            set
            {
                if (!ReferenceEquals(_image, value))
                {
                    _image = value;
                    OnPropertyChanged(nameof(Image));
                }
            }
        }
    }
}
