using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FifteenGames.Common.BusinessModels;

namespace FifteenGames.Wpf.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        public int Value { get; set; }
        public string Text => Value.ToString();

        public int Row { get; set; }
        public int Column { get; set; }

        public MoveDirection Direction { get; set; }
    }
}
