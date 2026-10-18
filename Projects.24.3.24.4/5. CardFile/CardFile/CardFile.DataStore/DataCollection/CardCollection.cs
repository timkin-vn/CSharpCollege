using CardFile.DataStore.Dtos;
using CardFile.DataStore.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataStore.DataCollection
{
    public class CardCollection
    {
        private readonly List<CardDto> _cards = new List<CardDto>();

        internal int NextId { get; set; } = 6;

        public CardCollection()
        {
            var rnd = new Random();

            var lastNames = new[] { "Морозов", "Смирнов", "Белозерский", "Крутов", "Светлова", "Иванов", "Новиков", "Федоров" };
            var firstNames = new[] { "Дмитрий", "Софья", "Максим", "Игорь", "Елена", "Иван", "Анна", "Павел" };
            var middleNames = new[] { "Олегович", "Павловна", "Сергеевич", "Валерьевич", "Александровна", "Иванович", "Денисович" };
            var faculties = new[] { "Информационные технологии", "Экономика и управление", "Радиотехника", "Дизайн и медиа" };
            var groups = new[] { "ИС-31", "ЭК-22", "ПО-41", "РТ-32", "ГД-11", "ИС-11" };

            _cards = new List< CardDto > ();

            for (int i = 1; i <= 6; i++)
            {
                bool isStudying = rnd.Next(0, 4) != 0;

                _cards.Add(new CardDto
                {
                    Id = i,
                    LastName = lastNames[rnd.Next(lastNames.Length)],
                    FirstName = firstNames[rnd.Next(firstNames.Length)],
                    MiddleName = middleNames[rnd.Next(middleNames.Length)],
                    BirthDate = new DateTime(rnd.Next(2003, 2006), rnd.Next(1, 13), rnd.Next(1, 28)),
                    Faculty = faculties[rnd.Next(faculties.Length)],
                    GroupNumber = groups[rnd.Next(groups.Length)],
                    RecordBookNumber = 220000 + rnd.Next(100, 999),
                    AverageScore = Math.Round(3.6 + rnd.NextDouble() * 1.4, 2),
                    IsBudget = rnd.Next(0, 2) == 1,
                    ExpulsionDate = isStudying ? null : (DateTime?)new DateTime(2025, rnd.Next(1, 13), rnd.Next(1, 28)),
                    PhotoPath = null
                });
            }
        }

        public IEnumerable<CardDto> GetAll()
        {
            return new List< CardDto > (_cards);
        }

        public int Save(CardDto card)
        {
            if (card.Id == 0)
            {
                var id = NextId++;
                card.Id = id;
                _cards.Add(card);
                return id;
            }

            var index = _cards.FindIndex(c => c.Id == card.Id);
            if (index < 0)
            {
                return -1;
            }

            _cards[index] = card;
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
            if (_cards.Any())
                NextId = _cards.Max(c => c.Id) + 1;
            else
                NextId = 1;
        }

        public void ReplaceAll(IEnumerable<CardDto> cards, int nextId)
        {
            _cards.Clear();
            _cards.AddRange(cards);
            NextId = nextId;
        }
    }
}
