using CardFile.DataStore.Dtos;
using CardFile.DataStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataStore.DataCollection
{
    public class CardCollection
    {
        private readonly List<CardDto> _cards = new List<CardDto>();

        internal int NextId { get; set; } = 5;

        public CardCollection()
        {
            MapperInitialization.PreRegister();

            _cards.Add(new CardDto
            {
                Id = 1,
                Title = "1984",
                Author = "George Orwell",
                Genre = "Антиутопия",
                PublicationDate = new DateTime(1949, 6, 8),
                Publisher = "Secker & Warburg",
                Language = "English",
                ArrivalDate = new DateTime(2023, 9, 1),
                BorrowedUntil = null,
                Price = 850m,
            });

            _cards.Add(new CardDto
            {
                Id = 2,
                Title = "Мастер и Маргарита",
                Author = "Михаил Булгаков",
                Genre = "Роман",
                PublicationDate = new DateTime(1967, 1, 1),
                Publisher = "YMCA-Press",
                Language = "Русский",
                ArrivalDate = new DateTime(2024, 2, 14),
                BorrowedUntil = new DateTime(2026, 4, 20),
                Price = 990m,
            });

            _cards.Add(new CardDto
            {
                Id = 3,
                Title = "Dune",
                Author = "Frank Herbert",
                Genre = "Научная фантастика",
                PublicationDate = new DateTime(1965, 8, 1),
                Publisher = "Chilton Books",
                Language = "English",
                ArrivalDate = new DateTime(2025, 5, 10),
                BorrowedUntil = null,
                Price = 1200m,
            });

            _cards.Add(new CardDto
            {
                Id = 4,
                Title = "Преступление и наказание",
                Author = "Фёдор Достоевский",
                Genre = "Классика",
                PublicationDate = new DateTime(1866, 1, 1),
                Publisher = "Русский вестник",
                Language = "Русский",
                ArrivalDate = new DateTime(2022, 11, 7),
                BorrowedUntil = new DateTime(2026, 4, 16),
                Price = 780m,
            });
        }

        public IEnumerable<CardDto> GetAll()
        {
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
            if (index < 0)
            {
                return false;
            }

            _cards.RemoveAt(index);
            return true;
        }

        public void ReplaceAll(IEnumerable<CardDto> cards)
        {
            _cards.Clear();
            _cards.AddRange(cards);
            NextId = _cards.Max(c => c.Id) + 1;
        }

        public void ReplaceAll(IEnumerable<CardDto> cards, int nextId)
        {
            _cards.Clear();
            _cards.AddRange(cards);
            NextId = nextId;
        }
    }
}
