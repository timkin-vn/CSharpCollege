using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel : INotifyPropertyChanged
    {
        
        private bool _isSelected; 
        public string _colorText;
        

        public int Row { get; set; }

        public int Column { get; set; }

        public int[] ColumnRow { get; set; }



        private string _colorButton = "MediumOrchid"; // Устанавливаем базовый цвет

        public string ColorButton
        {
            get => _colorButton;
            set
            {
                _colorButton = value;
                OnPropertyChanged(nameof(ColorButton));
            }
        }  
        public string ColorText
        {
            get => _colorText;
            set
            {
                _colorText = value;
                OnPropertyChanged(nameof(ColorText));
            }
        }

        public bool IsFaceUp
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsFaceUp));
                // Изменяем цвет кнопки при выделении
                OnPropertyChanged(nameof(ColorButtonPen));

            }
        }
        public string _сolorButtonPen = "MediumOrchid"; // Начальный цвет границы

        public string ColorButtonPen => IsFaceUp ? ColorText : _сolorButtonPen;
        

        // Метод для проверки совпадения

        public event PropertyChangedEventHandler PropertyChanged;


        

        public void MoveRight()
        {
            if (Column >= 3)
            {
                return;
            }

            Column++;
            OnPropertyChanged(nameof(Column));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
