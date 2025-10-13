using FifteenGame.Business.Models;
using System;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel
    {
        public int Num { get; set; }

        public string Text => Num == 0 ? "" : Num.ToString();

        public int Row { get; set; }

        public int Column { get; set; }
    }
}
