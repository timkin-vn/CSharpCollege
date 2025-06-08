using System;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.Entites
{
    [Serializable]
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
        public DateTime ArrivalDate { get; set; }

        [XmlAttribute("ArrivalDate")]
        public string ArrivalDateXml
        {
            get => ArrivalDate.ToShortDateString();
            set => ArrivalDate = DateTime.Parse(value);
        }

        [XmlAttribute("RepairReason")]
        public string RepairReason { get; set; }

        [XmlIgnore]
        public DateTime? CompletionDate { get; set; }

        [XmlAttribute("CompletionDate")]
        public string CompletionDateXml
        {
            get => CompletionDate.HasValue ? CompletionDate.Value.ToShortDateString() : "-";
            set => CompletionDate = (value == "-") ? (DateTime?)null : DateTime.Parse(value);
        }

        [XmlAttribute("RepairCost")]
        public decimal RepairCost { get; set; }
    }
}