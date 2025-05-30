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
        private readonly CardCollection _collection = new CardCollection();

        public CardFileService()
        {
            InitializeMapper.Register();
        }

        public IEnumerable<Card> GetAll()
        {
            return _collection.GetAll().Select(FromDto).ToList();
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

        private Card FromDto(CardDto card)
        {
            return Mapping.Mapper.Map<Card>(card);
            //return new Card
            //{
            //    Id = card.Id,
            //    FirstName = card.FirstName,
            //    LastName = card.LastName,
            //    MiddleName = card.MiddleName,
            //    BirthDate = card.BirthDate,
            //    Department = card.Department,
            //    Position = card.Position,
            //    EmploymentDate = card.EmploymentDate,
            //    DismissalDate = card.DismissalDate,
            //    Salary = card.Salary,
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
            //    EmploymentDate = card.EmploymentDate,
            //    DismissalDate = card.DismissalDate,
            //    Salary = card.Salary,
            //};
        }
    }
}
