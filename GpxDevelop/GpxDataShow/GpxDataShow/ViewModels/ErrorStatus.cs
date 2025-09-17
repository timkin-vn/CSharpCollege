using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpxDataShow.ViewModels
{
    [Flags]
    internal enum ErrorStatus
    {
        None = 0,
        Missed1Second = 1,
        MissedManySeconds = 2,
        SpeedTooDifferent = 4,
    }
}
