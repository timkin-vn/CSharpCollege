using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FifteenGame.Web.Models
{
    public class CellViewModel
    {
        public char Letter { get; set; }

        public string LetterText => Letter.ToString();

        public bool IsSelected { get; set; }
    }
}