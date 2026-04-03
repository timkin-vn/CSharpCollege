using CardFile.Business.Models;
using CardFile.Common.Infrastructure;
using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace CardFile.Business.Services
{
    public class CardFileService
    {
        private readonly CardCollection _collection = new CardCollection();

        public CardFileService() { }

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

        private Card FromDto(CardDto card) => Mapping.Mapper.Map<Card>(card);
        private CardDto ToDto(Card card) => Mapping.Mapper.Map<CardDto>(card);
    }
}