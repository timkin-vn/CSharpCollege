using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FifteenGame.Web.Models
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

        public bool IsLilyPad => Type == CellType.LilyPad;

        public bool IsAlgae => Type == CellType.Algae;

        public bool IsWater => Type == CellType.Water;

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

        public string CssClass
        {
            get
            {
                switch (Type)
                {
                    case CellType.Water: return "btn btn-primary";
                    case CellType.LilyPad: return "btn btn-info";
                    case CellType.Algae: return "btn btn-success";
                    case CellType.Frog: return "btn btn-warning";
                    case CellType.Home: return "btn btn-danger";
                    default: return "btn btn-secondary";
                }
            }
        }
    }
}