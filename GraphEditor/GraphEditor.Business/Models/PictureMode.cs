using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.Business.Models
{
    public enum PictureMode
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
