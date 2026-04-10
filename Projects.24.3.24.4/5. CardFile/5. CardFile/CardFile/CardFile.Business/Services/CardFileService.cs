using CardFile.Business.Infrastructure;
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

        public CardFileService()
        {
            MapperInitialization.PreRegister();
        }

        public IEnumerable<Card> GetAll()
        {
            EnsureMappingInitialized();
            var cardDtos = _collection.GetAll();
            return cardDtos.Select(FromDto).ToList();
        }

        public int Save(Card card)
        {
            EnsureMappingInitialized();
            return _collection.Save(ToDto(card));
        }

        public string GetCurrentFilePath()
        {
            return _collection.CurrentFilePath;
        }

        public void LoadFromFile(string filePath)
        {
            _collection.LoadFromFile(filePath);
        }

        public void SaveToFile(string filePath)
        {
            _collection.SaveToFile(filePath);
        }

        public bool Delete(int cardId)
        {
            return _collection.Delete(cardId);
        }

        private void EnsureMappingInitialized()
        {
            if (Mapping.Mapper == null)
            {
                Mapping.Initialize();
            }
        }

        private Card FromDto(CardDto card)
        {
            return Mapping.Mapper.Map<Card>(card);
        }

        private CardDto ToDto(Card card)
        {
            return Mapping.Mapper.Map<CardDto>(card);
        }
    }
}
