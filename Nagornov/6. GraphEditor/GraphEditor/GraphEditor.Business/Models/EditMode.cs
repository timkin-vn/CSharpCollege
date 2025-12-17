using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.Business.Models
{
    public enum EditMode
    {
        None,
        Creating,
        Moving,
        ResizeT,
        ResizeB,
        ResizeL,
        ResizeR,
        ResizeTL,
        ResizeTR,
        ResizeBL,
        ResizeBR,
        CreatingCircle
    }
}
