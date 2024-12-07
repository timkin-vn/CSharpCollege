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
    public class CardProductsFileService
    {
        private readonly CardProductsCollection _dataService = new CardProductsCollection();

        public CardProductsFileService()
        {
            MapperInitialize.Initialize();
        }

        public CardProducts Get(int id)
        {
            var dto = _dataService.Get(id);
            return FromDto(dto);
        }

        public IEnumerable<CardProducts> GetAll()
        {
            var dtoList = _dataService.GetAll();
            return dtoList.Select(FromDto).ToList();
        }

        public CardProducts Save(CardProducts card)
        {
            var id = _dataService.Save(ToDto(card));
            if (id > 0)
            {
                return FromDto(_dataService.Get(id));
            }

            return null;
        }

        public CardProducts Update(CardProducts card)
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

        private CardProducts FromDto(CardProductsDto dto)
        {
            return Mapping.Mapper.Map<CardProducts>(dto);
           /* {*/
               /* Id = dto.Id,

                NameProducts = dto.NameProducts,

                TypeProducts = dto.TypeProducts,

                DateManufacture = dto.DateManufacture,

                DateExpiration = dto.DateExpiration,


                CountProducts = dto.CountProducts,

                PriceOneProducts = dto.PriceOneProducts,

                SectionProducts = dto.SectionProducts,

                ShirtProducts = dto.ShirtProducts,*/

            /*};*/
        }

        private CardProductsDto ToDto(CardProducts card)
        {
           
            return Mapping.Mapper.Map<CardProductsDto>(card);
            /*{
                Id = card.Id,

                NameProducts = card.NameProducts,

                TypeProducts = card.TypeProducts,

                DateManufacture = card.DateManufacture,

                DateExpiration = card.DateExpiration,

                CountProducts = card.CountProducts,

                PriceOneProducts = card.PriceOneProducts,

                SectionProducts = card.SectionProducts,

                ShirtProducts = card.ShirtProducts,

            }*/;
        }
    }
}
