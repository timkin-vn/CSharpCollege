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
            MapperInitialization.PreRegister();

            _cards.Add(new CardDto
            {
                Id = 1,
                LastName = "Toyota",
                FirstName = "Camry",
                MiddleName = "Седан",
                Department = "Черный",
                Position = "Двигатель 2.5L",
                BirthDate = new DateTime(2021, 1, 1),
                EmploymentDate = new DateTime(2024, 5, 10),
                DismissalDate = new DateTime(2024, 11, 7),
                Salary = 3200000m
            });

            _cards.Add(new CardDto
            {
                Id = 2,
                LastName = "Haval",
                FirstName = "Jolion",
                MiddleName = "Кроссовер",
                Department = "Белый",
                Position = "Двигатель 1.5L",
                BirthDate = new DateTime(2023, 1, 1),
                EmploymentDate = new DateTime(2025, 1, 15),
                DismissalDate = null,
                Salary = 2400000m
            });

            _cards.Add(new CardDto
            {
                Id = 3,
                LastName = "BMW",
                FirstName = "X5",
                MiddleName = "Внедорожник",
                Department = "Синий",
                Position = "Двигатель 3.0L",
                BirthDate = new DateTime(2020, 1, 1),
                EmploymentDate = new DateTime(2023, 8, 21),
                DismissalDate = new DateTime(2024, 3, 11),
                Salary = 6500000m
            });

            _cards.Add(new CardDto
            {
                Id = 4,
                LastName = "Lada",
                FirstName = "Vesta",
                MiddleName = "Универсал",
                Department = "Серый",
                Position = "Двигатель 1.6L",
                BirthDate = new DateTime(2022, 1, 1),
                EmploymentDate = new DateTime(2024, 9, 2),
                DismissalDate = null,
                Salary = 1450000m
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
