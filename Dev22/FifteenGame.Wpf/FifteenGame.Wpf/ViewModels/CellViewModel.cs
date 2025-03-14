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
        public string _animalText; 
        

        public int Row { get; set; }

        public int Column { get; set; }

        public int[] ColumnRow { get; set; }




        public string AnimalText
        {
            get => _animalText;
            set
            {
                _animalText = value;
                OnPropertyChanged(nameof(AnimalText));
            }
        }
        public  string TextButoon => IsFaceUp ? AnimalText : "Programming\nLanguage";
        public string BruhButon => IsFaceUp ? "BlanchedAlmond" : "Tomato";
        public bool IsFaceUp
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsFaceUp));
                // Изменяем цвет кнопки при выделении
                OnPropertyChanged(nameof(ColorButtonPen)); 
                OnPropertyChanged(nameof(TextButoon));
                OnPropertyChanged(nameof(BruhButon));

            }
        }
        public string _сolorButtonPen = "DarkRed"; // Начальный цвет границы

        public string ColorButtonPen => IsFaceUp ? "Olive" : _сolorButtonPen;
        

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
