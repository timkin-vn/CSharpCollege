using System;

namespace LightsOut.Wpf.ViewModels
{
    public class CellViewModel
    {
        public bool IsOn { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }
    }
}