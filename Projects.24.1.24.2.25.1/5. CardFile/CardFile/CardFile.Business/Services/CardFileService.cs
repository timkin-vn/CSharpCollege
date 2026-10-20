using CardFile.Business.Models;
using CardFile.Common.Infrastructure;
using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using CardFile.DataStore.FileDataAccess;
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

        public void SaveToFile(string fileName)
        {
            var fileDataService = new FileDataService();
            fileDataService.SaveToFile(fileName, _collection);
        }

        public void OpenFromFile(string fileName)
        {
            var fileDataService = new FileDataService();
            fileDataService.OpenFromFile(fileName, _collection);
        }

        private Card FromDto(CardDto cardDto)
        {
            if (cardDto == null) return null;
            return Mapping.Mapper.Map<Card>(cardDto);
        }

        private CardDto ToDto(Card card)
        {
            if (card == null) return null;
            return Mapping.Mapper.Map<CardDto>(card);
        }
    }
}
