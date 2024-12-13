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
        public int Id { get; set; }

        public string Model { get; set; }

        public string Manufacturer { get; set; }

        [XmlElement("DatePurchase")]
        public long DatePurchaseTicks
        {
            get => DatePurchase.Ticks;
            set => DatePurchase = new DateTime(value);
        }

        [XmlIgnore]
        public DateTime DatePurchase { get; set; }

        public decimal Price { get; set; }

        public int Mileage { get; set; }
    }
}
