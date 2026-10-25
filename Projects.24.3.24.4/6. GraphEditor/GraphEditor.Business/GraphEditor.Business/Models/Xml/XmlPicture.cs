using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GraphEditor.Business.Models.Xml
{
    [Serializable]
    [XmlRoot("Picture")]
    public class XmlPicture
    {
        [XmlArray("FiguresList")]
        [XmlArrayItem("Rectangle", typeof(XmlRectangle))]
        [XmlArrayItem("Circle", typeof(XmlCircle))]
        [XmlArrayItem("Triangle", typeof(XmlTriangle))]
        public List<XmlFigure> Figures { get; set; }
    }
}