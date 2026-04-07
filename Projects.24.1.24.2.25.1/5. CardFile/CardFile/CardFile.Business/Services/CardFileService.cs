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
    public class CarFileService
    {
        private readonly CardCollection _collection = new CardCollection();

        public CarFileService()
        {
            MapperInitialization.PreRegister();
        }

        public IEnumerable<Car> GetAll()
        {
            var items = _collection.GetAll();
            return items.Select(FromDto).ToList();
        }

        public int Save(Car car)
        {
            return _collection.Save(ToDto(car));
        }

        public bool Delete(int carId)
        {
            return _collection.Delete(carId);
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

        private Car FromDto(CardDto dto)
        {
            return Mapping.Mapper.Map<Car>(dto);
        }

        private CardDto ToDto(Car car)
        {
            return Mapping.Mapper.Map<CardDto>(car);
        }
    }
}