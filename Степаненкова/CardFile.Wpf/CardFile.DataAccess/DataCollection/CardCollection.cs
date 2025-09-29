using CardFile.DataAccess.Dtos;
using CardFile.DataAccess.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardFile.DataAccess.DataCollection
{
    public class CardCollection
    {
        private List<CardDto> _cards = new List<CardDto>
        {
            new CardDto
            {
                Id = 1,
                FirstName = "Олег",
                MiddleName = "Александрович",
                LastName = "Захаров",
                BirthDate = new DateTime(1984, 11, 15),
                RegistrationDate = new DateTime(2020, 12, 12),
                Autor = "Адам Сильвера",
                Genre = "Драма",
                Book = "В конце они оба умрут",
                GetDate = new DateTime(2024, 4, 15),
                RefundDate = new DateTime(2024, 4, 25),
                Collection = 5,

            },
            new CardDto
            {
                Id = 2,
                FirstName = "Ольга",
                MiddleName = "Викторовна",
                LastName = "Фролова",
                BirthDate = new DateTime(1995, 7, 18),
                RegistrationDate = new DateTime(2022, 9, 20),
                Autor = "Джек Лондон",
                Genre = "Реалистический роман",
                Book = "Мартин Иден",
                GetDate = new DateTime(2025, 2, 10),
                RefundDate = new DateTime(2025, 2, 23),
                Collection = 2,
            },
            new CardDto
            {
                Id = 3,
                FirstName = "Геннадий",
                MiddleName = "Андреевич",
                LastName = "Василенко",
                BirthDate = new DateTime(1979, 3, 21),
                RegistrationDate = new DateTime(2023, 5, 6),
                Autor = "Шарлотта Бронте",
                Genre = "Роман",
                Book = "Джейн Эйр",
                GetDate = new DateTime(2025, 5, 25),
                RefundDate = null,
                Collection = 7,
            },
            new CardDto
            {
                Id = 4,
                FirstName = "Ирина",
                MiddleName = "Васильевна",
                LastName = "Мельникова",
                BirthDate = new DateTime(1989, 5, 8),
                RegistrationDate = new DateTime(2019, 11, 1),
                Autor = "Николас Спаркс",
                Genre = "Любовный роман",
                Book = "Дневник памяти",
                GetDate = new DateTime(2025, 5, 18),
                RefundDate = new DateTime(2025, 5, 31),
                Collection = 3,
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
