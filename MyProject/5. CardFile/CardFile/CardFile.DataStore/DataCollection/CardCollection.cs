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

        internal int NextId { get; set; } = 4;

        public CardCollection()
        {
            _cards.Add(new CardDto
            {
                Id = 1,
                FirstName = "Иван",
                MiddleName = "Петрович",
                LastName = "Смирнов",
                BirthDate = new DateTime(1954, 3, 12),
                DeathDate = new DateTime(2026, 3, 25),
                CauseOfDeath = "Инфаркт",
                PlaceOfDeath = "г. Москва",
                AdmissionDate = DateTime.Today,
                SectionNumber = "A-12",
                IsReleased = false
            });

            _cards.Add(new CardDto
            {
                Id = 2,
                FirstName = "Мария",
                MiddleName = "Ивановна",
                LastName = "Кузнецова",
                BirthDate = new DateTime(1978, 7, 4),
                DeathDate = new DateTime(2026, 3, 20),
                CauseOfDeath = "ДТП",
                PlaceOfDeath = "г. Санкт-Петербург",
                AdmissionDate = DateTime.Today.AddDays(-1),
                SectionNumber = "B-07",
                IsReleased = true
            });

            _cards.Add(new CardDto
            {
                Id = 3,
                FirstName = "Алексей",
                MiddleName = "Владимирович",
                LastName = "Орлов",
                BirthDate = new DateTime(1991, 11, 18),
                DeathDate = new DateTime(2026, 3, 29),
                CauseOfDeath = "Не установлена",
                PlaceOfDeath = "г. Казань",
                AdmissionDate = DateTime.Today,
                SectionNumber = "C-03",
                IsReleased = false
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