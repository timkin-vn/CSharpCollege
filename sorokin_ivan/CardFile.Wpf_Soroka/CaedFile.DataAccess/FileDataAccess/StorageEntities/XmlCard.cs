using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.StorageEntities
{
    [Serializable]
    public class XmlCard
    {
        [XmlAttribute("Id")]

        [XmlElement("DateManufacture")]
        public long DateManufactureTicks
        {
            get => DateManufacture.Ticks;
            set => DateManufacture = new DateTime(value);
        }

        [XmlIgnore]
        public DateTime DateManufacture { get; set; }
        [XmlElement("DateExpiration")]
        public long DateExpirationTicks
        {
            get => DateExpiration.Ticks;
            set => DateExpiration = new DateTime(value);
        }

        [XmlIgnore]
        public DateTime DateExpiration { get; set; }

        public int CountProducts { get; set; }

        public decimal PriceOneProducts { get; set; }

        public string SectionProducts { get; set; }

        public string ShirtProducts { get; set; }
    }
}
