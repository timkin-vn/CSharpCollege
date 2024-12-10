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

        [XmlElement("FirstName")]
        public string FirstName { get; set; }

        [XmlElement("LastName")]
        public string LastName { get; set; }

        [XmlElement("MiddleName")]
        public string MiddleName { get; set; }

        [XmlElement("BirthDate")]
        public long BirthDateTicks
        {
            get => BirthDate.Ticks;
            set => BirthDate = new DateTime(value);
        }

        [XmlIgnore]
        public DateTime BirthDate { get; set; }

        [XmlElement("Department")]
        public string Department { get; set; }

        [XmlElement("Position")]
        public string Position { get; set; }

        [XmlElement("SubordinatesCount")]
        public int SubordinatesCount { get; set; }

        [XmlElement("PaymentAmount")]
        public decimal PaymentAmount { get; set; }
    }
}
