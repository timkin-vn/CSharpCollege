using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LimerList.DataStore.FileAccess.Entities
{
    public class XmlLimer
    {
        [XmlAttribute("ID")]
        public int Id { get; set; }
        [XmlAttribute("FirstName")]
        public string FirstName { get; set; }
        [XmlAttribute("MiddleName")]
        public string MiddleName { get; set; }
        [XmlAttribute("LastName")]
        public string LastName { get; set; }
        [XmlIgnore]
        public DateTime BirthDate { get; set; }
        [XmlAttribute("BirthDate")]
        public string BirthDateString => BirthDate.ToShortDateString();

        [XmlAttribute("Group")]
        public string Group { get; set; }
    }
}
