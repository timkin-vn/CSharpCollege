using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class XmlCard
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("LoreName")]
        public string LoreName { get; set; }

        [XmlAttribute("PrimaryAttribute")]
        public string PrimaryAttribute { get; set; }

        [XmlAttribute("AttackType")]
        public string AttackType { get; set; }

        [XmlAttribute("Role")]
        public string Role { get; set; }

        [XmlIgnore]
        public DateTime ReleaseDate { get; set; }

        [XmlAttribute("ReleaseDate")]
        public string ReleaseDateText
        {
            get => ReleaseDate.ToShortDateString();
            set => ReleaseDate = DateTime.Parse(value);
        }

        [XmlIgnore]
        public DateTime? ReworkDate { get; set; }

        [XmlAttribute("ReworkDate")]
        public string ReworkDateText
        {
            get => ReworkDate?.ToShortDateString() ?? "-";
            set => ReworkDate = (value == "-") ? (DateTime?)null : DateTime.Parse(value);
        }

        [XmlAttribute("WinRate")]
        public decimal WinRate { get; set; }
    }
}
