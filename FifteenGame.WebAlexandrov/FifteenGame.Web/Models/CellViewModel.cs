using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using static FifteenGame.Business.Models.GameModel;

namespace FifteenGame.Web.Models
{
    public class CellViewModel : INotifyPropertyChanged
    {
        public GameModel Num { get; set; }

        public string Text => Num.ToString();

        public int Row { get; set; }

        public int Column { get; set; }

        public MoveDirection Direction { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsEmpty => string.IsNullOrEmpty(Text);
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


        public CellViewModel(int row, int column, GameModel num)
        {
            Row = row;
            Column = column;
            Num = num;
        }

        public UnitType Type { get; set; }



    }
}