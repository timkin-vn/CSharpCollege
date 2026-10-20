using CardFile.DataStore.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardFile.DataStore.DataCollection
{
    public class CardCollection
    {
        private readonly List<CardDto> _cards = new List<CardDto>();

        public int NextId { get; private set; } = 1;

        public IEnumerable<CardDto> GetAll()
        {
            return _cards.AsReadOnly();
        }

        public CardDto GetById(int id)
        {
            return _cards.FirstOrDefault(c => c.Id == id);
        }

        public int Save(CardDto card)
        {
            if (card == null) throw new ArgumentNullException(nameof(card));

            // Если Id == 0, значит это новый фильм — добавляем его
            if (card.Id == 0)
            {
                card.Id = NextId++;
                _cards.Add(card);
                return card.Id;
            }

            // Если Id задан, ищем существующий фильм и обновляем его свойства
            var existingCard = GetById(card.Id);
            if (existingCard != null)
            {
                existingCard.Title = card.Title;
                existingCard.Director = card.Director;
                existingCard.ReleaseYear = card.ReleaseYear;
                existingCard.Genre = card.Genre;
                existingCard.DurationMinutes = card.DurationMinutes;
                existingCard.Rating = card.Rating;
                existingCard.IsAvailable = card.IsAvailable;

                return existingCard.Id;
            }

            throw new Exception($"Фильм с Id = {card.Id} не найден для обновления.");
        }

        public bool Delete(int id)
        {
            var card = GetById(id);
            if (card != null)
            {
                return _cards.Remove(card);
            }
            return false;
        }

        public void ReplaceAll(IEnumerable<CardDto> newCards, int nextId)
        {
            _cards.Clear();
            if (newCards != null)
            {
                _cards.AddRange(newCards);
            }
            NextId = nextId;
        }

        // Перегрузка для совместимости, если где-то вызывается без указания NextId
        public void ReplaceAll(IEnumerable<CardDto> newCards)
        {
            _cards.Clear();
            if (newCards != null)
            {
                _cards.AddRange(newCards);
            }
            NextId = _cards.Any() ? _cards.Max(c => c.Id) + 1 : 1;
        }
    }
}
