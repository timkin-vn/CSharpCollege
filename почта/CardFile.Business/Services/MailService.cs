using CardFile.Business.Models;
using CardFile.Common.Infrastructure;
using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using CardFile.DataStore.FileDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardFile.Business.Services
{
    public class MailService
    {
        private readonly LetterCollection _collection = new LetterCollection();
        public MailService() => Business.Infrastructure.MapperInitialization.PreRegister();

        public IEnumerable<Letter> GetAll() => _collection.GetAll().Select(FromDto).ToList();
        public int Save(Letter letter) => _collection.Save(ToDto(letter));
        public bool Delete(int letterId) => _collection.Delete(letterId);

        public void SaveToFile(string fileName)
        {
            new FileDataService().SaveToFile(fileName, _collection);
        }
        public void OpenFromFile(string fileName)
        {
            new FileDataService().OpenFromFile(fileName, _collection);
        }

        private Letter FromDto(LetterDto dto) => Mapping.Mapper.Map<Letter>(dto);
        private LetterDto ToDto(Letter model) => Mapping.Mapper.Map<LetterDto>(model);
    }
}