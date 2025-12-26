using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RectangleEditor.Models
{
    internal enum EditMode
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
    }
}
