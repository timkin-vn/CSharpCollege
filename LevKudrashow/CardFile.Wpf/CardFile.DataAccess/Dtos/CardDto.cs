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
        public string Name { get; set; }
        public string Description { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string City { get; set; }
        public int MailIndex { get; set; }
        public double Rating { get; set; }
        public int CounterReviews { get; set; }
        public string Status { get; set; }

        public CardDto Clone()
        {
            return new CardDto
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Street = Street,
                House = House,
                City = City,
                MailIndex = MailIndex,
                Rating = Rating,
                CounterReviews = CounterReviews,
                Status = Status
            };
        }

        public void Update(CardDto from)
        {
            Name = from.Name;
            Description = from.Description;
            Street = from.Street;
            House = from.House;
            City = from.City;
            MailIndex = from.MailIndex;
            Rating = from.Rating;
            CounterReviews = from.CounterReviews;
            Status = from.Status;
        }
    }
}
