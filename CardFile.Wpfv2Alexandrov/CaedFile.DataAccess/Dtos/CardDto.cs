
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardFile.Common.Infrastructure;

namespace CardFile.DataAccess.Dtos
{
    public class CardDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public decimal Credit { get; set; }

        public DateTime DateOfEndCredit { get; set; }

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
