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
        ResizeR,
        ResizeL,
        ResizeT,
        ResizeB,
        ResizeTR,
        ResizeBR,
        ResizeTL,
        ResizeBL,
    }
}