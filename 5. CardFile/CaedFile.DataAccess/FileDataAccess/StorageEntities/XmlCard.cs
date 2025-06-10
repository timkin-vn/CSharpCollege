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

        public string LaptopModel { get; set; }
        public decimal LaptopPrice { get; set; }
        public string VideoCard { get; set; }
        public string Processor { get; set; }
        public string Storage { get; set; }
        public string Ram { get; set; }
        public string Warranty { get; set; }
    }
}
