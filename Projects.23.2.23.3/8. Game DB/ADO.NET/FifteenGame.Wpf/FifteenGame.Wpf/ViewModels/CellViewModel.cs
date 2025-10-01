﻿using FifteenGame.Common.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel
    {
        public int Num { get; set; }

        public string Text => Num.ToString();

        public int Row { get; set; }

        public int Column { get; set; }

        public MoveDirection Direction { get; set; }
    }
}
