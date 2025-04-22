using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel : INotifyPropertyChanged
    {
        public string _colorText;
        public bool _isMakeUp;
        public bool _istwoPar;

        public int Row { get; set; }

        public int Column { get; set; }

        public int[] ColumnRow { get; set; }
        public bool IsMakeUp
        {
            get => _isMakeUp;
            set
            {
                _isMakeUp = value;
                OnPropertyChanged(nameof(TextButton));
                OnPropertyChanged(nameof(IsMakeUp));
            }
        }
        public bool IstwoPar
        { get => _istwoPar;set {
                _istwoPar = value;
                OnPropertyChanged(nameof(TextButton));
                OnPropertyChanged(nameof(IstwoPar));
            } }


        public string ColorText
        {
            get => _colorText;
            set
            {
                _colorText = value;
                OnPropertyChanged(nameof(ColorText));
                
            }
        }
        
        public string TextButton => IsMakeUp || IstwoPar ? ColorText : "Овощ";

        // Метод для проверки совпадения

        public event PropertyChangedEventHandler PropertyChanged;


        



        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
