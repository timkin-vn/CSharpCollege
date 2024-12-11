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

        public string Title { get; set; }
        

        public DateTime EXP { get; set; }

        public string Fabricator { get; set; }

        public string Section { get; set; }

        public int Count { get; set; }
        
        public decimal Price { get; set; }

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
