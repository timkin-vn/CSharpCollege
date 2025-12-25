using Game2048.Common.Models;
using System.ComponentModel;

namespace Game2048.Wpf.ViewModels
{
    public class CellViewModel : INotifyPropertyChanged
    {
        private int _value;
        private string _text;
        private string _color;

        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
                Text = value == 0 ? "" : value.ToString();
                Color = GetTileColor(value);
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        public string Color
        {
            get { return _color; }
            set
            {
                _color = value;
                OnPropertyChanged(nameof(Color));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string GetTileColor(int value)
        {
            switch (value)
            {
                case 0: return "#CDC1B4";
                case 2: return "#EEE4DA";
                case 4: return "#EDE0C8";
                case 8: return "#F2B179";
                case 16: return "#F59563";
                case 32: return "#F67C5F";
                case 64: return "#F65E3B";
                case 128: return "#EDCF72";
                case 256: return "#EDCC61";
                case 512: return "#EDC850";
                case 1024: return "#EDC53F";
                case 2048: return "#EDC22E";
                default: return "#3C3A32";
            }
        }
    }
}
