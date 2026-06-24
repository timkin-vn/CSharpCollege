using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        private bool _hasShop;
        private bool _isCovered;

        public int Row { get; set; }
        public int Column { get; set; }
        public int PeopleCount { get; set; }

        public string Text => HasShop ? "🏪" : $"{PeopleCount} чел";

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

        public bool IsCovered
        {
            get => _isCovered;
            set
            {
                _isCovered = value;
                OnPropertyChanged(nameof(IsCovered));
                OnPropertyChanged(nameof(CellBackground));
            }
        }

        // Меняем цвет клетки в зависимости от статуса
        public string CellBackground
        {
            get
            {
                if (HasShop) return "Gold";
                if (IsCovered) return "LightGreen";
                return "LightCoral";
            }
        }
    }
}
