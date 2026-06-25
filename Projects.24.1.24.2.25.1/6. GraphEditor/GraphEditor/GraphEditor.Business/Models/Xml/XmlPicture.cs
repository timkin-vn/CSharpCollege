using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GraphEditor.Business.Models.Xml
{
    [Serializable]
    [XmlRoot("Picture")]
    public class XmlPicture
    {
        [XmlArray("Rectangles")]
        [XmlArrayItem("Rectangle")]
        public List<XmlRectangle> Rectangles { get; set; }

        [XmlArray("Triangles")]
        [XmlArrayItem("Triangle")]
        public List<XmlTriangle> Triangles { get; set; }

        [XmlArray("Circles")]
        [XmlArrayItem("Circle")]
        public List<XmlCircle> Circles { get; set; }

        [XmlArray("HilbertCurves")]
        [XmlArrayItem("HilbertCurve")]
        public List<XmlHilbertCurve> HilbertCurves { get; set; }
    }
}