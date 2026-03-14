using FifteenGame.Common.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel
    {
        public CellType Type { get; set; }

        public string DisplayText
        {
            get
            {
                switch (Type)
                {
                    case CellType.Water: return "💧";
                    case CellType.LilyPad: return "🌿";
                    case CellType.Algae: return "🌱";
                    case CellType.Frog: return "🐸";
                    case CellType.Home: return "🏠";
                    default: return "";
                }
            }
        }

        public bool IsClickable => Type == CellType.Algae || Type == CellType.LilyPad || Type == CellType.Water;
        public int Row { get; set; }
        public int Column { get; set; }

        public string ToolTip
        {
            get
            {
                switch (Type)
                {
                    case CellType.Water: return "Вода - можно переместить сюда кувшинку";
                    case CellType.LilyPad: return "Кувшинка - можно выбрать для перемещения";
                    case CellType.Algae: return "Водоросли - можно убрать";
                    case CellType.Frog: return "Лягушка";
                    case CellType.Home: return "Домик лягушки";
                    default: return "";
                }
            }
        }
    }
}