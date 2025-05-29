
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
        
        private bool _isConnect; 
        public string _namecomponents;
        

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
        public  string TextButoon => IsConenkt ? NameСomponents : "Не дорого";
        public string BruhButon => IsConenkt ? "DarkGoldenrod" : "Crimson";
        public bool IsConenkt
        {
            get => _isConnect;
            set
            {
                _isConnect = value;
                OnPropertyChanged(nameof(IsConenkt));
                
                OnPropertyChanged(nameof(ColorButtonPen)); 
                OnPropertyChanged(nameof(TextButoon));
                OnPropertyChanged(nameof(BruhButon));

            }
        }
        public string _сolorButtonPen = "Plum"; // Начальный цвет границы

        public string ColorButtonPen => IsConenkt ? "BlueViolet" : _сolorButtonPen;
        

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
