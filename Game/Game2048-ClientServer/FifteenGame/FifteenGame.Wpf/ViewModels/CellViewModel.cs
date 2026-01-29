using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FifteenGame.Common.Definitions;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel : INotifyPropertyChanged
    {
        private int _num;
        private string _displayValue;

        public int Num
        {
            get => _num;
            set
            {
                if (_num != value)
                {
                    _num = value;
                    OnPropertyChanged(nameof(Num));
                    OnPropertyChanged(nameof(BackgroundColor));
                    OnPropertyChanged(nameof(TextColor));
                    
                    // Обновляем DisplayValue при изменении Num
                    DisplayValue = value == Constants.FreeCellValue ? "" : value.ToString();
                }
            }
        }

        public string DisplayValue
        {
            get => _displayValue;
            set
            {
                if (_displayValue != value)
                {
                    _displayValue = value;
                    OnPropertyChanged(nameof(DisplayValue));
                }
            }
        }

        public int Row { get; set; }

        public int Column { get; set; }

        public string BackgroundColor
        {
            get
            {
                switch (Num)
                {
                    case -1: return "#CDC1B4";  // Пустая ячейка
                    case 0: return "#CDC1B4";   // Для совместимости
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

        public string TextColor
        {
            get
            {
                return Num <= 4 ? "#776E65" : "#F9F6F2";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
