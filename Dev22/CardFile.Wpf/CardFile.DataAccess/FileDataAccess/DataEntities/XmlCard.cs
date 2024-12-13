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
        public string Title { get; set; }

       

        [XmlElement("DateRealease")]
        public long realesehDateTicks
        {
            get => Daterealese.Ticks;
            set => Daterealese = new DateTime(value);
        }

        [XmlIgnore]
        public DateTime Daterealese { get; set; }

        [XmlElement("Director")]
        public string Director { get; set; }

        [XmlElement("FilmRegue")]
        public string Regue { get; set; }

        [XmlElement("Price")]
        public int Price { get; set; }

        [XmlElement("count")]
        public decimal COunt { get; set; }
    }
}
