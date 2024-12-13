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
    public class CardFileDataService
    {
        private readonly CardFileDataCollection _dataCollection = new CardFileDataCollection();

        public CardFileDataService()
        {
            MapperRegistrator.Register();
        }

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

        public void SaveToFile(string fileName)
        {
            var service = new FileDataService();
            service.SaveToFile(fileName, _dataCollection);
        }

        public void OpenFromFile(string fileName)
        {
            var service = new FileDataService();
            service.OpenFromFile(fileName, _dataCollection);
        }

        private Card FromDto(CardDto dto)
        {
            return Mapping.Mapper.Map<Card>(dto);
            //return new Card
            //{
            //    Id = dto.Id,
            //    FirstName = dto.FirstName,
            //    LastName = dto.LastName,
            //    MiddleName = dto.MiddleName,
            //    BirthDate = dto.BirthDate,
            //    Department = dto.Department,
            //    Position = dto.Position,
            //    SubordinatesCount = dto.SubordinatesCount,
            //    PaymentAmount = dto.PaymentAmount,
            //};
        }

        private CardDto ToDto(Card card)
        {
            return Mapping.Mapper.Map<CardDto>(card);
            //return new CardDto
            //{
            //    Id = card.Id,
            //    FirstName = card.FirstName,
            //    LastName = card.LastName,
            //    MiddleName = card.MiddleName,
            //    BirthDate = card.BirthDate,
            //    Department = card.Department,
            //    Position = card.Position,
            //    SubordinatesCount = card.SubordinatesCount,
            //    PaymentAmount = card.PaymentAmount,
            //};
        }
    }
}
