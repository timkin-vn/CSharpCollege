using FifteenGame.Business.Models;
using System;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel
    {
        public char State { get; set; }         // Состояние клетки (' ' - вода, 'S' - корабль, 'H' - попадание, 'M' - промах)
        public string Text { get; private set; } // Текст для отображения
        public string Color { get; private set; } // Цвет для отображения
        public int Row { get; set; }
        public int Column { get; set; }
        public MoveDirection Direction { get; set; } // Не используется

        public CellViewModel()
        {
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            if (State == 'H')
            {
                Text = "X";
                Color = "Red";
            }
            else if (State == 'M')
            {
                Text = "O";
                Color = "Gray";
            }
            else
            {
                Text = "";
                Color = "LightBlue";
            }
        }
    }
}