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
        public string _carText;
        public bool _isMakeUp;
       

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
       


        public string CarText
        {
            get => _carText;
            set
            {
                _carText = value;
                OnPropertyChanged(nameof(CarText));
                
            }
        }
        
        public string TextButton => IsMakeUp ? CarText : "Secret";

        // Метод для проверки совпадения

        public event PropertyChangedEventHandler PropertyChanged;


        



        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
