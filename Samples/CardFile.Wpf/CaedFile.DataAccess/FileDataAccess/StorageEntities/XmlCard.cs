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

        public string Title { get; set; }
        


        [XmlElement("EXP")]
        public long EXPTicks
        {
            get => EXP.Ticks;
            set => EXP = new DateTime(value);
        }

        [XmlIgnore]
        public DateTime EXP { get; set; }

        public string Fabricator { get; set; }

        public string Section { get; set; }

        public int Count { get; set; }
        
        public decimal Price { get; set; }
    }
}
