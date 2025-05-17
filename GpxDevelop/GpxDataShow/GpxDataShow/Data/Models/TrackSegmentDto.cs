using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GpxDataShow.Data.Models
{
    [Serializable]
    public class TrackSegmentDto
    {
        [XmlElement("trkpt")]
        public List<TrackPointDto> Points { get; set; }
    }
}
