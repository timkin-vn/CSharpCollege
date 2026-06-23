using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataAccess.Dtos
{
    public class CardDto
    {
        public int Id { get; set; }

        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Vin { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime? SaleDate { get; set; }

        public CardDto Clone()
        {
            return new CardDto
            {
                Id = Id,
                Brand = Brand,
                Model = Model,
                Year = Year,
                Vin = Vin,
                Color = Color,
                Price = Price,
                ArrivalDate = ArrivalDate,
                SaleDate = SaleDate
            };
        }
    }
}
