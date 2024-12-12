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
            return Mapping.Mapper.Map<CardDto>(this);
            //return new CardDto
            //{
            //    Id = Id,
            //    FirstName = FirstName,
            //    MiddleName = MiddleName,
            //    LastName = LastName,
            //    BirthDate = BirthDate,
            //    PaymentAmount = PaymentAmount,
            //    ChildrenCount = ChildrenCount,
            //};
        }

        public void Update(CardDto newCard)
        {
            Mapping.Mapper.Map(newCard, this);
            //FirstName = newCard.FirstName;
            //MiddleName = newCard.MiddleName;
            //LastName = newCard.LastName;
            //BirthDate = newCard.BirthDate;
            //PaymentAmount = newCard.PaymentAmount;
            //ChildrenCount = newCard.ChildrenCount;
        }
    }
}
