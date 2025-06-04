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
        private List<CardDto> _cards = new List<CardDto>
        {
            new CardDto
            {
                Id = 1,
                Name = "Помидоры",
                DeliverDate = new DateTime(2005, 11, 15),
                ExpirationDate = new DateTime(2006, 03, 15),
                Count = 159,
                Rating = 4.75,
            },
            new CardDto
            {
                Id = 2,
                Name = "Картошка",
                DeliverDate = new DateTime(2005, 11, 07),
                ExpirationDate = new DateTime(2006, 03, 14),
                Count = 52,
                Rating = 4.8,
            },
            new CardDto
            {
                Id = 3,
                Name = "Оливки",
                DeliverDate = new DateTime(2005, 10, 07),
                ExpirationDate = new DateTime(2006, 12, 14),
                Count = 15,
                Rating = 4.9,
            },
        };

        internal int CurrentId = 4;

        public CardCollection()
        {
            MapperRegistrator.Register();
        }

        public IEnumerable<CardDto> GetAll()
        {
            //var result = new List<CardDto>();

            //foreach (var card in _cards)
            //{
            //    result.Add(card.Clone());
            //}

            //return result;

            // LINQ = Language integrated query
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

        internal void ReplaceAll(IEnumerable<CardDto> source, int currentId)
        {
            _cards.Clear();
            _cards.AddRange(source);
            CurrentId = currentId;
        }
    }
}
