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

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public decimal PaymentAmount { get; set; }

        public int Sud { get; set; }

        public string ArticleNumber { get; set; }

        public string ArticleName { get; set; }

        public string IssuedArticle { get; set; }

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
            //    Sud = Sud,
            //    ArticleNumber = ArticleNumber,
            //    IssuedArticle = IssuedArticle,
            //    ArticleName = ArticleName,
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
            //Sud = newCard.Sud;
            //ArticleNumber = newCard.ArticleNumber;
            //IssuedArticle = newCard.IssuedArticle;
            //ArticleName = newCard.ArticleName;
        }
    }
}
