using CardFile.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataAccess.Dtos
{
    public class CardProductsDto
    {
        public int Id { get; set; }

        public string NameProducts { get; set; }

        public string TypeProducts { get; set; }

        public DateTime DateManufacture { get; set; }

        public DateTime DateExpiration { get; set; }

        public int CountProducts { get; set; }

        public decimal PriceOneProducts { get; set; }

        public string SectionProducts { get; set; }

        public string ShirtProducts { get; set; }

        public CardProductsDto Clone()
        {
            return Mapping.Mapper.Map<CardProductsDto>(this);
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

        public void Update(CardProductsDto newCard)
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
