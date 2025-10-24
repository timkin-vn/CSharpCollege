using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _2048Game.Web.Models
{
    public class TileViewModel
    {
        public int Value { get; set; }

        public string CssClass
        {
            get
            {
                if (Value == 0) return "tile-empty";
                else return $"tile-{Value}";
            }
        }
    }
}