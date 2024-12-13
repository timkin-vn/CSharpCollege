using CardFile.Common.Infrastructure;
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

        public string Model { get; set; }

        public string Manufacturer { get; set; }

        public DateTime DatePurchase { get; set; }

        public decimal Price { get; set; }

        public int Mileage { get; set; }

        public CardDto Clone()
        {
            return Mapping.Mapper.Map<CardDto>(this);
            //return new CardDto
            //{
            //    Id = Id,
            //    Model = Model,
            //    Manufacturer = Manufacturer,
            //    DatePurchase = DatePurchase,
            //    Price = Price,
            //    Mileage = Mileage,
            //};
        }

        public void Update(CardDto newCard)
        {
            Mapping.Mapper.Map(newCard, this);
            //Model = newCard.Model;
            //Manufacturer = newCard.Manufacturer;
            //Manufacturer = newCard.Manufacturer;
            //Price = newCard.Price;
            //Mileage = newCard.Mileage;
        }
    }
}
