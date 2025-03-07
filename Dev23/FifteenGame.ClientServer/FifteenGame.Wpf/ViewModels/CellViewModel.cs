﻿using FifteenGame.Common.BusinessModels;
using System.ComponentModel;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel : INotifyPropertyChanged
    {
        public int Num { get; set; }

        public string Text => Num.ToString();

        public int Row { get; set; }

        public int Column { get; set; }

        public MoveDirection Direction { get; set; }

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
