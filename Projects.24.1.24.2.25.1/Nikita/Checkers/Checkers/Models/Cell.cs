using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Checkers.Business.Models;

namespace Checkers.Models
{
    public class Cell : INotifyPropertyChanged
    {
        private Checker _checker;
        private bool _isSelected;
        private bool _isValidMove;

        public int Row { get; set; }
        public int Col { get; set; }
        public bool IsDark { get; set; }

        public Checker Checker
        {
            get => _checker;
            set { _checker = value; OnPropertyChanged(); }
        }

        public bool IsSelected
        {
            get => _isSelected;
            set { _isSelected = value; OnPropertyChanged(); }
        }

        public bool IsValidMove
        {
            get => _isValidMove;
            set { _isValidMove = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
