using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Match3GameWeb.Models
{
    public class CellViewModel
    {
        public int Value { get; set; }   // тип камня
        public string Image => $"{Value}.png"; // картинка
        public bool IsEmpty => Value == 0;
    }
}