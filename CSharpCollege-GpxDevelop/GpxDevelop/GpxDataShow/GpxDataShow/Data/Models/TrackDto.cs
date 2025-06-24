using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GpxDataShow.Data.Models
{
    [Serializable]
    public class TrackDto
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("trkseg")]
        public List<TrackSegmentDto> Segments { get; set; }
    }
}
