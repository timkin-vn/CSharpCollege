using CardFile.Business.Entities;
using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using System.Collections.Generic;
using System.Linq;

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
                Title = dto.Title,
                Author = dto.Author,
                PublicationDate = dto.PublicationDate,
                Genre = dto.Genre,
                PageCount = dto.PageCount,
                Price = dto.Price
            };
        }

        private CardDto ToDto(Card card)
        {
            return new CardDto
            {
                Id = card.Id,
                Title = card.Title,
                Author = card.Author,
                PublicationDate = card.PublicationDate,
                Genre = card.Genre,
                PageCount = card.PageCount,
                Price = card.Price
            };
        }
    }
}
