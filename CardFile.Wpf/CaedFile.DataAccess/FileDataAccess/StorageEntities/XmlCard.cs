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
        public string Bank_name { get; set; }
        public int ATM_count { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string City { get; set; }
        public decimal Money_count { get; set; }
        public int Money_limit { get; set; }
        public short[] Card_number { get; set; }
    }
}
