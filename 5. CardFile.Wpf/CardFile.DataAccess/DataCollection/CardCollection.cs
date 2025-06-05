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
                FirstName = "Анна",
                MiddleName = "Игоревна",
                LastName = "Смирнова",
                BirthDate = new DateTime(1992, 5, 20),
                RegistrationDate = new DateTime(2021, 3, 10),
                Autor = "Джоан Роулинг",
                Genre = "Фэнтези",
                Book = "Гарри Поттер и философский камень",
                GetDate = new DateTime(2024, 1, 5),
                RefundDate = new DateTime(2024, 1, 20),
                Collection = 3

            },
            new CardDto
            {
                Id = 2,
                FirstName = "Дмитрий",
                MiddleName = "Владимирович",
                LastName = "Петров",
                BirthDate = new DateTime(1978, 8, 30),
                RegistrationDate = new DateTime(2019, 7, 22),
                Autor = "Джордж Оруэлл",
                Genre = "Антиутопия",
                Book = "1984",
                GetDate = new DateTime(2024, 3, 12),
                Collection = 7
            },

        };

        internal int CurrentId = 3;

        public CardCollection()
        {
            MapperRegistrator.Register();
        }

        public IEnumerable<CardDto> GetAll()
        {
            
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