using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game2048.WEB.Models
{
    public class CellViewModel
    {
        public int Value { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public string ShowValue
        {
            get
            {
                return $"cell value-{Value}";
            }
        }
    }
}