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

        public string Text
        {
            get { return Value == 0 ? "" : Value.ToString(); }
        }

        public bool IsEmpty
        {
            get { return Value == 0; }
        }
    }
}