using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FifteenGame.Web.Models
{
    public class CellViewModel
    {
       
            public bool IsRevealed { get; set; }

            public bool IsFlagged { get; set; }

            public string ImagePath { get; set; }

            public bool IsMine { get; set; }

            public int Num { get; set; }


        
    }
}