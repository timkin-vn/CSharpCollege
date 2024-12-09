using CardFile.Business.Entities;
using CardFile.Business.Infrastructure;
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
        private readonly CardCollection _dataService = new CardCollection();

        public CardFileService()
        {
            MapperInitialize.Initialize();
        }

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

        public void SaveToFile(string fileName)
        {
            var fileDataService = new FileDataService();
            fileDataService.SaveToFile(fileName, _dataService);
        }

        public void OpenFile(string fileName)
        {
            var fileDataService = new FileDataService();
            fileDataService.OpenFile(fileName, _dataService);
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
