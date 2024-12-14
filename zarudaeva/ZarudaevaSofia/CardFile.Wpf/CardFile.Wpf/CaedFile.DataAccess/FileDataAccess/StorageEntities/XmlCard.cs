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

        public string MovieName { get; set; }

        public string MovieType { get; set; }

        [XmlElement("DateReles")]
        public long DateRelesTicks
        {
            get => DateReles.Ticks;
            set => DateReles = new DateTime(value);
        }

        [XmlIgnore]


        public DateTime DateReles { get; set; } 
        [XmlElement("TimeGoes")]
        public long TimeSpanTicks
        {
            get => TimeGoes.Ticks;
            set => TimeGoes = new TimeSpan(value);
        }

        [XmlIgnore]


        public TimeSpan TimeGoes { get; set; }

        public decimal PaymentAmount { get; set; }

        public int ChildrenCount { get; set; }
    }
}
