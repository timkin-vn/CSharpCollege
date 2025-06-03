using CardFile.DataAccess.Dtos;
using CardFile.DataAccess.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CardFile.DataAccess.DataCollection
{
    public class CardCollection
    {
        private readonly List<CardDto> _cards = new List<CardDto>();

        internal int NextId = 5;

        public CardCollection()
        {
            _cards.Add(new CardDto
            {
                Id = 1,
                Name = "Brabus 850",
                Type = "Тюн авто",
                Manufacturer = "Mercedes",
                strana = "Германия",
                Price = 20100100,
                StockQuantity = 2,
                lavka = 12,
                
            });
            _cards.Add(new CardDto
            {
                Id = 2,
                Name = "Ferrari stallone",
                Type = "Спорт-кар",
                Manufacturer = "Ferrari",
                strana = "Италия",
                Price = 45100100,
                StockQuantity = 12,
                lavka = 7,
            });
            _cards.Add(new CardDto
            {
                Id = 3,
                Name = "Maserati MC20",
                Type = "Спорт-кар",
                Manufacturer = "Maserati",
                strana = "Италия",
                Price = 70100100,
                StockQuantity = 1,
                lavka = 8,
            });
            _cards.Add(new CardDto
            {
                Id = 4,
                Name = "Dodge Charger",
                Type = "Масл-кар",
                Manufacturer = "Dodge",
                strana = "Америка",
                Price = 3200200,
                StockQuantity = 23,
                lavka = 17,
            });
            _cards.Add(new CardDto
            {
                Id = 4,
                Name = "Lada niva travel",
                Type = "Внедорожник",
                Manufacturer = "Lada",
                strana = "Россия",
                Price = 2200200,
                StockQuantity = 13,
                lavka = 3,
            });
            _cards.Add(new CardDto
            {
                Id = 4,
                Name = "Mercedes Amg G63",
                Type = "Внедорожник",
                Manufacturer = "Mercedes",
                strana = "Германия",
                Price = 14100100,
                StockQuantity = 14,
                lavka = 3,
            });
            _cards.Add(new CardDto
            {
                Id = 4,
                Name = "ВАЗ 2107 4ч4",
                Type = "Гоночная модель",
                Manufacturer = "ВАЗ",
                strana = "Россия",
                Price = 200200,
                StockQuantity = 32,
                lavka = 45,
            });


            InitializeMapper.Register();
        }

        public IEnumerable<CardDto> GetAll()
        {
            //var result = new List<CardDto>();

            //foreach (var card in _cards)
            //{
            //    result.Add(card.Clone());
            //}

            //return result;
            return _cards.Select(c => c.Clone()).ToList();
        }

        public int Save(CardDto card)
        {
            if (card.Id == 0)
            {
                var newCard = card.Clone();
                var id = NextId++;
                newCard.Id = id;
                _cards.Add(newCard);
                return id;
            }

            var index = _cards.FindIndex(c => c.Id == card.Id);
            if (index < 0)
            {
                return -1;
            }

            _cards[index] = card.Clone();
            return card.Id;
        }

        public bool Delete(int cardId)
        {
            var index = _cards.FindIndex(c => c.Id == cardId);
            if (index >= 0)
            {
                _cards.RemoveAt(index);
                return true;
            }

            return false;
        }

        internal void ReplaceAll(IEnumerable<CardDto> newCollection)
        {
            _cards.Clear();
            _cards.AddRange(newCollection);
            NextId = _cards.Max(c => c.Id) + 1;
        }

        internal void ReplaceAll(IEnumerable<CardDto> newCollection, int nextId)
        {
            _cards.Clear();
            _cards.AddRange(newCollection);
            NextId = nextId;
        }
    }
}
