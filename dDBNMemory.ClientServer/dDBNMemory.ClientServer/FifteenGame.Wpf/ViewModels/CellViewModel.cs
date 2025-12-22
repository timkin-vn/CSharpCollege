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
        
        private bool _isOpen; 
        public string _namecomponents;
        private bool _isMatched;

        public int Row { get; set; }

        public int Column { get; set; }

        public int[] ColumnRow { get; set; }

        public string NameСomponents
        {
            get => _namecomponents;
            set
            {
                _namecomponents = value;
                OnPropertyChanged(nameof(NameСomponents));
            }
        }

        // Показывать название овоща только если ячейка открыта или уже сопоставлена
        // Для закрытых ячеек показываем "Магазин"
        public string TextButoon => (_isOpen || _isMatched) ? NameСomponents : "Магазин";
        
        // Цвета в стиле овощей/магазина: 
        // - Зеленый для сопоставленных
        // - Светло-зеленый для выбранных
        // - Белый для закрытых
        public string BruhButon => _isMatched ? "#2ECC71" : (_isOpen ? "#A9DFBF" : "#FFFFFF");
        
        // Флаг, показывающий, открыта ли ячейка (выбрана пользователем)
        public bool IsOpen
        {
            get => _isOpen;
            set
            {
                _isOpen = value;
                OnPropertyChanged(nameof(IsOpen));
                OnPropertyChanged(nameof(ColorButtonPen)); 
                OnPropertyChanged(nameof(TextButoon));
                OnPropertyChanged(nameof(BruhButon));
                OnPropertyChanged(nameof(TextColor));
            }
        }
        
        // Флаг, показывающий, сопоставлена ли ячейка (найдена пара)
        public bool IsMatched
        {
            get => _isMatched;
            set
            {
                _isMatched = value;
                OnPropertyChanged(nameof(IsMatched));
                OnPropertyChanged(nameof(ColorButtonPen)); 
                OnPropertyChanged(nameof(TextButoon));
                OnPropertyChanged(nameof(BruhButon));
                OnPropertyChanged(nameof(TextColor));
            }
        }
        
        // Цвет границы: зеленый для открытых, темно-зеленый для сопоставленных, светло-серый для закрытых
        public string ColorButtonPen => _isOpen ? "#27AE60" : (_isMatched ? "#145A32" : "#D5D8DC");
        
        // Цвет текста: темно-зеленый для открытых/сопоставленных, черный для закрытых
        public string TextColor => (_isOpen || _isMatched) ? "#145A32" : "#000000";
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
