using CardFile.Business.Models;
using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
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

        public IEnumerable<Card> GetAll()
        {
            return _collection.GetAll().Select(FromDto).ToList();
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
            return new Card
            {
                Id = card.Id,
                FirstName = card.FirstName,
                LastName = card.LastName,
                MiddleName = card.MiddleName,
                BirthDate = card.BirthDate,
                Department = card.Department,
                Position = card.Position,
                EmploymentDate = card.EmploymentDate,
                DismissalDate = card.DismissalDate,
                Salary = card.Salary,
            };
        }

        private CardDto ToDto(Card card)
        {
            return new CardDto
            {
                Id = card.Id,
                FirstName = card.FirstName,
                LastName = card.LastName,
                MiddleName = card.MiddleName,
                BirthDate = card.BirthDate,
                Department = card.Department,
                Position = card.Position,
                EmploymentDate = card.EmploymentDate,
                DismissalDate = card.DismissalDate,
                Salary = card.Salary,
            };
        }
    }
}
