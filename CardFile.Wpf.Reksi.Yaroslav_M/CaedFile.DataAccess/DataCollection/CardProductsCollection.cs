using CardFile.DataAccess.Dtos;
using CardFile.DataAccess.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataAccess.DataCollection
{     enum  _typeProduct { Fruits, Vegetables, Dairy, Canned_Food, }  
    public class CardProductsCollection
    {
        private List<CardProductsDto> _cards = new List<CardProductsDto>
        {
            new CardProductsDto
            {
                Id = 1,
                NameProducts = "Яблоки" ,
                TypeProducts = _typeProduct.Fruits.ToString(),
                DateManufacture = new DateTime(2024,11,9),
                DateExpiration = new DateTime(2024,11,15),

                CountProducts = 1000,
                PriceOneProducts = 50,
                SectionProducts = "A",
                ShirtProducts = "3",
            },
            new CardProductsDto
            {
                Id = 2,
                NameProducts = "Оргурецы" ,
                TypeProducts = _typeProduct.Vegetables.ToString(),
                DateManufacture = new DateTime(2024,11,9),
                DateExpiration = new DateTime(2024,11,13),

                CountProducts = 4000,
                PriceOneProducts = 100,
                SectionProducts = "B",
                ShirtProducts = "1",
            },
            new CardProductsDto
            {
                Id = 3,
                NameProducts = "Рыбные консервы" ,
                TypeProducts = _typeProduct.Canned_Food.ToString(),
                DateManufacture = new DateTime(2023,11,9),
                DateExpiration = new DateTime(2024,9,13),

                CountProducts = 3000,
                PriceOneProducts = 125,
                SectionProducts = "Р",
                ShirtProducts = "4",
            },
            new CardProductsDto
            {
                Id = 4,
                NameProducts = "Сыры" ,
                TypeProducts = _typeProduct.Dairy.ToString(),
                DateManufacture = new DateTime(2024,11,9),
                DateExpiration = new DateTime(2024,11,16),

                CountProducts = 5000,
                PriceOneProducts = 145,
                SectionProducts = "М",
                ShirtProducts = "3",
            },
        };
        internal int CurrentId = 5;

        public CardProductsCollection()
        {
            MapperInitialize.Initialize();
        }

        public IEnumerable<CardProductsDto> GetAll()
        {
            return _cards.Select(c => c.Clone());
        }

        public CardProductsDto Get(int id)
        {
            return _cards.FirstOrDefault(c => c.Id == id)?.Clone();
        }

        public bool Update(CardProductsDto card)
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

        public int Save(CardProductsDto card)
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

        internal void ReplaceCollection(IEnumerable<CardProductsDto> collection)
        {
            _cards.Clear();
            _cards.AddRange(collection);
            CurrentId = _cards.Max(c => c.Id) + 1;
        }

        internal void ReplaceCollection(IEnumerable<CardProductsDto> collection, int currentId)
        {
            _cards.Clear();
            _cards.AddRange(collection);
            CurrentId = currentId;
        }
    }
}

