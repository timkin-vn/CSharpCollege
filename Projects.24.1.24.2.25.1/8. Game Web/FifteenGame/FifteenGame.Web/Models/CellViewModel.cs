using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FifteenGame.Web.Models
{
    public class CellViewModel
    {
        public int Value { get; set; }

        public string Text => Value == 1 ? "On" : "";

        public string CssClass => Value == 1 ? "light-cell light-cell-on" : "light-cell light-cell-off";
    }
}
