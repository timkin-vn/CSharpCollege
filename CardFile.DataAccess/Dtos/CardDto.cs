using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CardFile.DataAccess.Dtos
{
    public class CardDto
    {
        internal int Id;

        public string Name { get; set; }
        public string Type { get; set; }
        public string Manufacturer { get; set; }
        public string Country { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int ShopNumber { get; set; }
        public string SportType { get; set; }

        public CardDto Clone()
        {
            return new CardDto
            {
                Id = Id,
                Name = Name,
                Type = Type,
                Manufacturer = Manufacturer,
                Country = Country,
                Price = Price,
                StockQuantity = StockQuantity,
                ShopNumber = ShopNumber,
                SportType = SportType,
            };
        }
    }
}
