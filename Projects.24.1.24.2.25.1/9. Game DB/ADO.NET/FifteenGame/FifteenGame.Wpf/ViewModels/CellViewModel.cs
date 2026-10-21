using System.Windows.Media;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        private int _value;

        private int _row;
        public int Row
        {
            get { return _row; }
            set
            {
                _row = value;
                OnPropertyChanged(nameof(Row));
            }
        }

        private int _column;
        public int Column
        {
            get { return _column; }
            set
            {
                _column = value;
                OnPropertyChanged(nameof(Column));
            }
        }

        public int Value
        {
            get { return _value; }
            set
            {
                if (_value == value)
                    return;

                _value = value;
                OnPropertyChanged(nameof(Value));
                OnPropertyChanged(nameof(Text));
                OnPropertyChanged(nameof(Background));
                OnPropertyChanged(nameof(Foreground));
            }
        }

        public string Text
        {
            get { return Value == 0 ? string.Empty : Value.ToString(); }
        }

        public Brush Background
        {
            get
            {
                switch (Value)
                {
                    case 0: return CreateBrush("#FF292929");
                    case 2: return CreateBrush("#FFEEE4DA");
                    case 4: return CreateBrush("#FFEDE0C8");
                    case 8: return CreateBrush("#FFF2B179");
                    case 16: return CreateBrush("#FFF59563");
                    case 32: return CreateBrush("#FFF67C5F");
                    case 64: return CreateBrush("#FFF65E3B");
                    case 128: return CreateBrush("#FFEDCF72");
                    case 256: return CreateBrush("#FFEDCC61");
                    case 512: return CreateBrush("#FFEDC850");
                    case 1024: return CreateBrush("#FFEDC53F");
                    case 2048: return CreateBrush("#FFEDC22E");
                    default: return CreateBrush("#FF3C3A32");
                }
            }
        }

        public Brush Foreground
        {
            get
            {
                return Value <= 4
                    ? CreateBrush("#FF776E65")
                    : CreateBrush("#FFF9F6F2");
            }
        }

        private static Brush CreateBrush(string color)
        {
            return (Brush)new BrushConverter().ConvertFrom(color);
        }
    }
}