using CardFile.DataStore.Dtos;
using CardFile.DataStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardFile.DataStore.DataCollection
{
    public class CardCollection
    {
        private readonly List<CardDto> _cards = new List<CardDto>();
        internal int NextId { get; set; } = 5;

        public CardCollection()
        {
            // Заменяем "сотрудников" на "книги"
            _cards.Add(new CardDto
            {
                Id = 1,
                FirstName = "Преступление и наказание", // Title
                LastName = "Ф. Достоевский",          // Author
                Department = "Классика",              // Genre
                Position = "Эксмо",                   // Publisher
                BirthDate = new DateTime(1866, 1, 1),
                Salary = 850.00m                      // Price
            });

            _cards.Add(new CardDto
            {
                Id = 2,
                FirstName = "Автостопом по галактике",
                LastName = "Дуглас Адамс",
                Department = "Фантастика",
                Position = "АСТ",
                BirthDate = new DateTime(1979, 10, 12),
                Salary = 1200.00m
            });

            _cards.Add(new CardDto
            {
                Id = 3,
                FirstName = "Ведьмак: Последнее желание",
                LastName = "Анджей Сапковский",
                Department = "Фэнтези",
                Position = "Азбука",
                BirthDate = new DateTime(1993, 1, 1),
                Salary = 1500.00m
            });

            _cards.Add(new CardDto
            {
                Id = 4,
                FirstName = "Маленький принц",
                LastName = "Антуан де Сент-Экзюпери",
                Department = "Сказка",
                Position = "Детская литература",
                BirthDate = new DateTime(1943, 4, 6),
                Salary = 600.00m
            });

            MapperInitialization.PreRegister();
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
            if (index < 0) return -1;

            _cards[index] = card.Clone();
            return card.Id;
        }

        public bool Delete(int cardId)
        {
            var index = _cards.FindIndex(c => c.Id == cardId);
            if (index < 0) return false;
            _cards.RemoveAt(index);
            return true;
        }

        internal void ReplaceAll(IEnumerable<CardDto> newCollection)
        {
            _cards.Clear();
            _cards.AddRange(newCollection);
            NextId = _cards.Any() ? _cards.Max(c => c.Id) + 1 : 1;
        }

        internal void ReplaceAll(IEnumerable<CardDto> newCollection, int nextId)
        {
            _cards.Clear();
            _cards.AddRange(newCollection);
            NextId = nextId;
        }
    }
}
