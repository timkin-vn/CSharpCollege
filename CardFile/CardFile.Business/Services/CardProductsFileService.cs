﻿using CardFile.Business.Entities;
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
            //return new Card
            //{
            //    Id = dto.Id,
            //    FirstName = dto.FirstName,
            //    MiddleName = dto.MiddleName,
            //    LastName = dto.LastName,
            //    BirthDate = dto.BirthDate,
            //    PaymentAmount = dto.PaymentAmount,
            //    ChildrenCount = dto.ChildrenCount,
            //};
        }

        private CardProductsDto ToDto(CardProducts card)
        {
            return Mapping.Mapper.Map<CardProductsDto>(card);
            //return new CardDto
            //{
            //    Id = card.Id,
            //    FirstName = card.FirstName,
            //    MiddleName = card.MiddleName,
            //    LastName = card.LastName,
            //    BirthDate = card.BirthDate,
            //    PaymentAmount = card.PaymentAmount,
            //    ChildrenCount = card.ChildrenCount,
            //};
        }
    }
}
