using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CardFile.Business.Infrastructure;
using CardFile.Business.Models;
using CardFile.Common.Infrastructure;
using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using CardFile.DataStore.FileDataAccess;

namespace CardFile.Business.Services
{
    public class CardFileService
    {
        private readonly CardCollection _collection = new CardCollection();
        public CardFileService()
        {
            MapperInitialization.Preregister();
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
        public void SaveToFile(string filename)
        {
            var fileDataSevice = new FileDataService();
            fileDataSevice.SaveToFile(filename, _collection);
        }
        public void OpenFromFile(string fileName)
        {
            var fileDataService = new FileDataService();
            fileDataService.OpenFromFile(fileName, _collection);
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
