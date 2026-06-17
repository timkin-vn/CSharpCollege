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
        // Новый атрибут для прозрачности (255 — полностью непрозрачный)
        [XmlAttribute("Alpha")]
        public byte Alpha { get; set; } = 255;

        [XmlAttribute("Red")]
        public byte Red { get; set; }

        [XmlAttribute("Green")]
        public byte Green { get; set; }

        [XmlAttribute("Blue")]
        public byte Blue { get; set; }
    }
}

