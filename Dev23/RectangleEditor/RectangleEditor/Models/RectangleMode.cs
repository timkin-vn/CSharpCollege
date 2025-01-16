using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RectangleEditor.Models
{
    internal enum RectangleMode
    {
        None,
        Creating,
        Moving,
        ResizeTL,
        ResizeT,
        ResizeTR,
        ResizeR,
        ResizeBR,
        ResizeB,
        ResizeBL,
        ResizeL,
    }
}
