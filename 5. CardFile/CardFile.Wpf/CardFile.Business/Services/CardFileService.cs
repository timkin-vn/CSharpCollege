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
        private CardCollection _cardCollection = new CardCollection();

        public IEnumerable<Card> GetAll()
        {
            var dtos = _cardCollection.GetAll();
            return dtos.Select(FromDto).ToList();
        }

        public int Save(Card card)
        {
            return _cardCollection.Save(ToDto(card));
        }

        public bool Delete(int cardId)
        {
            return _cardCollection.Delete(cardId);
        }

        private Card FromDto(CardDto dto)
        {
            return new Card
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MiddleName = dto.MiddleName,
                BirthDate = dto.BirthDate,
                Department = dto.Department,
                Position = dto.Position,
                EmploymentDate = dto.EmploymentDate,
                DismissalDate = dto.DismissalDate,
                Salary = dto.Salary,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                IsVerified = dto.IsVerified,

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
                Email = card.Email,
                PhoneNumber = card.PhoneNumber,
                Address = card.Address,
                IsVerified = card.IsVerified,

            };
        }
    }
}
