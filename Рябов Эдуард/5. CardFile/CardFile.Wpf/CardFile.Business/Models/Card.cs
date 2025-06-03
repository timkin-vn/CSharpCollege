using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Business.Models
{
    public class Card
    {
       
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Manufacturer { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public string NameBay { get; set; }

        public long PhoneNumber { get; set; }

        public int QuantitySell { get; set; }

    }
}
