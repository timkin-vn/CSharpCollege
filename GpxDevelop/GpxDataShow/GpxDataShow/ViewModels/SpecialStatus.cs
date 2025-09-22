using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpxDataShow.ViewModels
{
    [Flags]
    public enum SpecialStatus
    {
        None = 0,
        MinLatitude = 1,
        MaxLatitude = 2,
        Step0_1Latitude = 4,
    }
}
