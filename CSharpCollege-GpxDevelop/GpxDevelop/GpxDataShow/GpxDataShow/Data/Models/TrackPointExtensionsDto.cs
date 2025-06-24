using System;
using System.Xml.Serialization;

namespace GpxDataShow.Data.Models
{
    [Serializable]
    public class TrackPointExtensionsDto
    {
        [XmlElement("speed")]
        public double? Speed { get; set; }
    }
}
