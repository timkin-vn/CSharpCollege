using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GpxDataShow.Data.Models
{
    [Serializable]
    [XmlRoot("gpx")]
    public class GpxData
    {
        [XmlAttribute("version")]
        public string Version { get; set; }

        [XmlAttribute("creator")]
        public string Creator { get; set; }

        [XmlElement("trk")]
        public TrackDto Track { get; set; }
    }
}
