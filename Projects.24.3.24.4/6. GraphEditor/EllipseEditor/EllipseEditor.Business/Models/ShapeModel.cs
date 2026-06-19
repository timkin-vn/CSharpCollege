using System;
using System.Drawing;
using System.Xml.Serialization;

namespace EllipseEditor.Business.Models
{

    [Serializable]
    [XmlInclude(typeof(EllipseModel))]
    public abstract class ShapeModel
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int FillArgb { get; set; }

        protected ShapeModel()
        {
            FillArgb = Color.CornflowerBlue.ToArgb();
        }

        [XmlIgnore]
        public Color FillColor
        {
            get { return Color.FromArgb(FillArgb); }
            set { FillArgb = value.ToArgb(); }
        }

        [XmlIgnore]
        public Rectangle Bounds
        {
            get { return new Rectangle(X, Y, Width, Height); }
        }

        public abstract bool Contains(int px, int py);

        public abstract ShapeModel Clone();
    }
}
