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

        public bool IsEmpty => GemType == 0;

        public string ImageName
        {
            get
            {
                switch (GemType)
                {
                    case 1:
                        return "crystal_green";
                    case 2:
                        return "crystal_red";
                    case 3:
                        return "crystal_violet";
                    case 4:
                        return "crystal_yellow";
                    case 5:
                        return "crystal_blue";
                    default:
                        return null;
                }
            }
        }

    }
}