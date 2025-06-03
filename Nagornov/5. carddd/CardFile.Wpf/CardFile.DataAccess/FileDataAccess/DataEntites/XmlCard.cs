using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.DataEntites
{
    [Serializable]
    public class XmlCard
    {
        
        [XmlAttribute("Id")]
        public int Id { get; set; }

       
        [XmlAttribute("Name")]
        public string Name { get; set; }

        
        [XmlAttribute("Type")]
        public string Type { get; set; }

        
        [XmlAttribute("Manufacturer")]
        public string LastName { get; set; }

 
        [XmlAttribute("strana")]
        public string strana { get; set; }

        
        [XmlAttribute("lavka")]
        public int lavka { get; set; }

        public int StockQuantity { get; set; }

        public decimal price { get; set; }
    }
}
