using FifteenGame.Common.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        private int _peopleCount;
        private bool _hasShop;
        private bool _isVeggie;
        private bool _isRevealed;

        public int Row { get; set; }
        public int Column { get; set; }

        public int PeopleCount
        {
            get => _peopleCount;
            set
            {
                _peopleCount = value;
                OnPropertyChanged(nameof(PeopleCount)); // Передаем текст кнопки
                OnPropertyChanged(nameof(Text)); // Обновляем текст кнопки
            }
        }

        public bool HasShop
        {
            get => _hasShop;
            set
            {
                _hasShop = value;
                OnPropertyChanged(nameof(HasShop));
                OnPropertyChanged(nameof(Text));
            }
        }

        public bool IsVeggie
        {
            get => _isVeggie;
            set
            {
                _isVeggie = value;
                OnPropertyChanged(nameof(IsVeggie));
                OnPropertyChanged(nameof(Text));
            }
        }

        public bool IsRevealed
        {
            get => _isRevealed;
            set
            {
                _isRevealed = value;
                OnPropertyChanged(nameof(IsRevealed));
                OnPropertyChanged(nameof(Text));
            }
        }

        // Текст для кнопок на карте
        public string Text
        {
            get
            {
                if (!IsRevealed)
                    return "🌫️\n Скрыто";

                if (HasShop)
                    return $"🌯 Лок: ({Row},{Column})\nКлиенты: {PeopleCount}\n[ ЛАРЁК ]";

                string zoneType = IsVeggie ? "🌱 ЗОЖ" : "🥩 Обычная";
                return $"📍 Лок: ({Row},{Column})\nЧел: {PeopleCount}\n{zoneType}";
            }
        }

        // Заглушки для старого кода, чтобы ничего не падало
        public int Value { get; set; }
        public MoveDirection Direction { get; set; }
    }
}
