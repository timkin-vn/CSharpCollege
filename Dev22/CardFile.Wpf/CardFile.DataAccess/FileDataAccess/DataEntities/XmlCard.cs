using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.DataEntities
{
    [Serializable]
    public class XmlCard
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }

        [XmlElement("Title")]
        public string FirstName { get; set; }

        [XmlElement("EXP")]
        public long EXPTicks
        {
            get => EXP.Ticks;
            set => EXP = new DateTime(value);
        }

        [XmlIgnore]
        public DateTime EXP { get; set; }

        [XmlElement("Fabricator")]
        public string Fabricator { get; set; }

        [XmlElement("Section")]
        public string Section { get; set; }

        [XmlElement("Count")]
        public int Count { get; set; }

        [XmlElement("Price")]
        public decimal Price { get; set; }
    }
}
