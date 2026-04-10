using System;
using System.Xml.Serialization;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class XmlCard
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }

        [XmlAttribute("Title")]
        public string Title { get; set; }

        [XmlAttribute("Author")]
        public string Author { get; set; }

        [XmlAttribute("Genre")]
        public string Genre { get; set; }

        [XmlAttribute("Year")]
        public int Year { get; set; }

        [XmlAttribute("Copies")]
        public int Copies { get; set; }

        [XmlIgnore]
        public DateTime AddedDate { get; set; }

        [XmlAttribute("AddedDate")]
        public string AddedDateText
        {
            get => AddedDate.ToShortDateString();
            set => AddedDate = DateTime.Parse(value);
        }

        [XmlIgnore]
        public DateTime? DeletedDate { get; set; }

        [XmlAttribute("DeletedDate")]
        public string DeletedDateText
        {
            get => DeletedDate?.ToShortDateString() ?? "-";
            set => DeletedDate = (value == "-") ? (DateTime?)null : DateTime.Parse(value);
        }
    }
}