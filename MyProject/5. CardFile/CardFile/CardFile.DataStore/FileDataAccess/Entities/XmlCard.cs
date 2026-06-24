using System;
using System.Xml.Serialization;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class XmlCard
    {
        [XmlAttribute("Id")]
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
        public string BirthDateText
        {
            get => BirthDate.ToShortDateString();
            set => BirthDate = DateTime.Parse(value);
        }

        [XmlIgnore]
        public DateTime DeathDate { get; set; }

        [XmlAttribute("DeathDate")]
        public string DeathDateText
        {
            get => DeathDate.ToShortDateString();
            set => DeathDate = DateTime.Parse(value);
        }

        [XmlAttribute("CauseOfDeath")]
        public string CauseOfDeath { get; set; }

        [XmlAttribute("PlaceOfDeath")]
        public string PlaceOfDeath { get; set; }

        [XmlIgnore]
        public DateTime AdmissionDate { get; set; }

        [XmlAttribute("AdmissionDate")]
        public string AdmissionDateText
        {
            get => AdmissionDate.ToShortDateString();
            set => AdmissionDate = DateTime.Parse(value);
        }

        [XmlAttribute("SectionNumber")]
        public string SectionNumber { get; set; }

        [XmlAttribute("IsReleased")]
        public bool IsReleased { get; set; }
    }
}