using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.Entites
{
    [Serializable]
    public class XmlCard
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }

        
        [XmlAttribute("DishName")]
        public string DishName { get; set; }
        
        [XmlAttribute("Category")]
        public string Category { get; set; }

        [XmlAttribute("Description")]
        public string Description { get; set; }

        [XmlAttribute("PortionSize")]
        public int PortionSize { get; set; }

        [XmlAttribute("Price")]
        public double Price { get; set; }
        
    }
}
