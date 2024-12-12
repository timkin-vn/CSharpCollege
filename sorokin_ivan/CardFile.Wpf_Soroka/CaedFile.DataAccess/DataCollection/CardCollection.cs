﻿using CardFile.DataAccess.Dtos;
using CardFile.DataAccess.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataAccess.DataCollection
{
    public class CardCollection
    {
        private List<CardDto> _cards = new List<CardDto>
        {
            new CardDto
            {
                Id = 1,
                NameMedication = "Анльгин",
                TypeMedication = "Таблетки ",
                DateManufacture = new DateTime(2024, 11, 15),
                DateExpiration = new DateTime(2024,12,17)   ,
                PriceOneMedication = 100m   ,
                CountMedication=22
              
            },
            new CardDto
            {
               Id = 2,
                NameMedication = "Кодрекс",
                TypeMedication = "Пакетики ",
                DateManufacture = new DateTime(2024, 11, 11),
                DateExpiration = new DateTime(2024,12,14)   ,
                PriceOneMedication = 150m   ,
                CountMedication=42
            },
            new CardDto
            {
                Id = 3,
                NameMedication = "Нурафен-Дейский ",
                TypeMedication = "Сиропы ",
                DateManufacture = new DateTime(2024, 11, 14),
                DateExpiration = new DateTime(2024,1,15)   ,
                PriceOneMedication = 250m   ,
                CountMedication=12
            },
            new CardDto
            {
                Id = 4,
                NameMedication = "Визин",
                TypeMedication = "Капли ",
                DateManufacture = new DateTime(2024, 11, 9),
                DateExpiration = new DateTime(2024,11,15)   ,
                PriceOneMedication = 1000m   ,
                CountMedication=25
            },
        };

        internal int CurrentId = 5;

        public CardCollection()
        {
            MapperInitialize.Initialize();
        }

        public IEnumerable<CardDto> GetAll()
        {
            return _cards.Select(c => c.Clone());
        }

        public CardDto Get(int id)
        {
            return _cards.FirstOrDefault(c => c.Id == id)?.Clone();
        }

        public bool Update(CardDto card)
        {
            if (card.Id == 0)
            {
                return false;
            }

            var existingCard = _cards.FirstOrDefault(c => c.Id == card.Id);
            if (existingCard == null)
            {
                return false;
            }

            existingCard.Update(card);
            return true;
        }

        public int Save(CardDto card)
        {
            if (card.Id == 0)
            {
                var newCard = card.Clone();
                newCard.Id = CurrentId++;
                _cards.Add(newCard);
                return newCard.Id;
            }

            var existingCard = _cards.FirstOrDefault(c => c.Id == card.Id);
            if (existingCard == null)
            {
                return -1;
            }

            existingCard.Update(card);
            return card.Id;
        }

        public bool Delete(int id)
        {
            var existingCard = _cards.FirstOrDefault(c => c.Id == id);

            if (existingCard == null)
            {
                return false;
            }

            _cards.Remove(existingCard);
            return true;
        }

        internal void ReplaceCollection(IEnumerable<CardDto> collection)
        {
            _cards.Clear();
            _cards.AddRange(collection);
            CurrentId = _cards.Max(c => c.Id) + 1;
        }

        internal void ReplaceCollection(IEnumerable<CardDto> collection, int currentId)
        {
            _cards.Clear();
            _cards.AddRange(collection);
            CurrentId = currentId;
        }
    }
}