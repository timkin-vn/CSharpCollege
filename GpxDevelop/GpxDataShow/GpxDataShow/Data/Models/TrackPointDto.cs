using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GpxDataShow.Data.Models
{
    [Serializable]
    public class TrackPointDto
    {
        [XmlAttribute("lat")]
        public string Latitude { get; set; }

        [XmlAttribute("lon")]
        public string Longitude { get; set; }

        [XmlElement("ele")]
        public double? Elevation { get; set; }

        [XmlElement("time")]
        public string Time { get; set; }


        [XmlElement("hdop")]
        public double? HorizontalDispersion { get; set; }

        [XmlElement("fix")]
        public string Fix { get; set; }


        [XmlElement("sat")]
        public int? SatelliteCount { get; set; }

        [XmlElement("extensions")]
        public TrackPointExtensionsDto Extensions { get; set; }

        [XmlElement("cmt")]
        public string Comment { get; set; }
    }
}
