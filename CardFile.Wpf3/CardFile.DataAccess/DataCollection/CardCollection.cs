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
                BookName = "Машина времени",
                AuthorFirstName = "Герберт",
                AuthorLastName = "Уэллс",
                Genre = "Научная фантастика",
                DateOfPublishing = new DateTime(1895, 01, 01),
                Edition = "Chapman & Hall",
                Price = 219,
                AmountOfPages = 144,
                DateOfDelisting = null,
                Rating = 4.5m,
            },
            new CardDto
            {
                Id = 2,
                BookName = "Убийство в Восточном экспрессе",
                AuthorFirstName = "Агата",
                AuthorLastName = "Кристи",
                Genre = "Детектив",
                DateOfPublishing = new DateTime(1934, 01, 01),
                Edition = "Collins Crime Club\t",
                Price = 366,
                AmountOfPages = 256,
                DateOfDelisting = null,
                Rating = 4.75m,
            },
            new CardDto
            {
                Id = 3,
                BookName = "1984",
                AuthorFirstName = "Джордж",
                AuthorLastName = "Оруэлл",
                Genre = "Антиутопия",
                DateOfPublishing = new DateTime(1949, 06, 08),
                Edition = "Secker & Warburg",
                Price = 289,
                AmountOfPages = 328,
                DateOfDelisting = new DateTime(2023, 12, 31),
                Rating = 4.8m,
            },
            new CardDto
            {
                Id = 4,
                BookName = "Гарри Поттер и философский камень",
                AuthorFirstName = "Джоанн",
                AuthorLastName = "Роулинг",
                Genre = "Фэнтези",
                DateOfPublishing = new DateTime(1997, 06, 26),
                Edition = "Bloomsbury",
                Price = 1080,
                AmountOfPages = 320,
                DateOfDelisting = null,
                Rating = 4.9m,
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
