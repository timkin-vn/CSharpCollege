using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LimerList.Business.Infrastructure;
using LimerList.Business.Models;
using LimerList.Common.Infrastructure;
using LimerList.DataStore.DataCollection;
using LimerList.DataStore.Dtos;
using LimerList.DataStore.FileAccess;

namespace LimerList.Business.Services
{
    public class LimerItemService
    {
        readonly LimerCollection _collection = new LimerCollection();
        public LimerItemService()
        {
            MapperInitialization.PreRegister();
        }
        public int Save(LimerItem limer)
        {
            return _collection.Save(ToDto(limer));
        }
        public bool Delete(int limerId)
        {
            return _collection.Delete(limerId);
        }
        public IEnumerable<LimerItem> GetAll()
        {
            var l = _collection.GetAll();
            return l.Select(FromDto).ToList();
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
        private LimerItem FromDto(LimerDto item)
        {
            return Mapping.Mapper.Map<LimerItem>(item);
        }
        private LimerDto ToDto(LimerItem item)
        {
            return Mapping.Mapper.Map<LimerDto>(item);
        }
    }
}
