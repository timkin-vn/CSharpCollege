using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
