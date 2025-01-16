﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RectangleEditor.Models
{
    internal class Marker
    {
        public Rectangle Rectangle { get; set; }

        public bool IsActive { get; set; }

        public RectangleMode Mode { get; set; }

        public Cursor Cursor { get; set; }
    }
}