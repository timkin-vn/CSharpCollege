using System;
using System.Xml.Serialization;

namespace GraphEditor.Business.Models.File
{
    [Serializable]
    public class XmlColorModel
    {
        [XmlAttribute("Red")]
        public int Red { get; set; }

        [XmlAttribute("Green")]
        public int Green { get; set; }

        [XmlAttribute("Blue")]
        public int Blue { get; set; }
    }
}
