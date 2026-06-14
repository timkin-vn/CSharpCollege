using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FifteenGame.Web.Models
{
    public class CellViewModel
    {
        public int Value { get; set; }

        public string Text => Value.ToString();

        public MoveDirection Direction { get; set; }

        public string DirectionText => Direction.ToString();

        public bool IsEmpty { get; set; }
    }
}