using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.DataEntites
{
    internal class JsonCard
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Manufacturer { get; set; }

        public int lavka { get; set; }

        public string strana { get; set; }

        public int StockQuantity { get; set; }

        public decimal price { get; set; }
    }
}
