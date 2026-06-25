using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FifteenGame.Web.Models
{
    public class CellViewModel
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int PeopleCount { get; set; }
        public bool HasShop { get; set; }
        public bool IsCovered { get; set; }
        public bool IsVeggie { get; set; }
        public bool IsRevealed { get; set; }
        public int VeggieNeighborsCount { get; set; }

        public string Text
        {
            get
            {
                if (HasShop) return "🏪";

                if (IsRevealed)
                {
                    if (IsVeggie) return $"{PeopleCount} чел\n(ЗОЖ)";

                    if (VeggieNeighborsCount > 0)
                    {
                        string radars = new string('з', VeggieNeighborsCount);
                        return $"{PeopleCount} чел\n{radars}";
                    }
                }

                return $"{PeopleCount} чел";
            }
        }

        public string CellBackground
        {
            get
            {
                if (HasShop) return IsVeggie ? "DarkOrchid" : "Gold";
                if (IsRevealed && IsVeggie) return "LightGray";
                if (IsCovered) return "LightGreen";
                return "LightCoral";
            }
        }
    }
}