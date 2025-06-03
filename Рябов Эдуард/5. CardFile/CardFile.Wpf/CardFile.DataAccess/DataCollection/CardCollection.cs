using CardFile.DataAccess.Dtos;
using CardFile.DataAccess.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Name = "Xeon e5 2650 v2",
                Type = "Процессор",
                Manufacturer = "Intel",
                Price = 20000,
                StockQuantity = 8,
                NameBay = "Максим",
                PhoneNumber = 89082364717,
                QuantitySell = 2,

            });
            _cards.Add(new CardDto
            {
                Id = 2,
                Name = "XSAS 1000W",
                Type = "Блок питания",
                Manufacturer = "XSAS",
                Price = 1500,
                StockQuantity = 4,
                NameBay = "Эдуард",
                PhoneNumber = 89102364717,
                QuantitySell = 1,
            });
            _cards.Add(new CardDto
            {
                Id = 3,
                Name = "Radeon RX580",
                Type = "Видеокарта",
                Manufacturer = "AMD",
                Price = 15000,
                StockQuantity = 23,
                NameBay = "Петр",
                PhoneNumber = 8952362916,
                QuantitySell = 4,
            });
            _cards.Add(new CardDto
            {
                Id = 4,
                Name = "X79 LGA2011",
                Type = "Материнская плата",
                Manufacturer = "Intel",
                Price = 3000,
                StockQuantity = 4,
                NameBay = "Андрей",
                PhoneNumber = 89093890219,
                QuantitySell = 2,
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
