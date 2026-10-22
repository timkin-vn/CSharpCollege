using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GraphEditor.Business.Models.Xml
{
    [Serializable]
    public class XmlColor
    {
        [XmlAttribute(nameof(Red))]
        public int Red { get; set; }
        [XmlAttribute(nameof(Green))]
        public int Green { get; set; }
        [XmlAttribute(nameof(Blue))]
        public int Blue { get; set; }
    }
}
