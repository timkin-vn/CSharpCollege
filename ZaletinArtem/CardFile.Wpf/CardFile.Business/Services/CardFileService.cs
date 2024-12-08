using CardFile.Business.Entities;
using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace CardFile.Business.Services
{
    public class CardFileService
    {
        private readonly CardFileDataCollection _dataService = new CardFileDataCollection();

        public Card Get(int id)
        {
            var dto = _dataService.Get(id);
            return FromDto(dto);
        }

        public IEnumerable<Card> GetAll()
        {
            var dtoList = _dataService.GetAll();
            return dtoList.Select(FromDto).ToList();
        }

        public Card Save(Card card)
        {
            var id = _dataService.Save(ToDto(card));
            if (id > 0)
            {
                return FromDto(_dataService.Get(id));
            }

            return null;
        }

        public Card Update(Card card)
        {
            if (_dataService.Update(ToDto(card)))
            {
                return FromDto(_dataService.Get(card.Id));
            }

            return null;
        }

        public bool Delete(int id)
        {
            return _dataService.Delete(id);
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
                Price = dto.Price,
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
                Price = card.Price,
            };
        }
    }
}
