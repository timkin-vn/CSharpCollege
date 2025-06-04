using System;
using System.Collections.Generic;
using System.Linq;
using CardFile.DataAccess.Dtos;
using CardFile.DataAccess.Infrastructure;

namespace CardFile.DataAccess.DataCollection
{
    public class CardCollection
    {
        private List<CardDto> _cards = new List<CardDto>
        {
            new CardDto
            {
                Id = 1,
                ProductName = "Молоко",
                Category = "Молочные продукты",
                Manufacturer = "Молочный комбинат",
                ProductionDate = DateTime.Now.AddDays(-5),
                ShelfLifeDays = 14,
                Price = 89.90m,
                QuantityInStock = 50,
                ExpirationDate = DateTime.Now.AddDays(9)
            },
            new CardDto
            {
                Id = 2,
                ProductName = "Хлеб",
                Category = "Хлебобулочные изделия",
                Manufacturer = "Хлебозавод №1",
                ProductionDate = DateTime.Now.AddDays(-1),
                ShelfLifeDays = 5,
                Price = 45.50m,
                QuantityInStock = 120,
                ExpirationDate = DateTime.Now.AddDays(4)
            }
        };

        internal int CurrentId = 3;

        public CardCollection()
        {
            MapperRegistrator.Register();
        }

        public IEnumerable<CardDto> GetAll()
        {
            return _cards.Select(c => c.Clone()).ToList();
        }

        public int Save(CardDto cardDto)
        {
            if (cardDto.Id == 0)
            {
                var newCard = cardDto.Clone();
                var id = CurrentId++;
                newCard.Id = id;
                _cards.Add(newCard);
                return id;
            }

            var index = _cards.FindIndex(c => c.Id == cardDto.Id);
            if (index >= 0)
            {
                _cards[index] = cardDto.Clone();
                return cardDto.Id;
            }

            return -1;
        }

        public bool Delete(int cardId)
        {
            var index = _cards.FindIndex(c => c.Id == cardId);
            if (index < 0)
            {
                return false;
            }

            _cards.RemoveAt(index);
            return true;
        }

        internal void ReplaceAll(IEnumerable<CardDto> source)
        {
            _cards.Clear();
            _cards.AddRange(source);
            CurrentId = _cards.Max(c => c.Id) + 1;
        }
    }
}