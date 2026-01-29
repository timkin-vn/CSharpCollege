using FifteenGame.Common.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel
    {
        public int Row { get; set; }

        public int Column { get; set; }

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