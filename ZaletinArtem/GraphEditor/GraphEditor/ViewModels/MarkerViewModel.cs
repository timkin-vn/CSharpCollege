using GraphEditor.Business.Models;
using System.Drawing;
using System.Windows.Forms;

namespace GraphEditor.ViewModels
{
    internal class MarkerViewModel
    {
        public Rectangle Rectangle { get; set; }

        public bool IsActive { get; set; }

        public PictureMode Mode { get; set; }

        public Cursor Cursor { get; set; }
    }
}
