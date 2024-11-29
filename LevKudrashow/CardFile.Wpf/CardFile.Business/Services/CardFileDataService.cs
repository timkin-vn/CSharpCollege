using CardFile.Business.Entities;
using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Business.Services
{
    public class CardFileDataService
    {
        private readonly CardFileDataCollection _dataCollection = new CardFileDataCollection();

        public IEnumerable<Card> GetAll()
        {
            return _dataCollection.GetAll().Select(FromDto).ToList();
        }

        public Card Get(int id)
        {
            return FromDto(_dataCollection.Get(id));
        }

        public Card Save(Card card)
        {
            var id = _dataCollection.Save(ToDto(card));
            if (id > 0)
            {
                card.Id = id;
                return card;
            }

            return null;
        }

        public bool Delete(int id)
        {
            return _dataCollection.Delete(id);
        }

        private Card FromDto(CardDto dto)
        {
            return new Card
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Street = dto.Street,
                House = dto.House,
                City = dto.City,
                MailIndex = dto.MailIndex,
                Rating = dto.Rating,
                CounterReviews = dto.CounterReviews,
                Status = dto.Status,
            };
        }

        private CardDto ToDto(Card card)
        {
            return new CardDto
            {
                Id = card.Id,
                Name = card.Name,
                Description = card.Description,
                Street = card.Street,
                House = card.House,
                City = card.City,
                MailIndex = card.MailIndex,
                Rating = card.Rating,
                CounterReviews = card.CounterReviews,
                Status = card.Status,
            };
        }
    }
}
