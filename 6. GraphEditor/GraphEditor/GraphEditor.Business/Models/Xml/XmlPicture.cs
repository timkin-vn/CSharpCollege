using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GraphEditor.Business.Models.Xml
{
    [Serializable]
    [XmlRoot("Picture")]
    public class XmlPicture
    {
        [XmlArray("Figures")]
        [XmlArrayItem("Figure")]
        public List<XmlFigure> Figures { get; set; }
    }
}