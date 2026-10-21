using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        public int Value { get; set; }

        public string Color { get; set; }

        public bool IsOpened { get; set; }

        public bool IsFound { get; set; }

        public string BackgroundColor => IsOpened || IsFound ? Color : "LightGray";

        public string Text => "";

        public int Row { get; set; }

        public int Column { get; set; }

        public int Index { get; set; }

        public CellModel Direction { get; set; }
    }
}
