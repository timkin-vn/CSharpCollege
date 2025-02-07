using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FifteenGame.Web.Models
{
    public class CellViewModel
    {
        public int Num { get; set; }

        public string NumText => Num.ToString();

        public MoveDirection Direction { get; set; }

        public string DirectionText => Direction.ToString();

        public bool IsEmpty { get; set; }
    }
}