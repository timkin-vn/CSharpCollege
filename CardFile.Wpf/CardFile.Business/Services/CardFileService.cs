﻿using CardFile.Business.Entities;
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

        private Card FromDto(CardDto dto)
        {
            return new Card
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                PaymentAmount = dto.PaymentAmount,
                ChildrenCount = dto.ChildrenCount,
                City = dto.City,
                Street = dto.Street,
                House = dto.House,
            };
        }

        private CardDto ToDto(Card card)
        {
            return new CardDto
            {
                Id = card.Id,
                FirstName = card.FirstName,
                MiddleName = card.MiddleName,
                LastName = card.LastName,
                BirthDate = card.BirthDate,
                PaymentAmount = card.PaymentAmount,
                ChildrenCount = card.ChildrenCount,
                City = card.City,
                Street = card.Street,
                House = card.House,
            };
        }
    }
}
