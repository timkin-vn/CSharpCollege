using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GraphEditor.Business.Models.Xml
{
    [Serializable]
    [XmlRoot("Picture")]
    public class XmlPicture
    {
        [XmlArray("Figures")]
        [XmlArrayItem("Figure")]
        public List<XmlRectangle> Rectangles { get; set; }
    }
}
