using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataAccess.Dtos
{
    public class CardDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public decimal PaymentAmount { get; set; }

        public int ChildrenCount { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string House { get; set; }

        public CardDto Clone()
        {
            return new CardDto
            {
                Id = Id,
                FirstName = FirstName,
                MiddleName = MiddleName,
                LastName = LastName,
                BirthDate = BirthDate,
                PaymentAmount = PaymentAmount,
                ChildrenCount = ChildrenCount,
                City = City,
                Street = Street,
                House = House
            };
        }

        public void Update(CardDto newCard)
        {
            FirstName = newCard.FirstName;
            MiddleName = newCard.MiddleName;
            LastName = newCard.LastName;
            BirthDate = newCard.BirthDate;
            PaymentAmount = newCard.PaymentAmount;
            ChildrenCount = newCard.ChildrenCount;
            City = newCard.City;
            Street = newCard.Street;
            House = newCard.House;
        }
    }
}
