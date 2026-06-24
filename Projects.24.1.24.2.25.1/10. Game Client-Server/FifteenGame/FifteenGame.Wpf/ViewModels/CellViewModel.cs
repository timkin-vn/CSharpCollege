using System.Windows.Media;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        private int _value;

        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
                OnPropertyChanged(nameof(Text));
                OnPropertyChanged(nameof(BackgroundBrush));
                OnPropertyChanged(nameof(IsEmpty));
            }
        }

        public string Text => Value == 0 ? "" : Value.ToString();

        public bool IsEmpty => Value == 0;

        public Brush BackgroundBrush => GetColor(Value);

        private Brush GetColor(int val)
        {
            switch (val)
            {
                case 0: return Brushes.Transparent;
                case 2: return (Brush)new BrushConverter().ConvertFrom("#eee4da");
                case 4: return (Brush)new BrushConverter().ConvertFrom("#ede0c8");
                case 8: return (Brush)new BrushConverter().ConvertFrom("#f2b179");
                case 16: return (Brush)new BrushConverter().ConvertFrom("#f59563");
                case 32: return (Brush)new BrushConverter().ConvertFrom("#f67c5f");
                case 64: return (Brush)new BrushConverter().ConvertFrom("#f65e3b");
                case 128: return (Brush)new BrushConverter().ConvertFrom("#edcf72");
                case 256: return (Brush)new BrushConverter().ConvertFrom("#edcc61");
                case 512: return (Brush)new BrushConverter().ConvertFrom("#edc850");
                case 1024: return (Brush)new BrushConverter().ConvertFrom("#edc53f");
                case 2048: return (Brush)new BrushConverter().ConvertFrom("#edc22e");
                default: return Brushes.Black;
            }
        }

        public int Row { get; set; }
        public int Column { get; set; }
    }
}