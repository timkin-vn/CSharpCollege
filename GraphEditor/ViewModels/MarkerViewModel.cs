using GraphEditor.Business.Models;
using System.Drawing;
using System.Windows.Forms;
using GraphEditor.Business.Models;

namespace GraphEditor.ViewModels {
    internal class MarkerViewModel {
        public static int MarkerHalfSize = 3;

        public Rectangle Rectangle { get; set; }

        public bool IsActive { get; set; }

        public EditMode EditMode { get; set; }

        public Cursor Cursor { get; set; }
    }
}