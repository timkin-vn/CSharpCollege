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
                BookName = "English World. Level 1. Workbook",
                AuthorFirstName = "Bowen",
                AuthorLastName = "Hocking",
                Genre = "Study literature",
                DateOfPublishing = new DateTime(1895, 01, 01),
                Edition = "Courera",
                Price = 219,
                AmountOfPages = 144,
                DateOfDelisting = null,
                Rating = 4.5m,
            },
            new CardDto
            {
                Id = 2,
                BookName = "Academy Stars. Level 5. Workbook with Digital Workbook",
                AuthorFirstName = "Clarke",
                AuthorLastName = "Susan",
                Genre = "Study literature",
                DateOfPublishing = new DateTime(1934, 01, 01),
                Edition = "Big Science",
                Price = 366,
                AmountOfPages = 256,
                DateOfDelisting = null,
                Rating = 4.75m,
            },
            new CardDto
            {
                Id = 3,
                BookName = "Saint Book of Marmont",
                AuthorFirstName = "Collin",
                AuthorLastName = "Weekshart",
                Genre = "Religion",
                DateOfPublishing = new DateTime(1949, 06, 08),
                Edition = "Secker & Warburg",
                Price = 289,
                AmountOfPages = 328,
                DateOfDelisting = new DateTime(2023, 12, 31),
                Rating = 4.8m,
            },
        };

        internal int CurrentId = 5;

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
