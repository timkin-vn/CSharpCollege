using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel
    {
        public int Value { get; set; }
        public string Text => Value == 0 ? "" : Value.ToString();

        public int Row { get; set; }
        public int Column { get; set; }
    }
}
