using LightsOutGame.Common.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOutGame.Wpf.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        public int Row { get; set; }

        public int Column { get; set; }

        public bool IsOn { get; set; }
    }
}
