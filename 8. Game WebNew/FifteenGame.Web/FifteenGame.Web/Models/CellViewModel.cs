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
        public int GemType { get; set; }
        public bool IsSelected { get; set; }
        public bool IsMatched { get; set; }

        public bool IsEmpty => GemType == GameField.EmptyCell;

        public string CssClass
        {
            get
            {
                var classes = "gem";

                if (IsSelected)
                    classes += " selected";

                if (IsMatched)
                    classes += " matched";

                return classes;
            }
        }

        public string GemColor
        {
            get
            {
                if (IsEmpty)
                    return "transparent";

                switch (GemType)
                {
                    case 0: return "#FF0000";
                    case 1: return "#0000FF";
                    case 2: return "#00FF00";
                    case 3: return "#FFFF00";
                    case 4: return "#800080";
                    case 5: return "#FFA500";
                    default: return "#808080";
                }
            }
        }
    }
}