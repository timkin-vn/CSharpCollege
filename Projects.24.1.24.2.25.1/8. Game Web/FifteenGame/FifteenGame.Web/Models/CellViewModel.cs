using FifteenGame.Business.Models;
using System;

namespace FifteenGame.Web.Models
{
    public class CellViewModel
    {
        public int Digit { get; set; }

        public string DisplayValue
        {
            get { return Digit.ToString(); }
        }

        public MoveDirection Direction { get; set; }

        public string DirectionText
        {
            get { return Convert.ToString(Direction); }
        }

        public bool IsVacant { get; set; }
    }
}