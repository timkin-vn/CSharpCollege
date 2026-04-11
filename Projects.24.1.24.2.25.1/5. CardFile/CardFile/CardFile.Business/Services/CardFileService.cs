using CardFile.Business.Infrastructure;
using CardFile.Business.Models;
using CardFile.Common.Infrastructure;
using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using CardFile.DataStore.FileDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Business.Services
{
    public class CardFileService
    {
        private readonly CardCollection _collection = new CardCollection();

        public CardFileService()
        {
            MapperInitialization.PreRegister();
        }

        public IEnumerable<Company> GetAll()
        {
            var cards = _collection.GetAll();
            return cards.Select(FromDto).ToList();
        }

        public int Save(Company card)
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
        
        private Company FromDto(CardDto card)
        {
            return Mapping.Mapper.Map<Company>(card);
            //return new Card
            //{
            //    Id = card.Id,
            //    FirstName = card.FirstName,
            //    MiddleName = card.MiddleName,
            //    LastName = card.LastName,
            //    BirthDate = card.BirthDate,
            //    Department = card.Department,
            //    Position = card.Position,
            //    EmploymentDate = card.EmploymentDate,
            //    DismissalDate = card.DismissalDate,
            //    Salary = card.Salary,
            //};
        }

        private CardDto ToDto(Company card)
        {
            return Mapping.Mapper.Map<CardDto>(card);
            //return new CardDto
            //{
            //    Id = card.Id,
            //    FirstName = card.FirstName,
            //    MiddleName = card.MiddleName,
            //    LastName = card.LastName,
            //    BirthDate = card.BirthDate,
            //    Department = card.Department,
            //    Position = card.Position,
            //    EmploymentDate = card.EmploymentDate,
            //    DismissalDate = card.DismissalDate,
            //    Salary = card.Salary,
            //};
        }
    }
}
