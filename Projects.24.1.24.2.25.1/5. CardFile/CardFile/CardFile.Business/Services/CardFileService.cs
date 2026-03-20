using CardFile.Business.Infrastructure;
using CardFile.Business.Models;
using CardFile.Common.Infrastructure;
using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Business.Services
{
    public class CardFileService
    {
        private readonly CardCollection _collection = new CardCollection();

        public CardFileService()
        {
            MapperInitialization.PreRegister();
        }

        public IEnumerable<Card> GetAll()
        {
            var cards = _collection.GetAll();
            return cards.Select(FromDto).ToList();
        }

        public int Save(Card card)
        {
            return _collection.Save(ToDto(card));
        }

        public bool Delete(int cardId)
        {
            return _collection.Delete(cardId);
        }

        private Card FromDto(CardDto card)
        {
            return Mapping.Mapper.Map<Card>(card);
            //return new Card
            //{
            //    Id = card.Id,
            //    FirstName = card.FirstName,
            //    MiddleName = card.MiddleName,
            //    LastName = card.LastName,
            //    BirthDate = card.BirthDate,
            //    Department = card.Department,
            //    Position = card.Position,
            //    EmploymentDate = card.EmploymentDate,
            //    DismissalDate = card.DismissalDate,
            //    Salary = card.Salary,
            //};
        }

        private CardDto ToDto(Card card)
        {
            return Mapping.Mapper.Map<CardDto>(card);
            //return new CardDto
            //{
            //    Id = card.Id,
            //    FirstName = card.FirstName,
            //    MiddleName = card.MiddleName,
            //    LastName = card.LastName,
            //    BirthDate = card.BirthDate,
            //    Department = card.Department,
            //    Position = card.Position,
            //    EmploymentDate = card.EmploymentDate,
            //    DismissalDate = card.DismissalDate,
            //    Salary = card.Salary,
            //};
        }
    }
}
