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
            _cards.Add(new CardDto
            {
                Id = 1,
                Title = "Купить продукты",
                Text = "Молоко, хлеб, яйца, сыр.",
                Category = "Дом",
                CreatedAt = new DateTime(2026, 4, 1),
                IsDone = false,
                IsPinned = true,
            });

            _cards.Add(new CardDto
            {
                Id = 2,
                Title = "Подготовить отчет",
                Text = "Собрать материал и оформить отчет по практике.",
                Category = "Учеба",
                CreatedAt = new DateTime(2026, 4, 3),
                IsDone = false,
                IsPinned = false,
            });

            _cards.Add(new CardDto
            {
                Id = 3,
                Title = "Позвонить другу",
                Text = "Уточнить время встречи на выходных.",
                Category = "Личное",
                CreatedAt = new DateTime(2026, 4, 4),
                IsDone = true,
                IsPinned = false,
            });

            _cards.Add(new CardDto
            {
                Id = 4,
                Title = "Повторить WPF",
                Text = "Посмотреть DataGrid, привязки и работу с окнами редактирования.",
                Category = "Программирование",
                CreatedAt = new DateTime(2026, 4, 5),
                IsDone = false,
                IsPinned = true,
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