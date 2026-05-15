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

        // Показывать название бренда автомобиля только если ячейка открыта или уже сопоставлена
        // Для закрытых ячеек показываем "?"
        public string TextButoon => (_isOpen || _isMatched) ? NameСomponents : "?";
        
        // Цвета в стиле игрового интерфейса: 
        // - Металлический синий для сопоставленных
        // - Светло-синий для выбранных
        // - Серый металлик для закрытых
        public string BruhButon => _isMatched ? "#3498DB" : (_isOpen ? "#AED6F1" : "#7F8C8D");
        
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
        
        // Цвет границы: синий для открытых, темно-синий для сопоставленных, серебристый для закрытых
        public string ColorButtonPen => _isOpen ? "#2980B9" : (_isMatched ? "#1A5276" : "#BDC3C7");
        
        // Цвет текста: белый для всех состояний для лучшей читаемости на цветном фоне
        public string TextColor => "#FFFFFF";
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
