using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataAccess.Dtos
{
    public class CardDto
    {
        // DTO = Data Transfer Object
        // id, название, когда привезли, срок годности, кол-во на складе, рейтинг товара
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DeliverDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int Count { get; set; }

        public double Rating { get; set; }


        public CardDto Clone()
        {
            return new CardDto
            {
                Id = Id,
                Name = Name,
                DeliverDate = DeliverDate,
                ExpirationDate = ExpirationDate,
                Count = Count,
                Rating = Rating
            };
        }
    }
}
