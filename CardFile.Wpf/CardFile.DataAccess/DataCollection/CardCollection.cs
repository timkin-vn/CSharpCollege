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
                Name = "Московская особая",
                YearOfProduction = new DateTime(2005, 2,5),
                Type = "Водка",
                DeliveryDate = new DateTime(2006, 4, 28),
                Price = 698,
            });
            _cards.Add(new CardDto
            {
                Id = 2,
                Name = "Белая берёза",
                YearOfProduction = new DateTime(2010, 10, 15),
                Type = "Водка",
                DeliveryDate = new DateTime(2011, 1, 20),
                Price = 745,
            });

            _cards.Add(new CardDto
            {
                Id = 3,
                Name = "Каберне Совиньон",
                YearOfProduction = new DateTime(2018, 9, 1),
                Type = "Вино",
                DeliveryDate = new DateTime(2019, 2, 10),
                Price = 1290,
            });

            _cards.Add(new CardDto
            {
                Id = 4,
                Name = "Жигулёвское Премиум",
                YearOfProduction = new DateTime(2022, 5, 12),
                Type = "Пиво",
                DeliveryDate = new DateTime(2022, 6, 5),
                Price = 110,
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
