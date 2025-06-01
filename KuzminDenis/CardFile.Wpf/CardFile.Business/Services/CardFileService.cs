using CardFile.Business.Infrastructure;
using CardFile.Business.Models;
using CardFile.Common.Infrastructure;
using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using CardFile.DataAccess.FileDataAccess;
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

        public CardFileService()
        {
            MapperRegistrator.Register();
        }

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

        public void SaveToFile(string fileName)
        {
            var fileDataService = new FileDataService();
            fileDataService.SaveToFile(fileName, _cardCollection);
        }

        public void OpenFromFile(string fileName)
        {
            var fileDataService = new FileDataService();
            fileDataService.OpenFromFile(fileName, _cardCollection);
        }

        private Card FromDto(CardDto dto)
        {
            return Mapping.Mapper.Map<Card>(dto);
        }

        private CardDto ToDto(Card card)
        {
            return Mapping.Mapper.Map<CardDto>(card);
        }
    }
}
